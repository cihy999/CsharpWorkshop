# 06: 序列如何結束或提前停止？

Status: claimed
Category: enhancement

**What to build:** 用完整示範比較正常結束、yield break 與消費端 break，讓讀者觀察產出範圍、未執行的程式及 finally 清理事件。

Blocked by: 05

**相依課程 Ticket：** 05：迭代器方法何時真正執行？

## 目標

區分產生端結束與消費端停止，理解已進入的清理區段如何執行。

## 先備知識

05 的執行時間線；03 的必要清理示範。

## 講解範圍

方法結尾、yield break、消費端 break、Dispose、finally；清理語法提供完整程式，不要求學生先自行寫出。

## 示範

以合法的 try／finally 迭代器展示正常耗盡、產生端提早結束及 foreach 取一項後 break；補充手動列舉提早 Dispose 的對照。

## 練習與解答

預測三種路徑的產出與清理標記，修改結束條件後核對；解釋兩種 break 的角色。

依「簡短說明 → 範例 → 預測輸出 → 執行觀察 → 修改練習」編排；解答放在題目後的獨立區段。

## Acceptance criteria

- [ ] 清理範例能建置並實測正常及提前結束路徑。
- [ ] 不宣稱 break 會繼續執行迭代器剩餘的一般程式，也不宣稱從未開始的迭代器必定執行主體 finally。
- [ ] 解答能說明 foreach 清理列舉器與手動使用者負責 Dispose 的關係。
- [ ] 本單元包含解釋題判準、實測預測題輸出與修改題參考解，三者與教材敘述一致。
- [ ] 正式技術敘述以官方文件查證，記錄來源、適用版本及驗證日期。
- [ ] 教材與範例沿用共用專案及主線筆記的組織；正式內容新增時同步維護查詢索引與連結，不將暫存文件列入正式索引。

## Notes

課程 Ticket 追蹤教材工作，不記錄學生作答、能力或個人完成進度。狀態 ready-for-agent 表示規格可用，仍須先完成阻擋項目。

## Comments

- 2026-10-24：依學生確認的九張 Ticket 拆分與阻擋關係發布；尚未開始教材實作。
- 2026-11-23：阻擋項目 Ticket 05 已 resolved；依學生要求準備第六單元，狀態設為 `claimed`。教材已備妥並完成製作檢查，尚未進行最後驗收，Ticket 維持進行中。

## 製作紀錄

