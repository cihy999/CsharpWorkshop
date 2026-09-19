# C#筆記 - Extension Methods

[Master C# Extension Methods Like a Senior Developer | How to write better Extension method in C#](https://youtu.be/No_U5S6vMTY?si=VHc1MCnWI4bEytlR) 的學習筆記。

## 教學摘要

針對影片[《Master C# Extension Methods Like a Senior Developer》](https://youtu.be/No_U5S6vMTY?si=VHc1MCnWI4bEytlR)所做的精煉摘要：

### 第一部分：核心價值 (High-Level Summary)

這部影片旨在解決開發者在 C# 中**過度濫用**或**錯誤使用**擴充方法（Extension Methods）的問題。核心結論是：擴充方法不應只是語法糖，而應作為**擴充第三方函式庫**、**注入領域特定邏輯**及**提升程式碼可讀性**的強大重構工具，從而實現更優雅的架構解耦。

### 第二部分：深度解析 (Deep Dive)

#### 🧠 核心哲學：為何需要擴充方法？

- **不破壞原始碼**：在無法修改（如第三方套件）或不應修改原始碼的情況下增加功能。
- **領域特定化 (Domain Specific)**：針對特定專案需求，為通用類別添加專屬邏輯，避免通用類別變得臃腫。
- **減少重複 (DRY)**：將分散在各處的重複判斷或轉換邏輯集中化。

#### 🛠️ 技術實作：結構化 Program.cs

- **封裝配置細節**：透過擴充 `IServiceCollection`，將繁雜的 Swagger 或 Mediator 配置隱藏在一個簡單的 `.AddSwagger()` 方法後。
- **靜態類別規範**：所有擴充方法必須定義在 **static class** 中，且第一個參數必須使用 **this 關鍵字**。
- **提升入口純淨度**：讓 `Program.cs` 專注於高層級的服務註冊，而非具體的實作細節。

#### 📊 數據處理：通用型重構

- **Predicate 注入**：利用擴充方法結合 `Predicate`，將複雜的過濾邏輯（如判斷帳戶是否啟用）從 Controller 移至擴充層。
- **泛型支援**：展示如何撰寫可應用於多種類型的擴充方法，提升程式碼的複用率。
- **格式轉換**：實作將數據物件直接轉換為 CSV 格式的擴充功能，簡化 API 的回傳邏輯。

### 第三部分：實踐建議 (Take Action)

- **總結重點**：
  1. **優先封裝第三方套件**：將所有與第三方 Library 相關的初始化配置（Config）轉化為擴充方法，以保持主程式簡潔。
  2. **邏輯下放**：當發現某個類別在多個專案中有不同的行為需求時，應使用擴充方法而非繼承，以保持原類別的單一職責（SRP）。
- **適用對象**：
  - 想要提升程式碼架構水準的 **C# / .NET 開發者**。
  - 正在準備高級開發人員面試，需要理解底層設計模式的**求職者**。

> 💡 **小技巧**：在命名擴充類別時，建議使用 `[擴充對象]Extensions`（例如 `ServiceCollectionExtensions`），這能讓專案結構更加清晰。

## 重構目標

`Program.cs` 原本長這樣（簡化示意）：

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //mediatr for loosely coupling
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));

        //swagger for API UI
        builder.Services.AddSwaggerGen(options =>
          {
              options.SwaggerDoc("v1", new OpenApiInfo
              {
                  Version = "v1",
                  Title = "MY API",
              });

              var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
          });
    }
}
```

只寫這樣就夠了：

```csharp
builder.Services.AddMediator();
builder.Services.AddSwagger();
```

1. **隱藏實作細節**：打開 `Program.cs` 時，不想看到一堆設定細節，只想看到「做了什麼」，而不是「怎麼做的」。
2. **跨專案重用、避免 copy-paste**：同樣的 MediatR/Swagger 設定可能在多個專案都需要。如果用 Extension Method 包起來，每個專案只要呼叫 `AddSwagger()`，背後就自動套用相同的設定，不用每次都複製貼上。

### 問題在哪裡？

MediatR 是**第三方套件**，沒辦法直接修改它的原始碼去加方法。

### 解法

用 Extension Methods！雖然無法修改 `IServiceCollection` 的原始碼，但可以透過擴充方法，讓 C# 看起來「好像」`IServiceCollection` 多了 `AddMediator()` 這個方法。

## 實作 Extensions Method

### 1. 建立 Extensions 資料夾與類別

建立一個 `extensions` 資料夾，並在裡面新增一個類別，命名為 `ServiceCollectionExtensions`。

**命名依據**：名稱取決於你在「擴充什麼」。這裡擴充的是 `IServiceCollection`（因為 `AddMediatR`、`AddSwaggerGen` 都是掛在它上面的），所以叫 `ServiceCollectionExtensions`。

### 2. 開始寫 Extension Method 的架構

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatr(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
    }

    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MY API",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
```

