namespace YieldTutorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 共用入口：後續單元在此加入選項，各單元的程式放在自己的檔案中。
            string lesson = args.Length == 0 ? "01" : args[0];

            switch (lesson)
            {
                case "01":
                    ListBasics.Run();
                    break;
                case "01-answer":
                    ListBasics.RunExerciseAnswer();
                    break;
                case "02":
                    EnumerableContract.Run();
                    break;
                case "02-answer":
                    EnumerableContract.RunExerciseAnswer();
                    break;
                case "03":
                    EnumeratorProtocol.Run();
                    break;
                case "03-answer":
                    EnumeratorProtocol.RunExerciseAnswer();
                    break;
                default:
                    Console.Error.WriteLine("可用選項：01、01-answer、02、02-answer、03、03-answer");
                    Environment.ExitCode = 1;
                    break;
            }
        }
    }
}
