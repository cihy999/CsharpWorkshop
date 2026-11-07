# 從 List 到 yield

開始上課請開啟 [HTML 短課程](lessons/0001-list-basics.html)，先預測再核對；複習可看 [List 基礎速查](reference/list-basics.html)。第二單元見 [單元 2](#unit-02)，第三單元見 [單元 3](#unit-03)。本文件保留完整草稿與原始素材。

本文件為暫存課程教材，Ticket 01 已於 2026-10-24 完成教材驗收；Ticket 02 已於 2026-11-07 完成教材驗收；Ticket 03 已於 2026-11-07 完成教材驗收。課程內容先集中於此暫存工作區，整套課程完成後由 Ticket 10 統一歸檔。主線由整數集合開始，再逐步銜接列舉與 yield。現已備妥第一至三單元；後續單元尚待編寫。原有回合事實說明保留於文末，供後續整理。

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
