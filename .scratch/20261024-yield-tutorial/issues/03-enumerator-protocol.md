# 03: foreach 如何取得下一個值？

Status: ready-for-agent
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

- [ ] foreach 與手動版本產出的值及順序相同，空序列亦正確結束。
- [ ] 不讀取尚未成功 MoveNext 或列舉結束後的 Current，不宣稱它必定為 null。
- [ ] 手動範例包含正確清理並提供必要說明；不依賴內部欄位或完整自製列舉器。
- [ ] 本單元包含解釋題判準、實測預測題輸出與修改題參考解，三者與教材敘述一致。
- [ ] 正式技術敘述以官方文件查證，記錄來源、適用版本及驗證日期。
- [ ] 教材與範例沿用共用專案及主線筆記的組織；正式內容新增時同步維護查詢索引與連結，不將暫存文件列入正式索引。

## Notes

課程 Ticket 追蹤教材工作，不記錄學生作答、能力或個人完成進度。狀態 ready-for-agent 表示規格可用，仍須先完成阻擋項目。

## Comments

- 2026-10-24：依學生確認的九張 Ticket 拆分與阻擋關係發布；尚未開始教材實作。
