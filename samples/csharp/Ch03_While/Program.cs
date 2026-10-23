namespace Ch03_While
{
    internal class Program
    {
        static void Main(string[] args)
        {
			GuessNumber();
        }

        static public void DoPretestLoop() 
        {
            int factor = 1, testnum = 0, count = 0;

            Console.Write("請輸入欲求因數的數值(1-50)：");

            if (!int.TryParse(Console.ReadLine(), out factor))
            {
                Console.WriteLine("\n 輸入錯誤！請輸入 1-50 的數字");
                return;
            }

            if (factor < 1 || factor > 50)
            {
				Console.WriteLine("\n 數字超出範圍！輸入 1-50 的數字");
				return;
			}

            Console.WriteLine($"\n == 求 1-100 能被{factor}整除的因數 ==\n");

			while (testnum <= 100)
            {
				testnum += factor;

                // 超出範圍就結束
                if (testnum > 100)
                {
					// 最後沒滿 5 個數字，就強制換行
					if (count % 5 > 0) Console.WriteLine();
                    break;
				}

				Console.Write($"\t{testnum}");

				count++;

				// 每 5 個數字就換行
				if (count % 5 == 0) Console.WriteLine();
			}

			Console.WriteLine($"\n == 由 1-100 能被{factor}整除的因數共{count} ==\n");
		}

		static public void DoPosttestLoop()
		{
			int factor = 1, testnum = 0, count = 0;

			Console.Write("請輸入欲求因數的數值(1-50)：");

			if (!int.TryParse(Console.ReadLine(), out factor))
			{
				Console.WriteLine("\n 輸入錯誤！請輸入 1-50 的數字");
				return;
			}

			if (factor < 1 || factor > 50)
			{
				Console.WriteLine("\n 數字超出範圍！輸入 1-50 的數字");
				return;
			}

			Console.WriteLine($"\n == 求 1-100 能被{factor}整除的因數 ==\n");

			do
			{
				testnum += factor;
				count++;

				Console.Write($"\t{testnum}");

				// 每 5 個數字就換行
				if (count % 5 == 0) Console.WriteLine();
			} while (100 - testnum >= factor);

			// 最後沒滿 5 個數字，就強制換行
			if (count % 5 > 0) Console.WriteLine();

			Console.WriteLine($"\n == 由 1-100 能被{factor}整除的因數共{count} ==\n");
		}

		static public void DoBreakContinue() 
		{
			int sum = 0, upper = 0, n = 0;

			Console.Write("\n 請輸入臨界值：");

			if (!int.TryParse(Console.ReadLine(), out upper))
			{
				Console.WriteLine("\n 輸入錯誤！請輸入數字");
				return;
			}

			Console.WriteLine($"\n 求 1 + ... + n <= {upper}(臨界值)");

			Console.WriteLine("\n ===== ");
			do
			{
				n++;

				if (sum + n <= upper)
				{
					sum += n;

					Console.Write($" {n}");
					if (n > 0 && upper - sum > n) Console.Write(" +");

					continue;
				}
				else 
				{
					break;
				}
			}
			while (sum < upper);
			Console.Write($" = {sum}");
			Console.WriteLine("\n ===== ");
		}

		static public void GuessNumber() 
		{
			int inputNum = 0;	// 紀錄使用者輸入的數字
			int randomNum = 0;  // 紀錄這次隨機數字
			int count = 0, min = 1, max = 100;

			Random r = new Random();
			randomNum = r.Next(min, max + 1);

			Console.WriteLine(" ===== 猜數字 ===== \n");

			while (true)
			{
				Console.WriteLine($" 數字範圍：{min} - {max}");
				Console.Write(" 猜猜看：");

				if (int.TryParse(Console.ReadLine(), out inputNum))
				{
					count++;

					if (inputNum < min || inputNum > max)
					{
						Console.WriteLine($"\n 請猜介於 {min} - {max} 的數字 \n");
						continue;
					}

					if (inputNum == randomNum)
					{
						Console.WriteLine($"\n 答對了！總共猜 {count} 次 \n");
						break;
					}
					else if (inputNum > randomNum)
					{
						max = inputNum;
						Console.WriteLine($"\n 再小一點！\n");
					}
					else if (inputNum < randomNum)
					{
						min = inputNum;
						Console.WriteLine($"\n 再大一點！\n");
					}
				}
				else 
				{
					Console.WriteLine("\n 請輸入數字！ \n");
				}
			}
		}
	}
}
