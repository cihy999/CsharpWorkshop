using System.Text.RegularExpressions;

namespace DecodeBytes.Provider
{
    public partial class CardNumber
    {
        public string Number { get; private set; }

        public CardNumber(string cardNumber)
        {
            CardNumberRegExp().IsMatch(cardNumber);
            Number = cardNumber;
        }

        // 優化正則表達式(Regular Expression)處理，
        // 透過 [GeneratedRegex] 標籤，C# 編譯器會在編譯時期就直接把這個正則表達式轉換成最佳化的 C# 程式碼，這會大大提升執行效率並減少記憶體消耗。
        // \d: 代表任何一個「數字」（0-9）
        // {4}: 代表前面的元素要出現「剛好 4 次」。所以 \d{4} 就是指「連續 4 個數字」
        // -: 就是普通的連字號
        // 組合起來: 它在尋找符合 1234-5678-9012-3456 這種格式的字串。
        // .NET 7 以上才支援 [GeneratedRegex] 標籤
        [GeneratedRegex(@"\\d{4}-\\d{4}-\\d{4}-\\d{4}")]
        private static partial Regex CardNumberRegExp();
    }
}
