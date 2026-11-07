# 03: foreach 如何取得下一個值？

Status: resolved
Category: enhancement

**What to build:** 提供 foreach 與手動列舉的完整對照，讓讀者依 MoveNext 的結果使用 Current，並正確清理列舉器。

Blocked by: 02

**相依課程 Ticket：** 02：為什麼 List 可以交給 IEnumerable？

## 目標

說明可列舉序列與列舉器的角色，手動要求下一個元素。

## 先備知識

02 的 IEnumerable<T> 與介面基本讀法。

## 講解範圍

GetEnumerator、IEnumerator<T>、MoveNext、Current 與必要的 Dispose／using 示範；不要求手寫完整列舉器類別。foreach 展開標為適用此範例的概念對照。

## 示範

對同一固定數字序列分別 foreach 與手動列舉，核對結果；觀察空序列、首次與結束時的 MoveNext 回傳值。

## 練習與解答

推演每次 MoveNext 的結果與合法的 Current 讀取時點，再修改手動列舉使其走訪不同的固定資料。

依「簡短說明 → 範例 → 預測輸出 → 執行觀察 → 修改練習」編排；解答放在題目後的獨立區段。

## Acceptance criteria

- [x] foreach 與手動版本產出的值及順序相同，空序列亦正確結束。
- [x] 不讀取尚未成功 MoveNext 或列舉結束後的 Current，不宣稱它必定為 null。
- [x] 手動範例包含正確清理並提供必要說明；不依賴內部欄位或完整自製列舉器。
- [x] 本單元包含解釋題判準、實測預測題輸出與修改題參考解，三者與教材敘述一致。
- [x] 正式技術敘述以官方文件查證，記錄來源、適用版本及驗證日期。
- [x] 教材與範例沿用共用專案及主線筆記的組織；正式內容新增時同步維護查詢索引與連結，不將暫存文件列入正式索引。

## Notes

課程 Ticket 追蹤教材工作，不記錄學生作答、能力或個人完成進度。狀態 ready-for-agent 表示規格可用，仍須先完成阻擋項目。

## Comments

- 2026-10-24：依學生確認的九張 Ticket 拆分與阻擋關係發布；尚未開始教材實作。
- 2026-11-07：阻擋項目 Ticket 02 已 resolved；依學生要求準備第三單元，狀態設為 `claimed`。教材已備妥並完成製作檢查，尚未進行最後驗收，Ticket 維持進行中。
- 2026-11-07：學生課後練習曾暫時修改共用專案（新增 `03-exercise` 選項並更改預設入口），已由學生自行捨棄；依學生要求完成最後驗收，所有條件通過，標為 `resolved`。本次不開始 Ticket 04、不執行正式歸檔。

## 製作紀錄

