---
title: C#筆記 - 列舉器與集合
tags: [C#]

---

參考資料：
- [Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch08 列舉器與集合](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ)

> 資料結構(Data structure)是資料在電腦中的儲存與組織方式，設計程式時我們可依據資料的特性選擇合適的資料結構，以得到高效率的處理方式。

> 常用的資料結構，例如：陣列(Array)、串列(List)、推疊(Stack)、佇列(Queue)、鏈結串列(LinkedList)、樹(Tree)、圖形(Graph)、雜湊表(Hashtable)

# IEnumerator 列舉器

不論各種資料結構的實作方式，基本上都會希望都具備「走訪」能力。

> 走訪 (Traverse)
> 能逐一瀏覽一個資料結構內所有的元素(或稱節點)。

支援非泛型集合的走訪，比如陣列(Array)。

## 使用列舉器走訪陣列

使用非泛型的列舉器，都要加上：
```csharp
using System.Collections;
```

```csharp
using System.Collections;

namespace ConsoleCreateInstance1D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Array ary1D = Array.CreateInstance(typeof(Int32), 5);
            for (int i = ary1D.GetLowerBound(0); i <= ary1D.GetUpperBound(0); i++)
            {
                ary1D.SetValue(10 + i * 10, i);
            }

            IEnumerator enumerator = ary1D.GetEnumerator();
            int k = 0;
            int cols = ary1D.GetLength(ary1D.Rank - 1);
            while (enumerator.MoveNext())
            {
                if (k < cols)
                {
                    k++;
                }
                else 
                {
                    Console.WriteLine();
                    k = 1;
                }

                Console.Write(" {0}. ary1D[{1}] = {2} \n", k, k, enumerator.Current);
            }

            Console.Read();
        }
    }
}
```

說明：
1. `Array.CreateInstance(typeof(Int32), 5)`: 建立指定型別、元素數的陣列，可以填多維度
2. `ary1D.GetEnumerator()`: 取得該陣列的列舉器
3. `enumerator.MoveNext()`: 移動到下一個元素，若有元素就回傳 true，否則回傳 false
4. `enumerator.Current`: 當前紀錄的元素，如果是開頭列舉器，會拿到null

![IEnumerator_202507031227](https://hackmd.io/_uploads/H11aLF7Hxe.png)

### `GetEnumerator()`

```csharp
IEnumerator enumerator = myAry.GetEnumerator();

// 加入型別檢查
IEnumerator<string> enumerator2 = ((IEnumerable<string>)myAry).GetEnumerator();
```

### `MoveNext()` & `Current`

列舉器一開始會指向開頭(在第一個元素之前)，開始用第一個元素前，必須呼叫一次`MoveNext()`。

移動到下一個元素，若有元素就回傳 true，否則回傳 false。

---

## Dictionary<TKey, TValue>

```csharp
var dict = new Dictionary<long, PlayerSetting>();

// s 的型別是 KeyValuePair<long, PlayerSetting>
bool hasAI = dict.Any(s => s.Key == FightDefine.AICharId);
bool hasAI = dict.Any(s => s.Value.ControlIntent == (int)EPlayerControlIntent.FightingAi);
```

### `.Any(...)` — LINQ 方法，檢查「是否存在任一元素符合條件」

`.Any()` 是 C# LINQ 的擴充方法，它接受一個 lambda 表達式作為條件，回傳 bool：

- 只要集合中有任何一個元素符合條件 → 回傳 true
- 全部都不符合 → 回傳 false

### 搭配 .Values 或 .Keys

```csharp
// 用 .Values：s 直接就是 PlayerSetting，不用再寫 s.Value
bool hasAI = dict.Values.Any(s => s.ControlIntent == (int)EPlayerControlIntent.FightingAi);

// 用 .Keys：k 直接就是 long
bool hasNegativeKey = dict.Keys.Any(k => k < 0);
```

需要注意的是，使用 `.Any()` 需要引用 `System.Linq` 命名空間（`using System.Linq;`）。不過在大多數 C# 專案中這個命名空間已經預設引入了。
