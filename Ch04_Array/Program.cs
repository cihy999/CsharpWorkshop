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
	}
}
