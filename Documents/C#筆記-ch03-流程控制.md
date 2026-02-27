# C#筆記 - ch03 - 流程控制

[Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch02 資料型別與主控台應用程式](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ) 的學習筆記。

一個程式由三種敘述組合而成：

- 循序結構：一般程式就是從上而下、一行一行執行。
- 選擇結構：改變程式流程，例如：`if...else...`
- 重複結構：重複執行多次，例如：`for`

## if...else

程式碼範例：

```csharp
static void Main(string[] args)
{
    int n1, n2, n3;
    int max;

    Console.Write("1. 請輸入第1個數值：");
    if (!int.TryParse(Console.ReadLine(), out n1))
    {
        Console.WriteLine("輸入錯誤！");
        return;
    }

    Console.Write("2. 請輸入第2個數值：");
    if (!int.TryParse(Console.ReadLine(), out n2))
    {
        Console.WriteLine("輸入錯誤！");
        return;
    }

    Console.Write("3. 請輸入第3個數值：");
    if (!int.TryParse(Console.ReadLine(), out n3))
    {
        Console.WriteLine("輸入錯誤！");
        return;
    }

    if (n1 > n2)
    {
        if (n1 > n3)
            max = n1;
        else
            max = n3;
    }
    else 
    {
        if (n2 > n3)
            max = n2;
        else
            max = n3;
    }

    Console.WriteLine($"\n=== {n1}, {n2}, {n3} 中最大的數值：{max}");
}
```

## if...else if...else

程式碼範例：

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        DoIfElseIf();
    }

    static void DoIfElseIf() 
    {
        Console.WriteLine("試問 Visual Studio 可以開發下列哪種應用程式？\n1.視窗程式\t2.Web程式\t3.裝置應用程式\t4.以上皆是");
        Console.Write("請輸入：");

        int option = 0;
        if (!int.TryParse(Console.ReadLine(), out option))
        {
            Console.WriteLine("\n輸入錯誤！");
            return;
        }

        if (option == 1 || option == 2 || option == 3)
            Console.WriteLine("答錯了！QQ");
        else if (option == 4)
            Console.WriteLine("答對了！真棒");
        else
            Console.WriteLine("蛤？");
    }
}
```

## switch

程式碼範例：

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("試問 Visual Studio 可以開發下列哪種應用程式？\n1.視窗程式\t2.Web程式\t3.裝置應用程式\t4.以上皆是");
        Console.Write("請輸入：");

        int option = 0;
        if (!int.TryParse(Console.ReadLine(), out option))
        {
            Console.WriteLine("\n輸入錯誤！");
            return;
        }

        switch (option) 
        { 
            case 1:
            case 2:
            case 3:
                Console.WriteLine("答錯了！QQ");
                break;
            case 4:
                Console.WriteLine("答對了！真棒");
                break;
            default:
                Console.WriteLine("蛤？");
                break;
        }
    }
}
```

## 三元運算子

程式碼範例：

```csharp
static void DoTenaryOperator()
{
    int n1, n2, n3;
    int max;

    Console.Write("1. 請輸入第1個數值：");
    if (!int.TryParse(Console.ReadLine(), out n1))
    {
        Console.WriteLine("\n輸入錯誤！");
        return;
    }

    Console.Write("2. 請輸入第2個數值：");
    if (!int.TryParse(Console.ReadLine(), out n2))
    {
        Console.WriteLine("\n輸入錯誤！");
        return;
    }

    Console.Write("3. 請輸入第3個數值：");
    if (!int.TryParse(Console.ReadLine(), out n3))
    {
        Console.WriteLine("\n輸入錯誤！");
        return;
    }

    max = n1 > n2 ? (n1 > n3 ? n2 : n3) : (n2 > n3 ? n2 : n3);

    Console.WriteLine($"\n=== {n1}, {n2}, {n3} 中最大的數值：{max}");
}
```

## for 迴圈

程式碼範例：

```csharp
static public void VeriftyPassword()
{
    string? pwd = "";
    int count = 0;

    for (count = 1; count <= 3; count++)
    {
        Console.Write("\n >>>> 請輸入密碼(四個字元)：");
        pwd = Console.ReadLine();

        if (!string.IsNullOrEmpty(pwd) && pwd == "best")
            break;
        else
            Console.WriteLine($"\n Sorry! 密碼錯誤{count}次，請重新輸入");

        Console.WriteLine();
    }

    if (pwd == "best")
        Console.WriteLine("\n ==== 登入成功！");
    else
        Console.WriteLine("\n **** 登入失敗！");
}
```

## for 蜂巢迴圈

程式碼範例：

