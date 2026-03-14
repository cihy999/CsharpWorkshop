---
paths:
  - "**/*.cs"
---

# C# 程式碼風格規定

參考 .Net Runtime 和 Google C# Style Guide

- [C# at Google Style Guide](https://google.github.io/styleguide/csharp-style.html)
- [dotnet/runtime - coding-style](https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md)

---

## 命名規則

| 對象 | 規則 | 範例 |
|------|------|------|
| 類別 | PascalCase | `MyClass` |
| 方法 | PascalCase | `GetValue()` |
| 列舉（型別與成員） | PascalCase | `enum Color { Red, Blue }` |
| 公開欄位 | PascalCase | `public int Count;` |
| 公開屬性 | PascalCase | `public string Name { get; set; }` |
| 命名空間 | PascalCase | `MyProject.Core` |
| 區域變數 | camelCase | `resultValue` |
| 方法參數 | camelCase | `userName` |
| `private` / `protected` / `internal` 欄位和屬性 | _camelCase | `_myField` |
| 介面 | I + PascalCase | `IInterface` |
| 縮寫詞 | 視為一個單字 | `MyRpc`（非 `MyRPC`） |

---

## 檔案與資料夾

- 檔名與資料夾名稱使用 PascalCase（`MyClass.cs`）
- 檔名應與主要類別同名
- 每個檔案原則上只放一個主要類別

---

## 程式碼組織

### 修飾符順序

```csharp
public protected internal private new abstract virtual override sealed static readonly extern unsafe volatile async
```

### Using 宣告

- 放在檔案頂部
- System 命名空間優先，其餘按字母順序

### 類別成員順序

1. 巢狀類別、列舉、委派、事件
2. 靜態、const、readonly 欄位
3. 一般欄位和屬性
4. Constructors和finalizers
5. 方法

每組內部再依存取層級排序：public → internal → protected internal → protected → private

---

## 格式規定

- **縮排**：4 個空格，禁止使用 Tab
- **每行一個陳述式**
- **空行**：避免超過一個連續空行
- **多餘空格**：避免行尾或多餘的空白字元

---

## 大括號

採用 **Allman 風格**，大括號獨佔一行。

- 單行 `if` 可省略大括號，但若任一區塊跨多行，所有區塊都必須加大括號

```csharp
// 正確：Allman 風格
if (condition)
{
    DoSomething();
}
else
{
    DoOther();
}

// 正確：單行可省略大括號
if (condition)
    DoSomething();

// 錯誤：混用單行與多行卻省略大括號
if (condition)
    DoSomething();
else
{
    DoOther();
    DoMore();
}
```

---

## 程式碼實踐

- 優先使用具名常數，避免魔法數字(magic numbers)
- 單行唯讀屬性：優先使用 expression body（`=>`）
- `out`：永遠放在參數列表的最後
- 字串組合一律使用字串插值（`$"..."`）

### var 關鍵字

- **鼓勵**：型別從右側明顯可見時（`var apple = new Apple();`）
- **不鼓勵**：基本型別（`int`、`bool`、`string`）、使用者需要知道型別時

```csharp
// 正確
var apple = new Apple();
var list = new List<string>();

// 錯誤
var success = true;
var count = 0;
```

---
