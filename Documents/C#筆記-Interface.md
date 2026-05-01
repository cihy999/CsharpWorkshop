# C#筆記 - Interface

這是一份針對影片《[Master C# Interfaces Like a Senior Developer](https://youtu.be/6JD-RdpVauY?si=ZQ1WfMn8ixxgmtzZ)》的精煉摘要，旨在幫助你掌握如何運用介面與後期綁定（Late Binding）開發具備高擴充性的專業軟體。

---

## 第一部分：核心價值 (High-Level Summary)

這部影片旨在解決軟體開發中常見的**過度耦合（Tight Coupling）**問題。核心結論是：透過 C# **介面（Interface）與動態後期綁定（Late Binding）**，開發者可以打造出無需修改核心程式碼即可動態增加功能的系統（如外掛式架構），實現真正的物件導向設計原則。

---

## 第二部分：深度解析 (Deep Dive)

### 🧠 核心觀念：介面與解耦 [[03:00](http://www.youtube.com/watch?v=6JD-RdpVauY&t=180)]

* **介面作為契約**：介面定義了「做什麼」而非「怎麼做」。透過介面通訊，應用程式能與具體實作隔離，達到**鬆散耦合（Loose Coupling）**。
* **多型性（Polymorphism）**：單一契約（Interface）可有多種實作。系統能根據需求動態切換不同的銀行服務提供者（Provider），而不影響主邏輯 [[05:18](http://www.youtube.com/watch?v=6JD-RdpVauY&t=318)]。

### 🛠️ 技術架構：ATM 專案設計 [[11:51](http://www.youtube.com/watch?v=6JD-RdpVauY&t=711)]

* **函式庫分解（Library-based Decomposition）**：將系統拆解為 `Common`（共用邏輯）、`Provider`（介面定義）以及多個具體的實作庫（如 `EdenZeroBank`）。
* **跨技術共享**：展示如何讓 **WinForms** 與 **WPF** 共享同一套核心代碼邏輯，僅需更換視覺化渲染層（Visualizer）[[12:10](http://www.youtube.com/watch?v=6JD-RdpVauY&t=730)]。

### 🛡️ 防禦性編程：領域實體化 [[18:45](http://www.youtube.com/watch?v=6JD-RdpVauY&t=1125)]

* **卡號驗證實體**：不直接使用字串處理卡號，而是建立 `CardNumber` 類別，在建構子中使用 **Regex** 進行嚴格驗證。
* **封裝邏輯**：確保無效的資料在進入核心業務邏輯前就被攔截，提升系統的穩定性與安全性 [[20:40](http://www.youtube.com/watch?v=6JD-RdpVauY&t=1240)]。

### ⚡ 進階實作：後期綁定與反射 (Reflection) [[40:16](http://www.youtube.com/watch?v=6JD-RdpVauY&t=2416)]

* **動態載入 Dll**：實作 `DefaultProviderService` 掃描特定資料夾（Libs），並使用 `Assembly.LoadFile` 載入外部元件 [[41:05](http://www.youtube.com/watch?v=6JD-RdpVauY&t=2465)]。
* **執行期實例化**：利用 `Activator.CreateInstance` 在程式執行時動態建立物件。這讓開發者能「不改動主程式」就加入新的銀行支援 [[43:26](http://www.youtube.com/watch?v=6JD-RdpVauY&t=2606)]。

---

### 第三部分：實踐建議 (Take Action)

* **總結重點**：
  1. **優先設計介面**：在撰寫具體邏輯前，先定義好通訊契約，這能強迫你思考模組間的權責劃分。
  2. **善用 DLL 插件化**：將易變動的第三方整合（如 API、支付閘道）放入獨立的專案，透過掃描資料夾的方式載入，達成系統的「熱插拔」功能。
* **適用對象**：
  * 想要進階為**資深開發者**的 C# 初中階工程師。
  * 需要設計**外掛系統（Plugin System）**或大型企業級應用的架構師。
  * 對 **物件導向設計原則（SOLID）** 有基礎但不知如何實際應用的學習者。

## Interface 程式碼示範

介面只宣告屬性、方法、事件，皆為公開（public）。

```csharp
public interface IBankProvider
{
    string ProviderName { get; }
    void AddToBalance(CardNumber cardNumber, decimal amount);
    decimal GetBalance(CardNumber cardNumber);
}
```

實作示範：

```csharp
public class EdenZeroProvider : IBankProvider
{
    public string ProviderName => "EdenZero";

    public void AddToBalance(CardNumber cardNumber, decimal amount)
    {
        if (!IsCardNumberExist(cardNumber.Number))
            throw new ArgumentException("Card number is not valid");
        Cards[cardNumber.Number] += amount;
    }

    public decimal GetBalance(CardNumber cardNumber)
    {
        if (!IsCardNumberExist(cardNumber.Number))
            throw new ArgumentException("Card number is not valid");
        return Cards[cardNumber.Number];
    }
}
```
