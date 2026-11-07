# 列舉器協定 Resources

## Knowledge

官方來源查證日期：2026-11-07；API 文件選用 .NET 10，共用 Console 範例以 SDK 10.0.402 驗證。

- [Microsoft Learn：IEnumerator<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1?view=net-10.0)
  主要閱讀來源。確認列舉器起始於第一個元素之前、結束後 MoveNext 持續回傳 false、不能把 Current 設回開頭須取得新列舉器、Reset 可擲出 NotSupportedException，以及介面繼承 IDisposable。
- [Microsoft Learn：IEnumerator<T>.Current](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1.current?view=net-10.0#remarks)
  確認 Current 在尚未 MoveNext、MoveNext 回傳 false、集合被修改時皆為未定義；兩次 MoveNext 之間回傳同一元素。
- [Microsoft Learn：IEnumerator.MoveNext](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator.movenext?view=net-10.0#remarks)
  確認回傳值的意義與越過結尾後的位置。
- [Microsoft Learn：using statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using)
  確認離開 using 區塊時呼叫 Dispose，即使發生例外也一樣。
- [C# 語言規格：The foreach statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#1395-the-foreach-statement)
  確認 foreach 語意等同取得列舉器、在 try 中以 while (MoveNext) 讀取 Current、在 finally 中 Dispose；編譯器依集合型別選擇 GetEnumerator。
- [Microsoft Learn：Iteration statements（foreach）](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement)
  確認來源集合為空時 foreach 主體不執行。
- [Microsoft Learn：List<T>.GetEnumerator](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.getenumerator?view=net-10.0)
  確認以 List<T> 型別呼叫時回傳 List<T>.Enumerator；本單元只作適用範圍說明。

Current 在未定義時點的實際結果（List<int> 為 0、List<string> 為 null、int[] 及非泛型列舉器擲出 InvalidOperationException）為 2026-11-07 以 .NET 10 SDK、啟用 ImplicitUsings 的臨時專案實測，屬實作細節，非官方文件引述。

## Wisdom (Communities)

本單元為固定的介面使用規則，目前沒有需要交由社群判斷的實務取捨。
