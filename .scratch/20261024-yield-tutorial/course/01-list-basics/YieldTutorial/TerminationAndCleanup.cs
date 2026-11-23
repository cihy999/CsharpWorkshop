namespace YieldTutorial;

internal static class TerminationAndCleanup
{
    public static void Run()
    {
        Console.WriteLine("路徑 1：正常走到方法結尾");
        foreach (int number in ReadNumbers(0))
        {
            Console.WriteLine("[消費端] 取得 " + number);
        }
        Console.WriteLine("[消費端] foreach 結束");

        Console.WriteLine("路徑 2：產生端 yield break 提早結束");
        foreach (int number in ReadNumbers(2))
        {
            Console.WriteLine("[消費端] 取得 " + number);
        }
        Console.WriteLine("[消費端] foreach 結束");

        Console.WriteLine("路徑 3：消費端取得 1 個元素後 break");
        foreach (int number in ReadNumbers(0))
        {
            Console.WriteLine("[消費端] 取得 " + number);
            if (number == 1)
            {
                Console.WriteLine("[消費端] 執行 break，離開 foreach");
                break;
            }
        }
        Console.WriteLine("[消費端] foreach 結束");

        Console.WriteLine("對照 1：手動列舉取得 1 個元素後離開 using");
        using (IEnumerator<int> enumerator = ReadNumbers(0).GetEnumerator())
        {
            if (enumerator.MoveNext())
            {
                Console.WriteLine("[消費端] 手動取得 " + enumerator.Current);
            }
            Console.WriteLine("[消費端] 準備離開 using");
        }
        Console.WriteLine("[消費端] 已離開 using");

        Console.WriteLine("對照 2：取得列舉器但從未 MoveNext 就離開 using");
        using (IEnumerator<int> enumerator = ReadNumbers(0).GetEnumerator())
        {
            Console.WriteLine("[消費端] 已取得列舉器，但不呼叫 MoveNext");
        }
        Console.WriteLine("[消費端] 已離開 using");
    }

    public static void RunExerciseAnswer()
    {
        Console.WriteLine("練習 A：stopAt = 3，消費端在 4 才 break");
        ConsumeWithBreak(3, 4);

        Console.WriteLine("練習 B：stopAt = 3，消費端在 2 就 break");
        ConsumeWithBreak(3, 2);
    }

    private static void ConsumeWithBreak(int stopAt, int consumerBreakAt)
    {
        foreach (int number in ReadNumbers(stopAt))
        {
            Console.WriteLine("[消費端] 取得 " + number);
            if (number == consumerBreakAt)
            {
                Console.WriteLine("[消費端] 達到消費端上限 " + consumerBreakAt + "，執行 break");
                break;
            }
        }
        Console.WriteLine("[消費端] foreach 結束");
    }

    private static IEnumerable<int> ReadNumbers(int stopAt)
    {
        try
        {
            Console.WriteLine("[產生端] 開啟資源（進入 try）");

            for (int n = 1; n <= 3; n++)
            {
                if (n == stopAt)
                {
                    Console.WriteLine("[產生端] 遇到 n = " + n + "，執行 yield break");
                    yield break;
                }

                Console.WriteLine("[產生端] 產出 " + n);
                yield return n;
                Console.WriteLine("[產生端] 從產出 " + n + " 之後繼續");
            }

            Console.WriteLine("[產生端] 迴圈正常走完");
        }
        finally
        {
            Console.WriteLine("[產生端] finally：釋放資源");
        }

        Console.WriteLine("[產生端] 走到方法結尾");
    }
}
