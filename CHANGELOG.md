# Changelog
本项目所有值得注意的变更都会记录在此文件中。
格式参考 [Keep a Changelog](https://keepachangelog.com/zh-CN/1.1.0/)，版本遵循 [语义化版本](https://semver.org/lang/zh-CN/)。

## [1.0.0] - 2026-07-17
首个可复现构建 / UPM 版本：修复 Unity 编译阻塞、现代化重建转换器工具，并将 Unity 运行时封装为 UPM 包。

### 新增（Added）
- **UPM 包 `com.alefeng.exceltoprotobuf`**：可通过 git URL（`?path=com.alefeng.exceltoprotobuf`）安装。
  - 运行时加载器 `ConfigManager`：按表名加载 `.bytes`，反序列化为强类型容器类 `<Sheet>_Map` 并按类型缓存。
  - 字节来源抽象 `IConfigBytesProvider` 与默认实现 `ResourcesConfigBytesProvider`（从 `Resources` 加载）。
  - 内置 `Google.Protobuf`（netstandard2.0）依赖、`ExcelToProtobuf.Runtime.asmdef`、`package.json`、`Basic Usage` 示例与 `Documentation~` 说明。
- **三语说明文档** `README.md` / `README_EN.md` / `README_JA.md`：含填表规则、转换流程、`dotnet` 构建运行、UPM 安装与运行时用法。
- 转换器**路径参数化**（`--excel` / `--proto-src` / `--proto-out` / `--cs-gen` / `--cs-out` / `--bytes-out` / `--txt-out`）与仓库根自动探测（`PipelineConfig.cs`）。
- Unity 侧 `Assets/Plugins/Google.Protobuf/`（含 `.meta`），使既有生成的配置类可在工程内编译。
- 根 `CHANGELOG.md`（本文件）。

### 修复（Fixed）
- **Unity 编译报错 CS0400**：`Assets/Source/System/ConfigSystem/Config/*.cs` 因项目缺少 `Google.Protobuf` 程序集而无法编译。
- `Program.CopyCs2Unity` 用 `return` 代替 `continue`：遇到非 `.cs` 文件会中止整段拷贝并跳过废弃文件清理。
- 数值解析统一改用 `InvariantCulture`：修复小数分隔符为逗号的系统区域（如德 / 法 / 俄语环境）下 `int` / `float` 解析出错（`Excel2Bytes`）。
- `floatarray` / `floatArray` 大小写在 `Excel2Proto` 与 `Excel2Bytes` 两阶段不一致，导致直接声明为 float 数组的列无法正确填充（两处 `switch` 统一并兼容大小写）。
- 数据行读取补充空行 / 空单元格保护，避免 `NullReferenceException`。
- `.proto` 与 `.txt` 输出改为显式 UTF-8（无 BOM），不再依赖系统默认编码（避免非 ASCII 乱码）。
- `HashAlgorithm.Create()`（过时 API，在 .NET Core / .NET 8 会失败）改为 `MD5.Create()`。
- `Console.WriteLine(string, ConsoleColor)` 的颜色参数被当作格式参数静默忽略，改为真正的彩色输出辅助方法。

### 变更（Changed）
- **转换器工程现代化重建**：由 .NET Framework 4.7.1 的旧式 `.csproj` 重写为 **SDK 风格 `net8.0`**，依赖改用 NuGet（`NPOI` / `Google.Protobuf` / `Google.Protobuf.Tools` / `Microsoft.CodeAnalysis.CSharp`），可从干净克隆一键编译；`protoc` 与 `Google.Protobuf` 固定 `3.11.2` 以兼容既有产物与 Unity 插件。
- **管线去脆弱化**：`proto → cs` 改为直接调用 `protoc`（来自 `Google.Protobuf.Tools`，跨平台）；`cs → dll` 改为 **Roslyn 进程内内存编译**，取代 `BuildProtos.bat` / `BuildDLL.bat` 与硬编码的 `csc.exe`。工具现可在 Windows / macOS / Linux 运行。
- `.gitignore` 修正：不再误伤转换器工程的 `*.csproj` / `*.sln`（改为仅忽略仓库根的 Unity 生成工程文件），并忽略 `Tools/**/bin`、`Tools/**/obj`。
- 运行结束不再无条件 `Console.ReadKey()`：被重定向（脚本 / CI）时直接退出，避免阻塞。
- 生成的 `.proto` / `.cs` 中间产物改为写入临时工作目录，不再落地到 `Tools/ExcelToProtobuf` 下。

### 移除（Removed）
- 转换器旧构建脚本与配置：`BuildProtos.bat`、`BuildDLL.bat`、`App.config`、`Properties/AssemblyInfo.cs`。
- 随仓库附带的 `protoc.exe` 与 `protoc-3.11.2-win64/`（改由 `Google.Protobuf.Tools` NuGet 提供）。
- 已提交的构建产物与中间物：`bin/Debug` 预编译二进制、`obj/` 缓存、生成目录 `Csharp/` 与 `Protos/`。
- 过时说明文件 `readme.txt`、`BuildProtosReadme.txt`（内容并入 README）。
