namespace YieldTutorial;

internal static class DeferredExecution
{
    public static void Run()
    {
        Console.WriteLine("情境 1：取得列舉器，但不要求元素");
        Console.WriteLine("[消費端] 呼叫 Steps() 之前");
        IEnumerable<int> first = Steps();
        Console.WriteLine("[消費端] 呼叫 Steps() 之後");
        RequestElements(first, 0);

        Console.WriteLine("情境 2：只要求 2 個元素");
        Console.WriteLine("[消費端] 呼叫 Steps() 之前");
        IEnumerable<int> second = Steps();
        Console.WriteLine("[消費端] 呼叫 Steps() 之後");
        RequestElements(second, 2);

        Console.WriteLine("情境 3：要求到序列結束");
        Console.WriteLine("[消費端] 呼叫 Steps() 之前");
        IEnumerable<int> third = Steps();
        Console.WriteLine("[消費端] 呼叫 Steps() 之後");
        RequestElements(third, 4);

        Console.WriteLine("對照：同樣回傳 IEnumerable<int>，但不是迭代器");
        Console.WriteLine("[消費端] 呼叫 StepsWithList() 之前");
        IEnumerable<int> withList = StepsWithList();
        Console.WriteLine("[消費端] 呼叫 StepsWithList() 之後，尚未列舉");
    }

    public static void RunExerciseAnswer()
    {
        Console.WriteLine("情境 1：取得列舉器，但不要求元素");
        Console.WriteLine("[消費端] 呼叫 StepsMoved() 之前");
        IEnumerable<int> first = StepsMoved();
        Console.WriteLine("[消費端] 呼叫 StepsMoved() 之後");
        RequestElements(first, 0);

        Console.WriteLine("情境 2：只要求 2 個元素");
        Console.WriteLine("[消費端] 呼叫 StepsMoved() 之前");
        IEnumerable<int> second = StepsMoved();
        Console.WriteLine("[消費端] 呼叫 StepsMoved() 之後");
        RequestElements(second, 2);

        Console.WriteLine("情境 3：要求到序列結束");
        Console.WriteLine("[消費端] 呼叫 StepsMoved() 之前");
        IEnumerable<int> third = StepsMoved();
        Console.WriteLine("[消費端] 呼叫 StepsMoved() 之後");
        RequestElements(third, 4);
    }

    private static void RequestElements(IEnumerable<int> sequence, int requestCount)
    {
        Console.WriteLine("[消費端] GetEnumerator() 之前");
        using (IEnumerator<int> enumerator = sequence.GetEnumerator())
        {
            Console.WriteLine("[消費端] GetEnumerator() 之後");

            for (int i = 1; i <= requestCount; i++)
            {
                Console.WriteLine("[消費端] 第 " + i + " 次 MoveNext() 之前");
                bool moved = enumerator.MoveNext();
                if (moved)
                {
                    Console.WriteLine("[消費端] 第 " + i + " 次 MoveNext() 之後：True，Current = " + enumerator.Current);
                }
                else
                {
                    Console.WriteLine("[消費端] 第 " + i + " 次 MoveNext() 之後：False，序列結束");
                }
            }

            Console.WriteLine("[消費端] 不再要求元素");
        }
    }

    private static IEnumerable<int> Steps()
    {
        Console.WriteLine("[產生端] 進入方法主體");
        Console.WriteLine("[產生端] 產出 1");
        yield return 1;

        Console.WriteLine("[產生端] 從產出 1 之後繼續，產出 2");
        yield return 2;

        Console.WriteLine("[產生端] 從產出 2 之後繼續，產出 3");
        yield return 3;

        Console.WriteLine("[產生端] 從產出 3 之後繼續，走到方法結尾");
    }

    private static IEnumerable<int> StepsMoved()
    {
        Console.WriteLine("[產生端] 產出 1");
        yield return 1;

        Console.WriteLine("[產生端] 從產出 1 之後繼續，產出 2");
        yield return 2;

        Console.WriteLine("[產生端] 移動過來的標記");
        Console.WriteLine("[產生端] 從產出 2 之後繼續，產出 3");
        yield return 3;

        Console.WriteLine("[產生端] 從產出 3 之後繼續，走到方法結尾");
    }

    private static IEnumerable<int> StepsWithList()
    {
        Console.WriteLine("[產生端] 進入 StepsWithList() 主體，建立 List");
        List<int> numbers = new List<int> { 1, 2, 3 };
        Console.WriteLine("[產生端] 用 return 交出 List");
        return numbers;
    }
}
