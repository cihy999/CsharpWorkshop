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

## foreach 陳述式

程式碼範例：

```csharp
static public void AverageHeight() 
{
    Console.Write("請輸入總人數：");

    int num = 0;
    if (!int.TryParse(Console.ReadLine(), out num))
    {
        Console.WriteLine("\n請輸入整數！");
        return;
    }

    double[] heights = new double[num];
    for (int i = 0; i < heights.Length; i++)
    {
        Console.Write($"\n請輸入第{i+1}位身高：");

        double h = 0;
        if (double.TryParse(Console.ReadLine(), out h))
        {
            heights[i] = h;
        }
    }

    double sum = 0, avg = 0;
    foreach (double h in heights) 
    {
        sum += h;
    }
    avg = sum / heights.Length;
    Console.WriteLine($"\n=== {num} 位平均身高：{avg:00.00} ===");
}
```

## 排序陣列

程式碼範例：

```csharp
static public void SortAndShowBlackPinkMember()
{
    Member[] blackPinkMembers =
    [
        new() { Name = "Jisoo", Age = 31 },
        new() { Name = "Jennie", Age = 30 },
        new() { Name = "Rosé", Age = 29 },
        new() { Name = "Lisa", Age = 28 }
    ];

    // 遞減排序(使用匿名方法來比較)
    Array.Sort(blackPinkMembers, (x, y) => 
    {
        // 當前方的 x 比後方的 y 還大，就回傳 -1，讓 x 排到前面
        if (x.Age > y.Age) return -1;
        // 當前方的 x 比後方的 y 還小，就回傳 1，讓 x 排到後面
        else if (x.Age < y.Age) return 1;
        // 如果一樣大，回傳 0，維持原本的相對順序
        return 0;
    });
    Console.WriteLine("== BLACKPINK 成員(遞減排序) ==\n");
    Console.WriteLine("姓名\t年齡");
    Console.WriteLine("==========");
    foreach (var m in blackPinkMembers)
    {
        Console.WriteLine($"{m.Name}\t{m.Age}");
    }

    Console.WriteLine("");

    // 遞增排序
    // 使用int.CompareTo()可以縮減實作行數
    Array.Sort(blackPinkMembers, (x, y) => x.Age.CompareTo(y.Age));
    Console.WriteLine("== BLACKPINK 成員(遞增排序) ==\n");
    Console.WriteLine("姓名\t年齡");
    Console.WriteLine("==========");
    foreach (var m in blackPinkMembers)
    {
        Console.WriteLine($"{m.Name}\t{m.Age}");
    }

    Console.WriteLine("");

    // 自訂排序
    Array.Sort(blackPinkMembers, CompareBlackPinkMember);
    Console.WriteLine("== BLACKPINK 成員(自訂排序) ==\n");
    Console.WriteLine("姓名\t年齡");
    Console.WriteLine("==========");
    foreach (var m in blackPinkMembers)
    {
        Console.WriteLine($"{m.Name}\t{m.Age}");
    }
}

static private int CompareBlackPinkMember(Member x, Member y) 
{
    int result = 0;

    // 1.排序名字
    if (string.IsNullOrEmpty(x.Name) && string.IsNullOrEmpty(y.Name))
        result = 0;
    else if (string.IsNullOrEmpty(x.Name))
        result = -1;
    else if (string.IsNullOrEmpty(y.Name))
        result = 1;
    else
    {
        // 使用字串內建的比較，可以按字母排序(看字元編碼的數字大小)
        //result = x.Name.CompareTo(y.Name);
        // 使用字串比較，自選規則: 忽略大小寫
        result = string.Compare(x.Name, y.Name, StringComparison.CurrentCultureIgnoreCase);
        // 名字長度比較
        //result = x.Name.Length.CompareTo(y.Name.Length);
    }

    if (result != 0) return result;

    // 2.若名稱相同，排序年齡
    result = x.Age.CompareTo(y.Age);

    return result;
}
```

## 不規則陣列(Jagged Array)

```csharp
static public void ShowJaggedArray() 
{ 
    // 建立不規則陣列
    double[][] amt = new double[3][];
    amt[0] = new double[] { 1100, 2200, 3300 };
    amt[1] = new double[] { 1500, 2500 };
    amt[2] = new double[] { 1000, 2000, 3000, 4000 };
    string[] companyNames = new string[] { "台北", "台中", "高雄" };
    string[] parts = new string[] { "第一處", "第二處", "第三處", "第四處" };
    double[] sums = new double[] { 0.0, 0.0, 0.0 };
    double total = 0;

    Console.WriteLine($"\t{parts[0]}\t{parts[1]}\t{parts[2]}\t{parts[3]}   (單位：千元)");
    for (int i = 0; i < amt.Length; i++)
    {
        Console.Write($"{companyNames[i]}");
        for (int j = 0; j < amt[i].Length; j++)
        {
            Console.Write($"\t{amt[i][j]}");
            sums[i] += amt[i][j];
        }
        total += sums[i] * 1000;

        Console.WriteLine();
    }

    Console.WriteLine();

    for (int n = 0; n < sums.Length; n++)
    {
        sums[n] *= 1000;
        Console.WriteLine($"{companyNames[n]}分公司營業額：{sums[n]:c}\t營業率：{sums[n]/total:p}");
    }

    Console.WriteLine($"總營業額：{total:c}元");
}
```

