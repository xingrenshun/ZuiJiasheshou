# Unity WebGL构建包体优化指南

## 一、构建配置优化

### 1. 启用构建大小分析

- **设置位置**：Player Settings > Publishing Settings > Analyze Build Size
- **作用**：生成详细的构建大小报告，帮助识别大资源和冗余代码
- **建议**：启用此选项，定期分析构建大小

### 2. 压缩格式优化

- **设置位置**：Player Settings > Publishing Settings > Compression Format
- **当前值**：Disabled（0）
- **建议**：选择Gzip或Brotli压缩
  - Gzip：兼容性好，压缩率适中
  - Brotli：压缩率更高，但需要较新的浏览器支持
- **服务器配置**：确保服务器正确配置`Content-Encoding`响应头

### 3. 代码剥离优化

- **设置位置**：Player Settings > Publishing Settings > Managed Stripping Level
- **当前值**：Medium（1）
- **建议**：提高到High（2），更激进地剥离未使用的代码
- **注意**：可能需要添加必要的链接器指令来防止误剥离

### 4. 异常支持

- **设置位置**：Player Settings > Publishing Settings > Exception Support
- **当前值**：Enabled（1）
- **建议**：设置为Disabled或Full Without Stacktrace，减少运行时开销和包体大小

### 5. WASM优化

- **设置位置**：Player Settings > Publishing Settings
- **建议**：
  - 关闭WASM算术异常支持
  - 关闭线程支持（如果不需要）
  - 使用默认的链接器目标

### 6. 资源嵌入

- **设置位置**：Player Settings > Publishing Settings > Use Embedded Resources
- **当前值**：Disabled（0）
- **建议**：保持关闭，让浏览器并行加载资源

## 二、资源优化

### 1. 音频资源优化（最优先级）

**问题**：`menu.wav`文件大小为36.7MB，是最大的资源文件

**优化方案**：

- **格式转换**：将WAV转换为OGG或MP3格式
  - OGG：Unity推荐的音频格式，压缩率高，质量好
  - MP3：兼容性好，但压缩率略低于OGG
- **采样率调整**：
  - 背景音乐：降低到44.1kHz或22.05kHz
  - 音效：降低到22.05kHz或11.025kHz
- **位深度调整**：从16位降低到8位（对大多数游戏音效足够）
- **压缩质量**：
  - 背景音乐：70-80%
  - 音效：50-70%

**工具**：
- Audacity：免费音频编辑工具，支持格式转换和优化
- FMOD或Wwise：专业音频 middleware，提供更高级的音频管理

### 2. 字体资源优化

**问题**：
- `汉仪蝶语体简 1.TTF`：3.4MB
- `DroidSansFallback.ttf`：3.1MB

**优化方案**：

- **移除未使用的字体**：只保留游戏中实际使用的字体
- **字体子集化**：只包含游戏中实际使用的字符
  - 使用FontForge或Glyphr Studio等工具
  - 或使用Google Fonts的子集化服务
- **使用动态字体加载**：只在需要时加载字体

### 3. 纹理资源优化

**问题**：多个背景图片超过1MB

**优化方案**：

- **纹理压缩**：
  - WebGL：使用ETC1或ASTC压缩格式
  - 确保启用纹理压缩（Player Settings > Graphics > Texture Compression）
- **分辨率调整**：
  - 背景图片：根据实际显示大小调整，避免过大分辨率
  - UI纹理：使用合适的分辨率，避免过度缩放
- **MIP贴图**：对于3D物体使用MIP贴图，减少远处物体的渲染开销
- **纹理格式**：
  - 不透明纹理：使用ETC1或ASTC
  - 透明纹理：使用ETC2或ASTC with alpha
  - UI纹理：考虑使用PVRTC或ETC2

### 4. 模型和动画优化

**优化方案**：

- **减少多边形数量**：
  - 使用LOD（Level of Detail）系统
  - 简化高多边形模型
- **动画压缩**：
  - 启用动画压缩（Animation Import Settings > Compression）
  - 调整关键帧压缩质量
