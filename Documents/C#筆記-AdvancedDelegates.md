---
version: "202606270950"
last_review: 2026-06-27
description: "筆記 C# Delegate 進階應用方式。"
---

# C#筆記 - Advanced Delegates

## Advanced C# Delegates like a Senior Developer: Real-World Examples in .NET!

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

適合已掌握 C# 基礎，想進一步跨越到資深開發者（Senior Developer）階級、或是想深入理解 .NET 底層設計與軟體架構模式的中階工程師。
