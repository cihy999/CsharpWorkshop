namespace Ch02_ConsoleEx1
{
    internal class Program
    {
        static void Main(string[] args)
        {
			DoConsoleEscapeSequence();
		}

		/// <summary>
		/// 控制台使用範例
		/// </summary>
		static void DoConsoleExample() 
		{
			string? goods;
			int price;

			// Console.Write(): 顯示字串、不換行
			Console.Write("請輸入品名：");

			//  Console.ReadLine() 讀取一串字直到按下Enter
			goods = Console.ReadLine();

			Console.Write("請輸入價錢：");

			// 使用 Null-coalescing 運算子 (??) 提供預設值
			price = int.Parse(Console.ReadLine() ?? "0");

			// Console.Write(): 顯示字串、不換行
			Console.WriteLine($"品名：{goods}, 價錢：{price} 記錄成功！");
		}

		/// <summary>
		/// 控制台文字格式化
		/// <para>語法: {index or 插值[,alignment][:formatChar]}</para>
		/// </summary>
		static void FormatConsoleString() 
		{
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("以10進位方式顯示資料：");
			Console.WriteLine("0123456789");			// 0123456789
			Console.WriteLine("{0,0:d10}", 123456);     // 0000123456
			Console.WriteLine("{0,10:d5}", 123);        //      00123
			Console.WriteLine("{0,-10:d5}", -123);      // -00123
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("以16進位方式顯示資料：");
			Console.WriteLine("01234567890123456789");	// 01234567890123456789
			Console.WriteLine($"{255:x}");              // ff
			Console.WriteLine($"{255,10:x}");           //         ff
			Console.WriteLine($"{255,-10:x}");          // ff
			Console.WriteLine($"{1,0:x4}");             // 0001
			Console.WriteLine($"{4,0:x8}");             // 00000004
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("以浮點方式顯示資料：");
			Console.WriteLine("01234567890123456789");	// 01234567890123456789
			Console.WriteLine("{0:f}", 123.456);		// 123.46
			Console.WriteLine("{0:f3}", 123.456);		// 123.456
			Console.WriteLine("{0,20:f3}", 123.4567);	//              123.457
			Console.WriteLine("{0,-20:f3}", -123.4567);	// -123.457
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("以指數方式顯示資料：");
			Console.WriteLine("01234567890123456789");	// 01234567890123456789
			Console.WriteLine("{0:e}", 123000);			// 1.230000e+005
			Console.WriteLine("{0:e3}", 123000);		// 1.230e+005
			Console.WriteLine("{0:e3}", 0.00123);		// 1.230e-003
			Console.WriteLine("{0,20:e3}", 0.00123);	//           1.230e-003
			Console.WriteLine("{0,-20:e3}", -0.00123);	// -1.230e-003
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("以貨幣方式顯示資料：");
			Console.WriteLine("01234567890123456789");	// 01234567890123456789
			Console.WriteLine($"{5000,0:c}");			// NT$5,000.00
			Console.WriteLine($"{-5,0:c3}");			// -NT$5.000
			Console.WriteLine($"{-5,15:c3}");           //	     -NT$5.000
			Console.WriteLine($"{-5,-15:c3}");			// -NT$5.000
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("以千位符號方式顯示資料：");
			Console.WriteLine("01234567890123456789");	// 01234567890123456789
			Console.WriteLine($"{123456:n}");			// 123,456.00
			Console.WriteLine($"{123456:n1}");			// 123,456.0
			Console.WriteLine($"{123456:n0}");			// 123,456
			Console.WriteLine($"{-123456,20:n}");		//          -123,456.00
			Console.WriteLine($"{123456,-20:n}");		// 123,456.00
			Console.WriteLine("-----------------------------------------");
		}

		/// <summary>
		/// 自訂數值格式輸出字串
		/// </summary>
		static void ConsoleToString() 
		{
			// 輸出結果：1. (080) 123-4567
			double myvar1 = 0801234567;
			Console.WriteLine("1. " + myvar1.ToString("(0##) ###-####"));

			// 輸出結果：2. -12345
			int myvar2 = -12345;
			Console.WriteLine("2. " + myvar2.ToString("######"));

			// 輸出結果：3. -012345
			int myvar3 = -12345;
			Console.WriteLine("3. " + myvar3.ToString("000000"));

			// 輸出結果：4. -2.46
			double myvar4 = -2.455;
			Console.WriteLine("4. " + myvar4.ToString("#.##"));

			// 輸出結果：5. - 2.40
			double myvar5 = -2.4;
			Console.WriteLine("5. " + myvar5.ToString("0.00"));

			// 輸出結果：6. -02.46
			double myvar6 = -2.455;
			Console.WriteLine("6. " + myvar6.ToString("00.00"));

			// 輸出結果：7. 1,234,567,890
			double myvar7 = 1234567890;
			Console.WriteLine("7. " + myvar7.ToString("#,#"));

			// 輸出結果：8. 1234568
			double myvar8 = 1234567890;
			Console.WriteLine("8. " + myvar8.ToString("#,"));

			// 輸出結果：9. 1235
			double myvar9 = 1234567890;
			Console.WriteLine("9. " + myvar9.ToString("#,,"));

			// 輸出結果：10. 1
			double myvar10 = 1234567890;
			Console.WriteLine("10. " + myvar10.ToString("#,,,"));

			// 輸出結果：11. 1,235
			double myvar11 = 1234567890;
			Console.WriteLine("11. " + myvar11.ToString("#,##0,,"));

			// 輸出結果：12. 8.6%
			double myvar12 = 0.086;
			Console.WriteLine("12. " + myvar12.ToString("#0.##%"));

			// 輸出結果：13. 8.65%
			double myvar13 = 0.08647;
			Console.WriteLine("13. " + myvar13.ToString("#0.##%"));

			// 輸出結果：14. 1.68E+4
			double myvar14 = 16800;
			Console.WriteLine("14. " + myvar14.ToString("0.###E+0"));

			// 輸出結果：15. 1.68E+004
			double myvar15 = 16800;
			Console.WriteLine("15. " + myvar15.ToString("0.###E+000"));

			// 輸出結果：16. 1.68E-002
			double myvar16 = 0.0168;
			Console.WriteLine("16. " + myvar16.ToString("0.###E-000"));

			// 輸出結果：18. 1234
			// ## 用於正數；(##) 用於負數
			// 若數值為正數以正數格式顯示，反之亦然
			int myvar18 = 1234;
			Console.WriteLine("18. " + myvar18.ToString("##;(##)"));

			// 輸出結果：19. (1234)
			int myvar19 = -1234;
			Console.WriteLine("19. " + myvar19.ToString("##;(##)"));

			// 輸出結果：新台幣 2,000 元
			double num = 2000;
			Console.WriteLine("20. " + num.ToString("新台幣 #,# 元"));
			Console.WriteLine("21. " + string.Format("新台幣{0:#,#}元", num));
			Console.WriteLine("22. " + $"{num:新台幣 #,# 元}");
		}

		/// <summary>
		/// 示範控制字元(Escape Sequence)
		/// </summary>
		static void DoConsoleEscapeSequence() 
		{
			string str1 = "Everyone say: \"Hello World\"";

			// 輸出: 123456789012345678901234567890
			Console.WriteLine("123456789012345678901234567890");

			// 輸出:         Everyone say: "Hello World"     Wonderful
			Console.Write("\t" + str1);
			Console.WriteLine("\t" + "Wonderful");

			// 輸出:
			// 
			// Welcome To VS 2022
			Console.Write("\nWelcome To VS 2022\n");

			// 輸出: 
			// 檔名：C:\Users\USER\Documents\Visual Studio 2022\Projects\cs-ch-02\ConsoleEscSeq\csharp.gitignore
			// @ 可讓逃脫字元"\"失效
			Console.WriteLine("檔名：C:\\Users\\USER\\Documents\\Visual Studio 2022\\Projects\\cs-ch-02\\ConsoleEscSeq\\csharp.gitignore");
			Console.WriteLine(@"檔名：C:\Users\USER\Documents\Visual Studio 2022\Projects\cs-ch-02\ConsoleEscSeq\csharp.gitignore");

			// 輸出: 
			// C# 2022 IS COOL!
			// "C# 2022 IS COOL!"
			Console.WriteLine("C# 2022 IS COOL!");
			Console.WriteLine("\"C# 2022 IS COOL!\"");
		}
    }
}
