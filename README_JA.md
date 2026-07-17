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

## 特徴
| 特徴                | 説明                                                                                          |
| ------------------- | --------------------------------------------------------------------------------------------- |
| ワンクリック処理    | `ExcelToProtobuf.exe` をダブルクリックするだけで Excel→Proto→C#→DLL→バイナリの全工程が自動実行。  |
| Excel 駆動          | フィールド名・データ型・データはすべて Excel 表由来。プランナーは表を編集するだけで済みます。      |
| 強い型付け          | 標準の Protobuf C# クラスを生成し、`Map<int, T>` の主キーで強く型付けされたコンフィグを取得。      |
| 効率的なバイナリ    | ランタイムは軽量な Protobuf バイナリ（`.bytes`）を読み込みます。小さく高速で、製品ビルド向け。      |
| 増分同期            | 生成された `.cs` はハッシュ比較され、変更されたファイルのみ上書き、不要なものは削除されます。       |
| プレーンテキスト対照 | 人が読める `.txt` データも出力し、プランナー／プログラマーがエクスポート結果を確認できます。       |
| 配列 / 辞書         | `repeated`（配列）と `map`（辞書）に対応。配列は 2 通りの書き方があります。                        |
| 拡張可能な proto    | `Config/Proto` に置いた手書きの `.proto` も、生成された proto と一緒にコンパイルされます。          |

## 💻 動作環境
- **Windows** OS。パイプラインは `.bat` バッチ、`protoc.exe`（win64）、システムの `csc.exe` に依存するため、現状 Windows のみ対応です。
- **.NET Framework 4.7.1**（`App.config` 参照）。`ConfigProto.dll` のコンパイルには `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe` を使うため、このパスが存在する必要があります。
- **protoc 3.11.2**（`Tools/ExcelToProtobuf/protoc-3.11.2-win64/` に同梱済み。別途インストール不要）。
- **NPOI / Google.Protobuf**（関連 DLL は `bin/Debug` に同梱済み。別途インストール不要）。
- 利用側は **Unity プロジェクト**で、ランタイムに `.bytes` をデシリアライズするため `Google.Protobuf` への参照が必要です。

## 📁 ディレクトリ構成
```
ExcelToProtobuf/
├─ Config/
│  ├─ Excel/                                  # ← プランナーが編集する表（*.xlsx）——入力
│  └─ Proto/                                  # ← 手書きの .proto（任意、一緒にコンパイル）
│
├─ Assets/                                    # Unity プロジェクト
│  ├─ Source/System/ConfigSystem/Config/      # → 生成されたコンフィグクラス *.cs（自動同期・増分）
│  ├─ ProductAssets/Config/                   # → シリアライズ済みバイナリ *.bytes（ランタイム読込）
│  └─ UnProductAssets/Config/                 # → プレーンテキスト *.txt（確認用、製品には含めない）
│
└─ Tools/ExcelToProtobuf/
   ├─ bin/Debug|Release/ExcelToProtobuf.exe   # メインプログラム（ダブルクリックで実行）
   ├─ Protos/                                 # 中間成果物：生成／収集された .proto
   ├─ Csharp/                                 # 中間成果物：protoc 生成の .cs とコンパイル済み ConfigProto.dll
   ├─ protoc.exe · protoc-3.11.2-win64/       # protobuf コンパイラ
   ├─ BuildProtos.bat                         # .proto → .cs
   └─ BuildDLL.bat                            # .cs → ConfigProto.dll
```

> [!NOTE]
> プログラムは**相対ディレクトリ構造**で各パスを特定します。実行ファイルのある `bin/Debug`（または `bin/Release`）から 2 階層上って `Tools/ExcelToProtobuf`、さらに 2 階層上ってリポジトリのルートを得ます。この階層構造を保ったまま、`bin` 配下の exe をダブルクリックで実行してください。

## 🔄 処理フロー
メインプログラム（`Program.cs`）は次の 6 ステップを順に実行し、各ステップの進捗をコンソールに出力します。

