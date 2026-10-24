# CsharpWorkshop

C#／.NET 知識庫，提供概念查詢、技術比較、問題解法與可執行範例。以知識主題分類，不記錄個人學習進度。

## 如何查詢

- 找概念或語法：由下方「主題索引」進入文章，再開啟對應範例。
- 找文章內的概念：由「關鍵字入口」查找，例如 `record`、`init`、`with`。
- 找可執行程式：由索引的範例欄開啟專案，或參考「建置與執行」。

既有筆記目前整篇保留，包含教材摘要、題目與作答；主題歸檔不代表內容已重新審訂。檔名與範例名稱中的章節編號保留作為來源識別，查詢以本索引為入口。

## 專案結構

```text
CsharpWorkshop/
├── README.md                  # 知識庫入口、分類規則與查詢索引
├── AGENTS.md                  # 講師行為規範與知識庫維護指示
├── .scratch/                  # 非 main 分支上的處理中專案與暫時筆記
├── docs/
│   ├── topics/                # 依主要知識領域歸檔的文章
│   │   ├── csharp/            # 語法、型別、方法、物件、委派與事件
│   │   ├── dotnet/            # 編譯與執行階段機制
│   │   ├── collections/       # 陣列、集合、列舉器與迭代
│   │   └── design/            # 設計原則與設計模式
│   └── agents/                # Agent 工作流程參考文件
├── samples/                   # 聚焦概念的可執行範例
│   ├── csharp/
│   ├── collections/
│   └── design/
└── projects/                  # 結合多個概念的整合範例
    └── P03_Interface/         # 介面、Provider、動態載入與 WinForms
```

### 分類與維護規則

- 文章依主要主題放入 `docs/topics/<領域>/`；跨主題內容只保留一份，由索引提供其他入口。以知識名稱分類，不以教材章節或學習階段建立主目錄。
- 聚焦概念的範例放入 `samples/<領域>/`；整合多個概念的情境放入 `projects/`。多專案 solution 連同相依專案整組保存。
- 新增或搬移內容時，同步維護本頁索引、相對連結與專案參照。文章與對應範例透過索引配對；沒有對應範例時明確標示。
- 現有筆記維持原文；內容拆分、合併、技術修訂與框架升級另行處理。既有題目與作答保留在原文章內，不據此建立學習進度。
- 未來新增文章使用主題名稱作為檔名，提供概念說明、範例、限制、相關概念與來源；適用版本、驗證日期屬於內容資訊。
- 教材來源保留於原文章。日後有獨立字幕或來源文件時放入 `docs/references/`；跨主題的問題解法放入 `docs/guides/`。有實際內容時再建立目錄。
- 以 repo 內容作為主要版本，HackMD 作為發布副本。

2026 roadmap 用於檢查知識涵蓋範圍與整理優先順序。後續可依實際內容增加 `concurrency`、`networking`、`data`、`operations` 等領域，涵蓋並發、網路、資料一致性與效能營運；不預先建立空目錄或學習進度表。

### 本專案擴充規則：暫存工作區

以下是 CsharpWorkshop 的擴充規則，與 Matt skills 的 tracker 設定分開維護。

- 任務、規格、狀態、討論與相依關係以 `.scratch/` 下的 Local Markdown 檔案追蹤，檔案配置見 `docs/agents/issue-tracker.md`；同一工作目錄也可存放處理中的專案、尚未整理成示範的程式與暫時筆記。
- 每個工作目錄使用 `.scratch/YYYYMMDD-<feature-slug>/`，日期採建立當天（Asia/Taipei），例如 `.scratch/20261024-yield-tutorial/`。Matt tracker 文件中的 `<feature-slug>` 或 `<effort>` 在本專案皆使用含日期前綴的目錄名稱。
- `.scratch/` 可以 commit 到非 `main` 分支，因此不加入 `.gitignore`。合併進 `main` 前，檢查合併結果，確保不包含 `.scratch/` 下的檔案；分支中的 Git 歷史可以保留。
- 跨裝置接續工作時，先 commit 並 push 工作分支，再於另一台裝置取得同一分支；未提交或僅存在本機的內容不會隨 Git 同步。
- 整理完成後，依上方分類規則將正式筆記或示範專案移入 `docs/topics/`、`samples/` 或 `projects/`，同步更新本頁索引、連結與專案參照。暫存內容不列入正式知識庫索引。
- 確認正式內容完整後，刪除對應的 `.scratch/YYYYMMDD-<feature-slug>/`，包含其中的任務與討論檔案，不保留工作目錄副本；過往紀錄可從已提交的 Git 歷史查閱。

