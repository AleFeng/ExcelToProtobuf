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
  <a href="./README_EN.md">English</a> |
  日本語
</p>

<p align="center">
  📥
  <a href="#-クイックスタート">クイックスタート</a> |
  <a href="#-excel-記入ルール">記入ルール</a> |
  <a href="#-unity-での使い方">Unity での使い方</a>
</p>

# ExcelToProtobuf - Excel コンフィグ表を Protobuf へ変換
ExcelToProtobuf は `Unity` プロジェクト向けの**コンフィグワークフローツール**です。プランナーが編集した Excel 表を、**ワンクリック**で Protobuf の C# データクラスとシリアライズ済みバイナリコンフィグデータに変換します。  
プランナーは Excel を管理するだけでよく、プログラマーは解析コードを一切書かずに、**強い型付け**で主キー（`Map`）ごとにコンフィグデータを C# から取得できます。  
パイプライン全体は 1 つの実行ファイルで駆動されます。**Excel → Proto → C# クラス → DLL → バイナリデータ**。ダブルクリックで全工程が完了し、成果物は Unity プロジェクトの対応フォルダへ自動的に同期されます。

## 🙏 依存とクレジット
本ツールは以下のオープンソースプロジェクトの上に成り立っています。感謝します。
- [NPOI](https://github.com/nissl-lab/npoi) —— `.xlsx` 表のデータを読み込みます。
- [Protocol Buffers](https://github.com/protocolbuffers/protobuf)（`protoc 3.11.2` + `Google.Protobuf`）—— データクラスの生成とバイナリシリアライズを行います。
- .NET Framework 同梱の `csc.exe` —— 生成した C# をロード時に使う `ConfigProto.dll` へコンパイルします。

## 📜 目次
- [概要](#exceltoprotobuf---excel-コンフィグ表を-protobuf-へ変換)
  - [特徴](#特徴)
- [💻 動作環境](#-動作環境)
- [📁 ディレクトリ構成](#-ディレクトリ構成)
- [🔄 処理フロー](#-処理フロー)
- [🌱 クイックスタート](#-クイックスタート)
  - [1. 表を用意する](#1-表を用意する)
  - [2. ツールを実行する](#2-ツールを実行する)
  - [3. 成果物を確認する](#3-成果物を確認する)
- [📊 Excel 記入ルール](#-excel-記入ルール)
  - [ヘッダー構造（先頭 4 行）](#ヘッダー構造先頭-4-行)
  - [制御マーカー：`#` と `__END__`](#制御マーカー-と-__end__)
  - [データ型の対応表](#データ型の対応表)
  - [配列（repeated）](#配列repeated)
  - [辞書（map）](#辞書map)
- [🧩 Unity での使い方](#-unity-での使い方)
- [🔧 カスタマイズと拡張](#-カスタマイズと拡張)
  - [新しいコンフィグ表を追加する](#新しいコンフィグ表を追加する)
  - [手書きの proto ファイル](#手書きの-proto-ファイル)
  - [データ型を拡張する](#データ型を拡張する)
- [🚧 注意事項と FAQ](#-注意事項と-faq)
- [📋 To-Do リスト](#-to-do-リスト)
- [📄 ライセンス](#-ライセンス)

## 特徴
| 特徴                | 説明                                                                                          |
| ------------------- | --------------------------------------------------------------------------------------------- |
| ワンクリック処理    | 1 つのコマンドで Excel→Proto→C#→(メモリ内コンパイル)→バイナリの全工程が自動実行。                |
| Excel 駆動          | フィールド名・データ型・データはすべて Excel 表由来。プランナーは表を編集するだけで済みます。      |
| 強い型付け          | 標準の Protobuf C# クラスを生成し、`Map<int, T>` の主キーで強く型付けされたコンフィグを取得。      |
| 効率的なバイナリ    | ランタイムは軽量な Protobuf バイナリ（`.bytes`）を読み込みます。小さく高速で、製品ビルド向け。      |
| 増分同期            | 生成された `.cs` はハッシュ比較され、変更されたファイルのみ上書き、不要なものは削除されます。       |
| プレーンテキスト対照 | 人が読める `.txt` データも出力し、プランナー／プログラマーがエクスポート結果を確認できます。       |
| 配列 / 辞書         | `repeated`（配列）と `map`（辞書）に対応。配列は 2 通りの書き方があります。                        |
| 拡張可能な proto    | `Config/Proto` に置いた手書きの `.proto` も、生成された proto と一緒にコンパイルされます。          |

## 💻 動作環境
**変換ツール（独立した .NET プログラム、Unity 外で実行）**
- **.NET 8 SDK** 以上。SDK スタイルのプロジェクトで、`dotnet build` / `dotnet run` でビルド・実行、または `dotnet publish` で自己完結型の携帯 exe を作成できます（ダブルクリック実行も維持）。
- 依存はすべて NuGet から自動復元：`NPOI`（xlsx 読込）、`Google.Protobuf` と `Google.Protobuf.Tools`（クロスプラットフォームの `protoc` 同梱）、`Microsoft.CodeAnalysis.CSharp`（Roslyn メモリ内コンパイル）。
- **クロスプラットフォーム**：Windows / macOS / Linux で動作。`.bat` バッチやシステムの `csc.exe` には依存しません。

**Unity ランタイムパッケージ（UPM）**
- Unity **2021.3** 以上；スクリプト API 互換レベル `.NET Standard 2.0/2.1`。
- `.bytes` をデシリアライズするための `Google.Protobuf`（netstandard2.0）をパッケージに同梱。別途導入は不要です。

## 📁 ディレクトリ構成
```
ExcelToProtobuf/
├─ Config/
│  ├─ Excel/                                  # ← プランナーが編集する表（*.xlsx）——入力
│  └─ Proto/                                  # ← 手書きの .proto（任意、一緒にコンパイル）
│
├─ Assets/                                    # サンプル / 利用側（Unity アセット）
│  ├─ Plugins/Google.Protobuf/                # Google.Protobuf.dll（生成クラスを本プロジェクトでコンパイルするため）
│  ├─ Source/System/ConfigSystem/Config/      # → 生成されたコンフィグクラス *.cs（自動同期・増分）
│  ├─ ProductAssets/Config/                   # → シリアライズ済みバイナリ *.bytes（ランタイム読込）
│  └─ UnProductAssets/Config/                 # → プレーンテキスト *.txt（確認用、製品には含めない）
│
├─ com.alefeng.exceltoprotobuf/               # UPM パッケージ（git URL で導入可能）
│  ├─ package.json
│  ├─ Runtime/                                # ConfigManager、バイト供給、asmdef、Plugins/Google.Protobuf.dll
│  └─ Samples~/BasicUsage/                    # ランタイム読込サンプル
│
└─ Tools/ExcelToProtobuf/                     # 変換ツール（独立した .NET 8 CLI）
   ├─ ExcelToProtobuf.csproj · .sln           # SDK スタイル（NuGet 復元、クリーンなクローンからビルド可能）
   ├─ Program.cs                              # パイプライン制御 + パス設定
   ├─ PipelineConfig.cs                       # 入出力パス（既定はリポジトリ構成、引数で上書き可）
   ├─ Excel2Proto.cs                          # Excel → .proto
   ├─ Excel2Bytes.cs                          # Excel → .bytes / .txt
   ├─ ProtocRunner.cs                         # protoc 呼び出し（.proto → .cs）
   └─ RoslynCompiler.cs                       # Roslyn による .cs のメモリ内コンパイル（リフレクション用）
```

> [!NOTE]
> ツールは実行ファイルの位置から**上方向に探索**して `Config/Excel` を含むリポジトリルートを特定するため、固定の階層は不要です。任意のパスはコマンドライン引数で上書きできます（[ツールを実行する](#2-ツールを実行する) 参照）。

## 🔄 処理フロー
メインプログラム（`Program.cs`）は次の 6 ステップを順に実行し、各ステップの進捗をコンソールに出力します。

1. **【Excel → Proto】** —— `Config/Excel/` 配下のすべての `.xlsx` を走査し、**Sheet ごと**に 1 つの `.proto` を生成します（中間ディレクトリへ出力）。
2. **【Proto コピー】** —— `Config/Proto/` の手書き `.proto` も取り込み、生成された proto と一緒にコンパイル対象にします。
3. **【Proto → C#】** —— `protoc`（NuGet の `Google.Protobuf.Tools`、クロスプラットフォーム）を直接呼び出し、`.proto` から C# クラスを生成します。
4. **【C# を Unity へコピー】** —— 生成した `*.cs` を `Assets/Source/System/ConfigSystem/Config/` へ同期します。ハッシュ比較により、**同一はスキップ・変更は上書き・不要は削除**します。
5. **【C# メモリ内コンパイル】** —— **Roslyn** で生成した `.cs` をメモリ内アセンブリにコンパイルします（`ConfigProto.dll` を書き出さず、システムの `csc` にも依存しません）。
6. **【コンフィグデータのシリアライズ保存】** —— 前ステップのメモリ内アセンブリを使い、行ごとに Protobuf オブジェクトへ格納し、`.bytes` として `Assets/ProductAssets/Config/` へ出力。あわせてプレーンテキスト `.txt` を `Assets/UnProductAssets/Config/` へ出力します。

> [!TIP]
> 各 Sheet からは 2 つの message が生成されます。データクラス `SheetName` と、コンテナ `SheetName_Map`（内部は `map<int32, SheetName> Items`）です。ランタイムではコンテナを解析するだけで、主キー `Id` から任意の行を高速に参照できます。

## 🌱 クイックスタート
### 1. 表を用意する
エクスポートしたい Excel（`.xlsx`）を `Config/Excel/` に置きます（サブフォルダも可。ツールは再帰的に探索します）。  
[📊 Excel 記入ルール](#-excel-記入ルール) に従ってヘッダーとデータを記入します。最小構成の表は次のとおりです。

| `#` / マーカー | Id   | ClassName | `__END__` |
| -------------- | ---- | --------- | --------- |
| （フィールド名）| Id   | ClassName |           |
| （説明）       | 番号 | 条件クラス |           |
| （型）         | key  | string    |           |
| （データ）     | 1001 | AttrCheck | `__END__` |

### 2. ツールを実行する
リポジトリのルートで実行します（初回は NuGet 依存を自動復元）：
```bash
dotnet run --project Tools/ExcelToProtobuf
```
コンソールに 6 ステップのログが順に出力され、末尾に「流程执行完毕」（処理完了）が表示されれば成功です。

「ダブルクリック実行」の携帯プログラムが欲しい場合は、自己完結型 exe を発行します：
```bash
dotnet publish Tools/ExcelToProtobuf -c Release -r win-x64 --self-contained
```

既定のパスはコマンドライン引数で上書きできます。例：
```bash
dotnet run --project Tools/ExcelToProtobuf -- --excel D:/MyGame/Config/Excel --bytes-out D:/MyGame/Assets/Config
```
対応引数：`--excel`、`--proto-src`、`--proto-out`、`--cs-gen`、`--cs-out`、`--bytes-out`、`--txt-out`。

### 3. 成果物を確認する
実行に成功すると、次の場所に生成された成果物が確認できます。
- `Assets/Source/System/ConfigSystem/Config/*.cs` —— 強く型付けされたコンフィグクラス。Unity プロジェクトと一緒にコンパイルされます。
- `Assets/ProductAssets/Config/*.bytes` —— ランタイムに読み込むバイナリコンフィグデータ。
- `Assets/UnProductAssets/Config/*.txt` —— エクスポートが正しいか確認するためのプレーンテキストデータ。

## 📊 Excel 記入ルール
ツールは Excel のヘッダーに固定の規約を持ちます。**1 つの Sheet が 1 つのコンフィグ表**であり、Sheet 名がそのまま生成される message／ファイル名になります。

### ヘッダー構造（先頭 4 行）
表の先頭 4 行は**ヘッダー**で、5 行目から**データ行**です。

| 行     | 用途           | 説明                                                                            |
| ------ | -------------- | ------------------------------------------------------------------------------- |
| 1 行目 | 制御マーカー   | 列ごとに `#`（この列を無視）、`__END__`（有効列はここまで）、または通常のフィールド。 |
| 2 行目 | フィールド名   | proto フィールド名（例：`Id`、`ClassName`）。アンダースコアで配列を構成できます（後述）。 |
| 3 行目 | 説明           | プランナー向けの注記。ツールはこの行を**無視**するため自由に記入できます。          |
| 4 行目 | データ型       | フィールドの型。[データ型の対応表](#データ型の対応表) を参照。                      |
| 5 行目～| データ         | 実際のコンフィグデータ。ある行の**先頭列**が `__END__` ならデータはそこで終了。      |

> [!IMPORTANT]
> 各 Sheet の**最初の有効列**が主キー（`Map` の `key`）として使われます。一意で `int` に変換可能な番号にしてください。主キーのセルが空のデータ行は**スキップ**されます。

### 制御マーカー：`#` と `__END__`
- **`#`**：1 行目のある列に書くと、**その列全体を無視**します。プランナー用の補助列など、エクスポートしたくない列に使います。
- **`__END__`**：
  - 1 行目のある列に書くと、**有効列はそこまで**となり、右側の列は解析されません。
  - **データ行の先頭列**に書くと、**データはそこで終了**し、下の行は解析されません。

### データ型の対応表
4 行目（型の行）は以下の型に対応します。

| Excel の型           | proto3 の型              | 記入例        | 説明                              |
| -------------------- | ------------------------ | ------------- | --------------------------------- |
| `int`                | `int32`                  | `100`         | 整数                              |
| `key`                | `int32`                  | `1001`        | 整数。意味的に主キーとして使用       |
| `float`              | `float`                  | `1.5`         | 浮動小数点数                      |
| `string`             | `string`                 | `Hello`       | 文字列                            |
| `intArray`           | `repeated int32`         | `[1,2,3]`     | 整数の配列                        |
| `floatArray`         | `repeated float`         | `[1.0,2.5]`   | 浮動小数点数の配列                |
| `stringArray`        | `repeated string`        | `[a,b,c]`     | 文字列の配列                      |
| `map<int,int>`       | `map<int32,int32>`       | `{1:10,2:20}` | 整数 → 整数 の辞書                |
| `map<int,string>`    | `map<int32,string>`      | `{1:a,2:b}`   | 整数 → 文字列 の辞書              |
| `map<string,int>`    | `map<string,int32>`      | `{a:1,b:2}`   | 文字列 → 整数 の辞書              |
| `map<string,string>` | `map<string,string>`     | `{a:x,b:y}`   | 文字列 → 文字列 の辞書            |

> [!WARNING]
> 型の行に対応表以外の型を書くと、ツールは「データ型が未定義」と報告して中断します。上表から選んでください。

### 配列（repeated）
配列には 2 通りの書き方があり、どちらか一方を使います。

1. **配列型の列**：列の型を直接 `intArray` / `floatArray` / `stringArray` にし、セルの値を `[]` で囲みカンマ区切りにします（例：`[1,2,3]`）。
2. **アンダースコア分割の列**：`Reward_1`、`Reward_2`、`Reward_3` のようなフィールド名の複数列で 1 つの配列を表します。各列の型は基本型（例：`int`）です。ツールはアンダースコア接頭辞が同じ列を `Reward` という 1 つの `repeated` フィールドへ**まとめ**、値を順番に収集します。

### 辞書（map）
辞書型のセルは `{}` で囲み、要素間はカンマ `,`、キーと値はコロン `:` で区切ります（例：`{1:100,2:200}`）。

## 🧩 Unity での使い方
### 導入（UPM、git URL）
`Window → Package Manager → + → Install package from git URL...` で次を貼り付けます：
```
https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf
```
または `Packages/manifest.json` の `dependencies` に追加：
```json
"com.alefeng.exceltoprotobuf": "https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf"
```
パッケージには `Google.Protobuf` が同梱され、ランタイム読込クラス `ConfigManager` を提供します。

> [!NOTE]
> 本リポジトリの `Assets/Plugins/Google.Protobuf/` は**サンプルプロジェクト用**の依存です。上記の UPM パッケージで `Google.Protobuf` を導入済みなら、このフォルダを重ねてコピーしないでください（アセンブリ重複の衝突を避けるため）。

### ConfigManager で読み込む（推奨）
変換ツールが出力した `.bytes` を `Resources/Config/` に置き（例：`Resources/Config/Adventure_Condition.bytes`）、生成された `.cs` クラスをプロジェクトに追加してから：
```csharp
using Deploy;            // 生成クラス
using ExcelToProtobuf;   // UPM パッケージの読込クラス

var config = new ConfigManager();                                    // 既定は Resources/Config
Adventure_Condition_Map map = config.Load<Adventure_Condition_Map>(); // 型名から表名を推定、キャッシュ付き
Adventure_Condition cfg = map.Items[1001];                           // 主キー Id で行を取得
Debug.Log(cfg.ClassName);
```
読込元を変える場合（Addressables / StreamingAssets など）は `IConfigBytesProvider` を実装し、`new ConfigManager(myProvider)` を渡します。

### 手動でパースする（パッケージ不使用）
生成クラスは標準の Protobuf メッセージなので、コンテナの `Parser` で直接デシリアライズもできます：
```csharp
Adventure_Condition_Map map = Adventure_Condition_Map.Parser.ParseFrom(asset.bytes);
```

> [!TIP]
> 生成されたクラスはすべて `Deploy` 名前空間にあります。データクラスは `SheetName`、コンテナは `SheetName_Map` で、その `Items` は主キー `Id` をキーとする辞書です。  
> ある表の内容を素早く確認したい場合は、`Assets/UnProductAssets/Config/` 配下の同名 `.txt` プレーンテキストを開いてください。

## 🔧 カスタマイズと拡張
### 新しいコンフィグ表を追加する
1. `Config/Excel/` に新しい `.xlsx` を作成します（既存ブックに Sheet を追加してもよい）。
2. [記入ルール](#-excel-記入ルール) に従い、4 行のヘッダーとデータ行を記入します。
3. `dotnet run --project Tools/ExcelToProtobuf` を再実行します。生成クラスは Unity へ自動同期され、不要になった旧表のクラスは自動的に削除されます。

### 手書きの proto ファイル
Excel 由来でないデータ構造（共通の enum やネスト構造など）がある場合は、手書きの `.proto` を `Config/Proto/` に置きます。表から生成された proto とまとめて C# にコンパイルされます。

### データ型を拡張する
新しいフィールド型に対応するには、2 か所のマッピングを（整合を保って）修正します。
- `Tools/ExcelToProtobuf/Excel2Proto.cs` の `GetProtoType` —— 生成される proto フィールドの型を決めます。
- `Tools/ExcelToProtobuf/Excel2Bytes.cs` の `GetRealVal` —— セルのテキストを実値へ解析する方法を決めます。

## 🚧 注意事項と FAQ
- **Sheet 名は重複不可**：異なる Excel ファイルであっても Sheet 名はグローバルに一意である必要があります。さもないとコンテナクラスの衝突により「Excel の Sheet 名が重複している可能性」というエラーになります。
- **proto ファイル名は重複不可**：すべての `.proto` は同一フォルダに集約してコンパイルされるため、異なるフォルダにあっても同名にはできません（`Config/Proto/` の手書き proto を含む）。
- **入出力パス**：既定では実行ファイルの位置から上方向に `Config/Excel` を含むリポジトリルートを探索します。構成が異なる場合は引数（`--excel` / `--cs-out` / `--bytes-out` など）で明示指定してください。
- **依存とバージョン**：`protoc` と `Google.Protobuf` は NuGet から取得し、`3.11.2` に固定（パッケージ同梱の Unity 用 `Google.Protobuf.dll` と互換）。システムの `csc` や protoc の手動インストールは不要です。
- **表の有効性**：4 行のヘッダー＋終了マーカーを含む表のみ有効と認識されます。データ行の先頭列が `__END__` になると読み取りを停止します。

## 📋 To-Do リスト
- ✅ 完了：SDK スタイル + NuGet 依存でクリーンなクローンからビルド可能；`.bat` とハードコードの `csc.exe` を撤廃（Roslyn メモリ内コンパイル）；クロスプラットフォーム `protoc`；パスの引数化；UPM パッケージ化とランタイム `ConfigManager` の提供。
- **堅牢性**
  - より充実したエラー通知と失敗時の中断処理（現在は一部のエラーがログ出力後に続行される）。
  - 配列/辞書のパースで値に区切り文字（`,` / `:`）を含むケースへの対応（現在は素朴な分割、既知の制限）。
- **使いやすさ**
  - GUI または Unity エディタ内のワンクリックエクスポート導線を提供する。
  - ツールとパッケージ全体で、より新しい `Google.Protobuf` バージョンへの整合・更新。

## 📄 ライセンス
本プロジェクトは **MIT** ライセンスです（[LICENSE](LICENSE) 参照）。