Extension Method 的基本規則：

- C# Extension Method 的**語法規定**，兩個條件缺一不可：
  1. **類別必須是 `static`**：Extension Method 不需要實例，它只是「掛載」到目標型別上
  2. **方法必須是 `static`**：同理，不透過物件呼叫，而是透過型別本身呼叫
- `this IServiceCollection services` 這個寫法告訴 C#：「我要把這個方法動態掛到 `IServiceCollection` 介面上。」這樣 Visual Studio 就能自動感知到 `IServiceCollection` 多了這個方法。
- **避免命名衝突。** MediatR 套件本身已經有一個叫做 `AddMediatR` 的 Extension Method。如果取同樣的名字，編譯器會搞混、無法區分要呼叫哪一個。所以作者故意把大寫的 `R` 改成小寫 `r`（`AddMediatr`），讓兩個方法能同時存在而不衝突。
- 回傳 `IServiceCollection` 維持原始設計的**方法鏈（Method Chaining）**

# 測驗：C# Extension Methods

## Q1 🔍 程式碼填空

下面是一個 Extension Method 的骨架，請填入正確的關鍵字（空格以 `___` 表示）：

```csharp
public ___(1)___ class ServiceCollectionExtensions
{
    public ___(2)___ IServiceCollection AddSwagger(___(3)___ IServiceCollection services)
    {
        // ... 實作細節
        return services;
    }
}
```

**(1)**、**(2)**、**(3)** 分別應填入什麼？

## Q1 作答

(1) static
(2) static
(3) this

## Q1 解析

**(1) static** — Extension Method 必須定義在靜態類別中，因為它不需要被實例化，只是「掛載」到目標型別上。

**(2) static** — 方法本身也必須是靜態的，透過型別呼叫而非物件實例。

**(3) this** — 這是 Extension Method 的靈魂關鍵字，告訴 C# 編譯器「把這個方法動態附加到 IServiceCollection 型別上」，讓 IntelliSense 和編譯器都能感知到它的存在。

---

## Q2 🤔 邏輯選擇題

作者在實作時，刻意把擴充方法命名為 `AddMediatr`（小寫 r），而不是 `AddMediatR`（大寫 R）。

**請問主要原因是什麼？**

A. 小寫命名是 C# Extension Method 的規定
B. 避免與 MediatR 套件本身已存在的 `AddMediatR` 衝突
C. 為了讓 Visual Studio IntelliSense 能正確感知這個方法
D. `IServiceCollection` 不允許兩個同名方法同時存在於同一類別

## Q2 作答

B

## Q2 解析

MediatR 套件已經提供了 `AddMediatR()` 這個 Extension Method。如果你也定義同名方法，編譯器在解析 `.AddMediatR()` 時會遇到歧義（ambiguous），無法判斷該呼叫哪一個，導致編譯錯誤。改名為 AddMediatr 是最直接的避衝突手段。

> 補充：選項 D 是混淆選項——實際上兩個同名方法是「可以」存在於不同的靜態類別中，問題在於呼叫時的歧義，而非語法上的不允許。

---

## Q3 🚀 執行結果預測

假設有以下程式碼，請問最後 `builder.Services` 的狀態為何？執行流程是否正確？

```csharp
builder.Services
    .AddMediator()
    .AddSwagger();
```

Extension Method 定義如下：

```csharp
public static IServiceCollection AddMediator(this IServiceCollection services)
{
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
    return services;
}

public static IServiceCollection AddSwagger(this IServiceCollection services)
{
    services.AddSwaggerGen(options => { /* ... */ });
    return services;
}
```

**請問：**

1. `AddSwagger()` 能成功鏈式呼叫的關鍵是什麼？
2. 如果 `AddMediator()` 的回傳型別改成 `void`，會發生什麼事？

## Q3 作答

1. 回傳了 services(`IServiceCollection`)
2. 不能使用鏈式呼叫

---
