namespace Ch02_Types
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int score = 100;        // 宣告 score 為整數變數，代表成績初值為 100
            double price = 90.5;    // 宣告 price 為浮點數變數，代表單價初值為 90.5
            char sex = 'M';         // 宣告 sex 為字元變數，代表性別初值為 M
            char c1 = '\x0061';     // 宣告 c1 為字元變數，以 a 字元的 ASCII 碼表示 61(16進位)
			char c2 = (char)97;     // 宣告 c2 為字元變數，以 a 字元的 ASCII 碼表示 97(10進位)
			char c3 = '\u0061';     // 宣告 c3 為字元變數，以 a 字元的 UniCode 表示(16進位)
            string bookName = "Visual C# 程式設計經典";   // 宣告 bookName 為字串變數，，代表書名初值為 "Visual C# 程式設計經典"

			Console.WriteLine($"score = {score}, type = {score.GetType()}"); // GetType(): 取得某個變數的資料型別
			Console.WriteLine($"price = {price}, type = {price.GetType()}");
            Console.WriteLine($"sex = {sex}, type = {sex.GetType()}");
            Console.WriteLine($"c1 = {c1}, type = {c1.GetType()}");
            Console.WriteLine($"c2 = {c2}, type = {c2.GetType()}");
            Console.WriteLine($"c3 = {c3}, type = {c3.GetType()}");
			Console.WriteLine($"bookName = {bookName}, type = {bookName.GetType()}");
		}
    }
}
