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
