<p align="center">
  <img alt="GitHub Repo License" src="https://img.shields.io/github/license/AleFeng/ExcelToProtobuf?color=blueviolet">
  <img alt="GitHub Repo Issues" src="https://img.shields.io/github/issues/AleFeng/ExcelToProtobuf?color=yellow">
  <img alt="GitHub Repo Stars" src="https://img.shields.io/github/stars/AleFeng/ExcelToProtobuf?color=blue">
  <img alt="Language" src="https://img.shields.io/badge/language-C%23-239120">
  <img alt="Unity" src="https://img.shields.io/badge/Unity-config%20pipeline-black">
</p>

<p align="center">
  🌍
  中文 |
  <a href="./README_EN.md">English</a> |
  <a href="./README_JA.md">日本語</a>
</p>

<p align="center">
  📥
  <a href="#-快速开始">快速开始</a> |
  <a href="#-excel-填表规则">填表规则</a> |
  <a href="#-在-unity-中使用">在 Unity 中使用</a>
</p>

# ExcelToProtobuf - Excel 配置表转 Protobuf 工具
ExcelToProtobuf 是一套面向 `Unity` 项目的**配置表工作流工具**，能把策划编辑的 Excel 表格**一键转换**为 Protobuf 的 C# 数据类与序列化后的二进制配置数据。  
策划只需在 Excel 中维护配置，程序端即可在 C# 中以**强类型**、按主键（`Map`）的方式读取配置数据，无需手写任何解析代码。  
整条流水线由一个可执行程序驱动：**Excel → Proto → C# 类 → DLL → 二进制数据**，双击运行即可完成全部步骤，并自动把产物同步到 Unity 工程对应目录。

