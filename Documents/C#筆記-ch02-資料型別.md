# C#筆記 - ch02 - 資料型別

[Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch02 資料型別與主控台應用程式](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ) 的學習筆記。

## 資料型別

分為：

1. 布林(bool)
2. 數值，例如int。可再分為有號(Signed)、無號(Unsigned)。
3. 文字，例如string
4. 自定義結構/類別/列舉

依記憶體管理方式，資料型別可以分為：

1. 實質型別(Value Type)：
   - 存在堆疊區(Stack)記憶體，記憶體位址直接存資料本身
   - 比如int、double、char、bool、結構、列舉等
2. 參考型別(Reference Type)：
   - 資料存在堆積區(Heap)記憶體，該資料的位址(指標)存在堆疊區(Stack)，所以拿資料時，先從堆疊拿到堆積區的記憶體位址，再去堆積區拿取實際資料。
   - 宣告參考型別變數，先堆疊區建立一個null值(空的，沒東西)，new之後才給堆積區(Heap)記憶體位址。
   - 比如string、class、interface、委派、陣列等

> 堆積區(Heap)記憶體由CLR(Common Language Runtime)動態配置(Dynamic Allocation)。
參考型別如果沒被指向，就會被系統定時釋放。

> 字串屬於參考類型，所以對字串做任何變動，都會在執行期間產生一個變動後的字串。

微軟文件：

