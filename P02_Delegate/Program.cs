using P02_Delegate.Models;

namespace P02_Delegate;

/// <summary>
/// 依《Master C# Delegates Like a Senior Developer》逐步重構 GetCards。
/// 資料來源為 CardData（記憶體），不涉及資料庫。
/// </summary>
internal class Program
{
    static void Main()
    {
        // 依教學順序取消註解，觀察重用性如何提升：
        // Step1_Hardcoded();
        // Step2_Parameter();
        // Step3_CustomDelegate();
        Step4_Predicate();
    }

    // --- 示範入口：對應影片各階段 ---

    static void Step1_Hardcoded()
    {
        PrintSection("Step 1：邏輯寫死在方法內（CustomerId == 4）");
        foreach (var card in GetCardsByCustomerIdEqualTo4())
            Console.WriteLine(card);
    }

    static void Step2_Parameter()
    {
        PrintSection("Step 2：魔術數字改為參數");
        foreach (var card in GetCardsByCustomerId(4))
            Console.WriteLine(card);
    }

    static void Step3_CustomDelegate()
    {
        PrintSection("Step 3：自訂 delegate，隔離 if 判斷（僅 CustomerId）");
        foreach (var card in GetCardsByCustomerDelegate(x => x < 4))
            Console.WriteLine(card);
    }

    static void Step4_Predicate()
    {
        PrintSection("Step 4：Predicate<Card> + Lambda（可過濾任意 Card 屬性）");
        foreach (var card in GetCards(x => x.HolderName == "Hanma Baki"))
            Console.WriteLine(card);
    }

    // --- 重構歷程：四個版本的 GetCards ---

    /// <summary>重用性 ≈ 0%：只能查 CustomerId == 4。</summary>
    static List<Card> GetCardsByCustomerIdEqualTo4()
    {
        List<Card> cards = [];
        foreach (var card in CardData.All)
        {
            if (card.CustomerId == 4)
                cards.Add(card);
        }
        return cards;
    }

    /// <summary>重用性 ≈ 25%：可換 CustomerId，但條件仍寫死在 if。</summary>
    static List<Card> GetCardsByCustomerId(int customerId)
    {
        List<Card> cards = [];
        foreach (var card in CardData.All)
        {
            if (card.CustomerId == customerId)
                cards.Add(card);
        }
        return cards;
    }

    /// <summary>重用性 ≈ 75%：判斷邏輯由外部傳入，但僅針對 CustomerId。</summary>
    static List<Card> GetCardsByCustomerDelegate(CustomerDelegate customerDelegate)
    {
        List<Card> cards = [];
        foreach (var card in CardData.All)
        {
            if (customerDelegate(card.CustomerId))
                cards.Add(card);
        }
        return cards;
    }

    /// <summary>重用性 100%：可對 Card 任意屬性下條件（建議用 Predicate 或 Func）。</summary>
    static List<Card> GetCards(Predicate<Card> predicate)
    {
        List<Card> cards = [];
        foreach (var card in CardData.All)
        {
            if (predicate(card))
                cards.Add(card);
        }
        return cards;
    }

    static void PrintSection(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
    }
}

// 自訂 delegate（Step 3）；實務上優先使用 Predicate<T> / Func<T, bool>
public delegate bool CustomerDelegate(int customerId);
