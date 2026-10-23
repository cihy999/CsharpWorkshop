---
title: C# 最佳實踐：in + readonly struct
tags: [C#]

---

---
version: "202608261636"
last_review: 2026-08-26
description: "說明 C# in 關鍵字、readonly struct 的用途、效能優化與 record 類型特性。"
---

# C# 最佳實踐：in + readonly struct

- `in` 用在方法參數上，表示這個參數以「傳址方式傳入但不可修改」（唯讀傳址），主要用來避免大型 struct 複製的效能成本，同時保證方法內不會意外修改呼叫端的資料。
- `readonly` 用在 `struct` 定義上，表示這個 `struct` 的所有欄位都不可變。

```csharp
public bool TryApplyEvent(in QuestEvent questEvent)
```

呼叫端傳入 `QuestEvent` 時，不會複製整個 struct，而是把它的唯讀參考交給方法：

```csharp
questModule.TryApplyEvent(in questEvent);
```

> 它們是獨立的關鍵字，可以單獨使用，但編譯器有個重要的效能優化考量：
> 如果你對一個「非 readonly 的 struct」使用 in 傳入，編譯器不能保證這個 struct 內部沒有會修改自身狀態的方法（因為理論上方法可能修改 this）。為了保證唯讀語意，編譯器會在方法內對每次呼叫該 struct 的成員方法時，產生隱藏的防禦性複製（defensive copy），這反而抵消了 in 想要避免複製的效能優勢。
> 如果 struct 本身標記為 readonly struct，編譯器就能確定它的所有欄位都不會被修改，因此搭配 in 傳遞時不需要做防禦性複製，效能才能真正發揮。

## 附錄：record

編譯器自動提供「依內容比較是否相等」、較好的字串輸出等資料物件功能，適合用來裝「**資料**」而非「有複雜行為的物件」。

- `class` 的預設哲學：「你是誰」（身分、記憶體位置）比較重要 → 即使兩個物件內容一樣也視為不同。
- `record` 的預設哲學：「你裝的內容是什麼」比較重要 → 只要值都一樣，就視為相同。

### 基本用法

**1. Positional record（位置式宣告，最簡潔）**：

```csharp
public record Person(string Name, int Age);

var p1 = new Person("小明", 20);

Console.WriteLine(p1.Name); // 小明
Console.WriteLine(p1);      // Person { Name = 小明, Age = 20 }
```

自動產生建構子、屬性、`Equals`、`GetHashCode`、`ToString`。

**2. 傳統寫法（屬性宣告）（`{ get; init; }`）**：

```csharp
public record Person
{
    public string Name { get; init; }
    public int Age { get; init; }
}

var p1 = new Person { Name = "小明", Age = 20 };
```

`init` 表示只能在建立物件時設定一次，之後不可修改（維持不可變性）。

**3. 特色：依內容比較（value equality）**：

```csharp
var p1 = new Person("小明", 20);
var p2 = new Person("小明", 20);

Console.WriteLine(p1 == p2);      // true，比較內容而非記憶體位置
Console.WriteLine(p1.Equals(p2)); // true
```

**4. `with`：複製並修改部分欄位**：

record 通常設計成不可變，若要「改值」則用 `with` 產生新複本，不影響原物件：

```csharp
var p1 = new Person("小明", 20);
var p2 = p1 with { Age = 21 };

Console.WriteLine(p1); // Person { Name = 小明, Age = 20 }（不變）
Console.WriteLine(p2); // Person { Name = 小明, Age = 21 }（新物件）
```

**5. 解構（Deconstruct）**：

positional record 天生支援解構：

```csharp
var (name, age) = p1;
```

**6. `record class`（預設）vs `record struct`（C# 10+）**：

```csharp
public record class Person(string Name, int Age); // 參考型別，存在 heap
public record struct Point(int X, int Y);         // 值型別，存在 stack，適合小型、頻繁複製的資料
```

### record 常見使用場景

- API 回應資料（DTO）
- 設定 / 參數物件
- 需要「值比較」而非「參考比較」的資料
- 需要不可變、但想方便產生「修改版本」的資料（配合 `with`）

一句話總結：**如果類別主要是用來「裝資料」，優先考慮用 `record`；小型、頻繁複製且需要值比較的資料，考慮 `record struct`。**
