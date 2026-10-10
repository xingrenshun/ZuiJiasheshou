#!/usr/bin/env python3
"""
UI资源压缩脚本 - 最佳射手项目
功能:
  1. 背景JPG: 缩放到最大1280px, JPEG quality 80
  2. 大尺寸PNG(>1024px): 缩放到最大1024px
  3. 所有PNG: 调色板量化(256色)减少体积
  4. 修改.meta文件: 添加WebGL平台覆写 + Crunched压缩 + 降低maxTextureSize
"""

import os
import sys
import shutil
from PIL import Image

# ============ 配置 ============
ASSETS_DIR = os.path.join(os.path.dirname(__file__), "Assets", "ArcherHero")
BACKUP_DIR = os.path.join(os.path.dirname(__file__), "asset_backup_pre_compress")

# 背景图最大边长
BG_MAX_SIZE = 1280
# PNG最大边长
PNG_MAX_SIZE = 1024
# JPEG压缩质量
JPEG_QUALITY = 80
# 量化颜色数
QUANTIZE_COLORS = 256

# ============ 统计 ============
stats = {
    "total_files": 0,
    "compressed": 0,
    "skipped": 0,
    "errors": 0,
    "original_size": 0,
    "compressed_size": 0,
}
details = []


def get_file_size(path):
    return os.path.getsize(path) if os.path.exists(path) else 0


def resize_if_needed(img, max_size):
    """如果图片超过max_size，等比缩小"""
    w, h = img.size
    if max(w, h) <= max_size:
        return img, False
    if w >= h:
        new_w = max_size
        new_h = int(h * max_size / w)
    else:
        new_h = max_size
        new_w = int(w * max_size / h)
    # 确保偶数尺寸(某些编解码器要求)
    new_w = new_w if new_w % 2 == 0 else new_w + 1
    new_h = new_h if new_h % 2 == 0 else new_h + 1
    return img.resize((new_w, new_h), Image.LANCZOS), True


def quantize_png(img):
    """PNG调色板量化，减少文件体积"""
    if img.mode == "RGBA":
        # 带透明通道: 用FASTOCTREE (MEDIANCUT不支持RGBA)
        quantized = img.quantize(colors=QUANTIZE_COLORS, method=Image.Quantize.FASTOCTREE)
        return quantized.convert("RGBA")
    elif img.mode == "RGB":
        quantized = img.quantize(colors=QUANTIZE_COLORS, method=Image.Quantize.MEDIANCUT)
        return quantized.convert("RGB")
    elif img.mode == "P":
        # 已经是调色板模式，限制颜色数
        if len(img.getpalette()) > QUANTIZE_COLORS * 4:
            return img.quantize(colors=QUANTIZE_COLORS, method=Image.Quantize.MEDIANCUT)
        return img
    return img


def compress_image(filepath):
    """压缩单张图片 - 直接覆盖原文件(已有备份)"""
    stats["total_files"] += 1
    orig_size = get_file_size(filepath)
    stats["original_size"] += orig_size

    try:
        ext = os.path.splitext(filepath)[1].lower()
        img = Image.open(filepath)
        w, h = img.size
        mode = img.mode

        resized = False
        new_img = img

        if ext in (".jpg", ".jpeg"):
            # 背景JPG: 缩小 + 重新压缩
            new_img, resized = resize_if_needed(img, BG_MAX_SIZE)
            new_img = new_img.convert("RGB")
            # 保存到临时目录再覆盖(避免sandbox删除问题)
            import tempfile
            tmp_dir = tempfile.gettempdir()
            tmp_path = os.path.join(tmp_dir, "unity_compress_tmp.jpg")
            new_img.save(tmp_path, "JPEG", quality=JPEG_QUALITY, optimize=True)
            new_size = get_file_size(tmp_path)
            if new_size < orig_size:
                shutil.copy2(tmp_path, filepath)
                stats["compressed"] += 1
                stats["compressed_size"] += new_size
                details.append(
                    f"  [JPG] {'缩放+压缩' if resized else '压缩'}  "
                    f"{w}x{h} -> {new_img.size[0]}x{new_img.size[1]}  "
                    f"{orig_size//1024}K -> {new_size//1024}K  {os.path.basename(filepath)}"
                )
            else:
                stats["skipped"] += 1
                stats["compressed_size"] += orig_size
        elif ext == ".png":
            # PNG: 缩小 + 量化
            new_img, resized = resize_if_needed(img, PNG_MAX_SIZE)
            new_img = quantize_png(new_img)

            import tempfile
            tmp_dir = tempfile.gettempdir()
            tmp_path = os.path.join(tmp_dir, "unity_compress_tmp.png")
            new_img.save(tmp_path, "PNG", optimize=True)

            new_size = get_file_size(tmp_path)
            if new_size < orig_size:
                shutil.copy2(tmp_path, filepath)
                stats["compressed"] += 1
                stats["compressed_size"] += new_size
                details.append(
                    f"  [PNG] {'缩放+' if resized else ''}量化  "
                    f"{w}x{h} -> {new_img.size[0]}x{new_img.size[1]}  "
                    f"{orig_size//1024}K -> {new_size//1024}K  {os.path.basename(filepath)}"
                )
            else:
                stats["skipped"] += 1
                stats["compressed_size"] += orig_size
        else:
            stats["skipped"] += 1
            stats["compressed_size"] += orig_size

    except Exception as e:
        stats["errors"] += 1
        stats["compressed_size"] += orig_size
        details.append(f"  [ERROR] {filepath}: {e}")


