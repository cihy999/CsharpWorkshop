# IEnumerable 約定 Resources

## Knowledge

官方來源查證日期：2026-11-07；API 文件選用 .NET 10，共用 Console 範例以 SDK 10.0.402 驗證。

- [Microsoft Learn：Interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces)
  主要閱讀來源。確認介面是成員約定、實作類別須提供全部成員、介面不能直接實例化。
- [Microsoft Learn：Generic types and methods](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics)
  確認型別參數 T 的讀法，以及角括號內型別決定集合可保存的資料。
- [Microsoft Learn：IEnumerable<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-10.0)
  確認此介面公開列舉器、支援對指定型別集合的簡單走訪。
- [Microsoft Learn：List<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-10.0)
  確認 List<T> 的宣告實作 IEnumerable<T>。
- [Microsoft Learn：Reference types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types)
  確認參考型別變數保存物件參考，兩個變數可參照同一物件。
- [Microsoft Learn：List<T>.GetEnumerator 備註](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.getenumerator?view=net-10.0#remarks)
  確認列舉期間修改集合會使列舉器失效並擲出 InvalidOperationException；本單元只作邊界提示。

編譯錯誤代碼（CS1061、CS0021、CS0428、CS0266）為 2026-11-07 以 .NET 10 SDK、啟用 ImplicitUsings 的臨時專案實測結果，非官方文件引述。

## Wisdom (Communities)

本單元為型別讀法與固定語法練習，目前沒有需要交由社群判斷的實務取捨。
