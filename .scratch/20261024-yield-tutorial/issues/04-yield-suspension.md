# 04: yield 如何分次提供元素？

Status: claimed
Category: enhancement

**What to build:** 讓讀者逐次要求一個簡單迭代器的元素，觀察每次產出的位置與區域變數，並能修改產出內容。

Blocked by: 03

**相依課程 Ticket：** 03：foreach 如何取得下一個值？

## 目標

解釋 yield return 的產出、暫停與繼續，以及區域變數保留。

## 先備知識

03 的取值流程與列舉器角色。

## 講解範圍

一般 return 與 yield return 的概念對照、迭代器方法、區域變數與執行位置；暫不深入完整時間線或編譯器狀態機實作。

## 示範

以三個固定數字或計數變數搭配標記輸出，逐次取值，展示產出後的程式在下一次要求元素時繼續。

## 練習與解答

預測各次產出值及繼續位置，修改計算或插入產出，再比較輸出。

依「簡短說明 → 範例 → 預測輸出 → 執行觀察 → 修改練習」編排；解答放在題目後的獨立區段。

## Acceptance criteria

- [ ] 提供可執行的 return 與 yield 寫法對照，沒有將一般 return 與 yield return 混寫成無效方法。
- [ ] 輸出能辨識每次產出與繼續的位置，參考解能解釋區域變數的值。
- [ ] 示範與練習不依賴編譯器產生的型別、狀態欄位或反編譯結果。
- [ ] 本單元包含解釋題判準、實測預測題輸出與修改題參考解，三者與教材敘述一致。
- [ ] 正式技術敘述以官方文件查證，記錄來源、適用版本及驗證日期。
- [ ] 教材與範例沿用共用專案及主線筆記的組織；正式內容新增時同步維護查詢索引與連結，不將暫存文件列入正式索引。

## Notes

課程 Ticket 追蹤教材工作，不記錄學生作答、能力或個人完成進度。狀態 ready-for-agent 表示規格可用，仍須先完成阻擋項目。

## Comments

- 2026-10-24：依學生確認的九張 Ticket 拆分與阻擋關係發布；尚未開始教材實作。
- 2026-11-21：阻擋項目 Ticket 03 已 resolved；依學生要求準備第四單元，狀態設為 `claimed`。教材已備妥並完成製作檢查，尚未進行最後驗收，Ticket 維持進行中。

## 製作紀錄

- 主線教材：[單元 4：yield 如何分次提供元素？](../course/01-list-basics/yield筆記.md#unit-04)，接在單元 3 之後、原有回合事實筆記之前，沿用同一篇主線筆記；單元 3 結尾補上往單元 4 的連結，開頭說明同步更新。
- HTML 短課程：[0004-yield-suspension.html](../course/04-yield-suspension/lessons/0004-yield-suspension.html)；速查頁：[yield-suspension.html](../course/04-yield-suspension/reference/yield-suspension.html)。樣式與互動元件復用 `course/01-list-basics/assets/`，未修改共用元件；單元目錄另有 [MISSION](../course/04-yield-suspension/MISSION.md)、[NOTES](../course/04-yield-suspension/NOTES.md)、[RESOURCES](../course/04-yield-suspension/RESOURCES.md)。第三課導覽列補上「下一課」。
- 示範程式：共用專案 [YieldTutorial](../course/01-list-basics/YieldTutorial/README.md) 新增 `YieldSuspension.cs`，入口選項 `04`、`04-answer`；未另建專案，預設入口仍為 `01`。
- return 與 yield 對照：`GetNumbersWithReturn` 與 `GetNumbersWithYield` 為兩個各自合法的方法，交出相同的 10、20、30；未在同一方法中混寫。
- 暫停與區域變數：`CountUp` 以 `count` 與「位置 1～3」標記輸出，消費端逐次呼叫 `MoveNext()`，並在兩次要求之間插入消費端輸出；只在 `MoveNext()` 回傳 True 後讀 `Current`。示範不使用編譯器產生的型別、狀態欄位或反編譯結果。
- 無法編譯寫法的實測：於系統 TEMP 的臨時專案確認混寫 `return` 與 `yield return` 得到 CS1622、回傳型別 `List<int>` 搭配 `yield return` 得到 CS1624；HTML 預測題的較短程式（含迴圈）輸出為 `A 1 B1 3 B2 6 B3 C`。臨時專案已刪除。
- 官方查證（2026-11-21，Microsoft Learn 當前版本，範例以 `net10.0` 驗證）：yield statement、Iterators、迭代器錯誤訊息、return 陳述式、C# 規格 Blocks 與 Enumerator objects／Advance the enumerator；來源與用途列於 RESOURCES。
- 環境：本機 SDK 已由 10.0.402 更新為 11.0.100；共用專案仍以 `net10.0` 建置，於 .NET Runtime 10.0.15 執行，未修改目標框架。
- teach 參照指紋（SKILL、MISSION、RESOURCES 格式）與相容性基準一致。

### 製作檢查結果（2026-11-21）

- 建置：`dotnet build` 成功，0 警告、0 錯誤（SDK 11.0.100、net10.0）。
- 執行：從 repo 根目錄執行 `01`～`04` 與各自的 `-answer` 共八個選項，輸出與專案 README 的固定輸出區塊以程式逐字比對一致；因 SDK 更換，前三單元一併重跑。預設入口輸出與 `01` 相同；未知選項列出八個可用選項並以結束碼 1 結束。
- 程式碼一致性：主線筆記單元 4 的四段程式碼區塊與 HTML 中的 `CountUp`、`CountUpModified` 及消費端程式，與 `YieldSuspension.cs` 逐字比對一致；解釋題的 `Broken` 刻意不在專案中。主線筆記與 HTML 的 `04`／`04-answer` 輸出區塊與實際執行一致。
- HTML 預測題：預期值與解答區塊一致，並經臨時專案實測；完整示範與較短程式的差異已在 HTML 說明。
- 互動：在瀏覽器以本機 HTTP 伺服器開啟，透過 DOM 驗證 27 個情境（五題選擇題的未作答與各選項；預測題的空白、換行、小寫、混合逗號分隔、缺值、多值、錯誤順序），並以實際點擊確認一題；全部回饋正確。將「迴圈的 i 與 total」選擇題移到預測題解答之前，避免解答先揭示答案，移動後重測 4 個情境正確。
- 視覺：課程頁與速查頁已在瀏覽器截圖檢查表格、程式碼區塊與展開的解答；兩頁無水平溢出。
- 外部錨點：在瀏覽器確認引用的 Microsoft Learn 錨點存在（execution-of-an-iterator、structure-of-an-iterator-method、the-return-statement、133-blocks、15155-enumerator-objects、151552-advance-the-enumerator）。
- 連結：新增／修改的 10 份文件（含本 Ticket 與 map）共 94 個本機連結、25 個錨點目標存在；其中 map 的 3 個 `#answer` 為 Markdown 標題錨點，已確認對應 Ticket 有 `## Answer` 標題。
- 索引：根目錄 README 未加入暫存課程；正式歸檔仍由 [Ticket 10](10-publish-course.md) 處理。
- 未驗證：列印版面、行動裝置寬度。
