# 01-list-basics 課堂筆記

## 問題：什麼叫做走訪？

「走訪」：依序查看資料中的每一個元素。

---

## 回答：`List<int>` 在程式裡負責做什麼？

保存一份數字資料的清單。

---

## 回答：`numbers[1]` 是什麼意思？

numbers 裡的第二個元素代表的數字。

---

## 回答：`foreach` 如何使用這份清單？

一個一個取出來看。

---

## 問題：for 跟 foreach 有什麼不同？

> **`for` 讓你控制迴圈如何前進；`foreach` 讓你逐一取得元素。**

以 `List<int>` 為例：

```csharp
// 每隔一個位置讀取：索引 0、2、4……
for (int i = 0; i < numbers.Count; i += 2)
{
    Console.WriteLine(numbers[i]);
}

// 逐一取得每個元素
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

---

## 問題：List<int> 的記憶體配置

```csharp
List<int> numbers = new List<int> { 10, 20, 30 };
```

這行程式可以先理解成：**建立一個 List 物件，由它管理一個內部陣列，整數值存放在陣列裡。**

概念上的記憶體關係如下：

```text
numbers（參考）
    │
    ▼
List<int> 物件
    ├─ 元素數量：3
    └─ 內部陣列的參考
              │
              ▼
           int[] 陣列
           ┌────┬────┬────┬────┐
索引       │ 0  │ 1  │ 2  │ 3  │
           ├────┼────┼────┼────┤
值         │ 10 │ 20 │ 30 │ 0  │
           └────┴────┴────┴────┘
                          ↑
                       尚未使用
```

編譯器會針對初始化內容呼叫 `Add`。[官方：集合初始化](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#collection-initializers)

以 **.NET 10 的實作**來說，空 List 起初使用共用的空陣列；第一次 `Add` 時，建立能容納 **4 個整數**的陣列。因此上面的圖有四格，但只有前三格是清單元素。[.NET 10 原始碼](https://github.com/dotnet/runtime/blob/v10.0.0/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs)

所以這裡有兩個不同的數量：

```csharp
Console.WriteLine(numbers.Count);    // 3：目前有幾個元素
Console.WriteLine(numbers.Capacity); // 4：目前內部陣列可容納幾個元素
```

### 容量不足時

陣列長度固定，因此 `Add` 超過容量時，List 會：

1. 配置一個更大的陣列。
2. 將既有元素複製到新陣列。
3. 改為參考新陣列。

舊陣列若沒有其他參考，就成為可由 GC 回收的垃圾。擴充會帶來**新陣列配置、資料複製，以及後續垃圾回收的成本**。

.NET 10 從預設空 List 逐次新增元素時，容量通常依序為 `0 → 4 → 8 → 16 → 32…`；這是實作細節。[原始碼](https://github.com/dotnet/runtime/blob/v10.0.0/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs)

### 已知數量時，預先指定容量

```csharp
List<int> numbers = new List<int>(100);
// Count = 0，Capacity = 100
```

這是**預留空間**，並非加入 100 個元素。接著以 `Add` 加入最多 100 個元素，不會因容量不足而擴充，可避免中間陣列的配置、複製與垃圾產生。[官方容量說明](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.capacity?view=net-10.0)

預留容量不代表固定記憶體位址；GC 仍可能搬移物件。
