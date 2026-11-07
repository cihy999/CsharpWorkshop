namespace YieldTutorial;

internal static class EnumerableContract
{
    public static void Run()
    {
        List<int> list = new List<int> { 10, 20, 30 };
        IEnumerable<int> sequence = list;

        Console.WriteLine("同一個物件：" + object.ReferenceEquals(list, sequence));

        list.Add(40);

        Console.WriteLine("透過 list 列舉：");
        foreach (int number in list)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("透過 sequence 列舉：");
        foreach (int number in sequence)
        {
            Console.WriteLine(number);
        }

        IEnumerable<int> returned = GetNumbers(list);

        Console.WriteLine("方法回傳的序列：");
        foreach (int number in returned)
        {
            Console.WriteLine(number);
        }
    }

    public static void RunExerciseAnswer()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        IEnumerable<int> sequence = GetNumbers(list);

        // sequence.Add(4); 無法編譯：IEnumerable<int> 沒有提供 Add。
        list.Add(4);

        Console.WriteLine("sequence 列舉：");
        foreach (int number in sequence)
        {
            Console.WriteLine(number);
        }
    }

    private static IEnumerable<int> GetNumbers(List<int> numbers)
    {
        return numbers;
    }
}
