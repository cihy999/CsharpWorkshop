# 02: 為什麼 List 可以交給 IEnumerable？

Status: resolved
Category: enhancement

**What to build:** 讓讀者以不同宣告型別使用同一個 List，辨認可直接使用的功能，並讀懂回傳 IEnumerable<int> 的方法。

Blocked by: 01

**相依課程 Ticket：** 01：List 如何保存與提供多個值？

## 目標

讀懂泛型與介面的基本意義，解釋 List<T> 與 IEnumerable<T> 的關係。

## 先備知識

01 的 List、索引與 foreach。

## 講解範圍

T 與 int 的基本讀法、介面能力約定、宣告型別提供的操作、方法回傳型別；不展開完整泛型或介面專題。

## 示範

同一個 List 同時由 List<int> 與 IEnumerable<int> 變數參照，在列舉前透過 List 新增，再觀察兩者列舉結果；提供回傳該序列的方法。

## 練習與解答

預測列舉前新增元素的結果，說明是否複製資料；辨認宣告為 IEnumerable<int> 時哪些操作不能直接使用。

依「簡短說明 → 範例 → 預測輸出 → 執行觀察 → 修改練習」編排；解答放在題目後的獨立區段。

## Acceptance criteria

- [x] 清楚說明賦值沒有建立另一份集合，也不等於將 List 轉為快照。
- [x] 泛型、介面與方法回傳型別皆有最小讀法示例，未要求其他泛型技巧。
- [x] 範例與參考解可執行；無法編譯的操作只作獨立說明，不破壞共用專案建置。
- [x] 本單元包含解釋題判準、實測預測題輸出與修改題參考解，三者與教材敘述一致。
- [x] 正式技術敘述以官方文件查證，記錄來源、適用版本及驗證日期。
- [x] 教材與範例沿用共用專案及主線筆記的組織；正式內容新增時同步維護查詢索引與連結，不將暫存文件列入正式索引。

## Notes

課程 Ticket 追蹤教材工作，不記錄學生作答、能力或個人完成進度。狀態 ready-for-agent 表示規格可用，仍須先完成阻擋項目。

## Comments

- 2026-10-24：依學生確認的九張 Ticket 拆分與阻擋關係發布；尚未開始教材實作。
- 2026-11-07：阻擋項目 Ticket 01 已 resolved；依學生要求準備第二單元，狀態設為 `claimed`。教材已備妥並完成製作檢查，尚未進行最後驗收，Ticket 維持進行中。
- 2026-11-07：學生課後練習曾暫時修改共用專案（新增練習選項並更改預設入口），已由學生自行捨棄；依學生要求完成最後驗收，所有條件通過，標為 `resolved`。本次不開始 Ticket 03、不執行正式歸檔。

## 製作紀錄

