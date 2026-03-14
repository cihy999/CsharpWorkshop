namespace Ch04_Method
{
	internal class LoginProcess 
	{
		public void LoginByUser(string userName, bool isMale)
		{
			Console.WriteLine(GetWelcome(userName, isMale));
		}

		public string GetWelcome(string userName, bool isMale)
		{
			string gender = isMale ? "先生" : "小姐";
			return $"{userName} {gender}，歡迎光臨！";
		}

		public static void Login(string userName, bool isMale) 
		{
			LoginProcess process = new LoginProcess();
			process.LoginByUser(userName, isMale);
		}
	}

	internal class SampleMethod 
	{
		public static void ShowCallValue() 
		{
			Console.WriteLine("===== Call by Value 傳值呼叫 =====");

			int a = 50, b = 100;
			Console.WriteLine($"1. 呼叫敘述，未進入方法前\t\t: a={a}, b={b}");

			CallValue(a, b);

			Console.WriteLine($"4. 呼叫敘述，離開方法回到原處時\t\t: a={a}, b={b}");
		}

		public static void ShowCallReference()
		{
			Console.WriteLine("===== Call by Reference 參考呼叫 =====");

			int a = 50, b = 100;
			Console.WriteLine($"1. 呼叫敘述，未進入方法前\t\t: a={a}, b={b}");

			CallReference(ref a, ref b);

			Console.WriteLine($"4. 呼叫敘述，離開方法回到原處時\t\t: a={a}, b={b}");
		}

		public static void ShowOuputParameter()
		{
			Console.WriteLine("===== Call Out 傳出參數 =====");

			Console.WriteLine($"1. 呼叫敘述，未進入方法前，未初始化 a、b");

			CallOut(out int a, out int b);

			Console.WriteLine($"4. 呼叫敘述，離開方法回到原處時\t\t: a={a}, b={b}");
		}

		public static void ShowMaxNumber() 
		{
            var nums = new int[10];
            Random r = new Random();
            string numStrings = "";
            for (int i = 0; i < nums.Length; i++)
            {
	            nums[i] = r.Next(101);
	            numStrings = numStrings + nums[i] + ", ";
            }

            int max = GetMax(nums);

            Console.WriteLine($"number: {numStrings}");
            Console.WriteLine($"Max number: {max}");
		}

        public static int Sum(int a, int b)
        { 
            return a + b;
        }

        public static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }

        public static string Sum(string a, string b)
        {
            return a + b;
        }

		private static void CallValue(int x, int y)
		{
			x = 33;
			y = 66;
			Console.WriteLine($"2. 方法內、交換前\t\t\t: x={x}, y={y}");

			int temp = x;
			x = y;
			y = temp;
			Console.WriteLine($"3. 方法內、交換後\t\t\t: x={x}, y={y}");
		}

		private static void CallReference(ref int x, ref int y)
		{
			x = 33;
			y = 66;
			Console.WriteLine($"2. 方法內、交換前\t\t\t: x={x}, y={y}");

			int temp = x;
			x = y;
			y = temp;
			Console.WriteLine($"3. 方法內、交換後\t\t\t: x={x}, y={y}");
		}

		private static void CallOut(out int x, out int y)
		{
			x = 33;
			y = 66;
			Console.WriteLine($"2. 方法內、交換前\t\t\t: x={x}, y={y}");

			int temp = x;
			x = y;
			y = temp;
			Console.WriteLine($"3. 方法內、交換後\t\t\t: x={x}, y={y}");
		}

        /// <summary>
        /// 取得最大值
        /// </summary>
        /// <param name="numbers">陣列可以省略ref</param>
        /// <returns></returns>
		private static int GetMax(int[] numbers)
		{
			int max = 0;

			for (int i = 0; i < numbers.Length; i++)
			{
				if (max < numbers[i])
					max = numbers[i];
			}

			return max;
		}
	}

    internal class Program
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args">陣列都是使用 Call by Reference，但可以省略 ref</param>
        static void Main(string[] args)
        {
            Console.WriteLine($"Sum(1, 10) = {SampleMethod.Sum(1, 10)}");
            Console.WriteLine($"Sum(11, 10, 12) = {SampleMethod.Sum(11, 10, 12)}");
            Console.WriteLine($"Sum(Fomula, 1) = {SampleMethod.Sum("Fomula", " 1")}");
        }
	}
}