- **合并网格**：合并静态物体的网格，减少Draw Call
- **移除未使用的模型和动画**：清理项目中未使用的资源

## 三、代码优化

### 1. 脚本后端

- **当前设置**：IL2CPP（已启用）
- **建议**：继续使用IL2CPP，比Mono更小、更快、更安全

### 2. 代码剥离

- **设置位置**：Player Settings > Publishing Settings > Managed Stripping Level
- **建议**：设置为High，配合链接器指令使用

### 3. 移除未使用的代码

- **清理未使用的脚本**：删除项目中未使用的C#脚本
- **移除未使用的插件**：删除未使用的第三方插件
- **优化依赖关系**：减少不必要的程序集引用

### 4. Shader优化

- **移除未使用的Shader**：删除项目中未使用的Shader
- **使用Shader变体剥离**：只编译实际使用的Shader变体
- **简化Shader复杂度**：避免过度复杂的Shader，使用更简单的替代方案

## 四、其他优化技巧

### 1. 资源加载优化

- **使用Asset Bundles**：将资源分成多个Bundle，按需加载
- **资源压缩**：启用资源压缩，减少下载大小
- **异步加载**：使用异步加载API，避免阻塞主线程

### 2. 构建后优化

- **使用CDN**：将构建文件部署到CDN，加速下载
- **启用HTTP/2**：减少网络请求开销
- **使用Service Worker**：实现离线缓存，提高加载速度

### 3. 定期清理

- **清理Library文件夹**：定期清理，避免缓存问题
- **使用Unity Asset Store的清理工具**：如Asset Cleaner等工具
- **定期分析构建报告**：识别新引入的大资源

## 五、具体资源优化建议

### 1. 优先优化的大资源

| 资源名称 | 大小 | 优化建议 |
|---------|------|----------|
| menu.wav | 36.7MB | 转换为OGG格式，降低采样率和位深度 |
| 汉仪蝶语体简 1.TTF | 3.4MB | 子集化处理，只保留使用的字符 |
| DroidSansFallback.ttf | 3.1MB | 子集化处理或替换为更小的替代字体 |
| night.jpg | 2.6MB | 压缩为ETC2格式，调整分辨率 |
| Night Moon.jpg | 2.1MB | 压缩为ETC2格式，调整分辨率 |

### 2. 音频资源批量优化

- 使用Audacity批量处理音频文件
- 统一设置：
  - 格式：OGG
  - 采样率：背景音乐44.1kHz，音效22.05kHz
  - 位深度：16位
  - 压缩质量：70%

### 3. 纹理资源批量优化

- 使用Unity的Texture Import Settings批量修改
- 统一设置：
  - Max Size：根据实际使用调整
  - Format：ETC2_RGBA8（透明）或ETC2_RGB8（不透明）
  - Mip Maps：根据需求启用
  - Compression Quality：Normal或High

## 六、优化前后对比

| 优化项 | 预计减少大小 |
|-------|------------|
| 音频压缩 | ~30MB |
| 字体优化 | ~5MB |
| 纹理压缩 | ~5MB |
| 代码剥离 | ~2-3MB |
| 压缩格式 | 减少30-50%的总体积 |

## 七、优化步骤

1. **启用构建大小分析**，生成初始报告
2. **优先优化大资源**：menu.wav音频文件
3. **优化字体资源**，进行子集化处理
4. **批量优化纹理资源**，启用压缩
5. **调整构建配置**：启用压缩、提高代码剥离级别
6. **生成新的构建报告**，对比优化效果
7. **迭代优化**，针对剩余大资源进行优化

## 八、注意事项

1. **测试兼容性**：优化后确保游戏在目标浏览器上正常运行
2. **性能测试**：优化后测试游戏性能，确保没有明显下降
3. **备份原始资源**：在优化前备份原始资源，以便恢复
4. **定期优化**：在开发过程中定期进行优化，避免问题积累

通过以上优化措施，预计可以将WebGL构建包体大小减少50%以上，显著提高加载速度和用户体验。