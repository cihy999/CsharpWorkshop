namespace Ch03_Switch
{
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
}
