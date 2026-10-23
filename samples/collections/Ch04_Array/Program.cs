using System.Collections;
using System.Runtime.CompilerServices;
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
			ShowJaggedArray();
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

		static public void SortAndShowBlackPinkMember()
		{
			Member[] blackPinkMembers =
			[
				new() { Name = "Jisoo", Age = 31 },
				new() { Name = "Jennie", Age = 30 },
				new() { Name = "Rosé", Age = 29 },
				new() { Name = "Lisa", Age = 28 }
			];

			// 遞減排序(使用匿名方法來比較)
			Array.Sort(blackPinkMembers, (x, y) => 
			{
				// 當前方的 x 比後方的 y 還大，就回傳 -1，讓 x 排到前面
				if (x.Age > y.Age) return -1;
				// 當前方的 x 比後方的 y 還小，就回傳 1，讓 x 排到後面
				else if (x.Age < y.Age) return 1;
				// 如果一樣大，回傳 0，維持原本的相對順序
				return 0;
			});
			Console.WriteLine("== BLACKPINK 成員(遞減排序) ==\n");
			Console.WriteLine("姓名\t年齡");
			Console.WriteLine("==========");
			foreach (var m in blackPinkMembers)
			{
				Console.WriteLine($"{m.Name}\t{m.Age}");
			}

			Console.WriteLine("");

			// 遞增排序
			// 使用int.CompareTo()可以縮減實作行數
			Array.Sort(blackPinkMembers, (x, y) => x.Age.CompareTo(y.Age));
			Console.WriteLine("== BLACKPINK 成員(遞增排序) ==\n");
			Console.WriteLine("姓名\t年齡");
			Console.WriteLine("==========");
			foreach (var m in blackPinkMembers)
			{
				Console.WriteLine($"{m.Name}\t{m.Age}");
			}

			Console.WriteLine("");

			// 自訂排序
			Array.Sort(blackPinkMembers, CompareBlackPinkMember);
			Console.WriteLine("== BLACKPINK 成員(自訂排序) ==\n");
			Console.WriteLine("姓名\t年齡");
			Console.WriteLine("==========");
			foreach (var m in blackPinkMembers)
			{
				Console.WriteLine($"{m.Name}\t{m.Age}");
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

		static public void ShowJaggedArray() 
		{ 
			// 建立不規則陣列
			double[][] amt = new double[3][];
			amt[0] = new double[] { 1100, 2200, 3300 };
			amt[1] = new double[] { 1500, 2500 };
			amt[2] = new double[] { 1000, 2000, 3000, 4000 };
			string[] companyNames = new string[] { "台北", "台中", "高雄" };
			string[] parts = new string[] { "第一處", "第二處", "第三處", "第四處" };
			double[] sums = new double[] { 0.0, 0.0, 0.0 };
			double total = 0;

			Console.WriteLine($"\t{parts[0]}\t{parts[1]}\t{parts[2]}\t{parts[3]}   (單位：千元)");
			for (int i = 0; i < amt.Length; i++)
			{
				Console.Write($"{companyNames[i]}");
				for (int j = 0; j < amt[i].Length; j++)
				{
					Console.Write($"\t{amt[i][j]}");
					sums[i] += amt[i][j];
				}
				total += sums[i] * 1000;

				Console.WriteLine();
			}

			Console.WriteLine();

			for (int n = 0; n < sums.Length; n++)
			{
				sums[n] *= 1000;
				Console.WriteLine($"{companyNames[n]}分公司營業額：{sums[n]:c}\t營業率：{sums[n]/total:p}");
			}

			Console.WriteLine($"總營業額：{total:c}元");
		}

		static private int CompareBlackPinkMember(Member x, Member y) 
		{
			int result = 0;

			// 1.排序名字
			if (string.IsNullOrEmpty(x.Name) && string.IsNullOrEmpty(y.Name))
				result = 0;
			else if (string.IsNullOrEmpty(x.Name))
				result = -1;
			else if (string.IsNullOrEmpty(y.Name))
				result = 1;
			else
			{
				// 使用字串內建的比較，可以按字母排序(看字元編碼的數字大小)
				//result = x.Name.CompareTo(y.Name);
				// 使用字串比較，自選規則: 忽略大小寫
				result = string.Compare(x.Name, y.Name, StringComparison.CurrentCultureIgnoreCase);
				// 名字長度比較
				//result = x.Name.Length.CompareTo(y.Name.Length);
			}

			if (result != 0) return result;

			// 2.若名稱相同，排序年齡
			result = x.Age.CompareTo(y.Age);

			return result;
		}
	}
}
