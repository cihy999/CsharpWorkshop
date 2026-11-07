# Mission: 說明 foreach 如何取得下一個值

## Why
依已確認的 [教材 spec](../../spec.md)，課程最終要協助讀者讀懂、改寫回合事實的 yield 方法。yield 方法交給 foreach 的是列舉器；本單元先以現成的 List 列舉器建立 GetEnumerator、MoveNext、Current 與 Dispose 的使用規則，作為單元 4 起觀察 yield 暫停與繼續的基礎。

## Success looks like
- 推演每次 MoveNext 的回傳值，指出合法讀取 Current 的時點，包含空序列。
- 解釋 Current 在開始前與結束後是未定義的，而不是必定為 null。
- 寫出等同 foreach 的手動列舉，以 using 正確清理，並改為走訪不同的固定資料。

## Constraints
- 程式沿用 [共用 Console 專案](../01-list-basics/YieldTutorial/README.md)，主線文字沿用 [主線筆記](../01-list-basics/yield筆記.md#unit-03)。
- 短篇說明、先預測再揭答、立即回饋與可執行 C# 範例；不合法的 Current 讀取只作實測說明，不放入共用專案。
- 只記錄教材設計與來源，不保存個人作答、能力或學習進度。

## Out of scope
- 手寫完整列舉器類別、編譯器對 List 或陣列的特殊處理、Reset 的使用、列舉期間修改集合、yield。
