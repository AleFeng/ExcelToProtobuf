<p align="center">
  <img alt="GitHub Repo License" src="https://img.shields.io/github/license/AleFeng/ExcelToProtobuf?color=blueviolet">
  <img alt="GitHub Repo Issues" src="https://img.shields.io/github/issues/AleFeng/ExcelToProtobuf?color=yellow">
  <img alt="GitHub Repo Stars" src="https://img.shields.io/github/stars/AleFeng/ExcelToProtobuf?color=blue">
  <img alt="Language" src="https://img.shields.io/badge/language-C%23-239120">
  <img alt="Unity" src="https://img.shields.io/badge/Unity-config%20pipeline-black">
</p>

<p align="center">
  🌍
  <a href="./README.md">中文</a> |
  English |
  <a href="./README_JA.md">日本語</a>
</p>

<p align="center">
  📥
  <a href="#-quick-start">Quick Start</a> |
  <a href="#-excel-authoring-rules">Authoring Rules</a> |
  <a href="#-using-in-unity">Using in Unity</a>
</p>

# ExcelToProtobuf - Excel Config Sheets to Protobuf
ExcelToProtobuf is a **config pipeline tool** for `Unity` projects. It **converts, with a single run**, the Excel sheets authored by designers into Protobuf C# data classes and serialized binary config data.  
Designers only maintain the Excel sheets; programmers then read the config data in C# in a **strongly typed** way, indexed by primary key (`Map`), without writing any parsing code.  
The whole pipeline is driven by one executable: **Excel → Proto → C# classes → DLL → binary data**. Double-click to run all steps, and the outputs are synced into the corresponding Unity project folders automatically.