1. **【Excel → Proto】** —— `Config/Excel/` 配下のすべての `.xlsx` を走査し、**Sheet ごと**に 1 つの `.proto` を生成して `Tools/ExcelToProtobuf/Protos/` へ出力します。
2. **【Proto コピー】** —— `Config/Proto/` の手書き `.proto` も `Protos/` へコピーし、生成された proto と一緒にコンパイル対象にします。
3. **【Proto → C#】** —— `BuildProtos.bat`（内部で `protoc` を実行）を呼び、`Protos/*.proto` から C# クラスを生成して `Csharp/` へ出力します。
4. **【C# を Unity へコピー】** —— `Csharp/*.cs` を `Assets/Source/System/ConfigSystem/Config/` へ同期します。ハッシュ比較により、**同一はスキップ・変更は上書き・不要は削除**します。
5. **【C# → DLL】** —— `BuildDLL.bat`（内部で `csc` を実行）を呼び、`Csharp/*.cs` を `ConfigProto.dll` へコンパイルします（次ステップでリフレクション読込）。
6. **【コンフィグデータのシリアライズ保存】** —— `ConfigProto.dll` を読み込み、Excel のデータ行を再度読み取って行ごとに Protobuf オブジェクトへ格納し、`.bytes` として `Assets/ProductAssets/Config/` へ出力。あわせてプレーンテキスト `.txt` を `Assets/UnProductAssets/Config/` へ出力します。

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
`Tools/ExcelToProtobuf/bin/Debug/ExcelToProtobuf.exe`（または `bin/Release/` の同名プログラム）をダブルクリックします。  
コンソールに 6 ステップのログが順に出力され、「流程执行完毕，按任意键退出」（処理完了、任意のキーで終了）が表示されれば成功です。

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
生成された C# クラスは標準の Protobuf メッセージです。ランタイムではコンテナの `Parser` で `.bytes` をデシリアライズします。以下は**サンプル**です（実際のアセット読込方法はプロジェクト次第：`Resources` / `Addressables` / `StreamingAssets` など）。

```csharp
using Deploy; // 生成クラスの名前空間

// 1. 任意の方法で .bytes のバイト配列を取得（ここでは Resources を例に）
TextAsset asset = Resources.Load<TextAsset>("Config/Adventure_Condition");

// 2. コンテナクラス（SheetName_Map）を解析
Adventure_Condition_Map map = Adventure_Condition_Map.Parser.ParseFrom(asset.bytes);

// 3. 主キー Id で任意の行を直接参照
Adventure_Condition cfg = map.Items[1001];
Debug.Log(cfg.ClassName);

// すべての行を列挙することも可能
foreach (var kv in map.Items)
{
    Debug.Log($"{kv.Key} => {kv.Value.ClassName}");
}
```

> [!TIP]
> 生成されたクラスはすべて `Deploy` 名前空間にあります。データクラスは `SheetName`、コンテナは `SheetName_Map` で、その `Items` は主キー `Id` をキーとする辞書です。  
> ある表の内容を素早く確認したい場合は、`Assets/UnProductAssets/Config/` 配下の同名 `.txt` プレーンテキストを開いてください。

## 🔧 カスタマイズと拡張
### 新しいコンフィグ表を追加する
1. `Config/Excel/` に新しい `.xlsx` を作成します（既存ブックに Sheet を追加してもよい）。
2. [記入ルール](#-excel-記入ルール) に従い、4 行のヘッダーとデータ行を記入します。
3. `ExcelToProtobuf.exe` を再実行します。生成クラスは Unity へ自動同期され、不要になった旧表のクラスは自動的に削除されます。

### 手書きの proto ファイル
Excel 由来でないデータ構造（共通の enum やネスト構造など）がある場合は、手書きの `.proto` を `Config/Proto/` に置きます。実行時に `Protos/` へコピーされ、まとめて C# にコンパイルされます。

### データ型を拡張する
新しいフィールド型に対応するには、2 か所のマッピングを（整合を保って）修正します。
- `Tools/ExcelToProtobuf/Excel2Proto.cs` の `GetProtoType` —— 生成される proto フィールドの型を決めます。
- `Tools/ExcelToProtobuf/Excel2Bytes.cs` の `GetRealVal` —— セルのテキストを実値へ解析する方法を決めます。

## 🚧 注意事項と FAQ
- **Sheet 名は重複不可**：異なる Excel ファイルであっても Sheet 名はグローバルに一意である必要があります。さもないとコンテナクラスの衝突により「Excel の Sheet 名が重複している可能性」というエラーになります。
- **proto ファイル名は重複不可**：すべての `.proto` は同一フォルダに集約してコンパイルされるため、異なるフォルダにあっても同名にはできません（`Config/Proto/` の手書き proto を含む）。
- **ディレクトリ構造を保つ**：ツールは相対パスで入出力フォルダを特定します。`Config/`、`Assets/…/Config/`、`Tools/ExcelToProtobuf/` の相対階層は動かさないでください。
- **csc / protoc のパス**：`BuildDLL.bat` は `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe` に依存し、`protoc` のバージョンは `3.11.2` です。環境が異なる場合は適宜調整してください。
- **表の有効性**：4 行のヘッダー＋終了マーカーを含む表のみ有効と認識されます。データ行の先頭列が `__END__` になると読み取りを停止します。

## 📋 To-Do リスト
- **堅牢性**
  - 固定の絶対パス `csc.exe` への依存を解消し、より柔軟な .NET／ビルド環境に対応する。
  - より充実したエラー通知と失敗時の中断処理（現在は一部のエラーがログ出力後に続行される）。
- **クロスプラットフォーム**
  - `.bat` と win64 `protoc` から脱却し、クロスプラットフォーム（macOS / Linux）での実行方法を模索する。
- **使いやすさ**
  - GUI または Unity エディタ内のワンクリックエクスポート導線を提供する。
  - パスやエクスポート設定を構成可能にする（現在のハードコードされたディレクトリ規約に代えて）。
