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

## Features
| Feature              | Description                                                                                        |
| -------------------- | ------------------------------------------------------------------------------------------------- |
| One-click pipeline   | Double-clicking `ExcelToProtobuf.exe` runs the whole Excel→Proto→C#→DLL→binary chain, hands-free.    |
| Excel-driven         | Field names, data types and data all come from the Excel sheets; designers just edit sheets.        |
| Strongly typed       | Generates standard Protobuf C# classes; read config in a strongly typed way via a `Map<int, T>` key. |
| Efficient binary     | The runtime loads compact Protobuf binaries (`.bytes`) — small and fast to parse, fit for shipping.  |
| Incremental sync     | Generated `.cs` files are hash-compared; only changed files are overwritten, obsolete ones removed.  |
| Plain-text mirror    | Also outputs human-readable `.txt` data so designers/programmers can verify the export.             |
| Arrays / maps        | Supports `repeated` (arrays) and `map` (dictionaries), with two ways to author arrays.               |
| Extensible proto     | Hand-written `.proto` files under `Config/Proto` are compiled together with the generated ones.      |

## 💻 Requirements
- **Windows** OS. The pipeline relies on `.bat` scripts, `protoc.exe` (win64) and the system `csc.exe`, so only Windows is supported for now.
- **.NET Framework 4.7.1** (see `App.config`). Compiling `ConfigProto.dll` uses `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe`, so that path must exist.
- **protoc 3.11.2** (bundled at `Tools/ExcelToProtobuf/protoc-3.11.2-win64/`; no separate install needed).
- **NPOI / Google.Protobuf** (the relevant DLLs are bundled under `bin/Debug`; no separate install needed).
- The consumer side is a **Unity project**; at runtime it needs a reference to `Google.Protobuf` to deserialize the `.bytes` data.

## 📁 Directory Layout
```
ExcelToProtobuf/
├─ Config/
│  ├─ Excel/                                  # ← Sheets authored by designers (*.xlsx) — the input
│  └─ Proto/                                  # ← Hand-written .proto (optional, compiled together)
│
├─ Assets/                                    # Unity project
│  ├─ Source/System/ConfigSystem/Config/      # → Generated config classes *.cs (auto-synced, incremental)
│  ├─ ProductAssets/Config/                   # → Serialized binaries *.bytes (loaded at runtime)
│  └─ UnProductAssets/Config/                 # → Plain-text *.txt (for verification, not shipped)
│
└─ Tools/ExcelToProtobuf/
   ├─ bin/Debug|Release/ExcelToProtobuf.exe   # Main program (double-click to run)
   ├─ Protos/                                 # Intermediate: generated / collected .proto
   ├─ Csharp/                                 # Intermediate: protoc-generated .cs and the compiled ConfigProto.dll
   ├─ protoc.exe · protoc-3.11.2-win64/       # protobuf compiler
   ├─ BuildProtos.bat                         # .proto → .cs
   └─ BuildDLL.bat                            # .cs → ConfigProto.dll
```

> [!NOTE]
> The program locates every path by **relative directory structure**: from the executable's `bin/Debug` (or `bin/Release`) it walks up two levels to `Tools/ExcelToProtobuf`, then two more to the repo root. Keep this layout intact and just double-click the exe under `bin`.

## 🔄 Pipeline
The main program (`Program.cs`) runs the following 6 steps in order, printing progress for each to the console:

