# Mission: 區分序列結束與消費端提前停止，並觀察清理行為

## Why
依已確認的 [教材 spec](../../spec.md)，課程最終要協助讀者讀懂、改寫回合事實的 yield 方法，並判斷何時改用 List。本單元銜接單元 3 的 `using`／`Dispose()` 與單元 5 的執行時間線，區分「產生端結束序列（走到方法結尾或 `yield break`）」與「消費端提前停止（`foreach` 內的 `break`）」，並透過完整示範觀察 `try`／`finally` 在正常與提前停止時的清理行為。

## Success looks like
- 區分走到方法結尾、產生端 `yield break` 與消費端 `foreach` 內 `break` 的角色與產出範圍。
- 預測正常結束、`yield break` 與消費端 `break` 三種路徑中，哪些一般程式不會執行、`finally` 何時執行。
- 說明 `foreach` 離開時自動呼叫 `Dispose()` 與手動列舉時呼叫者須自行 `Dispose()`（如 `using`）的關係，並指出從未開始列舉時不會執行主體內的 `finally`。
- 修改產生端與消費端的結束條件，預測誰先讓迴圈停下並以執行結果核對。

## Constraints
- 程式沿用 [共用 Console 專案](../01-list-basics/YieldTutorial/README.md)，主線文字沿用 [主線筆記](../01-list-basics/yield筆記.md#unit-06)。
- `try`／`finally` 清理語法直接提供完整可執行程式，不要求學生先自行手寫。
- 短篇說明、先預測再揭答、立即回饋與可執行 C# 範例；只記錄教材設計與來源，不保存個人作答、能力或學習進度。

## Out of scope
- 同一序列再次列舉與可變輸入（單元 7）；`List<T>` 與 `ToList()` 取捨（單元 8）；在迭代器中使用 `try`／`catch` 的例外處理專題、非同步串流 `IAsyncDisposable`、編譯器產生的狀態機與 `Dispose` 內部欄位。