- [bool （C# 參考）](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/bool)
- [整數數值類型 （C# 參考）](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)
- [浮點數值類型 （C# 參考）](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types)
- [字串型別](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/reference-types#the-string-type)

## 宣告變數

程式碼範例：

```csharp
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
```

## 運算子

程式碼範例：

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        // 算數運算子
        int i, j = 20, k = 3;
        i = j + k;
        Console.WriteLine($"{i} = {j} + {k}");
        i = j - k;
        Console.WriteLine($"{i} = {j} - {k}");
        i = j * k;
        Console.WriteLine($"{i} = {j} * {k}");
        i = j / k;
        Console.WriteLine($"{i} = {j} / {k}");
        i = j % k;
        Console.WriteLine($"{i} = {j} % {k}");

        // 比較運算子
        int a = 23, b = 98;
        Console.WriteLine($"{a} > {b} => {a > b}");
        Console.WriteLine($"{a} < {b} => {a < b}");
        Console.WriteLine($"{a} >= {b} => {a >= b}");
        Console.WriteLine($"{a} <= {b} => {a <= b}");
        Console.WriteLine($"{a} == {b} => {a == b}");
        Console.WriteLine($"{a} != {b} => {a != b}");

        // 邏輯運算子
        bool l = false , r = true;
        Console.WriteLine($"{l} && {r} => {l && r}");
        Console.WriteLine($"{l} || {r} => {l || r}");
        Console.WriteLine($"!{l}, !{r} => {!l}, {!r}");

        // 位元運算子
        // n1 = |0001|, n2 =|0010|, n3 = |0011|
        int n1 = 1, n2 = 2, n3 = 3;
        Console.WriteLine($"{n1} & {n2} => {n1 & n2}");
        Console.WriteLine($"{n1} | {n2} => {n1 | n2}");
        Console.WriteLine($"{n1} ^ {n2} => {n1 ^ n2}");
        Console.WriteLine($"~{n3} => {~n3}");

        // 位移運算子
        Console.WriteLine($"1 << 1 = {1 << 1}");
        Console.WriteLine($"1 >> 1 = {1 >> 1}");

        // 複合指定運算子
        i = 5;
        i += 1;
        Console.WriteLine($"i += 1 => {i}");
        i -= 1;
        Console.WriteLine($"i -= 1 => {i}");
        i *= 2;
        Console.WriteLine($"i *= 2 => {i}");
        i /= 2;
        Console.WriteLine($"i /= 2 => {i}");
        i %= 2;
        Console.WriteLine($"i %= 2 => {i}");
        i &= 2;
        Console.WriteLine($"i &= 2 => {i}");
        i |= 2;
        Console.WriteLine($"i |= 2 => {i}");
        i ^= 2;
        Console.WriteLine($"i ^= 2 => {i}");
        i <<= 2;
        Console.WriteLine($"i <<= 2 => {i}");
        i >>= 2;
        Console.WriteLine($"i >>= 2 => {i}");

        // 遞增遞減運算子
        i = 10;
        Console.WriteLine($"i++ = {i++}");	// 先丟 i 的值，再執行 i = i + 1
        Console.WriteLine($"++i = {++i}");  // 執行 i = i + 1，再丟 i 的值
    }
}
```

## 字串

### 字串合併

程式碼範例：

```csharp
/// <summary>
/// 字串合併
/// </summary>
static void ConcatenateString() 
{
    string str1 = "Antigravity";
    string str2 = str1 + "是一個很棒的助理！" + 100 + "分";
    Console.WriteLine(str2); // Antigravity是一個很棒的助理！100分
}
```

### 字串格式化

程式碼範例：

```csharp
/// <summary>
/// 字串格式化
/// </summary>
static void FormatString()
{
    string str1 = string.Format("{0}是一個很棒的助理！{1}分", "Antigravity", 100);
    Console.WriteLine(str1);
}
```

### 字串插值

程式碼範例：

```csharp
/// <summary>
/// 字串插值
/// </summary>
static void InterpolateString() 
{
    string name = "Antigravity";
    int score = 100;
    Console.WriteLine($"{name}是一個很棒的助理！{score}分");
}
```

## 資料型別轉換

### 隱含轉換(Implicit Conversion)

1. 兩者資料型別相容
2. 目的資料型別比原始資料型別範圍大時，比如int轉換為long
3. 參考型別(Reference Type)轉換則是將衍生類別轉換成基底類別，一律使用隱含轉換

> 注意 int、uint、long 資料型別轉換為 float 或是 double 時，有可能會使轉換的結果產生不精確。
>
> **原因：浮點數的記憶體結構 (IEEE 754)**
> 浮點數並非精確儲存每一個位元，而是採用類似「科學記號法」的方式儲存，分為三個部分：
>
> 1. **正負號 (Sign bit)**：決定正負。
> 2. **指數 (Exponent)**：決定數值的「大小範圍」。
> 3. **尾數 (Mantissa/Significand)**：決定數值的「精確度」。
>
> **為什麼會不精確？**
> 以 `float` 為例，其尾數空間僅約 23~24 bits（約 7 位有效數字）。
> 當一個 32 bits 的 `int` 數值超過了 `float` 尾數能表達的範圍時，多出來的位元就會被捨入 (Rounding)，導致精確度喪失。

| 型別         | 總位元  | 正負號 (Sign) | 指數 (Exponent) | 尾數 (Mantissa) |
| :----------- | :------ | :------------ | :-------------- | :-------------- |
| **`float`**  | 32 bits | 1 bit         | 8 bits          | 23 bits         |
| **`double`** | 64 bits | 1 bit         | 11 bits         | 52 bits         |

### 明確轉換(Explicit Conversion)

型態轉換(Type Cast)的方式強迫資料換成別的型別，可能會有資料遺失、例外問題。

程式碼範例：

```csharp
static void DoConversion() 
{ 
    int i = 0;
    long l = 0;
    float f = 0;
    double d = 0;

    i = 1;
    f = i;
    Console.WriteLine($"i = {i}, f = i => {f}");

    i = 1;
    d = i;
    Console.WriteLine($"i = {i}, d = i => {d}");

    l = 2L;
    d = l;
    Console.WriteLine($"l = {l}, d = l => {l}");

    // double(浮點數)轉換成int(整數)，小數部分會遺失
    d = 4.38974567569;
    i = (int)d;
    Console.WriteLine($"d = {d}, i = d => {i}");
    d = 0.1234578;
    l = (long)d;
    Console.WriteLine($"d = {d}, l = d => {l}");

    // int轉換成byte，因為縮小，所以會像順時針轉圈的效果(257 -> 2)
    byte b;
    i = 257;
    b = (byte)i;
    Console.WriteLine($"i = {i}, b = i => {b}");
}
```

## 專有名詞

- **識別字(Identifier)**：讓使用者定義一個方法、變數、類別等名稱。
- **關鍵字(Keywords)**：C#的功能定義，不可作為識別字，例如using。
- **常值(Literal)**：資料本身的值，例如123。
- **常數(Constant)**：執行期間維持不變。
- **變數(Variable)**：執行期間可以改變。
- **運算子(Operator)**
- **運算元(Operand)**
- **單元運算子(Unary Operator)**：如：-5
- **二元運算子(Binary Operator)**：如：a + b
- **三元運算子(Tenary Operator)**：如：max = a > b > a : b
