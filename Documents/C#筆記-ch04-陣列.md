# C#筆記 - ch04 - 陣列

[Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch02 資料型別與主控台應用程式](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ) 的學習筆記。

```csharp
internal class Program
{
   internal struct Member 
   {
       public string Name;
       public int Age;
   }

   static void Main(string[] args)
   {
        Member[] blackPinkMembers =
        [
            new() { Name = "Jisoo", Age = 31 },
            new() { Name = "Jennie", Age = 30 },
            new() { Name = "Rosé", Age = 29 },
            new() { Name = "Lisa", Age = 28 }
        ];

        Console.WriteLine("== BLACKPINK 成員 ==\n");
        Console.WriteLine("姓名\t年齡");
        Console.WriteLine("==========");
        for (int i = 0; i < blackPinkMembers.Length; i++)
        {
            Console.WriteLine($"{blackPinkMembers[i].Name}\t{blackPinkMembers[i].Age}");
        }
   }
}
```

## 建立陣列、初值設定

方法1：先建立空陣列，再逐一指定每個元素的屬性。

```csharp
Member[] blackPinkMembers = new Member[4];
blackPinkMembers[0].Name = "Jisoo";
blackPinkMembers[0].Age = 31;
blackPinkMembers[1].Name = "Jennie";
blackPinkMembers[1].Age = 30;
blackPinkMembers[2].Name = "Rosé";
blackPinkMembers[2].Age = 29;
blackPinkMembers[3].Name = "Lisa";
blackPinkMembers[3].Age = 28;
```

方法2：集合初始化(Collection Initializer) + 物件初始化(Object Initializer)，大幅縮減程式碼行數並提升可讀性。

```csharp
Member[] blackPinkMembers = new Member[4]
{
    new Member { Name = "Jisoo", Age = 31 },
    new Member { Name = "Jennie", Age = 30 },
    new Member { Name = "Rosé", Age = 29 },
    new Member { Name = "Lisa", Age = 28 },
};
```

方法3：使用目標型別的 `new()` 進行簡化 (C# 9.0 起支援)，如果編譯器已經知道你是要建立`Member`陣列，它就允許你把每個物件的 `new Member` 縮寫為 `new()`。

```csharp
Member[] blackPinkMembers = new Member[4]
{
    new() { Name = "Jisoo", Age = 31 },
    new() { Name = "Jennie", Age = 30 },
    new() { Name = "Rosé", Age = 29 },
    new() { Name = "Lisa", Age = 28 },
};
```

以上方法還可以使用 `var`，編譯器會通過「目標型別推斷 (Target-Typed Inference)」。

```csharp
var blackPinkMembers = new Member[4]
{
    new() { Name = "Jisoo", Age = 31 },
    new() { Name = "Jennie", Age = 30 },
    new() { Name = "Rosé", Age = 29 },
    new() { Name = "Lisa", Age = 28 },
};
```

1. 編譯器看到左邊：`var blackPinkMembers`，這被稱為「目標型別 (Target Type)」。
2. 編譯器看右邊的 [...]：左邊是 `Member[]`，所以編譯器確定這個中括號代表的是 `Member` 實體陣列。
3. 編譯器看裡面的 `new()`：因為外層被推斷出要裝的是 Member，所以內層的 new() 也能因此得知它必須呼叫 Member 的建構子。

方法4：集合表達式 (Collection Expressions, C# 12.0 起支援)，支援用中括號 `[]` 來直接建立陣列或串列，搭配 `new()` 會讓排版看起來就像 JSON 一樣整齊清爽。

```csharp
Member[] blackPinkMembers =
[
    new() { Name = "Jisoo", Age = 31 },
    new() { Name = "Jennie", Age = 30 },
    new() { Name = "Rosé", Age = 29 },
    new() { Name = "Lisa", Age = 28 }
];
```

### 補充：struct 為什麼可以使用 new 來初始化？

1. `new` 的真正作用是**呼叫「建構子 (Constructor)」**
   - 在 C# 中，`new` 關鍵字的主要目的是 「呼叫建構子來初始化資料」，不管是 class 還是 struct 都適用。
   - 當你對 `struct` 使用 `new` 時，你其實是在要求編譯器： 「請幫我在 Stack 呼叫 `Member` 的預設建構子，並且幫我把裡面所有的欄位，都初始化成預設值！」  
   - 如果沒有用 `new`，只寫了 `Member m;`，C# 會認為這塊記憶體只是分配好了，但裡面的資料還是「未賦值 (Unassigned)」狀態。此時如果在沒給值的情況下就去讀取 `m.Name` 或 `m.Age`，編譯器會報錯。

2. 是否用 Heap 取決於「型別本身」，而不是 `new`
   - 對於 `class` (參考型別)： 當你使用 `new` 時，它確實在 Heap (堆積) 配置空間，並回傳物件的手指 (Reference / 指標) 到 Stack 給變數。
   - 對於 `struct` (實值型別)： 雖然你用了 `new`，但 C# 知道它是實值型別，它依然會被直接保留在 Stack (堆疊) 上（或者是作為陣列 / 類別的一部分，存放在外部結構所在的區域中）。
   - 總結： 對 `struct` 使用 `new`，並不會把它變成記憶體配置在 Heap 的物件。這只是在要求編譯器把它的內容好好「歸零/初始化」而已！
