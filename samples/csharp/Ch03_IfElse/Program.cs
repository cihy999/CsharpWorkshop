namespace Ch03_IfElse
{
    internal class Program
    {
        static void Main(string[] args)
        {
			DoTenaryOperator();
		}

        static void DoIfElse() 
        {
			int n1, n2, n3;
			int max;

			Console.Write("1. 請輸入第1個數值：");
			if (!int.TryParse(Console.ReadLine(), out n1))
			{
				Console.WriteLine("\n輸入錯誤！");
				return;
			}

			Console.Write("2. 請輸入第2個數值：");
			if (!int.TryParse(Console.ReadLine(), out n2))
			{
				Console.WriteLine("\n輸入錯誤！");
				return;
			}

			Console.Write("3. 請輸入第3個數值：");
			if (!int.TryParse(Console.ReadLine(), out n3))
			{
				Console.WriteLine("\n輸入錯誤！");
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

		static void DoTenaryOperator()
		{
			int n1, n2, n3;
			int max;

			Console.Write("1. 請輸入第1個數值：");
			if (!int.TryParse(Console.ReadLine(), out n1))
			{
				Console.WriteLine("\n輸入錯誤！");
				return;
			}

			Console.Write("2. 請輸入第2個數值：");
			if (!int.TryParse(Console.ReadLine(), out n2))
			{
				Console.WriteLine("\n輸入錯誤！");
				return;
			}

			Console.Write("3. 請輸入第3個數值：");
			if (!int.TryParse(Console.ReadLine(), out n3))
			{
				Console.WriteLine("\n輸入錯誤！");
				return;
			}

			max = n1 > n2 ? (n1 > n3 ? n2 : n3) : (n2 > n3 ? n2 : n3);

			Console.WriteLine($"\n=== {n1}, {n2}, {n3} 中最大的數值：{max}");
		}
	}
}
