namespace Ch02_String
{
    internal class Program
    {
        static void Main(string[] args)
        {
			ConcatenateString();
			FormatString();
			InterpolateString();
		}

		/// <summary>
		/// 字串合併
		/// </summary>
		static void ConcatenateString() 
        {
			string str1 = "Antigravity";
			string str2 = str1 + "是一個很棒的助理！" + 100 + "分";
			Console.WriteLine(str2); // Antigravity是一個很棒的助理！100分
		}

		/// <summary>
		/// 字串格式化
		/// </summary>
		static void FormatString()
		{
			Console.WriteLine(string.Format("{0}是一個很棒的助理！{1}分", "Antigravity", 100));
		}

		/// <summary>
		/// 字串插值
		/// </summary>
		static void InterpolateString() 
		{
			string name = "Antigravity";
			int score = 100;
			Console.WriteLine($"{name}是一個很棒的助理！{score}分");
		}
	}
}
