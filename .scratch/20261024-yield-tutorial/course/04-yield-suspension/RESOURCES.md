# yield 暫停與繼續 Resources

## Knowledge

官方來源查證日期：2026-11-21；文件為 Microsoft Learn 當前版本（C# 語言參考記錄最新發行的語言版本），共用 Console 範例以 `net10.0`（SDK 11.0.100、.NET Runtime 10.0.15）驗證。

- [Microsoft Learn：yield statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield)
  主要閱讀來源。確認 yield return 提供下一個值、走到迭代器結尾即結束；「Execution of an iterator」一節說明每次繼續都從造成上次暫停的 yield return 之後執行到下一個 yield return；並列出 ref／out／in 參數、lambda、catch／finally 等使用限制。
- [Microsoft Learn：Iterators](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators)
  確認迭代器方法一次產生一個值、在兩次要求之間保留位置；用於術語與概念說明。延遲執行段落留給單元 5。
- [Microsoft Learn：Errors and warnings for iterator methods and yield return](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/iterator-yield#structure-of-an-iterator-method)
  確認迭代器中不能出現 return 陳述式（CS1622）、回傳型別須為迭代器介面（CS1624）、yield return 須接運算式（CS1627）。
- [Microsoft Learn：Jump statements（return）](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements#the-return-statement)
  確認 return 結束所在函式的執行，並將控制與結果交回呼叫端；用於 return 與 yield return 對照。
- [C# 語言規格：Blocks](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#133-blocks)
  確認含 yield 陳述式的區塊為 iterator block，其中出現 return 陳述式是編譯錯誤。
- [C# 語言規格：Advance the enumerator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#151552-advance-the-enumerator)
  確認遇到 yield return 時將值指派給 Current、暫停並保存所有區域變數與參數的值及位置，MoveNext 回傳 true；繼續時還原這些值，從該 yield return 之後執行；走到結尾時回傳 false。附註說明變數參照的物件內容可能已變動。
- [C# 語言規格：Enumerator objects](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#15155-enumerator-objects)
  確認列舉器物件「通常」由編譯器產生的類別實作，但未規定作法；用於說明本單元只依公開行為觀察。

CS1622、CS1624 的觸發寫法為 2026-11-21 以 SDK 11.0.100、`net10.0`、啟用 ImplicitUsings 的臨時專案實測；HTML 預測題的較短程式輸出（`A 1 B1 3 B2 6 B3 C`）亦於同一環境實測。不帶值的 `return;` 同樣得到 CS1622，於 2026-11-21 最後驗收時以相同環境補測。CS1627 引自官方錯誤清單，未另行實測。

## Wisdom (Communities)

本單元為語言規則與可重現的執行順序，目前沒有需要交由社群判斷的實務取捨。
