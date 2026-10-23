---
title: C#筆記 - Interface
tags: [C#]

---

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

---

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

## 實踐開放封閉原則 (Open-Closed Principle)

遍歷組件中所有的類型（Types），尋找實作了 `IBankProvider` 介面且非抽象的類別。

主程式完全不需要引用 `EdenZeroBank.dll` 或 `PrivateNMBank.dll`。只要把 DLL 丟進資料夾，程式執行時就會自動出現該銀行的功能。

```csharp
internal class ProviderService : IProviderService
{
    // Libs 資料夾存放各銀行的動態函式庫(dll)
    private const string FolderPath = "libs";
    private const string Extension = "*.dll";
    private readonly string _libsPath;

    public ProviderService()
    {
        _libsPath = ApplicationPath.PathTo(FolderPath);
    }

    public IEnumerable<IBankProvider> GetProviders()
    {
        // 掃描 Libs 資料夾下所有的 .dll 檔案
        string[] providers = Directory.GetFiles(_libsPath, Extension);
        foreach (string provider in providers)
        {
            // 載入組件後，建立銀行實例(通過反射 Reflection)
            Assembly assembly = Assembly.LoadFile(provider);
            Type[] assemblyTypes = assembly.GetTypes();
            IEnumerable<Type> providerTypes = assemblyTypes.Where(t => t.GetInterface(nameof(IBankProvider), true) != null);
            foreach (Type providerType in providerTypes)
            {
                object? instance = Activator.CreateInstance(providerType);
                if (instance is IBankProvider bankProvider)
                {
                    // 先回傳一個，等下次有人要再繼續跑
                    yield return bankProvider;
                }
            }
        }
    }
}
```

## 明確介面實作（Explicit Interface Implementation, EII）

EII 是 C# 實作介面的一種方式：**介面成員不直接掛在 class 的 public API 上**，而是「只有透過該介面型別」才能呼叫。

運用時機：

| 適合用 EII | 不適合用 EII |
|---|---|
| 介面是內部契約，不想污染 public API | 介面就是 class 的主要公開能力 |
| 需橋接 protected 方法給特定消費者 | 希望呼叫端直接用 class 型別呼叫 |
| 多介面同名方法需區分 | 只有一個介面、無命名衝突、本來就要 public |

### 1. 隱式實作（Implicit）— 最常見

```csharp
internal class ProviderService : IProviderService
{
    public IEnumerable<IBankProvider> GetProviders()
    {
        // ...
    }
}
```

- 必須加 `public`（或與介面成員相容的可見性）
- 介面成員 = class 的 public 成員
- 兩種呼叫方式都可以：

```csharp
service.GetProviders();                         // 直接呼叫
((IProviderService)service)..GetProviders();    // 透過介面呼叫
```

### 2. 明確實作（Explicit）

```csharp
public class EquipBagModule : IBagDataAccess
{
    BagSubData IBagDataAccess.GetSubData(uint slotId, bool isAutoCreate)
        => GetSubData(slotId, isAutoCreate);
}
```

- **不能**加 `public`、`private` 等修飾詞
- 語法固定：`回傳型別 介面名稱.成員名稱(...)`
- **只能**透過介面型別呼叫：

```csharp
IBagDataAccess access = module;
access.GetSubData(1);        // OK
module.GetSubData(1);        // 編譯錯誤（除非 class 另有同名 public 方法）
```

### EII 用途

#### 1. 封裝 — 不把內部契約暴露成 public API

情境：

- `DataClientModule` 的 `GetSubData` / `SetSubDataDirty` 是 **`protected`**
- `BagContainer` 需要這些能力，但不想讓 `EquipBagModule` 對外多出一組 public 資料存取方法

EII 剛好做「有限暴露」：

```csharp
// BagContainer 內部
_dataAccess.GetSubData(slotId);   // _dataAccess 型別是 IBagDataAccess

// 外部若持有 EquipBagModule
equipBag.AddItem(...);            // 公開 API
equipBag.GetSubData(...);         // 看不到（除非另外 public 實作）
```

#### 2. 解決同名／同簽名衝突

一個 class 實作多個介面，或 class 本身已有同名方法但語意不同時，EII 可以區分：

```csharp
public interface IReadable { string Read(); }
public interface IWritable { string Read(); }  // 同名但語意不同

public class Document : IReadable, IWritable
{
    public string Read() => "class version";

    string IReadable.Read() => "readable version";
    string IWritable.Read() => "writable version";
}
```

#### 3. 刻意隱藏實作細節

介面是給「特定消費者」用的內部契約（如 `IBagDataAccess`），不是給所有呼叫端的一般 API。

---

### 限制與注意事項

1. **EII 成員預設是 instance 成員**，不能是 `static`（C# 8+ 介面 static 成員是另一套規則）。
2. **不能在 class 內部用 `this.GetSubData()` 呼叫 EII 版本** — 要 cast：  
   `((IBagDataAccess)this).GetSubData(1)`
3. **繼承**：子 class 不會自動「繼承」父 class 的 EII；子 class 若也要滿足同一介面，需自行再實作。
4. **測試／Mock**：Mock 框架通常仍可依介面 mock，但直接測 class public surface 時看不到 EII 成員。
5. **效能**：EII 與隱式實作在執行期幾乎無差異，主要是 API 設計與編譯期可見性差異。
