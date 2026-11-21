# 05: 迭代器方法何時真正執行？

Status: resolved
Category: enhancement

**What to build:** 提供可重現的輸出時間線，讓讀者看出尚未列舉與只取部分元素時，迭代器方法主體執行到哪裡。

Blocked by: 04

**相依課程 Ticket：** 04：yield 如何分次提供元素？

## 目標

區分呼叫迭代器方法、取得列舉器與逐次取值的時機。

## 先備知識

04 的暫停與繼續。

## 講解範圍

同步 yield 迭代器方法的延遲執行；不擴大為所有 IEnumerable 方法或所有 LINQ 運算的通則。

## 示範

在呼叫前後、取得列舉器前後、每次 MoveNext 前後與迭代器主體中加入明確事件標記；比較不列舉、部分列舉及完整列舉。

## 練習與解答

先排序事件標記，再執行核對；移動一個主體內的輸出敘述並重新預測。

依「簡短說明 → 範例 → 預測輸出 → 執行觀察 → 修改練習」編排；解答放在題目後的獨立區段。

## Acceptance criteria

- [x] 三種消費情境均有實測事件順序與解答。
- [x] 明確限定結論於示範中的迭代器方法，不將延遲執行錯誤歸因於回傳型別本身。
- [x] 示範可區分 GetEnumerator 與 MoveNext，且不以時間延遲或執行速度猜測順序。
- [x] 本單元包含解釋題判準、實測預測題輸出與修改題參考解，三者與教材敘述一致。
- [x] 正式技術敘述以官方文件查證，記錄來源、適用版本及驗證日期。
- [x] 教材與範例沿用共用專案及主線筆記的組織；正式內容新增時同步維護查詢索引與連結，不將暫存文件列入正式索引。

## Notes

課程 Ticket 追蹤教材工作，不記錄學生作答、能力或個人完成進度。狀態 ready-for-agent 表示規格可用，仍須先完成阻擋項目。

## Comments

- 2026-10-24：依學生確認的九張 Ticket 拆分與阻擋關係發布；尚未開始教材實作。
- 2026-11-21：阻擋項目 Ticket 04 已 resolved；依學生要求準備第五單元，狀態設為 `claimed`。教材已備妥並完成製作檢查，尚未進行最後驗收，Ticket 維持進行中。
- 2026-11-21：學生課後練習已自行還原，工作區在筆記 commit `4aa9978` 之後沒有共用專案的變更；依學生要求完成最後驗收，所有條件通過，標為 `resolved`。本次不開始 Ticket 06／07、不執行正式歸檔。

## 製作紀錄