- 主線教材：[單元 3：foreach 如何取得下一個值？](../course/01-list-basics/yield筆記.md#unit-03)，接在單元 2 之後，沿用同一篇主線筆記；單元 2 結尾補上往單元 3 的連結。
- HTML 短課程：[0003-enumerator-protocol.html](../course/03-enumerator-protocol/lessons/0003-enumerator-protocol.html)；速查頁：[enumerator-protocol.html](../course/03-enumerator-protocol/reference/enumerator-protocol.html)。樣式與互動元件復用 `course/01-list-basics/assets/`；單元目錄另有 [MISSION](../course/03-enumerator-protocol/MISSION.md)、[NOTES](../course/03-enumerator-protocol/NOTES.md)、[RESOURCES](../course/03-enumerator-protocol/RESOURCES.md)。第二課導覽列補上「下一課」。
- 示範程式：共用專案 [YieldTutorial](../course/01-list-basics/YieldTutorial/README.md) 新增 `EnumeratorProtocol.cs`，入口選項 `03`、`03-answer`；未另建專案，預設入口仍為 `01`。
- 共用元件：`quiz.js` 預測題比對改為不分大小寫，以接受 `True`／`true`；前兩課的預期答案只有數字。
- Current 未定義的實測：於系統 TEMP 的臨時專案確認 `List<int>` 讀到 0、`List<string>` 讀到 null、`int[]` 與非泛型列舉器擲出 `InvalidOperationException`；臨時專案已刪除，共用專案只在合法時點讀取 Current。
- 既有正式筆記「列舉器與集合」的「開頭列舉器會拿到 null」與實測不符；已記入 [Ticket 10](10-publish-course.md)，本次未修改正式筆記。
- 官方查證（2026-11-07，.NET 10）：IEnumerator&lt;T&gt;、Current、MoveNext、using、C# 規格 foreach、iteration statements、List&lt;T&gt;.GetEnumerator；來源與用途列於 RESOURCES。
- teach 參照指紋（SKILL、MISSION、RESOURCES 格式）與相容性基準一致。

### 製作檢查結果（2026-11-07）

- 建置：`dotnet build` 成功，0 警告、0 錯誤（SDK 10.0.402、net10.0）。
- 執行：從 repo 根目錄執行 `01`、`01-answer`、`02`、`02-answer`、`03`、`03-answer` 與預設入口均成功；七者輸出與主線筆記、專案 README 的固定輸出區塊逐字比對一致。未知選項列出六個可用選項並以結束碼 1 結束。
- 程式碼一致性：主線筆記中 `Run()` 與 `RunExerciseAnswer()` 的程式碼區塊與 `EnumeratorProtocol.cs` 逐字比對一致。
- HTML 預測題：較短程式的預期值 `True 10 True 20 True 30 False` 於臨時專案實測；完整示範的格式差異已在 HTML 說明。
- 互動：在瀏覽器以本機 HTTP 伺服器開啟，透過 DOM 驗證 27 個情境（五題選擇題的未作答與各選項；預測題的空白、換行、小寫、混合逗號分隔、缺值、多值、錯誤答案），並以實際點擊確認一題；全部回饋正確。第二課預測題於 `quiz.js` 修改後重測 5 個情境，結果正確。
- 視覺：課程頁與速查頁已在瀏覽器截圖檢查；將「第幾次回傳 False」選擇題移到時間線表格之前，避免先揭示答案；速查頁時間線由中英混排的 `pre` 改為表格以修正欄位錯位。速查頁無水平溢出。
- 連結：新增／修改的 11 份文件（含本 Ticket 與 map）共 90 個本機連結、23 個錨點目標存在。
- 索引：根目錄 README 未加入暫存課程；正式歸檔仍由 [Ticket 10](10-publish-course.md) 處理。
- 未驗證：列印版面、行動裝置寬度。

## Answer

驗收日期：2026-11-07（Asia/Taipei）。結論：第三單元教材工作完成。

| 驗收面向 | 結果與依據 |
|---|---|
| foreach 與手動列舉一致 | `03` 輸出中 foreach 與手動列舉皆為 10、20、30，順序相同；空序列 foreach 主體未執行，第一次 `MoveNext()` 回傳 False。 |
| Current 讀取時點 | 共用專案只在 `MoveNext()` 回傳 True 後讀取 `Current`；第 4、5 次呼叫明示不讀。主線筆記、HTML 與速查頁皆說明開始前與結束後為「未定義」，以實測表列出 0、null 與例外三種結果，未宣稱必定為 null。 |
| 清理與範圍 | 手動列舉均放在 `using` 區塊，判準說明 `IEnumerator<T>` 實作 `IDisposable`、即使發生例外也會呼叫 `Dispose()`；只使用公開 API，完整自製列舉器列為選讀。 |
| 建置與執行 | 共用專案建置成功，0 警告、0 錯誤（SDK 10.0.402、net10.0）。從 repo 根目錄執行 `01`、`01-answer`、`02`、`02-answer`、`03`、`03-answer` 皆成功，輸出與主線筆記、專案 README 的固定輸出區塊以程式逐字比對一致；預設入口輸出與 `01` 相同；未知選項列出六個選項並以結束碼 1 結束。 |
| 題目、解答與判準 | 解釋題判準涵蓋 A、C 不合法的原因及 `using` 的作用；HTML 預測題預期值經實測；修改題參考解 `03-answer` 可執行。三者與教材敘述一致。 |
| 來源與版本 | [RESOURCES](../course/03-enumerator-protocol/RESOURCES.md) 列有 Microsoft Learn 與 C# 規格來源、.NET 10 適用版本及 2026-11-07 驗證日期；Current 未定義時的實際值註明為實測結果，非官方引述。 |
| 組織、互動與連結 | 教材自 commit `15f1c81` 後未變動，製作檢查中的 HTML 互動 27 個情境、瀏覽器視覺檢查與 90 個本機連結／23 個錨點結果仍適用。程式沿用共用專案，主線沿用同一篇筆記。 |
| 索引與歸檔邊界 | 根目錄 README 未連到暫存課程；正式筆記「列舉器與集合」的 null 說法與正式歸檔由 [Ticket 10](10-publish-course.md) 處理。 |

限制：列印版面與行動裝置寬度未驗證。CLI 初始化目錄使用系統 TEMP 下的 `CsharpWorkshop-dotnet-cli`。原始課堂筆記保持原文；本紀錄不保存學生作答、能力或個人學習進度。
