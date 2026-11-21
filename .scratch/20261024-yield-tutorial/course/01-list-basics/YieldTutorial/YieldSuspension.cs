namespace YieldTutorial;

internal static class YieldSuspension
{
    public static void Run()
    {
        Console.WriteLine("return 寫法：");
        foreach (int number in GetNumbersWithReturn())
        {
            Console.WriteLine("[消費端] 取得 " + number);
        }

        Console.WriteLine("yield return 寫法：");
        foreach (int number in GetNumbersWithYield())
        {
            Console.WriteLine("[消費端] 取得 " + number);
        }

        Console.WriteLine("逐次要求 CountUp 的元素：");
        using (IEnumerator<int> enumerator = CountUp().GetEnumerator())
        {
            Console.WriteLine("[消費端] 要求第 1 個");
            bool moved = enumerator.MoveNext();
            Console.WriteLine("[消費端] MoveNext = " + moved + "，Current = " + enumerator.Current);
            Console.WriteLine("[消費端] 先處理別的事，還沒要求下一個");

            Console.WriteLine("[消費端] 要求第 2 個");
            moved = enumerator.MoveNext();
            Console.WriteLine("[消費端] MoveNext = " + moved + "，Current = " + enumerator.Current);

            Console.WriteLine("[消費端] 要求第 3 個");
            moved = enumerator.MoveNext();
            Console.WriteLine("[消費端] MoveNext = " + moved + "，Current = " + enumerator.Current);

            Console.WriteLine("[消費端] 要求第 4 個");
            moved = enumerator.MoveNext();
            Console.WriteLine("[消費端] MoveNext = " + moved + "，序列結束，不讀 Current");
        }
    }

    public static void RunExerciseAnswer()
    {
        int received = 0;

        foreach (int number in CountUpModified())
        {
            received++;
            Console.WriteLine("[消費端] 取得 " + number);
        }

        Console.WriteLine("[消費端] 共取得 " + received + " 個值");
    }

    private static IEnumerable<int> GetNumbersWithReturn()
    {
        List<int> numbers = new List<int>();
        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);
        Console.WriteLine("[產生端] 三個值都放進 List，用 return 一次交出");
        return numbers;
    }

    private static IEnumerable<int> GetNumbersWithYield()
    {
        Console.WriteLine("[產生端] yield return 10");
        yield return 10;
        Console.WriteLine("[產生端] yield return 20");
        yield return 20;
        Console.WriteLine("[產生端] yield return 30");
        yield return 30;
        Console.WriteLine("[產生端] 走到方法結尾");
    }

    private static IEnumerable<int> CountUp()
    {
        int count = 0;
        Console.WriteLine("[產生端] 從方法開頭執行，count = " + count);

        count += 10;
        Console.WriteLine("[產生端] 位置 1：產出 count = " + count);
        yield return count; // 位置 1

        Console.WriteLine("[產生端] 從位置 1 之後繼續，count = " + count);
        count += 10;
        Console.WriteLine("[產生端] 位置 2：產出 count = " + count);
        yield return count; // 位置 2

        Console.WriteLine("[產生端] 從位置 2 之後繼續，count = " + count);
        count += 10;
        Console.WriteLine("[產生端] 位置 3：產出 count = " + count);
        yield return count; // 位置 3

        Console.WriteLine("[產生端] 從位置 3 之後繼續，走到方法結尾");
    }

    private static IEnumerable<int> CountUpModified()
    {
        int count = 1;
        Console.WriteLine("[產生端] 從方法開頭執行，count = " + count);

        count *= 2;
        Console.WriteLine("[產生端] 位置 1：產出 count = " + count);
        yield return count; // 位置 1

        Console.WriteLine("[產生端] 從位置 1 之後繼續，count = " + count);
        Console.WriteLine("[產生端] 新增的位置：產出 count + 100 = " + (count + 100));
        yield return count + 100; // 新增的位置

        Console.WriteLine("[產生端] 從新增的位置之後繼續，count = " + count);
        count *= 2;
        Console.WriteLine("[產生端] 位置 2：產出 count = " + count);
        yield return count; // 位置 2

        Console.WriteLine("[產生端] 從位置 2 之後繼續，count = " + count);
        count *= 2;
        Console.WriteLine("[產生端] 位置 3：產出 count = " + count);
        yield return count; // 位置 3

        Console.WriteLine("[產生端] 從位置 3 之後繼續，走到方法結尾");
    }
}