1. **[Excel → Proto]** — Scans every `.xlsx` under `Config/Excel/` and generates one `.proto` file **per Sheet**, into `Tools/ExcelToProtobuf/Protos/`.
2. **[Copy proto]** — Copies the hand-written `.proto` from `Config/Proto/` into `Protos/` as well, so they compile together with the generated ones.
3. **[Proto → C#]** — Calls `BuildProtos.bat` (which runs `protoc`) to generate C# classes from `Protos/*.proto` into `Csharp/`.
4. **[Copy C# to Unity]** — Syncs `Csharp/*.cs` into `Assets/Source/System/ConfigSystem/Config/`. Files are hash-compared: **unchanged are skipped, changed are overwritten, obsolete are deleted**.
5. **[C# → DLL]** — Calls `BuildDLL.bat` (which runs `csc`) to compile `Csharp/*.cs` into `ConfigProto.dll` (loaded by reflection in the next step).
6. **[Serialize config data]** — Loads `ConfigProto.dll`, reads the Excel data rows again, fills a Protobuf object per row, serializes to `.bytes` into `Assets/ProductAssets/Config/`, and also writes plain-text `.txt` into `Assets/UnProductAssets/Config/`.

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
Double-click `Tools/ExcelToProtobuf/bin/Debug/ExcelToProtobuf.exe` (or the same program under `bin/Release/`).  
The console prints the logs of the 6 steps in order; "流程执行完毕，按任意键退出" (pipeline finished, press any key to exit) means success.

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
The generated C# classes are standard Protobuf messages; at runtime you deserialize the `.bytes` via the container's `Parser`. The following is an **example** (the actual asset-loading method depends on your project — `Resources` / `Addressables` / `StreamingAssets`):

```csharp
using Deploy; // namespace of the generated classes

// 1. Obtain the .bytes byte array any way you like (Resources shown here)
TextAsset asset = Resources.Load<TextAsset>("Config/Adventure_Condition");

// 2. Parse the container class (SheetName_Map)
Adventure_Condition_Map map = Adventure_Condition_Map.Parser.ParseFrom(asset.bytes);

// 3. Index a row directly by its primary key Id
Adventure_Condition cfg = map.Items[1001];
Debug.Log(cfg.ClassName);

// Or iterate over all rows
foreach (var kv in map.Items)
{
    Debug.Log($"{kv.Key} => {kv.Value.ClassName}");
}
```

> [!TIP]
> All generated classes live in the `Deploy` namespace. The data class is `SheetName`, the container is `SheetName_Map`, and its `Items` is a dictionary keyed by the primary key `Id`.  
> To quickly verify a sheet's content, open the matching `.txt` plain-text file under `Assets/UnProductAssets/Config/`.

## 🔧 Customization & Extension
### Add a new config sheet
1. Create a new `.xlsx` under `Config/Excel/` (or add a Sheet to an existing workbook).
2. Fill the 4 header rows and the data rows per the [authoring rules](#-excel-authoring-rules).
3. Re-run `ExcelToProtobuf.exe`. The generated class is auto-synced to Unity, and obsolete old-sheet classes are cleaned up automatically.

### Hand-written proto files
If you have data structures that don't come from Excel (shared enums, nested structures, etc.), put the hand-written `.proto` under `Config/Proto/`. At run time they're copied into `Protos/` and compiled to C# together.

### Extend data types
To support a new field type, update both mappings (keep them consistent):
- `GetProtoType` in `Tools/ExcelToProtobuf/Excel2Proto.cs` — decides the generated proto field type.
- `GetRealVal` in `Tools/ExcelToProtobuf/Excel2Bytes.cs` — decides how a cell's text is parsed into the real value.

## 🚧 Notes & FAQ
- **Sheet names must be unique**: even across different Excel files, Sheet names must be globally unique, otherwise a container-class conflict raises the "possible duplicate Excel Sheet name" error.
- **Proto files must not share names**: since all `.proto` are collected into one folder to compile, they can't share names even across folders (including hand-written ones under `Config/Proto/`).
- **Keep the directory structure**: the tool locates input/output folders by relative paths — don't move the relative layout of `Config/`, `Assets/…/Config/`, or `Tools/ExcelToProtobuf/`.
- **csc / protoc paths**: `BuildDLL.bat` relies on `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe`; the `protoc` version is `3.11.2`. Adjust for a different environment.
- **Sheet validity**: a table is only recognized as valid if it has at least 4 header rows plus the end marker; data reading stops when the first column of a data row is `__END__`.

## 📋 To-Do List
- **Robustness**
  - Remove the dependency on the fixed absolute `csc.exe` path; support more flexible .NET / build environments.
  - Better error reporting and fail-fast handling (some errors currently just log and continue).
- **Cross-platform**
  - Move away from `.bat` and win64 `protoc`; explore a cross-platform (macOS / Linux) runner.
- **Usability**
  - Provide a GUI or an in-Unity one-click export entry.
  - Support configurable paths and export options (instead of the current hard-coded directory convention).
