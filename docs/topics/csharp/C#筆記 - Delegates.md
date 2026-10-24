---
title: C#筆記 - Delegates
tags: [C#]

---

---
version: "202606270950"
last_review: 2026-06-27
description: "筆記 C# Delegate 相關知識，包含初階與進階的使用方法。"
---

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

### 封裝資料類型：使用 Predicate

```csharp
using P02_Delegate.Models;

namespace P02_Delegate;

// 不宣告 Delegate，改用內建的 Predicate<T>
//public delegate bool CustomerDelegate(int customerId);
//public delegate bool CustomerCardDelegate(Card card);

internal class Program
{
    static void Main()
    {
        foreach (var card in GetCards(x => x == 4))
            Console.WriteLine(card);

        foreach (var card in GetCards(x => x.HolderName == "Hanma Baki"))
            Console.WriteLine(card);
    }

    static List<Card> GetCards(Predicate<int> predicate)
    {
        List<Card> cards = [];
        foreach (var card in CardData.All)
        {
            if (predicate(card.CustomerId))
                cards.Add(card);
        }
        return cards;
    }

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
}
```

## 進階技巧

以下是針對[Advanced C# Delegates like a Senior Developer: Real-World Examples in .NET!](https://youtu.be/LEcxrLb3OpY?si=izj-ohCPrxhHBr6B)內容所製作的精煉摘要：

---

### 第一部分：核心價值 (High-Level Summary)

這部影片旨在帶領開發者**超越 C# 委派（Delegates）的基礎語法，深入探討其在 .NET 平台底層的編譯機制與實務設計模式中的進階應用**。核心結論指出，委派不僅是 C# 的語法糖，更是實現事件驅動、異步處理、設計模式（如觀察者與策略模式）、LINQ 及函數式編程的**底層核心基石**。

---

### 第二部分：深度解析 (Deep Dive)

#### ⚙️ 底層編譯與機制

* 透過 JetBrains dotPeek 反編譯工具揭示，C# 中的委派在本質上是**編譯器自動生成的私有密封類別（private sealed class）** [[03:57](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=237)]。
* 當定義多個相同簽章的匿名委派時，.NET 平台會**優化並複用同一個編譯器生成類別**，僅在內部建立不同的方法映射 [[05:30](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=330)]。

#### 🔔 事件驅動編程 (EDP) 與 EAP 模式

* **事件驅動編程**的五大核心要素（事件、事件處理器、發行者、訂閱者、委派）中，**委派定義了事件處理器的簽章規格**，是 UI 框架（如 WinForms, WPF）點擊事件的底層核心 [[06:39](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=399)]。
* **事件型異步模式 (EAP)**（如舊版 `BackgroundWorker` 與 `WebClient`）在 async/await 出現前被廣泛使用，其進度通知與完成回呼皆是**依賴委派機制來避免阻塞主執行緒** [[14:19](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=859)], [[16:20](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=980)]。

#### 🧩 觀察者與策略設計模式

* **👁️ 觀察者模式**：可利用委派字典（Dictionary of Delegates）替代傳統的介面設計，讓任何**符合委派簽章的方法都能直接註冊為訂閱者**，達成更彈性的低耦合架構 [[19:36](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1176)]。
* **🎯 策略模式**：將演算法（如過濾條件）以委派（`Predicate`）形式由外部傳入。這樣能**在不修改主邏輯（Context）的前提下，動態抽換核心業務邏輯** [[24:57](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1497)]。

#### 🚀 LINQ、並行與函數式編程

* **📊 LINQ 擴充方法**：諸如 `Where`、`Select` 及 `FirstOrDefault` 等方法，底層皆大量使用 `Func` 委派來接收 Lambda 表達式 [[27:54](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1674)]。
* **⚔️ 並行處理 (Concurrency)**：不論是 Legacy 執行緒（`ThreadStart`）、執行緒池（`WaitCallback`），或是現代的 TPL 任務（`Task.Run`），**多執行緒與異步編程的底層皆是由委派驅動** [[29:51](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1791)], [[31:05](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1865)]。
* **🧠 函數式編程 (FP)**：C# 透過委派將函數視為**一等公民（First-Class Citizens）**。藉由擴充方法與委派的結合，可撰寫出無隱藏狀態、高可讀性的**宣告式代碼（Declarative Code）** [[32:27](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1947)]。

---

### 第三部分：實踐建議 (Take Action)

**總結重點**：

1. 建議在架構設計時，善用 `Action`、`Func` 與 `Predicate` 等內建委派來**實現邏輯抽離與策略模式**，提高代碼的擴充性與可測試性。
2. 理解委派底層的類別生成機制，能幫助開發者在撰寫高性能 .NET 應用時，優化記憶體配置並避免不必要的閉包（Closure）開銷。

**適用對象**：

適合已掌握 C# 基礎，想進一步跨越到資深開發者（Senior Developer）階級、或是想深入理解 .NET 底層設計與軟體架構模式的中階工程師。****
