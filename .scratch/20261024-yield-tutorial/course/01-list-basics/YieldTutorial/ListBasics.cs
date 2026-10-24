namespace YieldTutorial;

internal static class ListBasics
{
    public static void Run()
    {
        List<int> numbers = new List<int> { 10, 20, 30 };

        Console.WriteLine("新增前：");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        numbers.Add(40);

        Console.WriteLine("索引 0：");
        Console.WriteLine(numbers[0]);
        Console.WriteLine("索引 3：");
        Console.WriteLine(numbers[3]);

        Console.WriteLine("新增後：");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }

    public static void RunExerciseAnswer()
    {
        List<int> numbers = new List<int> { 10, 20, 30 };
        numbers.Add(40);

        // 在走訪前修改第二個元素，再於尾端加入一個整數。
        numbers[1] = 25;
        numbers.Add(50);

        Console.WriteLine("修改後的索引 1：");
        Console.WriteLine(numbers[1]);
        Console.WriteLine("修改後的完整清單：");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
