# 從 List 到 yield

開始上課請開啟 [HTML 短課程](lessons/0001-list-basics.html)，先預測再核對；複習可看 [List 基礎速查](reference/list-basics.html)。本文件保留完整草稿與原始素材。

本文件為暫存課程教材，Ticket 01 已於 2026-10-24 完成教材驗收；課程內容先集中於此暫存工作區，整套課程完成後由 Ticket 10 統一歸檔。主線由整數集合開始，再逐步銜接列舉與 yield。現已備妥第一單元；後續單元尚待編寫。原有回合事實說明保留於文末，供後續整理。

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

下一單元將說明 `List<int>` 中的型別讀法與介面約定，再銜接可列舉序列。本單元不需要先理解 yield 或列舉器的內部運作。其他集合概念可參考 [列舉器與集合](<../../../../docs/topics/collections/C%23筆記 - 列舉器與集合.md>)。

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
