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

    internal class Program
    {
        static void Main(string[] args)
        {
			LoginProcess.Login("Jake", true);
			LoginProcess.Login("Kelly", false);

			LoginProcess process = new LoginProcess();
			process.LoginByUser("Louis", true);
		}
	}
}
