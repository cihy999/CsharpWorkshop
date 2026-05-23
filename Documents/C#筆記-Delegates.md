# C#筆記 - Delegates

這是一份針對影片《[Master C# Delegates Like a Senior Developer](https://youtu.be/kNyf0NOYalE?si=ig4qUMEzytgixuiR)》所整理的精煉摘要，旨在幫助開發者從資深工程師的視角重新理解並運用 C# 委派（Delegates）。

---

## 第一部分：核心價值 (High-Level Summary)

這部影片旨在解決開發者雖了解「委派」語法，卻不知道「何時」與「如何」在實務中應用以提升代碼質量的問題。核心結論是：**委派本質上是將「邏輯表達式」封裝成參數，藉此消除硬編碼（Hard-coding），使方法達到 100% 的重用性與擴展性。**

---

## 第二部分：深度解析 (Deep Dive)

### 🛠️ 從硬編碼到參數化 [[03:00](http://www.youtube.com/watch?v=kNyf0NOYalE&t=180)]

- **初始問題**：原始程式碼將過濾條件（如 `ID == 4`）直接寫死在方法內，導致該方法無法處理其他 ID 的需求。
- **初步優化**：透過將魔術數字（Magic Number）提取為**變數參數**，重用性從 0% 提升至 25%，但仍侷限於單一屬性的比較 [[05:40](http://www.youtube.com/watch?v=kNyf0NOYalE&t=340)]。

### 🧠 邏輯封裝：委派的誕生 [[07:47](http://www.youtube.com/watch?v=kNyf0NOYalE&t=467)]

- **核心觀念**：當我們不僅想隔離「數值」，更想隔離整個「**判斷邏輯**」（If condition）時，就必須使用委派。
- **實作定義**：使用 `delegate` 關鍵字定義簽署（Signature），這就像是為方法準備一個**規格化插槽**，允許外部傳入符合規則的任何邏輯 [[10:07](http://www.youtube.com/watch?v=kNyf0NOYalE&t=607)]。

### ⚡ 語法糖與 Lambda 表達式 [[15:24](http://www.youtube.com/watch?v=kNyf0NOYalE&t=924)]

- **匿名簡化**：為了避免每次都要額外定義具名方法，資深開發者會使用 **Lambda 表達式**（`x => x.Property > value`）來快速注入邏輯。
- **視覺化轉移**：這將原本深藏在 `if` 語句中的邏輯，成功轉移到**方法調用端**，實現高度靈活性 [[16:28](http://www.youtube.com/watch?v=kNyf0NOYalE&t=988)]。

### 📊 內建委派：Func 與 Predicate [[20:42](http://www.youtube.com/watch?v=kNyf0NOYalE&t=1242)]

- **避免重複造輪子**：在 99% 的實務場景中，應優先使用 .NET 內建的 `Func<T, bool>` 或 `Predicate<T>`，而非自行宣告 `delegate` [[21:34](http://www.youtube.com/watch?v=kNyf0NOYalE&t=1294)]。
- **語法規範**：如果只需回傳布林值進行判斷，`Predicate` 是最直觀且符合語意的選擇 [[22:52](http://www.youtube.com/watch?v=kNyf0NOYalE&t=1372)]。

### 🛡️ 實戰場景與架構設計 [[24:17](http://www.youtube.com/watch?v=kNyf0NOYalE&t=1457)]

- **LINQ 核心**：現代 C# 的 LINQ（如 `.Where()`）本質上就是委派的大規模應用。
- **替代方案**：在某些簡單場景下，委派可以作為**策略模式（Strategy Pattern）**的輕量化替代方案，減少類別定義的複雜度 [[25:37](http://www.youtube.com/watch?v=kNyf0NOYalE&t=1537)]。

---

### 第三部分：實踐建議 (Take Action)

- **總結重點**：
  1. 當你發現一個方法內有多個類似的 `if` 邏輯，或需要根據不同場景改變判斷條件時，請立刻考慮將該判斷邏輯重構為 `Predicate` 或 `Func` 參數。
  2. 優先使用 Lambda 表達式搭配內建委派，保持代碼簡潔。

---

## Delegate 程式碼示範

### 邏輯封裝：使用委派

```csharp
using P02_Delegate.Models;

namespace P02_Delegate;

// 自訂 delegate（Step 3）；實務上優先使用 Predicate<T> / Func<T, bool>
public delegate bool CustomerDelegate(int customerId);

/// <summary>
/// 依《Master C# Delegates Like a Senior Developer》逐步重構 GetCards。
/// 資料來源為 CardData（記憶體），不涉及資料庫。
/// </summary>
internal class Program
{
    static void Main()
    {
        foreach (var card in GetCardsByCustomerDelegate(customerId => customerId == 3))
            Console.WriteLine(card);
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
}
```
