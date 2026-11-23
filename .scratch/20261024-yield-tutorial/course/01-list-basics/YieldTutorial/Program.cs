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
                case "04":
                    YieldSuspension.Run();
                    break;
                case "04-answer":
                    YieldSuspension.RunExerciseAnswer();
                    break;
                case "05":
                    DeferredExecution.Run();
                    break;
                case "05-answer":
                    DeferredExecution.RunExerciseAnswer();
                    break;
                case "06":
                    TerminationAndCleanup.Run();
                    break;
                case "06-answer":
                    TerminationAndCleanup.RunExerciseAnswer();
                    break;
                default:
                    Console.Error.WriteLine("可用選項：01、01-answer、02、02-answer、03、03-answer、04、04-answer、05、05-answer、06、06-answer");
                    Environment.ExitCode = 1;
                    break;
            }
        }
    }
}
