# Mission: 讀懂回傳 IEnumerable<int> 的方法

## Why
依已確認的 [教材 spec](../../spec.md)，課程最終要協助讀者讀懂、改寫回合事實的 yield 方法，並依需求選擇 yield 或 List。原案例的方法宣告為 `IEnumerable<int>`；本單元建立泛型、介面與回傳型別的讀法，並釐清 List 交給 IEnumerable 時沒有複製資料。

## Success looks like
- 解釋 `IEnumerable<int> sequence = list;` 後兩個變數參照同一物件，不是副本或快照。
- 預測列舉前透過 List 新增元素後的列舉結果。
- 辨認宣告為 `IEnumerable<int>` 時不能直接使用的操作，並改寫成可編譯的程式。

## Constraints
- 程式沿用 [共用 Console 專案](../01-list-basics/YieldTutorial/README.md)，主線文字沿用 [主線筆記](../01-list-basics/yield筆記.md#unit-02)。
- 短篇說明、先預測再揭答、立即回饋與可執行 C# 範例；無法編譯的寫法只作獨立說明。
- 只記錄教材設計與來源，不保存個人作答、能力或學習進度。

## Out of scope
- 自訂泛型型別、條件約束、共變數、明確轉型、LINQ、列舉器內部運作與 yield。
