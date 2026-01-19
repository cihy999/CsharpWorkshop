# C#筆記 - ch02 - 資料型別

[Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch02 資料型別與主控台應用程式](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ) 的學習筆記。

## 資料型別

分為：

1. 布林(bool)
2. 數值，例如int。可再分為有號(Signed)、無號(Unsigned)。
3. 文字，例如string
4. 自定義結構/類別/列舉

微軟文件：

- [bool （C# 參考）](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/bool)
- [整數數值類型 （C# 參考）](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)
- [浮點數值類型 （C# 參考）](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types)
- [字串型別](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/reference-types#the-string-type)

## 宣告變數

程式碼範例：

```csharp
namespace Ch02_Types
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int score = 100;        // 宣告 score 為整數變數，代表成績初值為 100
            double price = 90.5;    // 宣告 price 為浮點數變數，代表單價初值為 90.5
            char sex = 'M';         // 宣告 sex 為字元變數，代表性別初值為 M
            char c1 = '\x0061';     // 宣告 c1 為字元變數，以 a 字元的 ASCII 碼表示 61(16進位)
            char c2 = (char)97;     // 宣告 c2 為字元變數，以 a 字元的 ASCII 碼表示 97(10進位)
            char c3 = '\u0061';     // 宣告 c3 為字元變數，以 a 字元的 UniCode 表示(16進位)
            string bookName = "Visual C# 程式設計經典";   // 宣告 bookName 為字串變數，，代表書名初值為 "Visual C# 程式設計經典"

            Console.WriteLine($"score = {score}, type = {score.GetType()}"); // GetType(): 取得某個變數的資料型別
            Console.WriteLine($"price = {price}, type = {price.GetType()}");
            Console.WriteLine($"sex = {sex}, type = {sex.GetType()}");
            Console.WriteLine($"c1 = {c1}, type = {c1.GetType()}");
            Console.WriteLine($"c2 = {c2}, type = {c2.GetType()}");
            Console.WriteLine($"c3 = {c3}, type = {c3.GetType()}");
            Console.WriteLine($"bookName = {bookName}, type = {bookName.GetType()}");
        }
    }
}
```

## 專有名詞

- 識別字(Identifier)：讓使用者定義一個方法、變數、類別等名稱。
- 關鍵字(Keywords)：C#的功能定義，不可作為識別字，例如using。
- 常值(Literal)：資料本身的值，例如123。
- 常數(Constant)：執行期間維持不變。
- 變數(Variable)：執行期間可以改變。