def update_meta_file(meta_path, img_path):
    """
    修改.meta文件:
    1. DefaultTexturePlatform: 开启crunched压缩, maxTextureSize根据图片调整
    2. 添加WebGL平台覆写: crunched压缩
    3. Android/iPhone: 保持已有或设置crunched
    """
    if not os.path.exists(meta_path):
        return

    with open(meta_path, "r", encoding="utf-8") as f:
        content = f.read()

    # 获取图片实际尺寸来设置合适的maxTextureSize
    try:
        img = Image.open(img_path)
        w, h = img.size
        img.close()
        max_dim = max(w, h)
        if max_dim <= 256:
            tex_size = 256
        elif max_dim <= 512:
            tex_size = 512
        elif max_dim <= 1024:
            tex_size = 1024
        else:
            tex_size = 1024  # 超过1024的一律压到1024
    except:
        tex_size = 1024

    # 1. 修改DefaultTexturePlatform: 开启crunched压缩, 降低maxTextureSize
    # 找到 DefaultTexturePlatform block
    old_default = """  - serializedVersion: 3
    buildTarget: DefaultTexturePlatform
    maxTextureSize: 2048
    resizeAlgorithm: 0
    textureFormat: -1
    textureCompression: 1
    compressionQuality: 50
    crunchedCompression: 0
    allowsAlphaSplitting: 0
    overridden: 0
    androidETC2FallbackOverride: 0
    forceMaximumCompressionQuality_BC6H_BC7: 0"""

    new_default = f"""  - serializedVersion: 3
    buildTarget: DefaultTexturePlatform
    maxTextureSize: {tex_size}
    resizeAlgorithm: 0
    textureFormat: -1
    textureCompression: 1
    compressionQuality: 50
    crunchedCompression: 1
    allowsAlphaSplitting: 0
    overridden: 0
    androidETC2FallbackOverride: 0
    forceMaximumCompressionQuality_BC6H_BC7: 0"""

    if old_default in content:
        content = content.replace(old_default, new_default)
    else:
        # 尝试匹配变体 (maxTextureSize可能不同)
        import re
        content = re.sub(
            r"(  - serializedVersion: 3\n    buildTarget: DefaultTexturePlatform\n    maxTextureSize: )\d+(\n    resizeAlgorithm: 0\n    textureFormat: -1\n    textureCompression: 1\n    compressionQuality: 50\n    crunchedCompression: )0",
            rf"\g<1>{tex_size}\g<2>1",
            content,
        )

    # 2. 修改Standalone: 同样开启crunched
    old_standalone = """  - serializedVersion: 3
    buildTarget: Standalone
    maxTextureSize: 2048
    resizeAlgorithm: 0
    textureFormat: -1
    textureCompression: 1
    compressionQuality: 50
    crunchedCompression: 0
    allowsAlphaSplitting: 0
    overridden: 0
    androidETC2FallbackOverride: 0
    forceMaximumCompressionQuality_BC6H_BC7: 0"""

    new_standalone = f"""  - serializedVersion: 3
    buildTarget: Standalone
    maxTextureSize: {tex_size}
    resizeAlgorithm: 0
    textureFormat: -1
    textureCompression: 1
    compressionQuality: 50
    crunchedCompression: 1
    allowsAlphaSplitting: 0
    overridden: 0
    androidETC2FallbackOverride: 0
    forceMaximumCompressionQuality_BC6H_BC7: 0"""

    if old_standalone in content:
        content = content.replace(old_standalone, new_standalone)
    else:
        import re
        content = re.sub(
            r"(  - serializedVersion: 3\n    buildTarget: Standalone\n    maxTextureSize: )\d+(\n    resizeAlgorithm: 0\n    textureFormat: -1\n    textureCompression: 1\n    compressionQuality: 50\n    crunchedCompression: )0",
            rf"\g<1>{tex_size}\g<2>1",
            content,
        )

    # 3. 修改iPhone: 开启crunched
    import re
    content = re.sub(
        r"(  - serializedVersion: 3\n    buildTarget: iPhone\n    maxTextureSize: )\d+(\n    resizeAlgorithm: 0\n    textureFormat: -1\n    textureCompression: 1\n    compressionQuality: 50\n    crunchedCompression: )0",
        rf"\g<1>{tex_size}\g<2>1",
        content,
    )

    # 4. 修改Android: 开启crunched, maxTextureSize设为tex_size
    content = re.sub(
        r"(  - serializedVersion: 3\n    buildTarget: Android\n    maxTextureSize: )\d+(\n    resizeAlgorithm: 0\n    textureFormat: )\d+(\n    textureCompression: 1\n    compressionQuality: 50\n    crunchedCompression: )0",
        rf"\g<1>{tex_size}\n\g<2>-1\g<3>1",
        content,
    )

    # 5. 添加WebGL平台覆写 (如果不存在)
    if "buildTarget: WebGL" not in content:
        webgl_block = f"""  - serializedVersion: 3
    buildTarget: WebGL
    maxTextureSize: {tex_size}
    resizeAlgorithm: 0
    textureFormat: -1
    textureCompression: 1
    compressionQuality: 50
    crunchedCompression: 1
    allowsAlphaSplitting: 0
    overridden: 1
    androidETC2FallbackOverride: 0
    forceMaximumCompressionQuality_BC6H_BC7: 0"""
        # 在spriteSheet:之前插入
        content = content.replace("  spriteSheet:", webgl_block + "\n  spriteSheet:")

    # 6. 修改顶层maxTextureSize
    content = re.sub(
        r"(  maxTextureSize: )2048(\n  textureSettings:)",
        rf"\g<1>{tex_size}\g<2>",
        content,
    )

    with open(meta_path, "w", encoding="utf-8") as f:
        f.write(content)