## 主題索引

### C# 語言與型別

| 主題 | 文章 | 對應範例 |
|------|------|----------|
| 資料型別、轉型、字串、列舉 enum、結構 struct | [資料型別](<docs/topics/csharp/C%23筆記 - ch02 - 資料型別.md>) | [型別](samples/csharp/Ch02_Types/)、[運算子](samples/csharp/Ch02_Operator/)、[字串](samples/csharp/Ch02_String/) |
| 控制台 Console、格式化輸出、跳脫字元 | [控制台](<docs/topics/csharp/C%23筆記 - ch02 - 控制台.md>) | [控制台範例](samples/csharp/Ch02_ConsoleEx1/) |
| 條件判斷、迴圈、流程控制 | [流程控制](<docs/topics/csharp/C%23筆記 - ch03 - 流程控制.md>) | [if/else](samples/csharp/Ch03_IfElse/)、[switch](samples/csharp/Ch03_Switch/)、[for](samples/csharp/Ch03_For/)、[while](samples/csharp/Ch03_While/) |
| 方法、參數傳遞、ref／out／in、多載 | [方法](<docs/topics/csharp/C%23筆記 - ch04 - 方法.md>) | [方法範例](samples/csharp/Ch04_Method/) |
| 類別 class、屬性、建構函式、存取權限 | [物件與類別](<docs/topics/csharp/C%23筆記 - ch06 - 物件與類別.md>) | [類別範例](samples/csharp/Ch06_Class/) |
| 繼承、多型、抽象類別 | [繼承、多型、介面](<docs/topics/csharp/C%23筆記 - ch07 - 繼承、多型、介面.md>) | [繼承範例](samples/csharp/Ch07_Inherit/) |
| 介面 Interface、明確介面實作、Provider | [Interface](<docs/topics/csharp/C%23筆記 - Interface.md>) | [Provider 整合範例](projects/P03_Interface/) |
| 委派 Delegates、Lambda、Func、Predicate | [Delegates](<docs/topics/csharp/C%23筆記 - Delegates.md>) | [委派範例](samples/csharp/P02_Delegate/) |
| 事件 Events、訂閱與退訂、EventHandler | [Events](<docs/topics/csharp/C%23筆記 - Events.md>) | [事件與觀察者比較範例](samples/csharp/P03_Events/) |
| 擴充方法 Extension Methods | [Extension Methods](<docs/topics/csharp/C%23筆記 - Extension Methods.md>) | 尚無獨立專案 |
| 可為 null 的型別、null 運算子 | [nullable](<docs/topics/csharp/C%23筆記 - nullable.md>) | 尚無獨立專案 |
| in、readonly struct、record | [in + readonly struct](<docs/topics/csharp/C%23 最佳實踐：in + readonly struct.md>) | 尚無獨立專案 |

### .NET 執行機制

| 主題 | 文章 | 對應範例 |
|------|------|----------|
| Roslyn、CIL／IL、CLR、JIT、組件 Assembly、中繼資料 | [編譯流程](<docs/topics/dotnet/C%23筆記 - 編譯流程.md>) | 尚無獨立專案 |

### 集合與資料結構