- 主線教材：[單元 6：序列如何結束或提前停止？](../course/01-list-basics/yield筆記.md#unit-06)，接在單元 5 之後、原有回合事實筆記之前；單元 5 結尾補上往單元 6 的連結，開頭導覽與說明同步更新。
- HTML 短課程：[0006-termination-and-cleanup.html](../course/06-termination-and-cleanup/lessons/0006-termination-and-cleanup.html)；速查頁：[termination-and-cleanup.html](../course/06-termination-and-cleanup/reference/termination-and-cleanup.html)。樣式與互動元件復用 `course/01-list-basics/assets/`，未修改共用元件；單元目錄另有 [MISSION](../course/06-termination-and-cleanup/MISSION.md)、[NOTES](../course/06-termination-and-cleanup/NOTES.md)、[RESOURCES](../course/06-termination-and-cleanup/RESOURCES.md)。第五課導覽列（首尾）補上「下一課」。
- 示範程式：共用專案 [YieldTutorial](../course/01-list-basics/YieldTutorial/README.md) 新增 `TerminationAndCleanup.cs`，入口選項 `06`、`06-answer`；未另建專案，預設入口仍為 `01`。
- 清理與結束路徑示範：直接提供完整合法的 `try`／`finally` 迭代器 `ReadNumbers(int stopAt)`（不要求學生先自行手寫 `try`／`finally`），並將 `走到方法結尾` 置於 `finally` 之後，依序比較路徑 1（自然走到方法結尾）、路徑 2（產生端 `yield break`）、路徑 3（消費端 `foreach` 取 1 項後 `break`），外加對照 1（手動列舉取 1 項後離開 `using`，展示 `Dispose()` 觸發 `finally`）與對照 2（取得列舉器但從未呼叫 `MoveNext()` 就離開 `using`，展示未進入 `try` 時不執行主體內的 `finally`）。
- 練習：HTML 預測題以較短的 `Items(int stopAt)` 依序執行三條路徑，於系統 TEMP 臨時專案實測輸出 `Open A1 A2 Done Clean After Open B1 Clean Open C1 Clean`，臨時專案已刪除；修改題沿用 `ReadNumbers(stopAt)` 比較 `stopAt = 3` 搭配消費端上限 `4`（練習 A）與 `2`（練習 B），參考解以 `06-answer` 執行；解釋題附四點判準，說明兩種 `break` 角色、剩餘一般程式不執行、`foreach` 自動 `Dispose()` 與手動列舉者自行負責 `Dispose()` 的關係，以及未開始列舉不執行主體 `finally` 的邊界。
- 官方查證（2026-11-23，Microsoft Learn 當前版本，範例以 `net10.0` 驗證）：`yield` 語言參考（含 `#using-statements-in-iterators`、`#execution-of-an-iterator`）、Iterators（`#end-the-sequence-with-yield-break`）、Jump statements（`#the-break-statement`）、C# 規格 `15.15.5.2 Advance the enumerator`、`15.15.5.4 Dispose of resources`、`13.9.5.2 Synchronous foreach`、`13.15 The yield statement`；來源與用途列於 [RESOURCES](../course/06-termination-and-cleanup/RESOURCES.md)，外部錨點 `id` 均已查驗存在。
- `teach` 參照指紋（`SKILL.md`、`MISSION-FORMAT.md`、`RESOURCES-FORMAT.md`）於 2026-11-23 核對，皆與相容性基準一致。

### 製作檢查結果（2026-11-23）

- 建置：`dotnet build` 成功，0 警告、0 錯誤（SDK 11.0.100、`net10.0`、Runtime 10.0.15）。
- 執行：從 repo 根目錄執行 `01`～`06` 與各自的 `-answer` 共 12 個選項，輸出與專案 README 的固定輸出區塊逐字比對一致；預設入口輸出與 `01` 相同；在專案目錄執行 `dotnet run -- 06` 與 `dotnet run -- 06-answer` 輸出一致；未知選項 `06-exercise` 列出 12 個可用選項（不含練習選項）並以結束碼 1 結束。
- 程式碼與輸出一致性：主線筆記單元 6 與 HTML 課程頁中的 `ReadNumbers`、路徑 3 消費端、對照 1／2、`RunExerciseAnswer` 與 `ConsumeWithBreak`，均與 `TerminationAndCleanup.cs` 逐字比對一致（忽略縮排）；主線筆記的 `06`／`06-answer` 輸出區塊與實際執行一致，HTML 輸出區塊亦與實際輸出一致。三題選擇題選項字元數分別對齊為 8、9、8 字，正解位置錯開為 `c`、`a`、`b`。
- 瀏覽器互動與視覺檢查：透過本機 HTTP 伺服器搭配 Microsoft Edge Headless（視窗寬度 1024px）實際載入課程頁與速查頁，`course.css` 與 `quiz.js` 正常載入、頁面無水平溢出（`scrollWidth <= innerWidth`）、三個 `<details>` 解答區預設皆為收合，並在真實瀏覽器 DOM 點擊驗證 19 個互動情境（三題選擇題的未作答與各選項；預測題的空白、三行換行、大小寫與中英文標點混寫、單行空格、多填 Done／After、缺漏 Clean、順序錯誤），回饋全部正確；同時擷取首屏截圖檢視課程頁與速查頁排版。
- 連結：包含本 Ticket 在內的 9 份新增／修改文件共 92 個本機連結（含 CSS／JS 引用）、28 個錨點目標全部存在。
- 索引與個人筆記：根目錄 `README.md` 未加入暫存課程；`notes/note-*.md` 未變動；正式歸檔仍由 [Ticket 10](10-publish-course.md) 處理。
- 尚未驗證：列印版面、行動裝置窄視窗寬度（如 `<= 520px`）下的實際視覺呈現，以及課程頁下半部完整捲動截圖的人工逐段檢視。