- 主線教材：[單元 2：為什麼 List 可以交給 IEnumerable？](../course/01-list-basics/yield筆記.md#unit-02)，接在單元 1 之後，沿用同一篇主線筆記。
- HTML 短課程：[0002-ienumerable-contract.html](../course/02-ienumerable-contract/lessons/0002-ienumerable-contract.html)；速查頁：[ienumerable-contract.html](../course/02-ienumerable-contract/reference/ienumerable-contract.html)。樣式與互動元件復用 `course/01-list-basics/assets/`；單元目錄另有 MISSION、NOTES、RESOURCES。
- 示範程式：共用專案 [YieldTutorial](../course/01-list-basics/YieldTutorial/README.md) 新增 `EnumerableContract.cs`，入口選項 `02`、`02-answer`；未另建專案，預設入口仍為 `01`。
- 無法編譯的操作：`Add`、索引、`Count` 屬性、隱含轉回 `List<int>` 於系統 TEMP 的臨時專案實測，分別為 CS1061、CS0021、CS0428、CS0266；`Count()`（LINQ）可編譯。臨時專案已刪除，共用專案只以註解保留 `sequence.Add(4)`。
- 官方查證（2026-11-07，.NET 10）：介面、泛型、IEnumerable&lt;T&gt;、List&lt;T&gt; 實作清單、參考型別、List&lt;T&gt;.GetEnumerator 備註；來源與用途列於 [RESOURCES](../course/02-ienumerable-contract/RESOURCES.md)。
- teach 參照指紋（SKILL、MISSION、RESOURCES 格式）與相容性基準一致。

### 製作檢查結果（2026-11-07）

- 建置：`dotnet build` 成功，0 警告、0 錯誤（SDK 10.0.402、net10.0）。
- 執行：`01`、`01-answer`、`02`、`02-answer` 與預設入口均執行成功；四個選項的輸出與主線筆記、專案 README 的固定輸出區塊逐字比對一致。
- HTML 預測題：預期值 10、20、30、40 與 `02` 中「透過 sequence 列舉」段落一致；完整示範另含 ReferenceEquals 與其他兩段列舉，HTML 已說明差異。
- 互動：在瀏覽器以本機 HTTP 伺服器開啟，透過 DOM 驗證 17 個情境（三題選擇題的未作答與各選項、預測題的空白、錯誤、換行／中英文逗號分隔、多餘數字），並以實際點擊確認一題；全部回饋正確。
- 視覺：課程頁與速查頁已在瀏覽器截圖檢查；修正速查頁程式碼過寬與框線圖錯位，並將選擇題移到對照表之前，避免題目前先揭示答案。
- 連結：新增／修改的 6 份文件共 46 個本機連結與錨點目標存在。
- 索引：根目錄 README 未加入暫存課程；正式歸檔仍由 [Ticket 10](10-publish-course.md) 處理。
- 未驗證：列印版面、行動裝置寬度。

## Answer

驗收日期：2026-11-07（Asia/Taipei）。結論：第二單元教材工作完成。

| 驗收面向 | 結果與依據 |
|---|---|
| 賦值不是複製 | 主線筆記與 HTML 說明 `list` 與 `sequence` 參照同一物件，不建立新集合或快照；示範以 `ReferenceEquals` 輸出 `True`，並在列舉前 `Add(40)` 觀察兩者一致。 |
| 最小讀法 | 泛型 `T`、介面能力約定、方法回傳型別各有一段說明與程式例；自訂泛型、條件約束、共變數、LINQ 明列為範圍外。 |
| 建置與執行 | 共用專案建置成功，0 錯誤（SDK 10.0.402、net10.0）。從 repo 根目錄執行 `01`、`01-answer`、`02`、`02-answer` 與預設入口皆成功；預設入口輸出與 `01` 相同。無法編譯的四種寫法只在文字與表格說明，專案內僅以註解保留 `sequence.Add(4)`。 |
| 題目、解答與判準 | 四個選項的實際輸出與主線筆記、專案 README 的固定輸出區塊以程式逐字比對，全部一致；解釋題判準涵蓋同一物件、宣告型別、介面關係三點。 |
| 來源與版本 | 主線筆記、HTML 與 RESOURCES 列有 Microsoft Learn 來源、.NET 10 適用版本及 2026-11-07 驗證日期；編譯錯誤代碼註明為實測結果，非官方引述。 |
| 組織、互動與連結 | 教材自 commit `e291b86` 後未變動，製作檢查中的 HTML 互動 17 個情境、瀏覽器視覺檢查與 46 個本機連結／錨點結果仍適用。程式沿用共用專案，主線沿用同一篇筆記。 |
| 索引與歸檔邊界 | 根目錄 README 未連到暫存課程；正式歸檔由 [Ticket 10](10-publish-course.md) 處理。 |

限制：列印版面與行動裝置寬度未驗證。CLI 初始化目錄使用系統 TEMP 下的 `CsharpWorkshop-dotnet-cli`。原始課堂筆記保持原文；本紀錄不保存學生作答、能力或個人學習進度。
