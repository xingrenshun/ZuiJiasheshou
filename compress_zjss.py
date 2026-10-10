#!/usr/bin/env python3
"""压缩ZJSS目录的图片资源"""
import os, shutil, tempfile, re
from PIL import Image

ZJSS_DIR = os.path.join(os.path.dirname(__file__), "Assets", "ZJSS")
BACKUP_DIR = os.path.join(os.path.dirname(__file__), "asset_backup_pre_compress", "ZJSS")
PNG_MAX_SIZE = 1024
QUANTIZE_COLORS = 256

# Backup
if not os.path.exists(BACKUP_DIR):
    os.makedirs(BACKUP_DIR)
    for root, dirs, files in os.walk(ZJSS_DIR):
        for f in files:
            src = os.path.join(root, f)
            rel = os.path.relpath(src, ZJSS_DIR)
            dst = os.path.join(BACKUP_DIR, rel)
            os.makedirs(os.path.dirname(dst), exist_ok=True)
            shutil.copy2(src, dst)
    print("ZJSS备份完成")

stats = {"compressed": 0, "skipped": 0, "errors": 0, "orig": 0, "new": 0}

def resize_if_needed(img, max_size):
    w, h = img.size
    if max(w, h) <= max_size:
        return img, False
    if w >= h:
        new_w, new_h = max_size, int(h * max_size / w)
    else:
        new_h, new_w = max_size, int(w * max_size / h)
    new_w += new_w % 2
    new_h += new_h % 2
    return img.resize((new_w, new_h), Image.LANCZOS), True

def quantize_png(img):
    if img.mode == "RGBA":
        return img.quantize(colors=QUANTIZE_COLORS, method=Image.Quantize.FASTOCTREE).convert("RGBA")
    elif img.mode == "RGB":
        return img.quantize(colors=QUANTIZE_COLORS, method=Image.Quantize.MEDIANCUT).convert("RGB")
    elif img.mode == "P":
        return img
    return img

def update_meta(meta_path, img_path):
    if not os.path.exists(meta_path):
        return
    with open(meta_path, "r", encoding="utf-8") as f:
        content = f.read()
    try:
        im = Image.open(img_path)
        max_dim = max(im.size)
        im.close()
        tex_size = 256 if max_dim <= 256 else (512 if max_dim <= 512 else 1024)
    except:
        tex_size = 1024
    content = re.sub(r"(crunchedCompression: )0", r"\g<1>1", content)
    content = re.sub(r"(maxTextureSize: )2048", rf"\g<1>{tex_size}", content)
    if "buildTarget: WebGL" not in content:
        webgl_block = (
            "  - serializedVersion: 3\n"
            "    buildTarget: WebGL\n"
            f"    maxTextureSize: {tex_size}\n"
            "    resizeAlgorithm: 0\n"
            "    textureFormat: -1\n"
            "    textureCompression: 1\n"
            "    compressionQuality: 50\n"
            "    crunchedCompression: 1\n"
            "    allowsAlphaSplitting: 0\n"
            "    overridden: 1\n"
            "    androidETC2FallbackOverride: 0\n"
            "    forceMaximumCompressionQuality_BC6H_BC7: 0\n"
        )
        content = content.replace("  spriteSheet:", webgl_block + "  spriteSheet:")
    with open(meta_path, "w", encoding="utf-8") as f:
        f.write(content)

for root, dirs, files in os.walk(ZJSS_DIR):
    for f in files:
        ext = os.path.splitext(f)[1].lower()
        if ext not in (".png", ".jpg", ".jpeg"):
            continue
        filepath = os.path.join(root, f)
        orig_size = os.path.getsize(filepath)
        stats["orig"] += orig_size
        try:
            img = Image.open(filepath)
            w, h = img.size
            new_img, resized = resize_if_needed(img, PNG_MAX_SIZE)
            if ext == ".png":
                new_img = quantize_png(new_img)
            else:
                new_img = new_img.convert("RGB")

            tmp = os.path.join(tempfile.gettempdir(), "zjss_compress_tmp" + ext)
            if ext == ".png":
                new_img.save(tmp, "PNG", optimize=True)
            else:
                new_img.save(tmp, "JPEG", quality=80, optimize=True)
            new_size = os.path.getsize(tmp)
            if new_size < orig_size:
                shutil.copy2(tmp, filepath)
                stats["compressed"] += 1
                stats["new"] += new_size
                tag = "缩放+" if resized else ""
                print(f"  {tag}量化  {w}x{h}->{new_img.size[0]}x{new_img.size[1]}  {orig_size//1024}K->{new_size//1024}K  {f}")
            else:
                stats["skipped"] += 1
                stats["new"] += orig_size
            meta_path = filepath + ".meta"
            if os.path.exists(meta_path):
                update_meta(meta_path, filepath)
        except Exception as e:
            stats["errors"] += 1
            stats["new"] += orig_size
            print(f"  [ERROR] {f}: {e}")

saved = stats["orig"] - stats["new"]
pct = saved / stats["orig"] * 100 if stats["orig"] > 0 else 0
print(f"\nZJSS压缩完成: {stats['compressed']}张压缩, {stats['skipped']}跳过, {stats['errors']}错误")
print(f"  {stats['orig']/1024/1024:.1f}MB -> {stats['new']/1024/1024:.1f}MB, 节省 {saved/1024/1024:.1f}MB ({pct:.0f}%)")
