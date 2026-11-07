# 從 List 到 yield

開始上課請開啟 [HTML 短課程](lessons/0001-list-basics.html)，先預測再核對；複習可看 [List 基礎速查](reference/list-basics.html)。第二單元見 [單元 2](#unit-02)。本文件保留完整草稿與原始素材。

本文件為暫存課程教材，Ticket 01 已於 2026-10-24 完成教材驗收；Ticket 02 已於 2026-11-07 完成教材驗收。課程內容先集中於此暫存工作區，整套課程完成後由 Ticket 10 統一歸檔。主線由整數集合開始，再逐步銜接列舉與 yield。現已備妥第一、二單元；後續單元尚待編寫。原有回合事實說明保留於文末，供後續整理。

範例專案：[YieldTutorial](YieldTutorial/)。

<a id="unit-01"></a>

## 單元 1：List 如何保存與提供多個值？

適用版本：本單元以 .NET 10、SDK 10.0.402 驗證。驗證日期：2026-10-24。

### 簡短說明

假設要保存 `10`、`20`、`30` 三個整數，可以建立一個 `List<int>`。本單元先將它讀作「保存 int 的清單」；泛型與介面的細節留到下一單元。

```csharp
List<int> numbers = new List<int> { 10, 20, 30 };
```

左邊宣告變數 `numbers` 的型別，右邊建立清單並放入初始元素。`{ 10, 20, 30 }` 表示起初按此順序加入這三個值。

| 寫法 | 用途 | 本例的效果 |
|---|---|---|
| `new List<int> { 10, 20, 30 }` | 建立並保存多個值 | 清單起初有三個元素 |
| `numbers.Add(40)` | 在尾端新增一個值 | 清單變成 10、20、30、40 |
| `numbers[0]` | 讀取指定位置的值 | 讀到第一個值 10 |
| `foreach (int number in numbers)` | 逐一取得清單中的值並執行迴圈內容 | 每次將一個值放入區域變數 `number` |

索引從 **0** 開始，因此前三個元素的索引是 0、1、2。新增 40 後，它的索引才是 3。`numbers[1] = 25;` 則是把第二個元素改成 25，不是新增元素。[List 官方說明](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-10.0)、[Add](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-10.0)、[索引讀寫](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.item?view=net-10.0)。

保存資料與走訪資料是不同的用途：清單保存元素；索引讀取某個位置；`foreach` 逐一走訪。走訪本身不會把元素取走，這份清單仍保存原來的值。本單元的 `foreach` 只輸出數字，不修改清單。[foreach 官方說明](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement)。

### 範例

以下是 [ListBasics.cs](YieldTutorial/ListBasics.cs) 中 `Run()` 的內容。專案入口已負責呼叫這個方法。

```csharp
List<int> numbers = new List<int> { 10, 20, 30 };

Console.WriteLine("新增前：");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

numbers.Add(40);

Console.WriteLine("索引 0：");
Console.WriteLine(numbers[0]);
Console.WriteLine("索引 3：");
Console.WriteLine(numbers[3]);

Console.WriteLine("新增後：");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

`Console.WriteLine(...)` 每次輸出一行。兩個 `foreach` 都從清單開頭走訪到結尾；第二次走訪時，清單已經加入 40。

### 預測輸出

先不執行程式，寫下你的預測：

1. 「新增前」與「新增後」各會列出哪些數字？
2. `numbers[0]` 與 `numbers[3]` 各會輸出什麼？為什麼索引 3 是第四個元素？
3. 第一個 `foreach` 結束後，10、20、30 還在清單裡嗎？

### 執行觀察

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01
```

先與自己的預測比較，再查看下方解答。觀察兩件事：`Add` 改變清單內容；索引取值與 `foreach` 只是讀取這份清單。

### 修改練習

以範例中的清單為起點，在 `numbers.Add(40);` 後、開始最後一次走訪前：

1. 把第二個元素由 20 改成 25。
2. 在尾端加入 50。
3. 輸出索引 1 的值，再以 `foreach` 輸出完整清單。

先預測五個元素的順序，再修改與執行。這裡的修改均發生在走訪之前。

解釋題：請用自己的話說明「清單保存資料」、「索引取值」、「foreach 走訪」的差異，並指出範例中各自對應的程式碼。

### 解答與判準

#### 預測題解答

`01` 實測輸出如下：

```text
新增前：
10
20
30
索引 0：
10
索引 3：
40
新增後：
10
20
30
40
```

新增前有三個元素；`Add(40)` 把第四個元素加入尾端。索引從 0 起算，所以索引 3 讀到 40。第一輪走訪只輸出元素，沒有移除它們，因此第二輪仍會看到 10、20、30。

#### 修改題參考解

以下是同專案 `RunExerciseAnswer()` 的內容，可直接以 `01-answer` 選項執行：

```csharp
List<int> numbers = new List<int> { 10, 20, 30 };
numbers.Add(40);

numbers[1] = 25;
numbers.Add(50);

Console.WriteLine("修改後的索引 1：");
Console.WriteLine(numbers[1]);
Console.WriteLine("修改後的完整清單：");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

```powershell
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01-answer
```

實測輸出：

```text
修改後的索引 1：
25
修改後的完整清單：
10
25
30
40
50
```

`numbers[1] = 25` 替換第二個值，元素數量不變；`Add(50)` 才增加一個元素。

#### 解釋題判準

能清楚表達以下三點即可，不要求背誦原句：

- 保存：建立的 `List<int>` 保存多個整數，`Add` 可在尾端加入元素。
- 索引：`numbers[0]` 指定第一個位置，只讀取一個值；讀取不會移除元素。`numbers[1] = 25` 則改寫第二個位置。
- 走訪：`foreach` 在本例中按清單順序逐一讀取元素，每次用 `number` 接住一個值並輸出；它不是建立清單或新增元素的動作。

### 限制與相關概念

索引必須落在現有元素範圍內。三個元素時有效索引為 0～2；在 `Add(40)` 前讀取 `numbers[3]` 會擲出 `ArgumentOutOfRangeException`，不會自動建立第四個元素。空清單也不能讀取索引 0。[索引範圍與例外](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.item?view=net-10.0#exceptions)。

[下一單元](#unit-02)將說明 `List<int>` 中的型別讀法與介面約定，再銜接可列舉序列。本單元不需要先理解 yield 或列舉器的內部運作。其他集合概念可參考 [列舉器與集合](<../../../../docs/topics/collections/C%23筆記 - 列舉器與集合.md>)。

<a id="unit-02"></a>

## 單元 2：為什麼 List 可以交給 IEnumerable？

開始上課請開啟 [第二單元 HTML 短課程](../02-ienumerable-contract/lessons/0002-ienumerable-contract.html)；複習可看 [IEnumerable 約定速查](../02-ienumerable-contract/reference/ienumerable-contract.html)。

適用版本：本單元以 .NET 10、SDK 10.0.402 驗證。驗證日期：2026-11-07。

### 簡短說明

原案例的方法宣告是 `private static IEnumerable<int> EnumerateRoundFacts(...)`。要讀懂它，需要三個讀法：角括號裡的型別、介面約定，以及方法回傳型別。

**泛型的最小讀法。** `List<T>` 的 `T` 是型別參數，使用時填入實際型別；`List<int>` 讀作「元素型別是 int 的清單」，`IEnumerable<int>` 讀作「可逐一取得 int 的序列」。角括號內的型別決定可以放入或取出哪種資料，例如不能把字串加入 `List<int>`。[官方：泛型型別與方法](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics)。

**介面是能力約定。** 介面列出一組成員，實作它的類別必須提供這些成員；使用介面的程式只依賴約定，不必知道實際類別。介面本身不能直接 `new` 出物件。[官方：介面](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces)。`IEnumerable<T>` 的約定是「提供列舉器，讓呼叫端逐一走訪」；`List<T>` 的宣告列出它實作 `IEnumerable<T>`，所以 List 可以交給 IEnumerable 型別的變數。[官方：IEnumerable&lt;T&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-10.0)、[List&lt;T&gt; 的實作清單](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-10.0)。列舉器如何運作留到下一單元。

```csharp
List<int> list = new List<int> { 10, 20, 30 };
IEnumerable<int> sequence = list;
```

第二行**沒有建立另一份集合**，也**不是拍下當下內容的快照**。`List<int>` 是類別，屬於參考型別；變數保存的是物件參考，賦值後 `list` 與 `sequence` 指向同一個 List 物件，因此透過 `list` 做的修改，透過 `sequence` 也看得到。[官方：參考型別](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types)。

**宣告型別決定能直接寫哪些操作。** 物件仍是 List，但編譯器依變數的宣告型別檢查成員：

| 寫法 | 透過 `List<int> list` | 透過 `IEnumerable<int> sequence` |
|---|---|---|
| `foreach (int n in ...)` | 可以 | 可以 |
| `.Add(50)` | 可以 | 不能編譯（CS1061） |
| `[0]` 索引 | 可以 | 不能編譯（CS0021） |
| `.Count` 屬性 | 可以 | 不能編譯（CS0428，訊息提到方法群組是因為 LINQ 有同名的 `Count()`） |
| 賦值給 `List<int>` 變數 | 本身即是 | 不能隱含轉換（CS0266） |

錯誤代碼於 2026-11-07 以 .NET 10 SDK 在暫存專案實測；這些寫法不放入共用專案，以免專案無法建置。另外，`sequence.Count()`（有括號）可以編譯，它是 LINQ 擴充方法，不是 `IEnumerable<int>` 介面本身的成員；LINQ 不在本單元範圍。

**方法回傳型別。** 方法名稱前的型別是回傳型別：

```csharp
private static IEnumerable<int> GetNumbers(List<int> numbers)
{
    return numbers;
}
```

讀作「`GetNumbers` 接收一個 `List<int>`，回傳一個可逐一取得 int 的序列」。方法內回傳的是 List，但呼叫端只看到 `IEnumerable<int>`，因此只承諾「可以走訪」。原案例的 `EnumerateRoundFacts` 也是同樣的讀法，只是內部改用 yield 產生元素（單元 4 起說明）。

### 範例

以下是 [EnumerableContract.cs](YieldTutorial/EnumerableContract.cs) 中 `Run()` 的內容，`GetNumbers` 即上方的方法：

```csharp
List<int> list = new List<int> { 10, 20, 30 };
IEnumerable<int> sequence = list;

Console.WriteLine("同一個物件：" + object.ReferenceEquals(list, sequence));

list.Add(40);

Console.WriteLine("透過 list 列舉：");
foreach (int number in list)
{
    Console.WriteLine(number);
}

Console.WriteLine("透過 sequence 列舉：");
foreach (int number in sequence)
{
    Console.WriteLine(number);
}

IEnumerable<int> returned = GetNumbers(list);

Console.WriteLine("方法回傳的序列：");
foreach (int number in returned)
{
    Console.WriteLine(number);
}
```

`object.ReferenceEquals(a, b)` 判斷兩個變數是否參照同一個物件，回傳 `True` 或 `False`。`Add(40)` 發生在 `sequence` 賦值之後、任何列舉之前。

### 預測輸出

先不執行程式，寫下你的預測：

1. 「同一個物件」會輸出 `True` 還是 `False`？
2. `list.Add(40)` 之後，「透過 sequence 列舉」會列出 40 嗎？為什麼？
3. 「方法回傳的序列」會列出幾個數字？

### 執行觀察

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 02
```

觀察三段列舉的內容是否相同，並對照「同一個物件」的結果。

### 修改練習

1. 建立 `List<int> list = new List<int> { 1, 2, 3 };`，以 `IEnumerable<int> sequence = GetNumbers(list);` 取得序列。
2. 目標是讓 `sequence` 列舉時也看到 4。先試寫 `sequence.Add(4);`，建置並閱讀錯誤訊息，再改成可以編譯的寫法。
3. 以 `foreach` 列舉 `sequence`。

先預測輸出，再修改與執行。觀察完錯誤後刪除無法編譯的那一行，讓共用專案恢復可建置。

解釋題：請用自己的話說明，`IEnumerable<int> sequence = list;` 之後，為什麼 `list.Add(40)` 會讓 `sequence` 也列出 40？又為什麼 `sequence.Add(40)` 不能編譯？

### 解答與判準

#### 預測題解答

`02` 實測輸出如下：

```text
同一個物件：True
透過 list 列舉：
10
20
30
40
透過 sequence 列舉：
10
20
30
40
方法回傳的序列：
10
20
30
40
```

三個變數都參照同一個 List 物件，所以都列出四個數字。`sequence` 不是賦值當下的副本，列舉時讀到的是這份 List 的當前內容。

#### 修改題參考解

以下是同專案 `RunExerciseAnswer()` 的內容，可直接以 `02-answer` 選項執行：

```csharp
List<int> list = new List<int> { 1, 2, 3 };
IEnumerable<int> sequence = GetNumbers(list);

// sequence.Add(4); 無法編譯：IEnumerable<int> 沒有提供 Add。
list.Add(4);

Console.WriteLine("sequence 列舉：");
foreach (int number in sequence)
{
    Console.WriteLine(number);
}
```

```powershell
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 02-answer
```

實測輸出：

```text
sequence 列舉：
1
2
3
4
```

`sequence.Add(4)` 會得到 CS1061：`IEnumerable<int>` 未包含 `Add` 的定義。改由宣告為 `List<int>` 的 `list` 新增；`GetNumbers` 回傳的就是同一個 List，所以 `sequence` 也列出 4。

#### 解釋題判準

能清楚表達以下三點即可，不要求背誦原句：

- 同一物件：賦值只讓 `sequence` 參照同一個 List，沒有複製元素，也不是快照；修改後再列舉會看到新內容。
- 宣告型別：編譯器依變數的宣告型別決定能直接使用的成員；`IEnumerable<int>` 只約定可走訪，所以沒有 `Add`、索引或 `Count` 屬性。
- 介面關係：`List<int>` 實作了 `IEnumerable<int>`，所以能交給該型別的變數或方法回傳值。

### 限制與相關概念

- 本單元的修改都發生在列舉**之前**。在 `foreach` 走訪 List 的過程中修改同一份 List，下一次取值會擲出 `InvalidOperationException`；這與「兩次列舉之間修改」不同，留到單元 7 區分。[官方：List&lt;T&gt;.GetEnumerator 備註](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.getenumerator?view=net-10.0#remarks)。
- 並非所有 `IEnumerable<int>` 背後都是 List；陣列也實作它，yield 方法則每次列舉時才產生元素。呼叫端只依賴「可以走訪」的約定。
- 不在本單元範圍：自訂泛型型別、型別條件約束、共變數（`out T`）、明確轉型回 List、LINQ。完整介面語法可參考 [Interface 筆記](<../../../../docs/topics/csharp/C%23筆記 - Interface.md>)。

## 原有回合事實筆記（保留原文）

以下為原始素材，尚未依主線完整重編；其中外部行號與省略型別留待回合事實單元處理，不作第一單元的執行範例。

可以。先把這行理解成：

```
foreach (int fact in EnumerateRoundFacts(round))
```

意思是：

> 逐一取出這個回合成立的所有「事實」，並將每一個值放進 `fact`。
>

### `IEnumerable<int>` 是什麼？

`IEnumerable<int>` 表示：

> 一組可以被逐一列舉的 `int`。
>

它不一定是已經建立好的 `List<int>` 或陣列，只要能讓 `foreach` 逐一取得元素即可。

例如：

```csharp
IEnumerable<int> numbers = new List<int> { 10, 20, 30 };

foreach (int number in numbers)
{
    // 依序得到 10、20、30
}
```

### `yield return` 是什麼？

`yield return` 會「產出一個元素」，然後暫停函式；當 `foreach` 要下一個元素時，再從暫停處繼續。

目前的方法：

```
private static IEnumerable<int> EnumerateRoundFacts(FightingRoomBattleRoundData round)
{
    if (round.Result == BattleResult.Lose)
    {
        yield return QuestVariableDefines.RoundFactSelfLose;
    }
    else if (round.Result == BattleResult.Win)
    {
        yield return QuestVariableDefines.RoundFactSelfWin;
        if (round.CurrentHp == round.MaxHp)
            yield return QuestVariableDefines.RoundFactSelfPerfect;
    }

    // 還會繼續檢查 DoubleKo、RingOut、TimeUp……
}
```

假設玩家：

- 贏得回合
- 滿血獲勝
- 對手掉出場外

那它會依序產出：

```
RoundFactSelfWin
RoundFactSelfPerfect
RoundFactOpponentRingOut
```

第 101 行的 `foreach` 就會執行三次，每次把其中一個值放入 `fact`。

### 為什麼不用 `List<int>`？

也可以寫成：

```csharp
private static IEnumerable<int> EnumerateRoundFacts(...)
{
    var facts = new List<int>();

    facts.Add(...);
    facts.Add(...);

    return facts;
}
```

但 `yield return` 的好處是：

- 不需要自己建立及維護 `List<int>`
- 找到一個結果就能交給 `foreach`
- 很適合這種「一個回合可能符合零個、一個或多個條件」的邏輯

簡單記法：

- `IEnumerable<int>`：我可以依序提供多個 `int`
- `yield return x`：這次先提供 `x`
- `foreach`：一直取，直到沒有下一個值為止
