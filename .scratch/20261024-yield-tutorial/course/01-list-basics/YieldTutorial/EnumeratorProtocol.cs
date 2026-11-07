namespace YieldTutorial;

internal static class EnumeratorProtocol
{
    public static void Run()
    {
        IEnumerable<int> numbers = new List<int> { 10, 20, 30 };

        Console.WriteLine("foreach：");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("手動列舉：");
        using (IEnumerator<int> enumerator = numbers.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }

        Console.WriteLine("逐次呼叫 MoveNext：");
        using (IEnumerator<int> enumerator = numbers.GetEnumerator())
        {
            // 只在 MoveNext 回傳 True 之後讀取 Current。
            bool moved = enumerator.MoveNext();
            Console.WriteLine("第 1 次：" + moved + "，Current = " + enumerator.Current);

            moved = enumerator.MoveNext();
            Console.WriteLine("第 2 次：" + moved + "，Current = " + enumerator.Current);

            moved = enumerator.MoveNext();
            Console.WriteLine("第 3 次：" + moved + "，Current = " + enumerator.Current);

            moved = enumerator.MoveNext();
            Console.WriteLine("第 4 次：" + moved + "，已結束，不讀 Current");

            moved = enumerator.MoveNext();
            Console.WriteLine("第 5 次：" + moved + "，仍然結束");
        }

        IEnumerable<int> empty = new List<int>();

        Console.WriteLine("空序列 foreach 開始");
        foreach (int number in empty)
        {
            Console.WriteLine(number);
        }
        Console.WriteLine("空序列 foreach 結束");

        using (IEnumerator<int> enumerator = empty.GetEnumerator())
        {
            Console.WriteLine("空序列第 1 次 MoveNext：" + enumerator.MoveNext());
        }
    }

    public static void RunExerciseAnswer()
    {
        IEnumerable<int> numbers = new List<int> { 2, 4, 6, 8 };
        int position = 0;

        using (IEnumerator<int> enumerator = numbers.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                position++;
                Console.WriteLine("第 " + position + " 個：" + enumerator.Current);
            }
        }

        Console.WriteLine("共 " + position + " 個元素，MoveNext 呼叫 " + (position + 1) + " 次");
    }
}
