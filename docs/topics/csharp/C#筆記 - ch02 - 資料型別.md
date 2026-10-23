---
title: C#筆記 - ch02 - 資料型別
tags: [C#]

---

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

## 列舉(Enumeration)

程式碼範例：

```csharp
internal class Program
{
    enum WeekDays : int
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    static void Main(string[] args)
    {
        Console.WriteLine($"星期三列舉常數值：{(int)WeekDays.Wednesday}");
        Console.WriteLine($"星期五列舉常數值：{(int)WeekDays.Friday}");
    }
}
```

### 整數轉成 enum 的正確觀念

C# 把整數轉成 enum 時**完全不檢查合法性**，只是把 bit pattern 原封不動塞進去；`Enum.IsDefined` 是事後幫你檢查「這個底層數字，是否剛好等於某個具名成員的值」。

```csharp
enum Level { Low = 1, Mid = 2, High = 4 }
Level x = (Level)5;
```

x 的底層數值是多少？

A. 1（wrap 回 Low） B. 4（wrap 回 High） C. 5 D. 執行時拋例外

答案是**C**。即使是一般（非 [Flags]）enum，C# 轉型時只是把整數塞進去，不管範圍、不管是否有對應名稱。

> 這個坑在實務上特別陰險，因為它不會馬上爆炸——資料可能一路往下傳，直到某個 switch 沒有 default case、或某段邏輯用 if/else if 窮舉所有已知成員卻漏了「未知值」分支，才會在很遠的地方出現詭異行為（甚至完全沒有錯誤訊息，只是邏輯悄悄跳過）。

所以會用 `Enum.IsDefined` 檢查，來看看它在做什麼：

```csharp
Enum.IsDefined(typeof(WeekDays), 99); // false，因為沒有成員的值是 99
Enum.IsDefined(typeof(WeekDays), 1);  // true，因為 Monday = 1
Enum.IsDefined(typeof(WeekDays), 2);  // true，因為 Tuesday = 2
```

它做的事情很單純：把你給的值（或名稱）拿去跟這個 enum 型別所有具名成員的底層值做**逐一比對**，有一個相等就回 `true`，全部都不相等就回 `false`。這跟「數值有沒有超出範圍」沒有直接關係——它比的是「有沒有對應的名稱」，不是「有沒有超出某個上下限」。

兩者的關係總結：

| 動作 | 會不會檢查合法性 | 行為 |
|---|---|---|
| `(Color)99` 轉型 | 不會 | 直接把 99 存進去，不管有沒有對應名稱 |
| `Enum.IsDefined(...)` | 會（但是事後、你主動呼叫才做） | 逐一比對底層值，回報有沒有對應成員 |

所以常見的防禦性寫法是：

```csharp
int raw = 99;
WeekDays c = (WeekDays)raw;         // 轉型永遠成功
if (!Enum.IsDefined(typeof(WeekDays), c))
{
    // 這裡才是你要處理「非法值」的地方
    throw new ArgumentOutOfRangeException(nameof(raw));
}
```

> 補充：Flags enum 的例外情況：
> 如果 enum 標了 `[Flags]`，`Enum.IsDefined` 對組合值通常會回 `false`（因為組合值本身沒有單一具名成員對應），這是另一個常見坑。

### 驗證 Flags 值合法

正確做法是用位元運算，檢查這個值有沒有超出「所有合法旗標的聯集」範圍：

```csharp
[Flags]
enum Permission
{
    None  = 0,
    Read  = 1,   // 0001
    Write = 2,   // 0010
    Exec  = 4,   // 0100
    All   = Read | Write | Exec // 0111
}
```

```csharp
const Permission AllValid = Permission.Read | Permission.Write | Permission.Exec;

bool IsValidPermission(Permission p) => (p & ~AllValid) == 0;
```

#### 關鍵坑：`Enum.IsDefined` 不懂位元組合

```csharp
Enum.IsDefined(typeof(Permission), Permission.Read | Permission.Write); // false！
```

即使 `Read | Write = 3` 是一個完全合理、預期中的組合值，`Enum.IsDefined` 還是回 `false`——因為它的比對邏輯**沒有變**，還是「這個數字有沒有剛好等於某一個具名成員」。`3` 不等於 `Read(1)`、不等於 `Write(2)`、也不等於 `All(7)`，所以找不到，回 `false`。

這代表：**對 `[Flags]` enum 用 `Enum.IsDefined` 來驗證合法性，做法本身就是錯的**——它會把大量合法的組合值誤判成非法。

#### 檢查「有沒有某個旗標」用 `HasFlag`，不是 `IsDefined`

```csharp
if (p.HasFlag(Permission.Write)) { ... }
```

`HasFlag` 做的是位元 AND 比對，跟「這個值合不合法」是兩件事——它只回答「這個旗標有沒有被設定」，不驗證整個值乾不乾淨。

## 結構(Structure)

程式碼範例：

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        DemonstrateStruct();
    }

    // 定義 Product 產品結構資料型別
    private struct Product 
    {
        public string No, Name;
        public int Price;
    }

    static void DemonstrateStruct()
    {
        Product ps5_slim;
        ps5_slim.No = "CFI-2018B01";
        ps5_slim.Name = "PlayStation PS5 Slim 數位版 輕薄型主機";
        ps5_slim.Price = 14580;

        Console.WriteLine($"產品編號：{ps5_slim.No}");
        Console.WriteLine($"產品名稱：{ps5_slim.Name}");
        Console.WriteLine($"產品單價：{ps5_slim.Price}");
    }
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