def main():
    print("=" * 70)
    print("  UI资源压缩工具 - 最佳射手")
    print("=" * 70)

    # 1. 备份
    print("\n[1/3] 备份原始资源...")
    if not os.path.exists(BACKUP_DIR):
        os.makedirs(BACKUP_DIR)
        # 只备份要处理的目录
        for subdir in ["bg", "UI", "Sprite"]:
            src = os.path.join(ASSETS_DIR, subdir)
            dst = os.path.join(BACKUP_DIR, subdir)
            if os.path.exists(src):
                print(f"  备份 {subdir}...")
                shutil.copytree(src, dst)
        print(f"  备份完成 -> {BACKUP_DIR}")
    else:
        print("  备份已存在，跳过")

    # 2. 压缩图片
    print("\n[2/3] 压缩图片资源...")
    image_files = []
    for root, dirs, files in os.walk(ASSETS_DIR):
        for f in files:
            ext = os.path.splitext(f)[1].lower()
            if ext in (".png", ".jpg", ".jpeg"):
                image_files.append(os.path.join(root, f))

    image_files.sort(key=lambda x: get_file_size(x), reverse=True)

    for filepath in image_files:
        compress_image(filepath)

    # 3. 修改.meta文件
    print("\n[3/3] 更新纹理导入设置(.meta)...")
    meta_count = 0
    for root, dirs, files in os.walk(ASSETS_DIR):
        for f in files:
            if f.endswith(".png.meta") or f.endswith(".jpg.meta") or f.endswith(".jpeg.meta"):
                meta_path = os.path.join(root, f)
                img_path = meta_path[:-5]  # 去掉.meta
                if os.path.exists(img_path):
                    update_meta_file(meta_path, img_path)
                    meta_count += 1
    print(f"  更新了 {meta_count} 个.meta文件")

    # 汇总
    print("\n" + "=" * 70)
    print("  压缩结果汇总")
    print("=" * 70)
    print(f"  处理文件总数:   {stats['total_files']}")
    print(f"  成功压缩:       {stats['compressed']}")
    print(f"  跳过(已足够小): {stats['skipped']}")
    print(f"  错误:           {stats['errors']}")
    print(f"  原始总大小:     {stats['original_size']/1024/1024:.1f} MB")
    print(f"  压缩后总大小:   {stats['compressed_size']/1024/1024:.1f} MB")
    saved = stats["original_size"] - stats["compressed_size"]
    ratio = (saved / stats["original_size"] * 100) if stats["original_size"] > 0 else 0
    print(f"  节省空间:       {saved/1024/1024:.1f} MB ({ratio:.0f}%)")

    print("\n  --- 压缩详情 (前30条) ---")
    for d in details[:30]:
        print(d)
    if len(details) > 30:
        print(f"  ... 还有 {len(details)-30} 条")

    print(f"\n  原始备份在: {BACKUP_DIR}")
    print("  如需还原，删除压缩后的文件，从备份复制回来即可。")
    print("\n  ⚠️  下次在Unity中打开项目时，会自动重新导入纹理。")
    print("  建议在Unity中等待导入完成后再构建。")


if __name__ == "__main__":
    main()
