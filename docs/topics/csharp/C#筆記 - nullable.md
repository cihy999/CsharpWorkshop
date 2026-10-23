---
title: C#筆記 - nullable
tags: [C#]

---

---
version: "202608251447"
last_review: 2026-08-25
description: "介紹 C# nullable 相關語法糖應用。"
---

# nullable

## nullable reference types

C# 8.0 以後引入 nullable reference types（可為 null 的參考型別） 語法。

專案可以 .NET 5+ 以後的版本，這樣C#版本就可以使用8以上版本。

```csharp
public static string? GetDisplayName(this Enum value)
```

`string?` 表示這個方法回傳的字串可能是 null。

因為方法內部有可能找不到對應的屬性或值，所以結果可能沒有字串，回傳 null 是合理的情況。

用 `string?` 表示呼叫者要做好 null 檢查。

未給予預設值直接報錯：
![Nullable_202506141637](https://hackmd.io/_uploads/HJEJrncXgx.png)

需要標示為可能null：
![Nullable_202506141641](https://hackmd.io/_uploads/rknCB39Qgg.png)

## Null 運算子

### `?.` (Null-conditional operator)

```csharp
var attr = field?.GetCustomAttribute<MetaAttribute>();
```

`?.`是 Null 條件運算子 (Null-conditional operator)

> Null 條件運算子是 C# 6.0 開始的語法。

- 如果 `field` 不是 null，就呼叫 `GetCustomAttribute<MetaAttribute>()`。
- 如果 `field` 是 null，整個運算結果就是 null，不會拋例外。

```csharp
return attr?.DisplayName;
```

- 如果 `attr` 不是 null，回傳 `DisplayName` 屬性的值。
- 如果 `attr` 是 null，整個運算結果是 null。

這種寫法可以避免很多 null 參考錯誤（NullReferenceException），寫起來更簡潔安全。

### null 合併運算子

```csharp
return attr?.DisplayName ?? "None";
```

`??`是 null 合併運算子，當DisplayName為null時，回傳`"None"`。

### `null!` (null-forgiving operator)

`null!` 是 **null-forgiving operator**（告訴編譯器「這裡我保證不會有問題」）。

```csharp
public bool TryGetCustomizationSnapshot(int roleId, int slotIndex, out RoleCustomization snapshot)
{
    snapshot = null!;
    // 以下省略...
}
```

`TryGetCustomizationSnapshot` 的 `out RoleCustomization snapshot` 被當成 **不可為 null** 的參考型別。C# 規定：方法結束前必須給 `out` 賦值。

若寫 `snapshot = null;`，編譯器會警告「把 null 賦給不可為 null 的型別」。`null!` 的意思是：執行期仍是 `null`，但告訴編譯器「我知道這是 Try 模式，呼叫端會先看回傳的 `bool`，失敗時不會去用 `snapshot`」。

呼叫端正確用法是：`if (!TryGet...(..., out var snapshot))` 就不要碰 `snapshot`。`!` 不會在執行期把 `null` 變成非 null。

## `int?`

一般的 `int` 一定要有一個數值，不能是「沒有值」的狀態。但有時候我們需要表達「這個欄位根本沒有填」，跟「填了 0」是不同意思的，這時就需要 `int?`。

### 為什麼需要它？

想像一個問卷欄位「你的年齡」：

- 沒填 → 應該是「未知」
- 填了 0 → 表示年齡是 0 歲

如果只能用 `int`，你沒辦法區分「沒填」跟「填了 0」，因為 `int` 一定要有個數字，你可能只能被迫用 0 或 -1 當作「沒填」的暗號，但這樣很容易搞混。

`int?` 就是解決這個問題：它讓 `int` 多了一個「沒有值」的合法狀態。

你可以把 `int? value` 想成是這樣一個小包裝盒：

- `HasValue`：一個開關，告訴你「裡面到底有沒有放東西」
- `Value`：真正的數字（但如果 `HasValue` 是 `false`，這個數字沒有意義，不該去讀它）

所以：

- `int? value = null;` → 相當於把 `HasValue` 設成 `false`
- `int? value = 5;` → 相當於把 `HasValue` 設成 `true`，`Value` 設成 `5`

### 三種讀取方式

1. **直接讀 `.Value`**（危險）

```csharp
int number = value.Value; // 如果 HasValue 是 false，直接爆炸(丟例外)
```

2. **先檢查再讀**（安全，推薦）

```csharp
if (value is int number)
{
    // 只有 HasValue 是 true 時，才會進到這裡
    // 而且自動把值取出來放進 number 這個一般 int 變數
}
```

3. **給預設值**（安全，簡潔）

```csharp
int number = value ?? 0; // 如果 HasValue 是 false，就用 0 代替
```