| 主題 | 文章 | 對應範例 |
|------|------|----------|
| 陣列 Array、foreach、排序、不規則陣列 | [陣列](<docs/topics/collections/C%23筆記 - ch04 - 陣列.md>) | [陣列範例](samples/collections/Ch04_Array/) |
| 列舉器 IEnumerator、集合、Dictionary | [列舉器與集合](<docs/topics/collections/C%23筆記 - 列舉器與集合.md>) | 尚無獨立專案 |
| yield、迭代器 | [yield 筆記](docs/topics/collections/yield筆記.md) | 尚無獨立專案 |

### 設計模式

| 主題 | 文章 | 對應範例 |
|------|------|----------|
| 策略模式 Strategy | [策略模式](<docs/topics/design/C%23筆記 - 策略模式.md>) | [角色策略範例](samples/design/Pattern_Ch10_Strategy/) |
| 觀察者模式 Observer、Push／Pull | [觀察者模式](<docs/topics/design/C%23筆記 - 觀察者模式.md>) | [觀察者範例](samples/design/Pattern_Ch21_Observer/)、[事件比較範例](samples/csharp/P03_Events/) |

## 關鍵字入口

部分概念仍收錄於較大的既有文章，以下提供額外查詢入口。

| 想查的概念或問題 | 從這裡開始 |
|----------------|--------------|
| record、init、with | [Events 文章的補充說明](<docs/topics/csharp/C%23筆記 - Events.md>)、[in + readonly struct 的 record 附錄](<docs/topics/csharp/C%23 最佳實踐：in + readonly struct.md>) |
| 委派、事件與觀察者如何表達通知？ | [Events 的逐步範例](<docs/topics/csharp/C%23筆記 - Events.md>)、[比較程式](samples/csharp/P03_Events/) |
| 委派與策略模式有哪些關聯？ | [Delegates](<docs/topics/csharp/C%23筆記 - Delegates.md>)、[策略模式](<docs/topics/design/C%23筆記 - 策略模式.md>) |
| 明確介面實作、反射與動態載入 DLL | [Interface](<docs/topics/csharp/C%23筆記 - Interface.md>)、[Provider 整合範例](projects/P03_Interface/) |
| int?、?.、??、null! | [nullable](<docs/topics/csharp/C%23筆記 - nullable.md>) |
| IEnumerable／IEnumerator 與 yield | [列舉器與集合](<docs/topics/collections/C%23筆記 - 列舉器與集合.md>)、[yield](docs/topics/collections/yield筆記.md) |

## 建置與執行

保留各範例原有的 `.sln`／`.slnx`、專案名稱、命名空間與目標框架。可從主題索引進入資料夾，開啟其中的 solution，或在 repo 根目錄使用 .NET CLI。

| 範例群組 | 目標框架／需求 |
|----------|---------------|
| 大部分 samples | .NET 8 |
| `Ch07_Inherit`、`Pattern_Ch10_Strategy` | .NET 10；solution 使用 `.slnx` |
| `P03_Interface` | .NET 8；WinForms 主程式需要 Windows |

建置與執行委派範例：

```powershell
dotnet build samples/csharp/P02_Delegate/P02_Delegate.sln
dotnet run --project samples/csharp/P02_Delegate/P02_Delegate.csproj
```

建置事件比較範例（包含共用類別庫）：

```powershell
dotnet build samples/csharp/P03_Events/P03_Events.sln
dotnet run --project samples/csharp/P03_Events/EventsPractice/EventsPractice.csproj
```

建置介面整合範例：

```powershell
dotnet build projects/P03_Interface/P03_Interface.sln
```

此 WinForms 範例的 Provider DLL 原有載入方式依賴工作目錄。建置後，從主程式的輸出目錄執行，讓 `..\..\..\libs` 指向專案內的 `libs`：

```powershell
Push-Location projects/P03_Interface/DecodeBytes.WinForm/bin/Debug/net8.0-windows
try {
    dotnet DecodeBytes.WinForm.dll
}
finally {
    Pop-Location
}
```

本文提供分類與查詢入口；範例的程式行為與教學內容維持既有版本。
