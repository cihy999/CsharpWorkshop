# 序列結束、提前停止與清理 Resources

## Knowledge

官方來源查證日期：2026-11-23；文件為 Microsoft Learn 當前版本（C# 語言參考記錄最新發行的語言版本），共用 Console 範例以 `net10.0`（SDK 11.0.100、.NET Runtime 10.0.15）驗證。

- [Microsoft Learn：yield statement — provide the next element in an iterator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield)
  主要閱讀來源。確認 `yield break` 用於明確結束迭代、走到迭代器結尾也會結束迭代；確認 `yield return` 與 `yield break` 可寫在只有 `finally`（無 `catch`）的 `try` 區塊內，不能寫在 `finally` 區塊內；確認在迭代器中配置的資源於正常完成、`yield break` 或呼叫端提前中斷列舉（iterator disposed）時都會釋放（見 `#using-statements-in-iterators` 與 `#execution-of-an-iterator`）。
- [Microsoft Learn：Iterators — End the sequence with yield break](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators#end-the-sequence-with-yield-break)
  確認當特定條件應立即停止產生元素時使用 `yield break`，與自然走到方法主體結尾對照。
- [Microsoft Learn：Jump statements — The break statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements#the-break-statement)
  確認一般 `break` 只終止最內層包圍它的迴圈（如 `foreach`、`for`）或 `switch`，控制權移交該陳述式之後；用於區分消費端 `foreach` 內的 `break`、產生端內部迴圈的一般 `break`，以及結束整個迭代器的 `yield break`。
- [C# 語言規格：15.15.5.2 Advance the enumerator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#151552-advance-the-enumerator)
  確認執行到 `try` 內的 `yield return` 時，關聯的 `finally` **不會**在當下執行；執行到 `yield break` 時，會先執行外圍 `try` 的 `finally`，再將狀態設為 `after` 並讓 `MoveNext` 回傳 `false`；走到迭代器主體結尾時 `MoveNext` 回傳 `false`。
- [C# 語言規格：15.15.5.4 Dispose of resources](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#151554-dispose-of-resources)
  確認列舉器在尚未開始（`before`）狀態呼叫 `Dispose` 時只將狀態改為 `after`（不會執行主體內的 `finally`）；在暫停（`suspended`）狀態呼叫 `Dispose` 時，會如同最後執行的 `yield return` 是 `yield break` 一般執行包圍它的 `finally` 區塊（不會繼續執行剩餘的一般程式）；在已結束（`after`）狀態呼叫 `Dispose` 則無額外作用。
- [C# 語言規格：13.9.5.2 Synchronous foreach](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#13952-synchronous-foreach)
  確認同步 `foreach` 語意上將 `while (e.MoveNext())` 包在 `try` 中，並於外圍 `finally` 清理（Dispose）列舉器 `e`；因此消費端以 `break` 離開 `foreach` 時仍會觸發列舉器的 `Dispose()`，而手動呼叫 `GetEnumerator()` 者必須自行透過 `using` 或 `Dispose()` 負責清理。
- [C# 語言規格：13.15 The yield statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#1315-the-yield-statement)
  確認 `yield break` 由內而外執行包圍它的 `try` 陳述式的 `finally` 區塊後將控制權交回呼叫者，`yield break` 之後的端點不可達。

HTML 預測題的較短程式 `Items(stopAt)`（輸出 `Open A1 A2 Done Clean After Open B1 Clean Open C1 Clean`）於 2026-11-23 以 SDK 11.0.100、`net10.0`、啟用 ImplicitUsings 的臨時專案實測。

## Wisdom (Communities)

本單元聚焦於 C# 語言規則與確定性的清理順序，目前無須委由外部社群裁決。

## Gaps

- 迭代器內部的 `using` 宣告（編譯為 `try`／`finally`）在本單元以顯式 `try`／`finally` 呈現清理時間點，官方 `#using-statements-in-iterators` 列為主要閱讀對照；非同步串流 `IAsyncEnumerable<T>` 與 `DisposeAsync()` 不在主線範圍。
