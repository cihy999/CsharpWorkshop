---
title: C#筆記 - ch04 - 方法
tags: [C#]

---

# C#筆記 - ch04 - 方法

[Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch02 資料型別與主控台應用程式](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ) 的學習筆記。

```csharp
namespace Ch04_Method
{
    internal class LoginProcess 
    {
        public void LoginByUser(string userName, bool isMale)
        {
            Console.WriteLine(GetWelcome(userName, isMale));
        }

        public string GetWelcome(string userName, bool isMale)
        {
            string gender = isMale ? "先生" : "小姐";
            return $"{userName} {gender}，歡迎光臨！";
        }

        public static void Login(string userName, bool isMale) 
        {
            LoginProcess process = new LoginProcess();
            process.LoginByUser(userName, isMale);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            LoginProcess.Login("Jake", true);
            LoginProcess.Login("Kelly", false);
            
            LoginProcess process = new LoginProcess();
            process.LoginByUser("Louis", true);
        }
    }
}
```

## 引數的傳遞方法

```csharp
internal class SampleMethod 
{
    public static void ShowCallValue() 
    {
        Console.WriteLine("===== Call by Value 傳值呼叫 =====");

        int a = 50, b = 100;
        Console.WriteLine($"1. 呼叫敘述，未進入方法前\t\t: a={a}, b={b}");

        CallValue(a, b);

        Console.WriteLine($"4. 呼叫敘述，離開方法回到原處時\t\t: a={a}, b={b}");
    }

    public static void ShowCallReference()
    {
        Console.WriteLine("===== Call by Reference 參考呼叫 =====");

        int a = 50, b = 100;
        Console.WriteLine($"1. 呼叫敘述，未進入方法前\t\t: a={a}, b={b}");

        CallReference(ref a, ref b);

        Console.WriteLine($"4. 呼叫敘述，離開方法回到原處時\t\t: a={a}, b={b}");
    }

    public static void ShowOuputParameter()
    {
        Console.WriteLine("===== Call Out 傳出參數 =====");

        Console.WriteLine($"1. 呼叫敘述，未進入方法前，未初始化 a、b");

        CallOut(out int a, out int b);

        Console.WriteLine($"4. 呼叫敘述，離開方法回到原處時\t\t: a={a}, b={b}");
    }

    private static void CallValue(int x, int y)
    {
        x = 33;
        y = 66;
        Console.WriteLine($"2. 方法內、交換前\t\t\t: x={x}, y={y}");

        int temp = x;
        x = y;
        y = temp;
        Console.WriteLine($"3. 方法內、交換後\t\t\t: x={x}, y={y}");
    }

    private static void CallReference(ref int x, ref int y)
    {
        x = 33;
        y = 66;
        Console.WriteLine($"2. 方法內、交換前\t\t\t: x={x}, y={y}");

        int temp = x;
        x = y;
        y = temp;
        Console.WriteLine($"3. 方法內、交換後\t\t\t: x={x}, y={y}");
    }

    private static void CallOut(out int x, out int y)
    {
        x = 33;
        y = 66;
        Console.WriteLine($"2. 方法內、交換前\t\t\t: x={x}, y={y}");

        int temp = x;
        x = y;
        y = temp;
        Console.WriteLine($"3. 方法內、交換後\t\t\t: x={x}, y={y}");
    }
}

internal class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args">陣列都是使用 Call by Reference，但可以省略 ref</param>
    static void Main(string[] args)
    {
        SampleMethod.ShowOuputParameter();
    }
}
```

陣列可以省略 ref（因為陣列是 reference type）：

```csharp
/// <summary>
/// 取得最大值
/// </summary>
/// <param name="numbers">陣列可以省略ref</param>
/// <returns></returns>
private static int GetMax(int[] numbers)
{
    int max = 0;

    for (int i = 0; i < numbers.Length; i++)
    {
        if (max < numbers[i])
            max = numbers[i];
    }

    return max;
}
```

### in

`in` 是 C# 7.2 加入的參數修飾詞，可以理解為唯讀的 `ref`。

|     | ref | out | in |
| --- | --- | --- | --- |
| 傳遞方式 |  傳址 | 傳址 | 傳址 |
| 呼叫前需初始化 | ✅ | ❌ | ✅ |
| 方法內可修改 | ✅ | ✅（且必須） | ❌ 編譯錯誤 |
| 用途 | 雙向傳遞 | 從方法輸出值 | 避免複製大型結構體 |

```csharp
internal class LoginProcess 
{
    internal struct User 
    { 
        public string Name;
        public bool IsMale;
    }

    public void LoginByUser(string userName, bool isMale)
    {
        Console.WriteLine(GetWelcome(userName, isMale));
    }

    public string GetWelcome(string userName, bool isMale)
    {
        string gender = isMale ? "先生" : "小姐";
        return $"{userName} {gender}，歡迎光臨！";
    }

    public static void Login(in User user)
    {
        LoginProcess process = new LoginProcess();
        process.LoginByUser(user.Name, user.IsMale);
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        LoginProcess.User user = new() { Name = "Jack", IsMale = true };
        LoginProcess.Login(user);
    }
}
```

## 方法多載

```csharp
public static int Sum(int a, int b)
{ 
    return a + b;
}

public static int Sum(int a, int b, int c)
{
    return a + b + c;
}

public static string Sum(string a, string b)
{
    return a + b;
}
```

## 專有名詞

- **實引數(Actual Arguments)**：若方法A的某個敘述呼叫方法B，則方法A的參數稱為實引數。
- **虛引數(Dummy Arguments)**：若方法A的某個敘述呼叫方法B，則方法B的參數稱為虛引數。
- **傳值呼叫(Call by Value)**：將實引數的值複製到虛引數中。
- **參考呼叫(Call by Reference)**
- **傳出參數(Output Parameters)**
- 方法**主體(Body)**：方法實作的內容。

# C# 測驗 - Ch04 方法 (Method)

## 🚀 題目一：執行結果預測

執行以下程式碼後，第 4 步驟印出的 `a` 和 `b` 值是多少？

```csharp
int a = 50, b = 100;
// 步驟1：a=50, b=100

CallValue(a, b);

// 步驟4：a=?, b=?
Console.WriteLine($"4. 呼叫敘述，離開方法回到原處時: a={a}, b={b}");

// ---
private static void CallValue(int x, int y)
{
    x = 33;
    y = 66;
    // 步驟2：x=33, y=66

    int temp = x;
    x = y;
    y = temp;
    // 步驟3：x=66, y=33
}
```

**步驟4 的 a、b 各是多少？**

## 題目一作答

a = 50
b = 100

---

## 🔍 題目二：程式碼填空

下方程式碼呼叫 `CallReference`，但關鍵字被挖空了。請填入正確的關鍵字，讓 `a`、`b` 在方法執行後，於呼叫端也能看到變更：

```csharp
int a = 50, b = 100;

CallReference(_____ a, _____ b);  // ← 填入關鍵字

// ---
private static void CallReference(_____ int x, _____ int y)  // ← 填入關鍵字
{
    x = 33;
    y = 66;
    // ... 交換邏輯
}
```

**空格應填入什麼？**

## 題目二作答

```csharp
int a = 50, b = 100;

CallReference(ref a, ref b);

private static void CallReference(ref int x, ref int y)
{
    x = 33;
    y = 66;
    // ... 交換邏輯
}
```

---

## 🤔 題目三：邏輯選擇題

關於 `out` 參數，下列哪一個敘述是**正確的**？

**(A)** 呼叫端必須在傳入 `out` 參數前，先將變數初始化（賦值）

**(B)** 方法內部可以不對 `out` 參數賦值，編譯器不會報錯

**(C)** `out` 參數在方法內**必須**被賦值，否則編譯錯誤

**(D)** `out` 和 `ref` 完全相同，只是寫法不同

## 題目三作答

C