## 🙏 依赖与致谢
本工具站在以下开源项目之上，感谢它们：
- [NPOI](https://github.com/nissl-lab/npoi) —— 读取 `.xlsx` 表格数据。
- [Protocol Buffers](https://github.com/protocolbuffers/protobuf)（`protoc 3.11.2` + `Google.Protobuf`）—— 生成数据类并进行二进制序列化。
- .NET Framework 自带的 `csc.exe` —— 把生成的 C# 编译为运行期加载用的 `ConfigProto.dll`。

## 📜 目录
- [简介](#exceltoprotobuf---excel-配置表转-protobuf-工具)
  - [工具特性](#工具特性)
- [💻 环境要求](#-环境要求)
- [📁 目录结构](#-目录结构)
- [🔄 工作流程](#-工作流程)
- [🌱 快速开始](#-快速开始)
  - [1. 准备配置表](#1-准备配置表)
  - [2. 运行工具](#2-运行工具)
  - [3. 查看产物](#3-查看产物)
- [📊 Excel 填表规则](#-excel-填表规则)
  - [表头结构（前 4 行）](#表头结构前-4-行)
  - [控制标记：`#` 与 `__END__`](#控制标记-与-__end__)
  - [数据类型对照表](#数据类型对照表)
  - [数组（repeated）](#数组repeated)
  - [字典（map）](#字典map)
- [🧩 在 Unity 中使用](#-在-unity-中使用)
- [🔧 自定义与扩展](#-自定义与扩展)
  - [新增一张配置表](#新增一张配置表)
  - [手写 proto 文件](#手写-proto-文件)
  - [扩展数据类型](#扩展数据类型)
- [🚧 注意事项与常见问题](#-注意事项与常见问题)
- [📋 待办事项列表](#-待办事项列表)

## 工具特性
| 特性             | 描述                                                                                     |
| ---------------- | ---------------------------------------------------------------------------------------- |
| 一键流水线       | 一条命令即可完成 Excel→Proto→C#→(内存编译)→二进制数据 全部步骤，无需人工干预。               |
| Excel 驱动       | 字段名、数据类型、配置数据全部来自 Excel 表格，策划维护表格即可，程序无需改动。               |
| 强类型访问       | 生成标准 Protobuf C# 类，程序端以强类型 + `Map<int, T>` 主键的方式读取配置。                 |
| 高效序列化       | 运行期加载的是 Protobuf 二进制（`.bytes`），体积小、解析快，适合正式包体。                    |
| 增量同步         | 生成的 `.cs` 按哈希比对，仅覆盖发生变化的文件，并自动删除已废弃的配置表类。                    |
| 明文对照         | 额外输出人类可读的 `.txt` 明文数据，方便策划与程序核对导出结果。                              |
| 数组 / 字典      | 支持 `repeated`（数组）与 `map`（字典），并提供两种数组写法（数组类型列 / 下划线拆分列）。      |
| 可扩展 proto     | `Config/Proto` 下可放置手写的 `.proto`，与表格生成的 proto 一起编译。                         |

## 💻 环境要求
**转换器工具（独立 .NET 程序，在 Unity 外运行）**
- **.NET 8 SDK** 或更高。工具为 SDK 风格项目，用 `dotnet build` / `dotnet run` 构建运行，或 `dotnet publish` 成自包含便携 exe 以保留双击即用。
- 依赖全部通过 NuGet 自动还原：`NPOI`（读 xlsx）、`Google.Protobuf` 与 `Google.Protobuf.Tools`（自带跨平台 `protoc`）、`Microsoft.CodeAnalysis.CSharp`（Roslyn 内存编译）。
- **跨平台**：Windows / macOS / Linux 均可运行，不再依赖 `.bat` 批处理或系统 `csc.exe`。

**Unity 运行时包（UPM）**
- Unity **2021.3** 或更高；脚本 API 兼容级别 `.NET Standard 2.0/2.1`。
- 随包内置 `Google.Protobuf`（netstandard2.0），用于反序列化 `.bytes`，无需另行引入。

## 📁 目录结构
```
ExcelToProtobuf/
├─ Config/
│  ├─ Excel/                                  # ← 策划编辑的配置表（*.xlsx），工具的输入
│  └─ Proto/                                  # ← 手写的 .proto（可选，会被一并编译）
│
├─ Assets/                                    # 示例 / 消费端（Unity 资源）
│  ├─ Plugins/Google.Protobuf/                # Google.Protobuf.dll（供生成的配置类在本工程内编译）
│  ├─ Source/System/ConfigSystem/Config/      # → 生成的配置类 *.cs（自动同步、增量覆盖）
│  ├─ ProductAssets/Config/                   # → 序列化后的二进制 *.bytes（运行期加载）
│  └─ UnProductAssets/Config/                 # → 明文 *.txt（仅供核对，不进正式包）
│
├─ com.alefeng.exceltoprotobuf/               # UPM 包（可通过 git URL 安装）
│  ├─ package.json
│  ├─ Runtime/                                # ConfigManager、字节来源、asmdef、Plugins/Google.Protobuf.dll
│  └─ Samples~/BasicUsage/                    # 运行时加载示例
│
└─ Tools/ExcelToProtobuf/                     # 转换器（独立 .NET 8 命令行工具）
   ├─ ExcelToProtobuf.csproj · .sln           # SDK 风格工程（NuGet 还原依赖，可从零编译）
   ├─ Program.cs                              # 管线编排 + 路径配置
   ├─ PipelineConfig.cs                       # 输入/输出路径（默认沿用仓库布局，可用参数覆盖）
   ├─ Excel2Proto.cs                          # Excel → .proto
   ├─ Excel2Bytes.cs                          # Excel → .bytes / .txt
   ├─ ProtocRunner.cs                         # 调用 protoc（.proto → .cs）
   └─ RoslynCompiler.cs                       # Roslyn 内存编译 .cs（供反射序列化）
```

> [!NOTE]
> 工具默认从可执行文件位置**向上查找**包含 `Config/Excel` 的仓库根目录来定位输入/输出，因此无需固定的目录层级。也可用命令行参数覆盖任意路径（见 [运行工具](#2-运行工具)）。

## 🔄 工作流程
主程序（`Program.cs`）依次执行以下 6 个步骤，控制台会打印每一步的进度：

1. **【Excel 转 Proto】** —— 遍历 `Config/Excel/` 下所有 `.xlsx`，按表格结构为**每个 Sheet** 生成一个 `.proto` 文件（输出到中间目录）。
2. **【Proto 拷贝】** —— 把 `Config/Proto/` 下手写的 `.proto` 一并纳入，与自动生成的 proto 共同参与编译。
3. **【Proto 转 C#】** —— 直接调用 `protoc`（来自 NuGet `Google.Protobuf.Tools`，跨平台）把 `*.proto` 生成为 C# 类。
4. **【C# 拷贝至 Unity】** —— 把生成的 `*.cs` 同步到 `Assets/Source/System/ConfigSystem/Config/`。按文件哈希比对，**相同则跳过、变化则覆盖、废弃则删除**。
5. **【C# 内存编译】** —— 用 **Roslyn** 在进程内把生成的 `.cs` 编译为内存程序集（不再落地 `ConfigProto.dll`，也不依赖系统 `csc`）。
6. **【序列化保存配置表数据】** —— 用上一步的内存程序集，逐行填充 Protobuf 对象，序列化为 `.bytes` 输出到 `Assets/ProductAssets/Config/`；同时输出明文 `.txt` 到 `Assets/UnProductAssets/Config/`。

> [!TIP]
> 每个 Sheet 会生成两个 message：数据类 `SheetName` 与容器类 `SheetName_Map`（内部是 `map<int32, SheetName> Items`）。运行期只需解析容器类，即可通过主键 `Id` 快速索引任意一行数据。

## 🌱 快速开始
### 1. 准备配置表
把需要导出的 Excel（`.xlsx`）放到 `Config/Excel/` 目录下（可分子文件夹，工具会递归查找）。  
按 [📊 Excel 填表规则](#-excel-填表规则) 填写表头与数据。一个最简单的表格如下：

| `#` / 标记 | Id  | ClassName   | `__END__` |
| ---------- | --- | ----------- | --------- |
| （字段名） | Id  | ClassName   |           |
| （说明）   | 编号 | 条件类名     |           |
| （类型）   | key | string      |           |
| （数据）   | 1001 | AttrCheck   | `__END__` |

### 2. 运行工具
在仓库根目录执行（首次会自动还原 NuGet 依赖）：
```bash
dotnet run --project Tools/ExcelToProtobuf
```
控制台会依次输出 6 个步骤的日志，末尾出现「流程执行完毕」即表示成功。

需要「双击即用」的便携程序时，发布自包含 exe：
```bash
dotnet publish Tools/ExcelToProtobuf -c Release -r win-x64 --self-contained
```

可用命令行参数覆盖默认路径，例如：
```bash
dotnet run --project Tools/ExcelToProtobuf -- --excel D:/MyGame/Config/Excel --bytes-out D:/MyGame/Assets/Config
```
支持的参数：`--excel`、`--proto-src`、`--proto-out`、`--cs-gen`、`--cs-out`、`--bytes-out`、`--txt-out`。

### 3. 查看产物
运行完成后，你会在以下位置看到自动生成的产物：
- `Assets/Source/System/ConfigSystem/Config/*.cs` —— 强类型配置类，随 Unity 工程一起编译。
- `Assets/ProductAssets/Config/*.bytes` —— 运行期加载的二进制配置数据。
- `Assets/UnProductAssets/Config/*.txt` —— 明文数据，用于核对导出是否正确。

## 📊 Excel 填表规则
工具对 Excel 的表头有固定约定。**每个 Sheet 就是一张配置表**，Sheet 的名字即最终生成的 message / 文件名。

### 表头结构（前 4 行）
表格的前 4 行是**表头**，从第 5 行开始是**数据行**：

| 行号     | 用途       | 说明                                                                 |
| -------- | ---------- | -------------------------------------------------------------------- |
| 第 1 行  | 控制标记   | 每列填 `#`（忽略此列）、`__END__`（有效列到此为止）或留作普通字段列。   |
| 第 2 行  | 字段名     | 生成 proto 字段的名字，如 `Id`、`ClassName`。带下划线可组成数组（见下）。 |
| 第 3 行  | 字段说明   | 供策划阅读的注释，工具**忽略**此行，可随意填写。                        |
| 第 4 行  | 数据类型   | 字段类型，见 [数据类型对照表](#数据类型对照表)。                        |
| 第 5 行~ | 数据       | 实际配置数据。某数据行**第一列**填 `__END__` 表示数据到此结束。          |

> [!IMPORTANT]
> 每个 Sheet 的**第一有效列**会被当作主键（`Map` 的 `key`），务必是唯一且可转为 `int` 的编号。数据行若该主键单元格为空则**跳过该行**。

### 控制标记：`#` 与 `__END__`
- **`#`**：写在第 1 行某列，表示**忽略整列**。常用于放置只给策划看、不导出的辅助列。
- **`__END__`**：
  - 写在第 1 行某列，表示**有效列到此为止**，其右侧的列不再解析。
  - 写在某**数据行的第一列**，表示**数据到此结束**，其下方的行不再解析。

### 数据类型对照表
第 4 行（类型行）支持以下类型：

| Excel 类型          | proto3 类型              | 填写示例            | 说明                                    |
| ------------------- | ------------------------ | ------------------- | --------------------------------------- |
| `int`               | `int32`                  | `100`               | 整数                                    |
| `key`               | `int32`                  | `1001`              | 整数，语义上作为主键（首个有效列）        |
| `float`             | `float`                  | `1.5`               | 浮点数                                  |
| `string`            | `string`                 | `Hello`             | 字符串                                  |
| `intArray`          | `repeated int32`         | `[1,2,3]`           | 整数数组                                |
| `floatArray`        | `repeated float`         | `[1.0,2.5]`         | 浮点数组                                |
| `stringArray`       | `repeated string`        | `[a,b,c]`           | 字符串数组                              |
| `map<int,int>`      | `map<int32,int32>`       | `{1:10,2:20}`       | 整数→整数 字典                          |
| `map<int,string>`   | `map<int32,string>`      | `{1:a,2:b}`         | 整数→字符串 字典                        |
| `map<string,int>`   | `map<string,int32>`      | `{a:1,b:2}`         | 字符串→整数 字典                        |
| `map<string,string>`| `map<string,string>`     | `{a:x,b:y}`         | 字符串→字符串 字典                      |

> [!WARNING]
> 类型行填写了对照表之外的类型时，工具会报「数据类型未定义」并中断，请从上表中选择。

### 数组（repeated）
数组有两种写法，二选一即可：

1. **数组类型列**：把某列类型直接写成 `intArray` / `floatArray` / `stringArray`，该列单元格用 `[]` 包裹、逗号分隔，如 `[1,2,3]`。
2. **下划线拆分列**：用多列表达一个数组，字段名形如 `Reward_1`、`Reward_2`、`Reward_3`，每列类型为基础类型（如 `int`）。工具会把下划线前缀相同的列**合并**为一个名为 `Reward` 的 `repeated` 字段，并按顺序收集各列的值。

### 字典（map）
字典类型的单元格用 `{}` 包裹，键值对之间用逗号 `,` 分隔，键与值之间用冒号 `:` 分隔，例如：`{1:100,2:200}`。

## 🧩 在 Unity 中使用
### 安装（UPM，git URL）
`Window → Package Manager → + → Install package from git URL...`，粘贴：
```
https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf
```
或在 `Packages/manifest.json` 的 `dependencies` 中加入：
```json
"com.alefeng.exceltoprotobuf": "https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf"
```
包内已内置 `Google.Protobuf`，并提供运行时加载器 `ConfigManager`。

> [!NOTE]
> 本仓库的 `Assets/Plugins/Google.Protobuf/` 是**示例工程自用**的依赖；如果你已通过上面的 UPM 包引入了 `Google.Protobuf`，请勿再把该目录一并拷进项目，以免程序集重复冲突。

### 用 ConfigManager 加载（推荐）
把转换器生成的 `.bytes` 放到 `Resources/Config/` 下（例如 `Resources/Config/Adventure_Condition.bytes`），并把生成的配置类 `.cs` 加入工程，然后：
```csharp
using Deploy;            // 生成类的命名空间
using ExcelToProtobuf;   // UPM 包提供的加载器

var config = new ConfigManager();                                   // 默认 Resources/Config
Adventure_Condition_Map map = config.Load<Adventure_Condition_Map>(); // 按类型名自动推断表名，带缓存
Adventure_Condition cfg = map.Items[1001];                          // 按主键 Id 取一行
Debug.Log(cfg.ClassName);
```
更换资源来源（Addressables / StreamingAssets 等）：实现 `IConfigBytesProvider` 并传入 `new ConfigManager(myProvider)`。

### 手动解析（不使用本包）
生成的 C# 类是标准 Protobuf 消息，也可直接用容器类的 `Parser` 反序列化：
```csharp
Adventure_Condition_Map map = Adventure_Condition_Map.Parser.ParseFrom(asset.bytes);
```

> [!TIP]
> 生成的类均位于 `Deploy` 命名空间下。数据类为 `SheetName`，容器类为 `SheetName_Map`，容器内的 `Items` 是以主键 `Id` 为键的字典。  
> 若想快速核对某张表的内容是否正确，可直接打开 `Assets/UnProductAssets/Config/` 下同名的 `.txt` 明文文件查看。

## 🔧 自定义与扩展
### 新增一张配置表
1. 在 `Config/Excel/` 下新建 `.xlsx`（或在已有工作簿中新增 Sheet）。
2. 按 [填表规则](#-excel-填表规则) 填好前 4 行表头与数据行。
3. 重新运行 `dotnet run --project Tools/ExcelToProtobuf`。生成的类会自动同步到 Unity，废弃的旧表类会被自动清理。

### 手写 proto 文件
如果你有一些不来自 Excel 的数据结构（如公共枚举、嵌套结构），可以把手写的 `.proto` 放到 `Config/Proto/` 目录，运行时会与表格生成的 proto 一起编译成 C#。

### 扩展数据类型
若需要支持新的字段类型，需同时修改两处映射（保持一致）：
- `Tools/ExcelToProtobuf/Excel2Proto.cs` 中的 `GetProtoType` —— 决定生成的 proto 字段类型。
- `Tools/ExcelToProtobuf/Excel2Bytes.cs` 中的 `GetRealVal` —— 决定如何把单元格文本解析为真实值。

## 🚧 注意事项与常见问题
- **Sheet 名称不可重复**：即使在不同的 Excel 文件中，Sheet 名也必须全局唯一，否则会因容器类冲突导致「可能存在重复的 Excel 表 Sheet 名称」错误。
- **proto 文件不可重名**：由于所有 `.proto` 会被收集到同一目录编译，即使位于不同文件夹也不能重名（含 `Config/Proto/` 下手写的 proto）。
- **输入/输出路径**：默认从可执行文件位置向上查找含 `Config/Excel` 的仓库根目录；若目录布局不同，用命令行参数（`--excel` / `--cs-out` / `--bytes-out` 等）显式指定。
- **依赖与版本**：`protoc` 与 `Google.Protobuf` 由 NuGet 提供，固定为 `3.11.2`（与随包的 Unity `Google.Protobuf.dll` 保持兼容）；无需系统 `csc` 或手动安装 protoc。
- **表格有效性**：一张表至少要包含 4 行表头 + 结束标记才会被识别为有效表；数据行首列遇到 `__END__` 即停止读取。

## 📋 待办事项列表
- ✅ 已完成：SDK 风格工程 + NuGet 依赖，可从零编译；移除 `.bat` 与硬编码 `csc.exe`（改用 Roslyn 内存编译）；跨平台 `protoc`；路径参数化；封装为 UPM 包并提供运行时 `ConfigManager`。
- **工具健壮性**
  - 更完善的错误提示与失败中断处理（当前部分错误仅打印日志后继续）。
  - 数组/字典解析对值内含分隔符（`,` / `:`）的支持（当前为朴素切分，属已知限制）。
- **易用性**
  - 提供图形界面或 Unity 编辑器内一键导出入口。
  - 支持多版本 `Google.Protobuf` 与更高 protobuf 版本的对齐升级。
