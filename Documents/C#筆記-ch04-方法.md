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

## 補充 - 靜態(static)

在類別中宣告，其生命週期是程式執行開始到結束。靜態變數會被放到全域變數區，因此類別不需要建立物件就能使用，且物件都共用同一份靜態變數。

宣告C# 靜態變數會根據型別給初始值。

靜態方法(static method)：不需要建立物件就能使用，且物件都共用同一份靜態方法。

## 專有名詞

- **實引數(Actual Arguments)**：若方法A的某個敘述呼叫方法B，則方法A的參數稱為實引數。
- **虛引數(Dummy Arguments)**：若方法A的某個敘述呼叫方法B，則方法B的參數稱為虛引數。
- **傳值呼叫(Call by Value)**：將實引數的值複製到虛引數中。
- **參考呼叫(Call by Reference)**
- **傳出參數(Output Parameters)**
- 方法**主體(Body)**：方法實作的內容。
