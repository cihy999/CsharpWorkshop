# 教材設計備註

- 此目錄為第五單元的教學工作區；lessons、reference 路徑相對於此目錄。樣式與互動元件復用 `../01-list-basics/assets/`，不另建副本；本單元未修改共用元件。
- 程式放在共用專案 `../01-list-basics/YieldTutorial/DeferredExecution.cs`，入口選項 `05`、`05-answer`；Markdown 主線為 `../01-list-basics/yield筆記.md#unit-05`。預設入口仍為 `01`。
- 三種情境共用同一個 `Steps()` 與消費端 `RequestElements(sequence, requestCount)`，只差要求次數 0、2、4。呼叫方法寫在 `Run()` 裡、`RequestElements` 之外，讓「呼叫前後」與「GetEnumerator 前後」成為各自獨立的標記；不使用委派傳入方法，避免增加本課未教的概念。
- 情境 1 刻意取得列舉器但不呼叫 MoveNext，同時證明「呼叫」與「GetEnumerator」都不執行主體；Ticket 的「不列舉」以此呈現。
- 為滿足「不將延遲執行歸因於回傳型別」，`Run()` 最後加入 `StepsWithList()` 對照：回傳型別同為 `IEnumerable<int>`，呼叫時即執行主體。官方 Iterators 文章也指出只看 `IEnumerable` 無法推斷執行時機。
- 示範只靠單執行緒的輸出先後判斷順序，不使用 `Thread.Sleep`、`Task.Delay` 或 `Stopwatch`。
- 修改題移動的是原本開頭的標記，移到 `yield return 2;` 之後並改名為「移動過來的標記」，避免標記文字與位置不符；結果在三種情境分別為不出現、不出現、第 3 次 MoveNext 時出現，用來對比部分與完整列舉。學生練習用 `StepsMovedExercise()`，專案參考解為 `StepsMoved()`，沿用單元 4 的命名分工。
- HTML 排序題用較短的 `Two()`，候選標記含一個不會出現的 R，練習「部分列舉時後段不執行」；程式與輸出已在系統 TEMP 的臨時專案實測，臨時專案已刪除。
- 情境 2 離開 `using` 會呼叫 Dispose，`Steps()` 沒有 finally 所以無輸出；教材只點出這件事，清理細節留給單元 6。
- 不使用規格的 before／running／suspended／after 狀態名稱，也不提編譯器產生的型別；規格中「第一次 GetEnumerator 可能回傳自身」屬實作附註，未納入教材。
- 專案規範優先：不建立 learning-records，瀏覽器練習不寫入儲存空間。
- teach 參照指紋於 2026-11-21 核對，與相容性基準一致。