- 主線教材：[單元 5：迭代器方法何時真正執行？](../course/01-list-basics/yield筆記.md#unit-05)，接在單元 4 之後、原有回合事實筆記之前；單元 4 結尾補上往單元 5 的連結，開頭說明同步更新。
- HTML 短課程：[0005-deferred-execution.html](../course/05-deferred-execution/lessons/0005-deferred-execution.html)；速查頁：[deferred-execution.html](../course/05-deferred-execution/reference/deferred-execution.html)。樣式與互動元件復用 `course/01-list-basics/assets/`，未修改共用元件；單元目錄另有 [MISSION](../course/05-deferred-execution/MISSION.md)、[NOTES](../course/05-deferred-execution/NOTES.md)、[RESOURCES](../course/05-deferred-execution/RESOURCES.md)。第四課導覽列補上「下一課」。
- 示範程式：共用專案 [YieldTutorial](../course/01-list-basics/YieldTutorial/README.md) 新增 `DeferredExecution.cs`，入口選項 `05`、`05-answer`；未另建專案，預設入口仍為 `01`。
- 三種情境：同一個 `Steps()` 迭代器，消費端 `RequestElements` 分別要求 0、2、4 次；呼叫方法、`GetEnumerator()`、每次 `MoveNext()` 前後各有標記，可區分「呼叫」「取得列舉器」「要求元素」。順序只依單執行緒的輸出先後，不使用計時或等待。
- 回傳型別限定：`StepsWithList()` 同樣回傳 `IEnumerable<int>`，呼叫時即執行主體；教材結論限定於主體含 `yield return` 的同步迭代器方法，不推廣到 LINQ 或所有 `IEnumerable` 方法。
- 練習：HTML 排序題以較短的 `Two()` 排列標記（含一個不會出現的 R），於系統 TEMP 臨時專案實測輸出 `A B P C1 Q D2 E`，臨時專案已刪除；修改題移動開頭標記到 `yield return 2;` 之後，參考解 `StepsMoved()` 以 `05-answer` 執行。
- 官方查證（2026-11-21，Microsoft Learn 當前版本，範例以 `net10.0` 驗證）：Iterators 的 deferred element production、yield statement 的 Execution of an iterator、C# 規格 Enumerable objects／Advance the enumerator／Enumerator objects；來源與用途列於 RESOURCES。
- teach 參照指紋與相容性基準一致。

### 製作檢查結果（2026-11-21）

- 建置：`dotnet build` 成功，0 警告、0 錯誤（SDK 11.0.100、net10.0、Runtime 10.0.15）。
- 執行：從 repo 根目錄執行 `01`～`05` 與各自的 `-answer` 共十個選項，輸出與專案 README 的固定輸出區塊以程式逐字比對一致；預設入口輸出與 `01` 相同；未知選項列出十個可用選項並以結束碼 1 結束。
- 程式碼一致性：主線筆記單元 5 與 HTML 中的 `Steps`、`StepsMoved`、`StepsWithList`、`RequestElements` 及 `Run()` 消費端程式，與 `DeferredExecution.cs` 逐字比對一致（忽略縮排）；主線筆記的 `05`／`05-answer` 輸出區塊與實際執行一致，HTML 輸出片段皆包含於實際輸出。
- 互動：在瀏覽器以本機 HTTP 伺服器透過 DOM 驗證 19 個情境（三題選擇題的未作答與各選項；排序題的空白、空格、換行、小寫與混合分隔、多填 R、缺值、錯誤順序），全部回饋正確。之後為錯開正解位置，將第 2 題正解由第二個選項移到第一個（`data-answer` 同步改為 `a`），並把修改題提問的「之後」改為「時」；這兩處修改留到最後驗收補測。
- 連結：新增／修改的 8 份文件共 73 個本機連結、24 個錨點目標存在。
- 索引：根目錄 README 未加入暫存課程；正式歸檔仍由 [Ticket 10](10-publish-course.md) 處理。
- 製作當下未驗證：瀏覽器視覺檢查、速查頁顯示、外部 Microsoft Learn 錨點、列印版面、行動裝置寬度。前三項於下方驗收補測完成；列印版面與行動裝置寬度仍未驗證。

### 驗收補測（2026-11-21）

- 建置與執行：`dotnet build` 成功，0 警告、0 錯誤（SDK 11.0.100、net10.0、Runtime 10.0.15）。重跑預設入口、`05`、`05-answer`，輸出與專案 README 的對應固定輸出區塊逐字一致；預設入口與 `01` 的 README 輸出一致。未知選項 `05-exercise` 以結束碼 1 結束，訊息列出十個可用選項，不含練習選項。`01`～`04` 自製作檢查後程式未變，該次輸出比對仍適用。
- 第 2 題：在瀏覽器點選第一個選項「不會，沒人再要求」後按核對，狀態顯示「答對了」及原回饋。接著以 DOM 送出未作答與選項 b、c、a：分別為「請先選一個答案。」、「再想一下。」、「再想一下。」、「答對了。」。`data-answer` 為 `a`。
- 修改題提問在頁面上為「若出現，在第幾次 `MoveNext()` 時？」。
- 視覺：課程頁與速查頁已在瀏覽器檢視標題、表格、程式碼區塊與修改題；兩頁載入共用 `course.css`。視窗寬度 943px 時，頁面本身沒有水平溢出；程式碼區塊內部仍可橫向捲動。
- 外部錨點：Microsoft Learn 頁面含對應 `id` 屬性：`understand-deferred-element-production`、`execution-of-an-iterator`、`15156-enumerable-objects`、`151552-advance-the-enumerator`、`15155-enumerator-objects`。

## Answer

驗收日期：2026-11-21（Asia/Taipei）。結論：第五單元教材工作完成。

| 驗收面向 | 結果與依據 |
|---|---|
| 三種消費情境 | `05` 實測輸出與專案 README、主線筆記的固定輸出逐字一致。情境 1 沒有任何 `[產生端]`；情境 2 只要求兩次，沒有「從產出 2 之後繼續」；情境 3 第 4 次 `MoveNext()` 回傳 False，並印出走到方法結尾。 |
| 結論限定於迭代器方法 | 同一輸出的對照段顯示 `StepsWithList()` 同樣回傳 `IEnumerable<int>`，呼叫時就印完主體。教材將延遲執行限定於主體含 `yield return` 的同步迭代器方法，不推廣到 LINQ 或所有 `IEnumerable` 方法。 |
| GetEnumerator 與 MoveNext | 呼叫前後、`GetEnumerator()` 前後、每次 `MoveNext()` 前後各有標記。程式沒有等待或計時，順序只由輸出先後決定。 |
| 建置與執行 | 共用專案建置成功，0 警告、0 錯誤（SDK 11.0.100、net10.0、Runtime 10.0.15）。本次重跑預設入口、`05`、`05-answer`，輸出與 README 對應區塊逐字一致；預設入口與 `01` 相同。未知選項 `05-exercise` 以結束碼 1 結束，並列出十個可用選項，不含練習選項。`01`～`04` 自製作檢查後程式未變，該次輸出比對仍適用。 |
| 題目、解答與判準 | 解釋題判準、排序題預期 `A B P C1 Q D2 E`、修改題參考解與 `05-answer` 輸出一致。驗收時點選第 2 題第一個選項得到「答對了」；未作答與另外兩個選項的回饋也正確。修改題提問為「在第幾次 `MoveNext()` 時？」。 |
| 來源與版本 | [RESOURCES](../course/05-deferred-execution/RESOURCES.md) 列有 Microsoft Learn 與 C# 規格來源、適用版本及 2026-11-21 驗證日期。驗收時確認五個引用錨點的 `id` 屬性存在。 |
| 組織、視覺與連結 | 程式沿用共用專案，主線沿用同一篇筆記。課程頁與速查頁已在瀏覽器檢視；兩頁載入共用 `course.css`，視窗寬度 943px 時頁面本身沒有水平溢出。本機連結沿用製作檢查的 73 個連結、24 個錨點。 |
| 索引與歸檔邊界 | 根目錄 README 未連到暫存課程；正式歸檔由 [Ticket 10](10-publish-course.md) 處理。 |

限制：列印版面與行動裝置寬度未驗證；程式碼區塊內部仍可橫向捲動。CLI 初始化目錄使用系統 TEMP 下的 `CsharpWorkshop-dotnet-cli`。原始課堂筆記保持原文；本紀錄不保存學生作答、能力或個人學習進度。
