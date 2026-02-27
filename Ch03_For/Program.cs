namespace Ch03_For
{
    internal class Program
    {
		static void Main(string[] args)
		{
			DoNextFor();
		}

		static public void VeriftyPassword()
		{
			string? pwd = "";
			int count = 0;

			for (count = 1; count <= 3; count++)
			{
				Console.Write("\n >>>> 請輸入密碼(四個字元)：");
				pwd = Console.ReadLine();

				if (!string.IsNullOrEmpty(pwd) && pwd == "best")
					break;
				else
					Console.WriteLine($"\n Sorry! 密碼錯誤{count}次，請重新輸入");

				Console.WriteLine();
			}

			if (pwd == "best")
				Console.WriteLine("\n ==== 登入成功！");
			else
				Console.WriteLine("\n **** 登入失敗！");
		}

		static public void DoNextFor()
		{
			for (int i = 1; i <= 5; i++)
			{
				for (int k = 1; k <= i; k++)
				{
					Console.Write($"\t{k}");
				}

				Console.WriteLine();
			}
		}
	}
}
