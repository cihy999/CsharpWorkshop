# Mission: 說明迭代器方法何時真正執行

## Why
依已確認的 [教材 spec](../../spec.md)，課程最終要協助讀者讀懂、改寫回合事實的 yield 方法，並判斷何時改用 List。本單元以事件標記建立「呼叫方法 → 取得列舉器 → 逐次 MoveNext」的時間線，接上單元 4 的暫停與繼續，作為單元 6 提前停止、單元 7 重複列舉的基礎。

## Success looks like
- 排出呼叫迭代器方法、GetEnumerator 與每次 MoveNext 前後的事件順序，並說明主體從第一次 MoveNext 才開始執行。
- 推演只要求部分元素時，主體停在哪個 yield return、哪些程式沒有執行。
- 移動主體內的一行輸出後，重新預測三種消費情境的輸出並執行核對。
- 說明延遲執行屬於迭代器方法，不是 `IEnumerable<T>` 回傳型別本身造成的。

## Constraints
- 程式沿用 [共用 Console 專案](../01-list-basics/YieldTutorial/README.md)，主線文字沿用 [主線筆記](../01-list-basics/yield筆記.md#unit-05)。
- 短篇說明、先預測再揭答、立即回饋與可執行 C# 範例；順序以輸出先後判斷，不使用計時或等待。
- 只記錄教材設計與來源，不保存個人作答、能力或學習進度。

## Out of scope
- yield break、消費端 break、Dispose 與 finally 清理（單元 6）；再次列舉與可變輸入（單元 7）；LINQ 各運算的延遲行為、非同步迭代器、編譯器產生的狀態機與 IL。