## 🙏 Dependencies & Credits
This tool stands on the following open-source projects — thanks to them:
- [NPOI](https://github.com/nissl-lab/npoi) — reads `.xlsx` sheet data.
- [Protocol Buffers](https://github.com/protocolbuffers/protobuf) (`protoc 3.11.2` + `Google.Protobuf`) — generates data classes and serializes binary data.
- The `csc.exe` bundled with .NET Framework — compiles the generated C# into `ConfigProto.dll` used at load time.

## 📜 Table of Contents
- [Introduction](#exceltoprotobuf---excel-config-sheets-to-protobuf)
  - [Features](#features)
- [💻 Requirements](#-requirements)
- [📁 Directory Layout](#-directory-layout)
- [🔄 Pipeline](#-pipeline)
- [🌱 Quick Start](#-quick-start)
  - [1. Prepare the sheets](#1-prepare-the-sheets)
  - [2. Run the tool](#2-run-the-tool)
  - [3. Inspect the output](#3-inspect-the-output)
- [📊 Excel Authoring Rules](#-excel-authoring-rules)
  - [Header structure (first 4 rows)](#header-structure-first-4-rows)
  - [Control markers: `#` and `__END__`](#control-markers--and-__end__)
  - [Type mapping table](#type-mapping-table)
  - [Arrays (repeated)](#arrays-repeated)
  - [Dictionaries (map)](#dictionaries-map)
- [🧩 Using in Unity](#-using-in-unity)
- [🔧 Customization & Extension](#-customization--extension)
  - [Add a new config sheet](#add-a-new-config-sheet)
  - [Hand-written proto files](#hand-written-proto-files)
  - [Extend data types](#extend-data-types)
- [🚧 Notes & FAQ](#-notes--faq)
- [📋 To-Do List](#-to-do-list)
- [📄 License](#-license)

## Features
| Feature              | Description                                                                                        |
| -------------------- | ------------------------------------------------------------------------------------------------- |
| One-click pipeline   | A single command runs the whole Excel→Proto→C#→(in-memory compile)→binary chain, hands-free.         |
| Excel-driven         | Field names, data types and data all come from the Excel sheets; designers just edit sheets.        |
| Strongly typed       | Generates standard Protobuf C# classes; read config in a strongly typed way via a `Map<int, T>` key. |
| Efficient binary     | The runtime loads compact Protobuf binaries (`.bytes`) — small and fast to parse, fit for shipping.  |
| Incremental sync     | Generated `.cs` files are hash-compared; only changed files are overwritten, obsolete ones removed.  |
| Plain-text mirror    | Also outputs human-readable `.txt` data so designers/programmers can verify the export.             |
| Arrays / maps        | Supports `repeated` (arrays) and `map` (dictionaries), with two ways to author arrays.               |
| Extensible proto     | Hand-written `.proto` files under `Config/Proto` are compiled together with the generated ones.      |

## 💻 Requirements
**Converter tool (standalone .NET program, runs outside Unity)**
- **.NET 8 SDK** or newer. It's an SDK-style project: build/run with `dotnet build` / `dotnet run`, or `dotnet publish` into a self-contained portable exe to keep the double-click experience.
- All dependencies are restored from NuGet: `NPOI` (reads xlsx), `Google.Protobuf` and `Google.Protobuf.Tools` (bundled cross-platform `protoc`), and `Microsoft.CodeAnalysis.CSharp` (Roslyn in-memory compile).
- **Cross-platform**: runs on Windows / macOS / Linux — no more `.bat` scripts or system `csc.exe`.

**Unity runtime package (UPM)**
- Unity **2021.3** or newer; scripting API compatibility level `.NET Standard 2.0/2.1`.
- Ships `Google.Protobuf` (netstandard2.0) inside the package to deserialize `.bytes`, so nothing extra to install.

## 📁 Directory Layout
```
ExcelToProtobuf/
├─ Config/
│  ├─ Excel/                                  # ← Sheets authored by designers (*.xlsx) — the input
│  └─ Proto/                                  # ← Hand-written .proto (optional, compiled together)
│
├─ Assets/                                    # Example / consumer (Unity assets)
│  ├─ Plugins/Google.Protobuf/                # Google.Protobuf.dll (so the generated classes compile here)
│  ├─ Source/System/ConfigSystem/Config/      # → Generated config classes *.cs (auto-synced, incremental)
│  ├─ ProductAssets/Config/                   # → Serialized binaries *.bytes (loaded at runtime)
│  └─ UnProductAssets/Config/                 # → Plain-text *.txt (for verification, not shipped)
│
├─ com.alefeng.exceltoprotobuf/               # UPM package (installable via git URL)
│  ├─ package.json
│  ├─ Runtime/                                # ConfigManager, byte providers, asmdef, Plugins/Google.Protobuf.dll
│  └─ Samples~/BasicUsage/                    # Runtime loading sample
│
└─ Tools/ExcelToProtobuf/                     # Converter (standalone .NET 8 CLI tool)
   ├─ ExcelToProtobuf.csproj · .sln           # SDK-style project (NuGet-restored, builds from a clean clone)
   ├─ Program.cs                              # Pipeline orchestration + path config
   ├─ PipelineConfig.cs                       # Input/output paths (default repo layout, overridable via args)
   ├─ Excel2Proto.cs                          # Excel → .proto
   ├─ Excel2Bytes.cs                          # Excel → .bytes / .txt
   ├─ ProtocRunner.cs                         # Invokes protoc (.proto → .cs)
   └─ RoslynCompiler.cs                       # Roslyn in-memory compile of .cs (for reflective serialization)
```

> [!NOTE]
> The tool locates paths by **walking up** from the executable to find the repo root containing `Config/Excel`, so no fixed nesting is required. Any path can be overridden via command-line args (see [Run the tool](#2-run-the-tool)).

## 🔄 Pipeline
The main program (`Program.cs`) runs the following 6 steps in order, printing progress for each to the console:

1. **[Excel → Proto]** — Scans every `.xlsx` under `Config/Excel/` and generates one `.proto` file **per Sheet** (into an intermediate directory).
2. **[Copy proto]** — Brings in the hand-written `.proto` from `Config/Proto/` too, so they compile together with the generated ones.
3. **[Proto → C#]** — Invokes `protoc` directly (from the NuGet `Google.Protobuf.Tools`, cross-platform) to generate C# classes from the `.proto` files.
4. **[Copy C# to Unity]** — Syncs the generated `*.cs` into `Assets/Source/System/ConfigSystem/Config/`. Files are hash-compared: **unchanged are skipped, changed are overwritten, obsolete are deleted**.
5. **[C# in-memory compile]** — Uses **Roslyn** to compile the generated `.cs` into an in-memory assembly (no more `ConfigProto.dll` on disk, no dependency on the system `csc`).
6. **[Serialize config data]** — Using that in-memory assembly, fills a Protobuf object per row, serializes to `.bytes` into `Assets/ProductAssets/Config/`, and writes plain-text `.txt` into `Assets/UnProductAssets/Config/`.

> [!TIP]
> Each Sheet produces two messages: the data class `SheetName` and the container `SheetName_Map` (internally a `map<int32, SheetName> Items`). At runtime you only parse the container and index any row quickly by its primary key `Id`.

## 🌱 Quick Start
### 1. Prepare the sheets
Put the Excel files (`.xlsx`) to export under `Config/Excel/` (subfolders are fine; the tool searches recursively).  
Fill the header and data following the [📊 Excel Authoring Rules](#-excel-authoring-rules). A minimal sheet looks like this:

| `#` / marker | Id   | ClassName | `__END__` |
| ------------ | ---- | --------- | --------- |
| (field name) | Id   | ClassName |           |
| (comment)    | id   | condition |           |
| (type)       | key  | string    |           |
| (data)       | 1001 | AttrCheck | `__END__` |

### 2. Run the tool
From the repo root (the first run restores NuGet dependencies):
```bash
dotnet run --project Tools/ExcelToProtobuf
```
The console prints the logs of the 6 steps in order; "流程执行完毕" (pipeline finished) means success.

For a double-click portable program, publish a self-contained exe:
```bash
dotnet publish Tools/ExcelToProtobuf -c Release -r win-x64 --self-contained
```

Override default paths via command-line args, e.g.:
```bash
dotnet run --project Tools/ExcelToProtobuf -- --excel D:/MyGame/Config/Excel --bytes-out D:/MyGame/Assets/Config
```
Supported args: `--excel`, `--proto-src`, `--proto-out`, `--cs-gen`, `--cs-out`, `--bytes-out`, `--txt-out`.

### 3. Inspect the output
After a successful run, you'll find the generated artifacts here:
- `Assets/Source/System/ConfigSystem/Config/*.cs` — strongly typed config classes, compiled with the Unity project.
- `Assets/ProductAssets/Config/*.bytes` — the binary config data loaded at runtime.
- `Assets/UnProductAssets/Config/*.txt` — plain-text data to verify the export is correct.

## 📊 Excel Authoring Rules
The tool has a fixed convention for the sheet header. **Each Sheet is one config table**; the Sheet name becomes the generated message / file name.

### Header structure (first 4 rows)
The first 4 rows are the **header**; data rows start at row 5:

| Row     | Purpose        | Description                                                                          |
| ------- | -------------- | ------------------------------------------------------------------------------------ |
| Row 1   | Control marker | Per column: `#` (ignore this column), `__END__` (valid columns end here), or a field. |
| Row 2   | Field name     | The proto field name, e.g. `Id`, `ClassName`. Underscores can form arrays (see below). |
| Row 3   | Comment        | A note for designers; the tool **ignores** this row, fill it freely.                  |
| Row 4   | Data type      | The field type; see the [Type mapping table](#type-mapping-table).                    |
| Row 5+  | Data           | The actual config data. A data row whose **first column** is `__END__` ends the data. |

> [!IMPORTANT]
> The **first valid column** of each Sheet is used as the primary key (the `Map` key); it must be a unique number convertible to `int`. A data row whose primary-key cell is empty is **skipped**.

### Control markers: `#` and `__END__`
- **`#`**: put in row 1 of a column to **ignore the whole column**. Handy for helper columns meant only for designers, not exported.
- **`__END__`**:
  - In row 1 of a column: **valid columns end here**; columns to its right are not parsed.
  - In the **first column of a data row**: **data ends here**; rows below are not parsed.

### Type mapping table
Row 4 (the type row) supports the following types:

| Excel type           | proto3 type              | Example       | Description                                  |
| -------------------- | ------------------------ | ------------- | -------------------------------------------- |
| `int`                | `int32`                  | `100`         | Integer                                      |
| `key`                | `int32`                  | `1001`        | Integer, used semantically as the primary key |
| `float`              | `float`                  | `1.5`         | Floating point                               |
| `string`             | `string`                 | `Hello`       | String                                       |
| `intArray`           | `repeated int32`         | `[1,2,3]`     | Integer array                                |
| `floatArray`         | `repeated float`         | `[1.0,2.5]`   | Float array                                  |
| `stringArray`        | `repeated string`        | `[a,b,c]`     | String array                                 |
| `map<int,int>`       | `map<int32,int32>`       | `{1:10,2:20}` | int → int dictionary                         |
| `map<int,string>`    | `map<int32,string>`      | `{1:a,2:b}`   | int → string dictionary                      |
| `map<string,int>`    | `map<string,int32>`      | `{a:1,b:2}`   | string → int dictionary                      |
| `map<string,string>` | `map<string,string>`     | `{a:x,b:y}`   | string → string dictionary                   |

> [!WARNING]
> If the type row contains a type outside this table, the tool reports "data type undefined" and stops. Pick a type from the table above.

### Arrays (repeated)
There are two ways to author an array — pick either:

1. **Array-type column**: set a column's type directly to `intArray` / `floatArray` / `stringArray`, and wrap the cell value in `[]` with comma separators, e.g. `[1,2,3]`.
2. **Underscore split columns**: express one array with several columns whose field names look like `Reward_1`, `Reward_2`, `Reward_3`, each of a base type (e.g. `int`). The tool **merges** columns sharing the same underscore prefix into a single `repeated` field named `Reward`, collecting the values in order.

### Dictionaries (map)
Wrap a map cell in `{}`, separate entries with commas `,`, and separate key and value with a colon `:`, e.g. `{1:100,2:200}`.

## 🧩 Using in Unity
### Install (UPM, git URL)
`Window → Package Manager → + → Install package from git URL...`, then paste:
```
https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf
```
Or add to `dependencies` in `Packages/manifest.json`:
```json
"com.alefeng.exceltoprotobuf": "https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf"
```
The package bundles `Google.Protobuf` and provides the `ConfigManager` runtime loader.

> [!NOTE]
> This repo's `Assets/Plugins/Google.Protobuf/` is for the **example project's own** compilation; if you already pulled in `Google.Protobuf` via the UPM package above, don't also copy that folder into your project — it would cause a duplicate-assembly conflict.

### Load with ConfigManager (recommended)
Put the converter's `.bytes` under `Resources/Config/` (e.g. `Resources/Config/Adventure_Condition.bytes`), add the generated `.cs` classes to your project, then:
```csharp
using Deploy;            // generated classes
using ExcelToProtobuf;   // loader from the UPM package

var config = new ConfigManager();                                    // defaults to Resources/Config
Adventure_Condition_Map map = config.Load<Adventure_Condition_Map>(); // table name inferred from type, cached
Adventure_Condition cfg = map.Items[1001];                           // index a row by primary key Id
Debug.Log(cfg.ClassName);
```
For a different source (Addressables / StreamingAssets, etc.), implement `IConfigBytesProvider` and pass `new ConfigManager(myProvider)`.

### Manual parsing (without the package)
The generated classes are standard Protobuf messages, so you can also deserialize via the container's `Parser`:
```csharp
Adventure_Condition_Map map = Adventure_Condition_Map.Parser.ParseFrom(asset.bytes);
```

> [!TIP]
> All generated classes live in the `Deploy` namespace. The data class is `SheetName`, the container is `SheetName_Map`, and its `Items` is a dictionary keyed by the primary key `Id`.  
> To quickly verify a sheet's content, open the matching `.txt` plain-text file under `Assets/UnProductAssets/Config/`.

## 🔧 Customization & Extension
### Add a new config sheet
1. Create a new `.xlsx` under `Config/Excel/` (or add a Sheet to an existing workbook).
2. Fill the 4 header rows and the data rows per the [authoring rules](#-excel-authoring-rules).
3. Re-run `dotnet run --project Tools/ExcelToProtobuf`. The generated class is auto-synced to Unity, and obsolete old-sheet classes are cleaned up automatically.

### Hand-written proto files
If you have data structures that don't come from Excel (shared enums, nested structures, etc.), put the hand-written `.proto` under `Config/Proto/`. They are compiled to C# together with the sheet-generated protos.

### Extend data types
To support a new field type, update both mappings (keep them consistent):
- `GetProtoType` in `Tools/ExcelToProtobuf/Excel2Proto.cs` — decides the generated proto field type.
- `GetRealVal` in `Tools/ExcelToProtobuf/Excel2Bytes.cs` — decides how a cell's text is parsed into the real value.

## 🚧 Notes & FAQ
- **Sheet names must be unique**: even across different Excel files, Sheet names must be globally unique, otherwise a container-class conflict raises the "possible duplicate Excel Sheet name" error.
- **Proto files must not share names**: since all `.proto` are collected into one folder to compile, they can't share names even across folders (including hand-written ones under `Config/Proto/`).
- **Input/output paths**: by default the tool walks up from the executable to find the repo root containing `Config/Excel`; if your layout differs, pass explicit args (`--excel` / `--cs-out` / `--bytes-out`, etc.).
- **Dependencies & version**: `protoc` and `Google.Protobuf` come from NuGet, pinned to `3.11.2` (compatible with the Unity `Google.Protobuf.dll` shipped in the package); no system `csc` or manual protoc install needed.
- **Sheet validity**: a table is only recognized as valid if it has at least 4 header rows plus the end marker; data reading stops when the first column of a data row is `__END__`.

## 📋 To-Do List
- ✅ Done: SDK-style project + NuGet deps that build from a clean clone; removed `.bat` and the hard-coded `csc.exe` (now Roslyn in-memory compile); cross-platform `protoc`; parameterized paths; packaged as UPM with a runtime `ConfigManager`.
- **Robustness**
  - Better error reporting and fail-fast handling (some errors currently just log and continue).
  - Array/map parsing when values contain the delimiters (`,` / `:`) — currently a naive split (known limitation).
- **Usability**
  - Provide a GUI or an in-Unity one-click export entry.
  - Align/upgrade to a newer `Google.Protobuf` version across tool and package.

## 📄 License
Released under the **MIT** license — see [LICENSE](LICENSE).