# C# 陣列測驗 (Ch04)

準備好接受挑戰了嗎？來看看你對剛剛這些觀念的熟悉程度！我根據你的筆記和程式碼設計了三道題目，請試著回答看看（**先不要偷看答案喔，直接回覆你的想法即可**）：

## 題目一：🤔 邏輯選擇題 (Multiple Choice)

**主題：關於 `struct` 與陣列的記憶體配置**

在你的筆記中提到了使用 `new()` 來初始化 `struct Member`，例如：

```csharp
Member[] blackPinkMembers = new Member[4]
{
    new() { Name = "Jisoo", Age = 31 },
    // ...略
};
```

請問在 C# 中，對 `struct`（實值型別）使用 `new` 關鍵字，背後代表的真實意義是什麼？

- **(A)** 將該 `struct` 轉換為參考型別 (Reference Type)，並像 `class` 一樣在 Heap (堆積) 中分配獨立的記憶體。
- **(B)** 呼叫該 `struct` 的建構子將所有欄位初始化為預設值，且記憶體依然保留在 Stack (堆疊) 上（或隨著陣列連續分配）。
- **(C)** `new` 只是編譯器的語法糖，實際上它完全不做事，如果你沒手動給值，欄位會處於「未賦值 (Unassigned)」的報錯狀態。
- **(D)** 為了支援 C# 12 的集合表達式 `[]` 所做的妥協，只有在陣列裡才需要寫 `new`。

## 題目一作答

**(B)** 呼叫該 `struct` 的建構子將所有欄位初始化為預設值，且記憶體依然保留在 Stack (堆疊) 上（或隨著陣列連續分配）。

---

## 題目二：🔍 程式碼填空 (Code Completion)

**主題：陣列的自訂排序邏輯**

在你的 `Program.cs` 的 `SortAndShowBlackPinkMember` 方法中，你使用了匿名方法（Lambda）來對 `blackPinkMembers` 陣列進行**遞減排序**（年齡數字大的排在前面）。
請觀察下方的程式碼，並告訴我 `(1)` 與 `(2)` 的空格應該填入什麼數字，才能達到「遞減」的正確效果：

```csharp
Array.Sort(blackPinkMembers, (x, y) => 
{
    // 當前方的 x 比後方的 y 還大，要讓 x 排到前面
    if (x.Age > y.Age) return ____(1)____; 

    // 當前方的 x 比後方的 y 還小，要讓 x 排到後面
    else if (x.Age < y.Age) return ____(2)____; 

    // 如果一樣大，維持原本相對順序
    return 0;
});
```

## 題目二作答

```csharp
Array.Sort(blackPinkMembers, (x, y) => 
{
    // 當前方的 x 比後方的 y 還大，要讓 x 排到前面
    if (x.Age > y.Age) return -1; 

    // 當前方的 x 比後方的 y 還小，要讓 x 排到後面
    else if (x.Age < y.Age) return 1; 

    // 如果一樣大，維持原本相對順序
    return 0;
});
```

---

## 題目三：⚠️ 副作用與極端值辨識 (Edge Cases)

**主題：字串排序的潛在風險**

在你的 `CompareBlackPinkMember` 自訂排序函式中，你在比較名字之前，特別寫了一大段針對 `null` 或空字串的防呆邏輯：

```csharp
if (string.IsNullOrEmpty(x.Name) && string.IsNullOrEmpty(y.Name))
    result = 0;
else if (string.IsNullOrEmpty(x.Name))
    result = -1; // 讓沒有名字的人排到前面
else if (string.IsNullOrEmpty(y.Name))
    result = 1;
else
{
    // 兩者皆非空值，才執行正常的字串比較...
}
```

**問題**：

如果今天一位粗心的工程師**拔除掉這段 `string.IsNullOrEmpty` 的檢查**，直接對所有成員執行 `x.Name.CompareTo(y.Name)` 或 `string.Compare(...)`。當不小心建立出一個 `new Member { Name = null, Age = 20 }` 的成員並進行排序時，程式碼預期會發生什麼事情？這段防呆邏輯救了我們什麼？

## 題目三作答

1. `x.Name.CompareTo(y.Name)` 應該會出現 null 相關的例外，導致程式終止。
2. `string.Compare(...)` 可能會出現 null 相關的例外，但如果內部有檢查的話，或許不會出現。
   - 關於 string.Compare(...)： C# 的靜態方法 string.Compare() 內部確實有做 null 檢查（它是 null-safe 的）。在 C# 的預設規則中，null 會被視為比任何字串都還要「小」，而兩個 null 比較會視為相等 (0)。所以如果你拔掉防呆只用 string.Compare，程式不會當機，而是會自動把 null 排在最前面。
3. `new Member { Name = null, Age = 20 }` 若有檢查 `string.IsNullOrEmpty` 的話，就會進入 null 的比對，看是雙方沒名字等於0，所以一樣，還是誰沒有名字就排到後面。

---
