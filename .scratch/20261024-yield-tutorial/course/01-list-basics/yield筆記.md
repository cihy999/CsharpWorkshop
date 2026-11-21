# 從 List 到 yield

開始上課請開啟 [HTML 短課程](lessons/0001-list-basics.html)，先預測再核對；複習可看 [List 基礎速查](reference/list-basics.html)。第二單元見 [單元 2](#unit-02)，第三單元見 [單元 3](#unit-03)，第四單元見 [單元 4](#unit-04)，第五單元見 [單元 5](#unit-05)。本文件保留完整草稿與原始素材。

本文件為暫存課程教材，Ticket 01 已於 2026-10-24 完成教材驗收；Ticket 02 已於 2026-11-07 完成教材驗收；Ticket 03 已於 2026-11-07 完成教材驗收；Ticket 04 已於 2026-11-21 完成教材驗收。課程內容先集中於此暫存工作區，整套課程完成後由 Ticket 10 統一歸檔。主線由整數集合開始，再逐步銜接列舉與 yield。現已備妥第一至四單元；後續單元尚待編寫。原有回合事實說明保留於文末，供後續整理。

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

[下一單元](#unit-03)說明 `IEnumerable<T>` 交出的列舉器如何一次提供一個元素。

<a id="unit-03"></a>

## 單元 3：foreach 如何取得下一個值？

開始上課請開啟 [第三單元 HTML 短課程](../03-enumerator-protocol/lessons/0003-enumerator-protocol.html)；複習可看 [列舉器協定速查](../03-enumerator-protocol/reference/enumerator-protocol.html)。

適用版本：本單元以 .NET 10、SDK 10.0.402 驗證。驗證日期：2026-11-07。

### 簡短說明

上一單元說 `IEnumerable<T>` 約定「可以走訪」。實際走訪時有兩個角色：

| 角色 | 型別 | 負責什麼 |
|---|---|---|
| 可列舉序列 | `IEnumerable<int>` | 以 `GetEnumerator()` 交出一個新的列舉器；序列本身不記錄走到哪裡 |
| 列舉器 | `IEnumerator<int>` | 記住目前位置，以 `MoveNext()` 前進、`Current` 讀取目前元素、`Dispose()` 收尾 |

[官方：IEnumerable&lt;T&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-10.0)、[官方：IEnumerator&lt;T&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1?view=net-10.0)。

**列舉器的位置規則。** 列舉器剛建立時位於**第一個元素之前**，要先呼叫一次 `MoveNext()` 才會移到第一個元素。`MoveNext()` 回傳 `true` 表示成功移到下一個元素；回傳 `false` 表示已經越過最後一個元素，之後再呼叫也會持續回傳 `false`。[官方：MoveNext 備註](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator.movenext?view=net-10.0#remarks)。

**Current 只在 MoveNext 回傳 true 之後讀。** 官方說明在以下情況 `Current` 是**未定義**的：列舉器剛建立、尚未呼叫 `MoveNext()`；最近一次 `MoveNext()` 回傳 `false`；集合在列舉期間被修改。在兩次 `MoveNext()` 之間，`Current` 保持同一個元素。[官方：IEnumerator&lt;T&gt;.Current 備註](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1.current?view=net-10.0#remarks)。

「未定義」的意思是**規格不保證結果**，不是「會得到 null」。2026-11-07 以 .NET 10 在暫存專案實測，不同列舉器的結果各不相同：

| 讀取 `Current` 的時機 | `List<int>`（`IEnumerator<int>`） | `List<string>` | `int[]` | 透過非泛型 `IEnumerator` 讀 `List<int>` |
|---|---|---|---|---|
| 尚未 `MoveNext()` | 讀到 0 | 讀到 null | 擲出 `InvalidOperationException` | 擲出 `InvalidOperationException` |
| `MoveNext()` 回傳 `false` 之後 | 讀到 0 | 讀到 null | 擲出 `InvalidOperationException` | 擲出 `InvalidOperationException` |

這些是特定實作在指定版本的行為，不是公開約定；程式不應依賴其中任何一種。共用專案只在合法時點讀取 `Current`，以上實測不放入專案。

**用 using 收尾。** `IEnumerator<T>` 同時實作 `IDisposable`，因此有 `Dispose()`。有些列舉器背後連著檔案或資料庫等資源，走訪結束時需要釋放。[官方：IEnumerator&lt;T&gt; 實作者備註](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1?view=net-10.0#notes-to-implementers)。手動列舉時把列舉器放進 `using` 區塊：控制離開區塊時一定呼叫 `Dispose()`，即使區塊內發生例外也一樣。[官方：using 陳述式](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using)。

List 的列舉器沒有需要釋放的資源，所以本單元看不出 `Dispose()` 的效果；但手動列舉時通常不知道背後是哪一種列舉器，一律用 `using` 才正確。可觀察的清理效果留到單元 6，以 yield 方法的 `finally` 示範。

**foreach 的概念對照。** 以本單元宣告為 `IEnumerable<int>` 的 `numbers` 為例，`foreach (int number in numbers) { ... }` 可以對照成：

```csharp
using (IEnumerator<int> enumerator = numbers.GetEnumerator())
{
    while (enumerator.MoveNext())
    {
        int number = enumerator.Current;
        // 迴圈主體
    }
}
```

C# 規格把 `foreach` 定義為「取得列舉器 → 在 `try` 中以 `while (MoveNext())` 讀取 `Current` → 在 `finally` 中 `Dispose`」；`using` 也是以 `try`／`finally` 呼叫 `Dispose`，因此兩者在本例的效果相同。[C# 規格：foreach 陳述式](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#1395-the-foreach-statement)。

這只是適用本例的概念對照，不是編譯器的逐字輸出。編譯器依變數的宣告型別選擇 `GetEnumerator`：若同一份清單宣告為 `List<int>`，取得的是 [`List<int>.Enumerator`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.getenumerator?view=net-10.0)，`Dispose` 的寫法也略有不同；陣列與 `Span<T>` 另有處理方式。走訪規則相同，但本單元不展開這些差異。

### 範例

以下是 [EnumeratorProtocol.cs](YieldTutorial/EnumeratorProtocol.cs) 中 `Run()` 的內容：

```csharp
IEnumerable<int> numbers = new List<int> { 10, 20, 30 };

Console.WriteLine("foreach：");
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

Console.WriteLine("手動列舉：");
using (IEnumerator<int> enumerator = numbers.GetEnumerator())
{
    while (enumerator.MoveNext())
    {
        Console.WriteLine(enumerator.Current);
    }
}

Console.WriteLine("逐次呼叫 MoveNext：");
using (IEnumerator<int> enumerator = numbers.GetEnumerator())
{
    // 只在 MoveNext 回傳 True 之後讀取 Current。
    bool moved = enumerator.MoveNext();
    Console.WriteLine("第 1 次：" + moved + "，Current = " + enumerator.Current);

    moved = enumerator.MoveNext();
    Console.WriteLine("第 2 次：" + moved + "，Current = " + enumerator.Current);

    moved = enumerator.MoveNext();
    Console.WriteLine("第 3 次：" + moved + "，Current = " + enumerator.Current);

    moved = enumerator.MoveNext();
    Console.WriteLine("第 4 次：" + moved + "，已結束，不讀 Current");

    moved = enumerator.MoveNext();
    Console.WriteLine("第 5 次：" + moved + "，仍然結束");
}

IEnumerable<int> empty = new List<int>();

Console.WriteLine("空序列 foreach 開始");
foreach (int number in empty)
{
    Console.WriteLine(number);
}
Console.WriteLine("空序列 foreach 結束");

using (IEnumerator<int> enumerator = empty.GetEnumerator())
{
    Console.WriteLine("空序列第 1 次 MoveNext：" + enumerator.MoveNext());
}
```

「逐次呼叫」段落刻意不用迴圈，逐行寫出五次 `MoveNext()`，方便對照每次的回傳值。`bool` 與字串相接時輸出 `True`／`False`。每次 `GetEnumerator()` 都取得新的列舉器，所以第二、三段都從第一個元素之前重新開始。

### 預測輸出

先不執行程式，寫下你的預測：

1. 「foreach」與「手動列舉」兩段輸出的值與順序相同嗎？
2. 「逐次呼叫 MoveNext」的五次各回傳什麼？哪幾次之後可以讀 `Current`？
3. 「空序列 foreach 開始」與「結束」之間有輸出嗎？空序列的第一次 `MoveNext()` 回傳什麼？

### 執行觀察

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 03
```

觀察三件事：`foreach` 與手動列舉的結果一致；三個元素需要第四次 `MoveNext()` 才得知結束；空序列第一次就回傳 `False`，`foreach` 主體一次也不執行。

### 修改練習

1. 修改手動列舉，改為走訪固定資料 `2, 4, 6, 8`，保留 `using`。
2. 在迴圈中以計數變數輸出「第 n 個：值」。
3. 迴圈結束後輸出元素個數與 `MoveNext()` 的呼叫次數。

先預測 `MoveNext()` 會被呼叫幾次，再修改與執行。可以寫在 `EnumeratorProtocol.cs` 新增的方法中，或直接改 `Run()` 後再還原。

解釋題：下面的程式有兩處讀取 `Current` 的時機不合法，請指出來並說明原因；再說明為什麼手動列舉要放進 `using`。

```csharp
using (IEnumerator<int> enumerator = numbers.GetEnumerator())
{
    Console.WriteLine(enumerator.Current);      // A

    while (enumerator.MoveNext())
    {
        Console.WriteLine(enumerator.Current);  // B
    }

    Console.WriteLine(enumerator.Current);      // C
}
```

### 解答與判準

#### 預測題解答

`03` 實測輸出如下：

```text
foreach：
10
20
30
手動列舉：
10
20
30
逐次呼叫 MoveNext：
第 1 次：True，Current = 10
第 2 次：True，Current = 20
第 3 次：True，Current = 30
第 4 次：False，已結束，不讀 Current
第 5 次：False，仍然結束
空序列 foreach 開始
空序列 foreach 結束
空序列第 1 次 MoveNext：False
```

`foreach` 與手動列舉走的是同一套規則，所以值與順序相同。前三次 `MoveNext()` 各移到一個元素，之後才讀 `Current`；第四次越過結尾回傳 `False`，第五次仍是 `False`。空序列第一次 `MoveNext()` 就回傳 `False`，所以 `foreach` 主體不執行，兩行訊息之間沒有輸出。

#### 修改題參考解

以下是同專案 `RunExerciseAnswer()` 的內容，可直接以 `03-answer` 選項執行：

```csharp
IEnumerable<int> numbers = new List<int> { 2, 4, 6, 8 };
int position = 0;

using (IEnumerator<int> enumerator = numbers.GetEnumerator())
{
    while (enumerator.MoveNext())
    {
        position++;
        Console.WriteLine("第 " + position + " 個：" + enumerator.Current);
    }
}

Console.WriteLine("共 " + position + " 個元素，MoveNext 呼叫 " + (position + 1) + " 次");
```

```powershell
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 03-answer
```

實測輸出：

```text
第 1 個：2
第 2 個：4
第 3 個：6
第 4 個：8
共 4 個元素，MoveNext 呼叫 5 次
```

四個元素需要四次回傳 `True` 的 `MoveNext()`，再加一次回傳 `False` 才結束迴圈，共五次。`position++` 只在成功移動後執行，所以它等於元素個數。

#### 解釋題判準

能清楚表達以下三點即可，不要求背誦原句：

- A 不合法：列舉器剛建立，位於第一個元素之前，尚未呼叫 `MoveNext()`。
- C 不合法：`while` 結束代表最近一次 `MoveNext()` 回傳 `false`，列舉器已越過結尾。A 與 C 的 `Current` 都是未定義，實際可能讀到 0、null 或擲出例外，不能說一定是 null。B 在 `MoveNext()` 回傳 `true` 之後讀取，是合法的。
- `using`：`IEnumerator<T>` 實作 `IDisposable`；`using` 在離開區塊時一定呼叫 `Dispose()`，即使中途發生例外，讓需要清理的列舉器能釋放資源。

### 限制與相關概念

- 本單元只使用現成的列舉器，不要求手寫列舉器類別。官方的 [IEnumerator&lt;T&gt; 範例](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1?view=net-10.0#examples) 示範如何實作，列為選讀。
- `Reset()` 主要為 COM 互通而存在，實作可以直接擲出 `NotSupportedException`；要重新走訪，請再呼叫一次 `GetEnumerator()` 取得新的列舉器。
- 列舉期間修改集合會使列舉器失效，留到單元 7 區分。
- [列舉器與集合](<../../../../docs/topics/collections/C%23筆記 - 列舉器與集合.md>) 使用非泛型 `IEnumerator` 走訪陣列；其中「開頭列舉器會拿到 null」的說法不成立：2026-11-07 以 .NET 10 實測，該筆記以 `Array.CreateInstance` 建立的陣列，其非泛型列舉器在開頭與結束後讀取 `Current` 都擲出 `InvalidOperationException`。正式歸檔時由 Ticket 10 一併修正。

[下一單元](#unit-04)改由 yield 方法交出列舉器，觀察 `MoveNext()` 每次讓方法執行到哪裡。

<a id="unit-04"></a>

## 單元 4：yield 如何分次提供元素？

開始上課請開啟 [第四單元 HTML 短課程](../04-yield-suspension/lessons/0004-yield-suspension.html)；複習可看 [yield 暫停與繼續速查](../04-yield-suspension/reference/yield-suspension.html)。

適用版本：本單元以 .NET 10 驗證（SDK 11.0.100 建置 `net10.0`，於 .NET Runtime 10.0.15 執行）。驗證日期：2026-11-21。

### 簡短說明

原案例的 `EnumerateRoundFacts` 宣告回傳 `IEnumerable<int>`，方法裡卻沒有建立 List，而是寫了好幾個 `yield return`。本單元比較兩種「讓方法交出多個值」的寫法：

| | 一般 `return` | `yield return` |
|---|---|---|
| 交出什麼 | 整份結果，例如已放好三個值的 List，一次交出 | 序列的下一個元素，一次一個 |
| 交出之後 | 方法執行結束，控制回到呼叫端 | 方法暫停；下次要求元素時，從這一行之後繼續 |
| 區域變數 | 方法結束後不再使用 | 暫停期間保留，繼續時沿用原本的值 |
| 方法何時結束 | 執行到 `return` | 執行到方法結尾（或 `yield break`，單元 6） |

[官方：return 陳述式](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements#the-return-statement)、[官方：yield 陳述式](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield)。

**迭代器方法。** 主體中有 `yield return` 的方法稱為迭代器（iterator）。它的回傳型別必須是 `IEnumerable<T>`、`IEnumerator<T>` 或對應的非泛型介面；本課程使用 `IEnumerable<int>`。`yield return` 後面一定要接一個運算式，該值就是這次交出的元素。[官方：迭代器](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators)、[官方：迭代器方法的結構規則](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/iterator-yield#structure-of-an-iterator-method)。

**兩種寫法不能混在同一個方法。** C# 規格規定，迭代器區塊中出現 `return` 陳述式是編譯錯誤；要交出值就用 `yield return`。[C# 規格：區塊（iterator block）](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#133-blocks)。2026-11-21 以 .NET 10 在暫存專案實測：

| 寫法 | 結果 |
|---|---|
| 同一個方法裡有 `yield return 10;` 與 `return new List<int> { 20 };` | CS1622：無法從迭代器傳回值 |
| 同一個方法裡有 `yield return 10;` 與不帶值的 `return;` | 同樣是 CS1622 |
| 回傳型別寫成 `List<int>`，主體使用 `yield return` | CS1624：`List<int>` 不是 Iterator 介面類型 |

這些寫法不放入共用專案，以免專案無法建置。本單元把兩種寫法放在兩個各自合法的方法中對照。

**對上單元 3 的列舉器。** 使用序列的一方稱為**消費端**（例如 `foreach`），迭代器方法稱為**產生端**。消費端每呼叫一次 `MoveNext()`，產生端就往下執行一段：

| 產生端執行到 | `MoveNext()` 回傳 | `Current` |
|---|---|---|
| 一個 `yield return 運算式;` | `true` | 這個運算式的值 |
| 方法結尾 | `false` | 未定義，不讀 |

第一次 `MoveNext()` 從方法開頭執行；之後每一次都從上次暫停的那個 `yield return` 的**下一行**繼續，直到遇到下一個 `yield return` 或方法結尾。暫停時，所有區域變數的值與暫停位置都會保存；繼續時還原這些值。兩次要求之間，產生端不會自己往下跑。[C# 規格：MoveNext 的行為](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#151552-advance-the-enumerator)、[官方：迭代器的執行](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield#execution-of-an-iterator)。

本單元一律在同一行呼叫迭代器方法並取得列舉器，只觀察「每次要求元素」時產生端執行到哪裡。呼叫方法、`GetEnumerator()` 與第一次 `MoveNext()` 各自的時間點，留到單元 5。

### 範例

以下是 [YieldSuspension.cs](YieldTutorial/YieldSuspension.cs) 中的三個產生端方法。輸出中以 `[產生端]` 開頭的行由這些方法印出：

```csharp
private static IEnumerable<int> GetNumbersWithReturn()
{
    List<int> numbers = new List<int>();
    numbers.Add(10);
    numbers.Add(20);
    numbers.Add(30);
    Console.WriteLine("[產生端] 三個值都放進 List，用 return 一次交出");
    return numbers;
}

private static IEnumerable<int> GetNumbersWithYield()
{
    Console.WriteLine("[產生端] yield return 10");
    yield return 10;
    Console.WriteLine("[產生端] yield return 20");
    yield return 20;
    Console.WriteLine("[產生端] yield return 30");
    yield return 30;
    Console.WriteLine("[產生端] 走到方法結尾");
}

private static IEnumerable<int> CountUp()
{
    int count = 0;
    Console.WriteLine("[產生端] 從方法開頭執行，count = " + count);

    count += 10;
    Console.WriteLine("[產生端] 位置 1：產出 count = " + count);
    yield return count; // 位置 1

    Console.WriteLine("[產生端] 從位置 1 之後繼續，count = " + count);
    count += 10;
    Console.WriteLine("[產生端] 位置 2：產出 count = " + count);
    yield return count; // 位置 2

    Console.WriteLine("[產生端] 從位置 2 之後繼續，count = " + count);
    count += 10;
    Console.WriteLine("[產生端] 位置 3：產出 count = " + count);
    yield return count; // 位置 3

    Console.WriteLine("[產生端] 從位置 3 之後繼續，走到方法結尾");
}
```

兩個前段方法都回傳 `IEnumerable<int>`，交出的值也都是 10、20、30；差別只在方法內部用 `return` 還是 `yield return`。`CountUp` 用區域變數 `count` 計數，註解標出三個 `yield return` 的位置。

消費端是 `Run()` 的內容。前兩段用 `foreach`，第三段沿用單元 3 的逐次呼叫寫法：

```csharp
Console.WriteLine("return 寫法：");
foreach (int number in GetNumbersWithReturn())
{
    Console.WriteLine("[消費端] 取得 " + number);
}

Console.WriteLine("yield return 寫法：");
foreach (int number in GetNumbersWithYield())
{
    Console.WriteLine("[消費端] 取得 " + number);
}

Console.WriteLine("逐次要求 CountUp 的元素：");
using (IEnumerator<int> enumerator = CountUp().GetEnumerator())
{
    Console.WriteLine("[消費端] 要求第 1 個");
    bool moved = enumerator.MoveNext();
    Console.WriteLine("[消費端] MoveNext = " + moved + "，Current = " + enumerator.Current);
    Console.WriteLine("[消費端] 先處理別的事，還沒要求下一個");

    Console.WriteLine("[消費端] 要求第 2 個");
    moved = enumerator.MoveNext();
    Console.WriteLine("[消費端] MoveNext = " + moved + "，Current = " + enumerator.Current);

    Console.WriteLine("[消費端] 要求第 3 個");
    moved = enumerator.MoveNext();
    Console.WriteLine("[消費端] MoveNext = " + moved + "，Current = " + enumerator.Current);

    Console.WriteLine("[消費端] 要求第 4 個");
    moved = enumerator.MoveNext();
    Console.WriteLine("[消費端] MoveNext = " + moved + "，序列結束，不讀 Current");
}
```

### 預測輸出

先不執行程式，寫下你的預測：

1. 「return 寫法」與「yield return 寫法」中，`[產生端]` 與 `[消費端]` 的行各自如何排列？兩者取得的值與順序相同嗎？
2. 「逐次要求」的第 2 次要求時，產生端印出的第一行是什麼？`count` 是多少？
3. 「先處理別的事」那行之後、「要求第 2 個」之前，產生端會印出任何東西嗎？
4. 第 4 次要求時 `MoveNext()` 回傳什麼？產生端最後印出哪一行？

### 執行觀察

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 04
```

觀察三件事：return 版的產生端訊息在所有值之前只出現一次，yield 版則與消費端交錯；兩次要求之間產生端沒有輸出；每次繼續時印出的 `count` 都是上次產出時的值。

### 修改練習

1. 在 `YieldSuspension.cs` 新增方法 `CountUpExercise()`，內容先複製 `CountUp`。
2. 把 `count` 的起始值改為 1，三處 `count += 10` 都改為 `count *= 2`。
3. 在「從位置 1 之後繼續」那行之後、下一次計算之前，插入一行標記與一個新的產出 `yield return count + 100;`。
4. 另寫消費端，用 `foreach` 取完所有值，每次輸出「取得 n」，最後輸出共取得幾個值。

先預測：共會取得幾個值、依序是什麼？新產出之後繼續時，`count` 是 2 還是 102？要執行時，可暫時在 `Program.cs` 加一個選項（例如 `04-exercise`），練習後再還原。

解釋題：

1. 「逐次要求」的第 2 次要求時，產生端印出 `count = 10`。為什麼不是 0，也不是 20？
2. 下面的方法為什麼不能編譯？若要讓它依序產出 10、20，該怎麼改？

```csharp
private static IEnumerable<int> Broken()
{
    yield return 10;
    return new List<int> { 20 };
}
```

### 解答與判準

#### 預測題解答

`04` 實測輸出如下：

```text
return 寫法：
[產生端] 三個值都放進 List，用 return 一次交出
[消費端] 取得 10
[消費端] 取得 20
[消費端] 取得 30
yield return 寫法：
[產生端] yield return 10
[消費端] 取得 10
[產生端] yield return 20
[消費端] 取得 20
[產生端] yield return 30
[消費端] 取得 30
[產生端] 走到方法結尾
逐次要求 CountUp 的元素：
[消費端] 要求第 1 個
[產生端] 從方法開頭執行，count = 0
[產生端] 位置 1：產出 count = 10
[消費端] MoveNext = True，Current = 10
[消費端] 先處理別的事，還沒要求下一個
[消費端] 要求第 2 個
[產生端] 從位置 1 之後繼續，count = 10
[產生端] 位置 2：產出 count = 20
[消費端] MoveNext = True，Current = 20
[消費端] 要求第 3 個
[產生端] 從位置 2 之後繼續，count = 20
[產生端] 位置 3：產出 count = 30
[消費端] MoveNext = True，Current = 30
[消費端] 要求第 4 個
[產生端] 從位置 3 之後繼續，走到方法結尾
[消費端] MoveNext = False，序列結束，不讀 Current
```

兩種寫法取得的值與順序相同。return 版在 `return` 前已把三個值放進 List，方法隨即結束，之後 `foreach` 只是走訪這份 List。yield 版每交出一個值就暫停，消費端處理完、要求下一個時，才從暫停處繼續，因此兩端的輸出交錯；最後一次要求讓方法走到結尾，`foreach` 才結束。

逐次要求時，每次 `MoveNext()` 讓產生端從上次暫停處的下一行執行到下一個 `yield return`；「先處理別的事」之後沒有產生端輸出，因為還沒有人要求下一個。第 4 次要求時，產生端從位置 3 之後執行到方法結尾，`MoveNext()` 回傳 `False`。

#### 修改題參考解

以下是同專案 `CountUpModified()` 與 `RunExerciseAnswer()` 的內容，可直接以 `04-answer` 選項執行：

```csharp
private static IEnumerable<int> CountUpModified()
{
    int count = 1;
    Console.WriteLine("[產生端] 從方法開頭執行，count = " + count);

    count *= 2;
    Console.WriteLine("[產生端] 位置 1：產出 count = " + count);
    yield return count; // 位置 1

    Console.WriteLine("[產生端] 從位置 1 之後繼續，count = " + count);
    Console.WriteLine("[產生端] 新增的位置：產出 count + 100 = " + (count + 100));
    yield return count + 100; // 新增的位置

    Console.WriteLine("[產生端] 從新增的位置之後繼續，count = " + count);
    count *= 2;
    Console.WriteLine("[產生端] 位置 2：產出 count = " + count);
    yield return count; // 位置 2

    Console.WriteLine("[產生端] 從位置 2 之後繼續，count = " + count);
    count *= 2;
    Console.WriteLine("[產生端] 位置 3：產出 count = " + count);
    yield return count; // 位置 3

    Console.WriteLine("[產生端] 從位置 3 之後繼續，走到方法結尾");
}
```

```csharp
int received = 0;

foreach (int number in CountUpModified())
{
    received++;
    Console.WriteLine("[消費端] 取得 " + number);
}

Console.WriteLine("[消費端] 共取得 " + received + " 個值");
```

```powershell
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 04-answer
```

實測輸出：

```text
[產生端] 從方法開頭執行，count = 1
[產生端] 位置 1：產出 count = 2
[消費端] 取得 2
[產生端] 從位置 1 之後繼續，count = 2
[產生端] 新增的位置：產出 count + 100 = 102
[消費端] 取得 102
[產生端] 從新增的位置之後繼續，count = 2
[產生端] 位置 2：產出 count = 4
[消費端] 取得 4
[產生端] 從位置 2 之後繼續，count = 4
[產生端] 位置 3：產出 count = 8
[消費端] 取得 8
[產生端] 從位置 3 之後繼續，走到方法結尾
[消費端] 共取得 4 個值
```

插入一個 `yield return` 就多一個元素，共四個值：2、102、4、8。`yield return count + 100` 只把運算式的結果交給消費端，沒有改寫 `count`，所以繼續時 `count` 仍是 2，下一次 `count *= 2` 得到 4。

#### 解釋題判準

能清楚表達以下三點即可，不要求背誦原句：

- `count = 10`：第 1 次 `MoveNext()` 執行到位置 1 時暫停，保存了 `count` 的值 10 與暫停位置。第 2 次從位置 1 的下一行繼續，不會回到開頭重新執行 `int count = 0`；印出那行在 `count += 10` 之前，所以是 10，之後才變成 20。
- `Broken` 不能編譯：方法中有 `yield return` 就是迭代器，迭代器中不能出現 `return` 陳述式，不帶值的 `return;` 也不行（CS1622）。改為 `yield return 20;`；若想一次回傳整份 List，則整個方法改用 List 與 `return`，不使用 `yield return`。
- 對照：一般 `return` 交出整份結果後方法就結束；`yield return` 每次交出一個元素後暫停，等下一次要求才繼續。

### 限制與相關概念

- 保存的是變數本身的值。若區域變數參照 List 等物件，物件內容在暫停期間仍可能被其他程式修改；C# 規格在 MoveNext 的說明中特別註記這點。重複列舉與可變輸入留到單元 7。
- 本單元不區分呼叫方法、取得列舉器與第一次 `MoveNext()` 的時間點，也不討論只取部分元素時方法執行到哪裡，這些留到單元 5。`yield break`、`foreach` 中途 `break` 與 `finally` 清理留到單元 6。
- 迭代器有使用限制：參數不能是 `ref`、`out` 或 `in`，不能寫在 lambda 中，`yield return` 不能放在 `catch`、`finally` 或帶有 `catch` 的 `try` 中。[官方：yield 的使用限制](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield)。
- C# 規格只描述列舉器物件的外部行為，並說明它「通常」由編譯器產生的類別實作，未規定作法。本單元只觀察公開行為（`MoveNext()` 的回傳值、`Current` 與輸出），不依賴產生的型別名稱、欄位或反編譯結果。編譯器狀態機為選讀，不在主線範圍。[C# 規格：列舉器物件](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#15155-enumerator-objects)。
- 回到原案例：`EnumerateRoundFacts` 在勝利且滿血時，會依序經過兩個 `yield return`，所以 `foreach` 先取得 `RoundFactSelfWin`，再取得 `RoundFactSelfPerfect`；之後從暫停處往下檢查其他條件。完整案例在單元 9 整理。

[下一單元](#unit-05)把時間往前推：呼叫迭代器方法、取得列舉器與第一次 `MoveNext()` 時，方法主體各執行到哪裡。

<a id="unit-05"></a>

## 單元 5：迭代器方法何時真正執行？

開始上課請開啟 [第五單元 HTML 短課程](../05-deferred-execution/lessons/0005-deferred-execution.html)；複習可看 [迭代器延遲執行速查](../05-deferred-execution/reference/deferred-execution.html)。

適用版本：本單元以 .NET 10 驗證（SDK 11.0.100 建置 `net10.0`，於 .NET Runtime 10.0.15 執行）。驗證日期：2026-11-21。

### 簡短說明

單元 4 一律在同一行呼叫迭代器方法並取得列舉器，只看「每次要求元素」時產生端執行到哪裡。本單元把這一行拆開，分成三個時間點觀察：

| 消費端做的事 | 迭代器方法主體 |
|---|---|
| 呼叫迭代器方法 | 不執行；只回傳一個可列舉的物件 |
| 對它呼叫 `GetEnumerator()` | 不執行；只回傳列舉器 |
| 第 1 次 `MoveNext()` | 從方法開頭執行到第一個 `yield return`（或方法結尾） |
| 之後每次 `MoveNext()` | 從上次暫停處的下一行執行到下一個 `yield return`（或方法結尾） |
| 不再要求 | 暫停處之後的程式不執行 |

呼叫迭代器方法時不執行主體、開始列舉才執行，稱為**延遲執行**（deferred execution）。消費端從不列舉，主體就一行都不會執行；只要求部分元素，主體就停在最後一次交出元素的 `yield return`。[官方：Iterators 的延遲產生元素](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators#understand-deferred-element-production)、[官方：迭代器的執行](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield#execution-of-an-iterator)。

C# 規格把「呼叫」與「取得列舉器」分開描述：回傳可列舉介面的迭代器被呼叫時，不立即執行迭代器區塊，而是回傳一個可列舉物件；它的 `GetEnumerator()` 回傳列舉器；列舉器第一次 `MoveNext()` 時，才從迭代器區塊的開頭執行。[C# 規格：Enumerable objects](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#15156-enumerable-objects)、[C# 規格：MoveNext 的行為](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#151552-advance-the-enumerator)。

**結論只針對迭代器方法。** 延遲執行來自主體中的 `yield return`，不是回傳型別 `IEnumerable<int>` 造成的。單元 4 的 return 版同樣回傳 `IEnumerable<int>`，呼叫時就跑完整個主體。官方文件也提醒，迭代器與集合都可以公開 `IEnumerable<T>`，只看介面無法推斷執行時機。LINQ 等其他方法的延遲行為各有規則，不在本單元範圍。

### 範例

以下是 [DeferredExecution.cs](YieldTutorial/DeferredExecution.cs) 中的產生端。`Steps()` 是迭代器；`StepsWithList()` 是同樣回傳 `IEnumerable<int>` 的一般方法，作為對照：

```csharp
private static IEnumerable<int> Steps()
{
    Console.WriteLine("[產生端] 進入方法主體");
    Console.WriteLine("[產生端] 產出 1");
    yield return 1;

    Console.WriteLine("[產生端] 從產出 1 之後繼續，產出 2");
    yield return 2;

    Console.WriteLine("[產生端] 從產出 2 之後繼續，產出 3");
    yield return 3;

    Console.WriteLine("[產生端] 從產出 3 之後繼續，走到方法結尾");
}

private static IEnumerable<int> StepsWithList()
{
    Console.WriteLine("[產生端] 進入 StepsWithList() 主體，建立 List");
    List<int> numbers = new List<int> { 1, 2, 3 };
    Console.WriteLine("[產生端] 用 return 交出 List");
    return numbers;
}
```

消費端把每個動作前後都印出標記。`RequestElements` 取得列舉器後，呼叫 `MoveNext()` 指定的次數：

```csharp
private static void RequestElements(IEnumerable<int> sequence, int requestCount)
{
    Console.WriteLine("[消費端] GetEnumerator() 之前");
    using (IEnumerator<int> enumerator = sequence.GetEnumerator())
    {
        Console.WriteLine("[消費端] GetEnumerator() 之後");

        for (int i = 1; i <= requestCount; i++)
        {
            Console.WriteLine("[消費端] 第 " + i + " 次 MoveNext() 之前");
            bool moved = enumerator.MoveNext();
            if (moved)
            {
                Console.WriteLine("[消費端] 第 " + i + " 次 MoveNext() 之後：True，Current = " + enumerator.Current);
            }
            else
            {
                Console.WriteLine("[消費端] 第 " + i + " 次 MoveNext() 之後：False，序列結束");
            }
        }

        Console.WriteLine("[消費端] 不再要求元素");
    }
}
```

`Run()` 依序執行三種情境，只差在要求幾次；呼叫方法寫在 `RequestElements` 之外，讓「呼叫前後」與「GetEnumerator() 前後」成為不同的標記。最後呼叫 `StepsWithList()` 但不列舉：

```csharp
Console.WriteLine("情境 1：取得列舉器，但不要求元素");
Console.WriteLine("[消費端] 呼叫 Steps() 之前");
IEnumerable<int> first = Steps();
Console.WriteLine("[消費端] 呼叫 Steps() 之後");
RequestElements(first, 0);

Console.WriteLine("情境 2：只要求 2 個元素");
Console.WriteLine("[消費端] 呼叫 Steps() 之前");
IEnumerable<int> second = Steps();
Console.WriteLine("[消費端] 呼叫 Steps() 之後");
RequestElements(second, 2);

Console.WriteLine("情境 3：要求到序列結束");
Console.WriteLine("[消費端] 呼叫 Steps() 之前");
IEnumerable<int> third = Steps();
Console.WriteLine("[消費端] 呼叫 Steps() 之後");
RequestElements(third, 4);

Console.WriteLine("對照：同樣回傳 IEnumerable<int>，但不是迭代器");
Console.WriteLine("[消費端] 呼叫 StepsWithList() 之前");
IEnumerable<int> withList = StepsWithList();
Console.WriteLine("[消費端] 呼叫 StepsWithList() 之後，尚未列舉");
```

程式只有一個執行緒，沒有等待或計時；事件順序完全由輸出的先後決定。

### 預測輸出

先不執行程式，寫下你的預測：

1. 情境 1 會出現幾行 `[產生端]`？
2. 情境 2 的第一行 `[產生端]` 出現在哪一行 `[消費端]` 之後？
3. 情境 2 結束時，主體停在哪個 `yield return`？`Steps()` 的哪幾行輸出沒有出現？
4. 情境 3 的第 4 次 `MoveNext()` 回傳什麼？產生端最後印出哪一行？
5. 對照段落中，`StepsWithList()` 的兩行 `[產生端]` 出現在「呼叫 StepsWithList() 之後」之前還是之後？

### 執行觀察

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 05
```

觀察三件事：每個情境的第一行 `[產生端]` 都在「第 1 次 MoveNext() 之前」之後；情境 2 沒有「從產出 2 之後繼續」；`StepsWithList()` 的主體在呼叫時就印完。

### 修改練習

1. 在 `DeferredExecution.cs` 新增方法 `StepsMovedExercise()`，內容先複製 `Steps()`。
2. 把第一行 `Console.WriteLine("[產生端] 進入方法主體");` 移到 `yield return 2;` 之後、「從產出 2 之後繼續」那行之前。
3. 這行已不在開頭，把文字改成 `"[產生端] 移動過來的標記"`。
4. 仿照 `Run()` 的三個情境改呼叫新方法。

先預測：三種情境中，「移動過來的標記」各會不會出現？若出現，在第幾次 `MoveNext()` 時？要執行時，可暫時在 `Program.cs` 加一個選項（例如 `05-exercise`），練習後再還原。

解釋題：

1. 情境 1 呼叫了 `Steps()` 也取得了列舉器，產生端卻一行都沒印。主體什麼時候才開始執行？情境 2 為什麼看不到「從產出 2 之後繼續」？
2. 有人說：「這個方法回傳 `IEnumerable<int>`，所以呼叫時不會執行。」用本單元的對照說明這句話哪裡不對。

### 解答與判準

#### 預測題解答

`05` 實測輸出如下：

```text
情境 1：取得列舉器，但不要求元素
[消費端] 呼叫 Steps() 之前
[消費端] 呼叫 Steps() 之後
[消費端] GetEnumerator() 之前
[消費端] GetEnumerator() 之後
[消費端] 不再要求元素
情境 2：只要求 2 個元素
[消費端] 呼叫 Steps() 之前
[消費端] 呼叫 Steps() 之後
[消費端] GetEnumerator() 之前
[消費端] GetEnumerator() 之後
[消費端] 第 1 次 MoveNext() 之前
[產生端] 進入方法主體
[產生端] 產出 1
[消費端] 第 1 次 MoveNext() 之後：True，Current = 1
[消費端] 第 2 次 MoveNext() 之前
[產生端] 從產出 1 之後繼續，產出 2
[消費端] 第 2 次 MoveNext() 之後：True，Current = 2
[消費端] 不再要求元素
情境 3：要求到序列結束
[消費端] 呼叫 Steps() 之前
[消費端] 呼叫 Steps() 之後
[消費端] GetEnumerator() 之前
[消費端] GetEnumerator() 之後
[消費端] 第 1 次 MoveNext() 之前
[產生端] 進入方法主體
[產生端] 產出 1
[消費端] 第 1 次 MoveNext() 之後：True，Current = 1
[消費端] 第 2 次 MoveNext() 之前
[產生端] 從產出 1 之後繼續，產出 2
[消費端] 第 2 次 MoveNext() 之後：True，Current = 2
[消費端] 第 3 次 MoveNext() 之前
[產生端] 從產出 2 之後繼續，產出 3
[消費端] 第 3 次 MoveNext() 之後：True，Current = 3
[消費端] 第 4 次 MoveNext() 之前
[產生端] 從產出 3 之後繼續，走到方法結尾
[消費端] 第 4 次 MoveNext() 之後：False，序列結束
[消費端] 不再要求元素
對照：同樣回傳 IEnumerable<int>，但不是迭代器
[消費端] 呼叫 StepsWithList() 之前
[產生端] 進入 StepsWithList() 主體，建立 List
[產生端] 用 return 交出 List
[消費端] 呼叫 StepsWithList() 之後，尚未列舉
```

1. 情境 1 沒有任何 `[產生端]`：呼叫 `Steps()` 與 `GetEnumerator()` 都不執行主體，而這個情境一次都沒有呼叫 `MoveNext()`。
2. 三個情境中，第一行 `[產生端]` 都緊接在「第 1 次 MoveNext() 之前」之後，不在「呼叫 Steps() 之後」或「GetEnumerator() 之後」之後。
3. 情境 2 停在 `yield return 2`。「從產出 2 之後繼續，產出 3」與「從產出 3 之後繼續，走到方法結尾」都沒有出現，因為沒有第 3、4 次要求。離開 `using` 時會呼叫列舉器的 `Dispose()`，`Steps()` 沒有 `finally`，所以沒有輸出；清理行為留到單元 6。
4. 第 4 次回傳 `False`；產生端從 `yield return 3` 之後執行到方法結尾，最後印出「從產出 3 之後繼續，走到方法結尾」。
5. `StepsWithList()` 的兩行在「呼叫之後」之前：它不是迭代器，呼叫時就執行整個主體，再用 `return` 交出 List。

#### 修改題參考解

以下是同專案 `StepsMoved()` 的內容；`RunExerciseAnswer()` 以相同的三個情境呼叫它，可直接以 `05-answer` 選項執行：

```csharp
private static IEnumerable<int> StepsMoved()
{
    Console.WriteLine("[產生端] 產出 1");
    yield return 1;

    Console.WriteLine("[產生端] 從產出 1 之後繼續，產出 2");
    yield return 2;

    Console.WriteLine("[產生端] 移動過來的標記");
    Console.WriteLine("[產生端] 從產出 2 之後繼續，產出 3");
    yield return 3;

    Console.WriteLine("[產生端] 從產出 3 之後繼續，走到方法結尾");
}
```

```powershell
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 05-answer
```

實測輸出：

```text
情境 1：取得列舉器，但不要求元素
[消費端] 呼叫 StepsMoved() 之前
[消費端] 呼叫 StepsMoved() 之後
[消費端] GetEnumerator() 之前
[消費端] GetEnumerator() 之後
[消費端] 不再要求元素
情境 2：只要求 2 個元素
[消費端] 呼叫 StepsMoved() 之前
[消費端] 呼叫 StepsMoved() 之後
[消費端] GetEnumerator() 之前
[消費端] GetEnumerator() 之後
[消費端] 第 1 次 MoveNext() 之前
[產生端] 產出 1
[消費端] 第 1 次 MoveNext() 之後：True，Current = 1
[消費端] 第 2 次 MoveNext() 之前
[產生端] 從產出 1 之後繼續，產出 2
[消費端] 第 2 次 MoveNext() 之後：True，Current = 2
[消費端] 不再要求元素
情境 3：要求到序列結束
[消費端] 呼叫 StepsMoved() 之前
[消費端] 呼叫 StepsMoved() 之後
[消費端] GetEnumerator() 之前
[消費端] GetEnumerator() 之後
[消費端] 第 1 次 MoveNext() 之前
[產生端] 產出 1
[消費端] 第 1 次 MoveNext() 之後：True，Current = 1
[消費端] 第 2 次 MoveNext() 之前
[產生端] 從產出 1 之後繼續，產出 2
[消費端] 第 2 次 MoveNext() 之後：True，Current = 2
[消費端] 第 3 次 MoveNext() 之前
[產生端] 移動過來的標記
[產生端] 從產出 2 之後繼續，產出 3
[消費端] 第 3 次 MoveNext() 之後：True，Current = 3
[消費端] 第 4 次 MoveNext() 之前
[產生端] 從產出 3 之後繼續，走到方法結尾
[消費端] 第 4 次 MoveNext() 之後：False，序列結束
[消費端] 不再要求元素
```

「移動過來的標記」在情境 1、2 都不出現，只在情境 3 的第 3 次 `MoveNext()` 時出現。它前面有兩個 `yield return`，所以要等第 3 次要求才會執行；只要求 2 個時永遠不印。原本在開頭時，第 1 次 `MoveNext()` 就會印出。一行程式何時執行，取決於它在哪兩個 `yield return` 之間，以及消費端要求了幾次。

#### 解釋題判準

能清楚表達以下三點即可，不要求背誦原句：

- 呼叫迭代器方法與 `GetEnumerator()` 都不執行主體；第 1 次 `MoveNext()` 才從開頭執行到第一個 `yield return`。
- 之後每次要求只再往下執行一段。情境 2 只要求兩次，主體停在 `yield return 2`，之後的程式沒有被要求執行，所以「從產出 2 之後繼續」不會出現。
- 延遲執行是迭代器方法（主體含 `yield return`）的行為，不是 `IEnumerable<int>` 回傳型別造成的：`StepsWithList()` 回傳型別相同，呼叫時就跑完主體。只看回傳型別，判斷不出方法何時執行。

### 限制與相關概念

- 本單元的結論限定於同步、主體含 `yield return` 的迭代器方法。LINQ 的 `Where`、`Select` 等方法也有延遲行為，但各運算細節不同，未在此驗證；也不推廣到所有回傳 `IEnumerable<T>` 的方法。
- 情境 2 離開 `using` 時會呼叫 `Dispose()`。`yield break`、`foreach` 中途 `break`，以及 `Dispose()` 與 `finally` 的清理行為留到單元 6。
- 每次呼叫 `Steps()` 都得到新的序列；同一個序列再次列舉時主體會不會重跑，留到單元 7。
- C# 規格只描述可列舉物件與列舉器的外部行為，未規定實作方式。本單元只觀察公開行為（輸出順序、`MoveNext()` 的回傳值與 `Current`），不依賴編譯器產生的型別、欄位或反編譯結果。[C# 規格：列舉器物件](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#15155-enumerator-objects)。
- 回到原案例：`EnumerateRoundFacts(round)` 被呼叫時還沒檢查任何條件；`foreach` 第一次要求元素時，才開始判斷勝負。若消費端只取第一個事實就停下，後面的條件也不會被檢查。完整案例在單元 9 整理。

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
