using System.Xml.Linq;

namespace Ch04_Array
{
    internal class Program
    {
        internal struct Member 
        {
            public string Name;
            public int Age;
        }

        static void Main(string[] args)
        {
			ShowBlackPinkMember();
		}

		static public void ShowBlackPinkMember()
		{
			Member[] blackPinkMembers =
			[
				new() { Name = "Jisoo", Age = 31 },
				new() { Name = "Jennie", Age = 30 },
				new() { Name = "Rosé", Age = 29 },
				new() { Name = "Lisa", Age = 28 }
			];

			Console.WriteLine("== BLACKPINK 成員 ==\n");
			Console.WriteLine("姓名\t年齡");
			Console.WriteLine("==========");
			for (int i = 0; i < blackPinkMembers.Length; i++)
			{
				Console.WriteLine($"{blackPinkMembers[i].Name}\t{blackPinkMembers[i].Age}");
			}
		}
		static public void AverageHeight() 
		{
			Console.Write("請輸入總人數：");

			int num = 0;
			if (!int.TryParse(Console.ReadLine(), out num))
			{
				Console.WriteLine("\n請輸入整數！");
				return;
			}

			double[] heights = new double[num];
			for (int i = 0; i < heights.Length; i++)
			{
				Console.Write($"\n請輸入第{i+1}位身高：");

				double h = 0;
				if (double.TryParse(Console.ReadLine(), out h))
				{
					heights[i] = h;
				}
			}

			double sum = 0, avg = 0;
			foreach (double h in heights) 
			{
				sum += h;
			}
			avg = sum / heights.Length;
			Console.WriteLine($"\n=== {num} 位平均身高：{avg:00.00} ===");
		}
	}
}
