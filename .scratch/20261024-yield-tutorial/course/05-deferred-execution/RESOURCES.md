# 迭代器延遲執行 Resources

## Knowledge

官方來源查證日期：2026-11-21；文件為 Microsoft Learn 當前版本（C# 語言參考記錄最新發行的語言版本），共用 Console 範例以 `net10.0`（SDK 11.0.100、.NET Runtime 10.0.15）驗證。

- [Microsoft Learn：Iterators — Understand deferred element production](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators#understand-deferred-element-production)
  主要閱讀來源。確認呼叫迭代器方法不會立即執行主體、開始列舉時才產生元素（deferred execution）、從不列舉就不執行主體；並指出迭代器與集合都可公開 `IEnumerable<T>`，無法只從介面推斷執行時機。用於本單元的核心敘述與回傳型別的限定。
- [Microsoft Learn：yield statement — Execution of an iterator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield#execution-of-an-iterator)
  確認「Calling an iterator doesn't execute it immediately」，開始列舉時執行到第一個 yield return；官方範例的輸出順序與本單元情境 3 相同。
- [C# 語言規格：Enumerable objects](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#15156-enumerable-objects)
  確認回傳可列舉介面的迭代器被呼叫時不立即執行迭代器區塊，而是建立並回傳 enumerable object；其 GetEnumerator 回傳 enumerator object。用於區分「呼叫」與「GetEnumerator」都不執行主體。
- [C# 語言規格：Advance the enumerator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#151552-advance-the-enumerator)
  確認 MoveNext 在 before 狀態時從開頭執行迭代器區塊，之後從造成暫停的 yield return 之後繼續；用於「第一次 MoveNext 才開始」與部分列舉的推論。
- [C# 語言規格：Enumerator objects](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#15155-enumerator-objects)
  確認列舉器物件「通常」由編譯器產生的類別實作，未規定作法，並說明 Dispose 在 suspended 狀態執行 finally；用於說明本單元只依公開行為觀察，清理留到單元 6。

HTML 排序題的較短程式（輸出 `A B P C1 Q D2 E`，R 不出現）於 2026-11-21 以 SDK 11.0.100、`net10.0`、啟用 ImplicitUsings 的臨時專案實測。

## Wisdom (Communities)

本單元為語言規則與可重現的執行順序，目前沒有需要交由社群判斷的實務取捨。

## Gaps

- LINQ 各運算的延遲／立即執行差異未納入本單元；若日後需要，另以 LINQ 官方文件（例如 Classification of standard query operators by manner of execution）整理為選讀。