```csharp
static public void DoNextFor() 
{
    for (int i = 1; i <= 5; i++)
    {
        for (int k = 1; k <= i; k++)
        {
            Console.Write($"\t{k}");
        }

        Console.WriteLine();
    }
}
```

## while 迴圈

**前測試**程式碼範例：

```csharp
static public void DoPretestLoop() 
{
    int factor = 1, testnum = 0, count = 0;

    Console.Write("請輸入欲求因數的數值(1-50)：");

    if (!int.TryParse(Console.ReadLine(), out factor))
    {
        Console.WriteLine("\n 輸入錯誤！請輸入 1-50 的數字");
        return;
    }

    if (factor < 1 || factor > 50)
    {
        Console.WriteLine("\n 數字超出範圍！輸入 1-50 的數字");
        return;
    }

    Console.WriteLine($"\n == 求 1-100 能被{factor}整除的因數 ==\n");

    while (testnum <= 100)
    {
        testnum += factor;

        // 超出範圍就結束
        if (testnum > 100)
        {
            // 最後沒滿 5 個數字，就強制換行
            if (count % 5 > 0) Console.WriteLine();
            break;
        }

        Console.Write($"\t{testnum}");

        count++;

        // 每 5 個數字就換行
        if (count % 5 == 0) Console.WriteLine();
    }

    Console.WriteLine($"\n == 由 1-100 能被{factor}整除的因數共{count} ==\n");
}
```

**後測試**程式碼範例：

```csharp
static public void DoPosttestLoop()
{
    int factor = 1, testnum = 0, count = 0;

    Console.Write("請輸入欲求因數的數值(1-50)：");

    if (!int.TryParse(Console.ReadLine(), out factor))
    {
        Console.WriteLine("\n 輸入錯誤！請輸入 1-50 的數字");
        return;
    }

    if (factor < 1 || factor > 50)
    {
        Console.WriteLine("\n 數字超出範圍！輸入 1-50 的數字");
        return;
    }

    Console.WriteLine($"\n == 求 1-100 能被{factor}整除的因數 ==\n");

    do
    {
        testnum += factor;
        count++;

        Console.Write($"\t{testnum}");

        // 每 5 個數字就換行
        if (count % 5 == 0) Console.WriteLine();
    } while (100 - testnum >= factor);

    // 最後沒滿 5 個數字，就強制換行
    if (count % 5 > 0) Console.WriteLine();

    Console.WriteLine($"\n == 由 1-100 能被{factor}整除的因數共{count} ==\n");
}
```

## break & continue

程式碼範例：

```csharp
static public void DoBreakContinue() 
{
    int sum = 0, upper = 0, n = 0;

    Console.Write("\n 請輸入臨界值：");

    if (!int.TryParse(Console.ReadLine(), out upper))
    {
        Console.WriteLine("\n 輸入錯誤！請輸入數字");
        return;
    }

    Console.WriteLine($"\n 求 1 + ... + n <= {upper}(臨界值)");

    Console.WriteLine("\n ===== ");
    do
    {
        n++;

        if (sum + n <= upper)
        {
            sum += n;

            Console.Write($" {n}");
            if (n > 0 && upper - sum > n) Console.Write(" +");

            continue;
        }
        else 
        {
            break;
        }
    }
    while (sum < upper);
    Console.Write($" = {sum}");
    Console.WriteLine("\n ===== ");
}
```

```csharp
static public void GuessNumber() 
{
    int inputNum = 0;	// 紀錄使用者輸入的數字
    int randomNum = 0;  // 紀錄這次隨機數字
    int count = 0, min = 1, max = 100;

    Random r = new Random();
    randomNum = r.Next(min, max + 1);

    Console.WriteLine(" ===== 猜數字 ===== \n");

    while (true)
    {
        Console.WriteLine($" 數字範圍：{min} - {max}");
        Console.Write(" 猜猜看：");

        if (int.TryParse(Console.ReadLine(), out inputNum))
        {
            count++;

            if (inputNum < min || inputNum > max)
            {
                Console.WriteLine($"\n 請猜介於 {min} - {max} 的數字 \n");
                continue;
            }

            if (inputNum == randomNum)
            {
                Console.WriteLine($"\n 答對了！總共猜 {count} 次 \n");
                break;
            }
            else if (inputNum > randomNum)
            {
                max = inputNum;
                Console.WriteLine($"\n 再小一點！\n");
            }
            else if (inputNum < randomNum)
            {
                min = inputNum;
                Console.WriteLine($"\n 再大一點！\n");
            }
        }
        else 
        {
            Console.WriteLine("\n 請輸入數字！ \n");
        }
    }
}
```
