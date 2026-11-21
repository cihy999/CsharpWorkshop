# 04-yield-suspension 課堂筆記

## 課堂練習的回答

### 01 / return 交出整份，yield return 交出一個

yield return 交出一個值之後，方法接著會？

答：暫停，等下次要求。

方法裡已有 yield return 10;，還能寫 return list; 嗎？

答：不行，編譯會失敗。

### 02 / 每次 MoveNext，跑到下一個 yield return

兩次要求之間，迴圈的 i 與 total 會？

答：保留原本的值。

### 03 / 區域變數會記得

第 2 次 MoveNext() 時，「從位置 1 之後繼續」印出的 count 是？

答：10。

### 04 / 先預測，再執行

依序輸入每一行輸出（每行一個，或用空格、逗號分隔；大小寫不拘）。

答：

```text
A
1
B1
3
B2
6
B3
C
```

兩次要求之間，迴圈的 i 與 total 會？

答：保留原本的值。

### 05 / 自己改一次

```csharp
public static void RunExercise()
{
    int received = 0;

    foreach (int number in CountUpExercise())
    {
        received++;
        Console.WriteLine("[消費端] 取得 " + number);
    }

    Console.WriteLine("[消費端] 共取得 " + received + " 個值");
}

public static IEnumerable<int> CountUpExercise()
{
    int count = 1;
    Console.WriteLine("[產生端] 從方法開頭執行，count = " + count);

    count *= 2;
    Console.WriteLine("[產生端] 位置 1：產出 count = " + count);
    yield return count; // 位置 1

    Console.WriteLine("[產生端] 從位置 1 之後繼續，count = " + count);
    Console.WriteLine("[產生端] 位置 2：產出 count + 100 = " + (count + 100));
    yield return count + 100; // 位置 2

    Console.WriteLine("[產生端] 從位置 2 之後繼續，count = " + count);
    count *= 2;
    Console.WriteLine("[產生端] 位置 3：產出 count = " + count);
    yield return count; // 位置 3

    Console.WriteLine("[產生端] 從位置 3 之後繼續，count = " + count);
    count *= 2;
    Console.WriteLine("[產生端] 位置 4：產出 count = " + count);
    yield return count; // 位置 4

    Console.WriteLine("[產生端] 從位置 4 之後繼續，走到方法結尾");
}
```

看 CountUp 這段：

```csharp
int count = 0;                // 開頭
count += 10;
yield return count;           // 位置 1：交出 10，暫停
Console.WriteLine("從位置 1 之後繼續，count = " + count);   // ← 第 2 次要求時印出這行
count += 10;
```

問：第 2 次 MoveNext() 印出這行時，為什麼是 10？為什麼不是 0，也不是 20？

答：

因為迭代器紀錄 count = 10、位置 1，所以第 2 次 MoveNext() 印出這行時，是 10。count 的值不僅有被保留，執行位置也記錄，所以不會從頭開始執行，因此不是 0；還尚未執行到第二次的 count += 10; 這行，所以不會是 20。

下面的方法為什麼不能編譯？若要讓它依序產出 10、20，該怎麼改？

```csharp
static IEnumerable<int> Broken()
{
    yield return 10;
    return new List<int> { 20 };
}
```

答：不能混用 yield return、return。

比較精確的寫法是：「方法裡有 yield return 就是迭代器，迭代器不能用 return 值; 回傳（CS1622），要提早結束得用 yield break;」。

```csharp
// 一次產生一個值
static IEnumerable<int> Correct1()
{
    yield return 10;
    yield return 20;
}

// 一次產生全部值
static IEnumerable<int> Correct2()
{
    return new List<int> { 10, 20 };
}
```

## 補充：`yield return` 與 `return List` 的使用時機

兩種寫法交給 `foreach` 時結果一樣，差別在**值什麼時候產生、存在哪裡**。`return List` 先把全部值算好放進記憶體，再一次交出；`yield return` 則是消費端要一個，才算一個。

### 適合用 `yield return` 的情況

- **消費端可能不會全部取完。** 例如一百萬筆裡只要前 3 筆，消費端 `break` 之後，後面的值根本不會被算出來。用 List 就得先把一百萬筆全部算完。
- **資料量大，或根本沒有盡頭。** 例如「無限產生下一個序號」，List 裝不下無限多個元素，迭代器沒問題，因為它一次只產出一個。
- **想邊產生邊處理。** 例如逐行讀大檔案，讀一行處理一行，不必先把整個檔案載入記憶體。

### 適合用 `return List` 的情況

- **需要 `Count`、索引 `[i]`，或要走訪很多次。** List 的值已經存好，重複讀不會重算。迭代器每走訪一次，就會把方法從頭再執行一次。
- **結果必須「現在」就確定。** 例如要在資料庫連線關閉前把資料全部讀出來，或想要拿到一份不會再變動的快照。
- **資料量小而且固定。** 這種情況兩種寫法差不多，List 比較直觀，除錯時也看得到整份內容。

一個簡單的判斷方式：**問自己「呼叫端需要的是『一份結果』，還是『一串依序產生的值』」。** 要一份能數、能索引、能重複讀的結果，就用 List；只是要依序取值，而且可能取不完或量很大，就用 `yield return`。

補充一點：如果決定回傳 List，回傳型別也可以宣告成 `List<int>` 或 `IReadOnlyList<int>`，而不只是 `IEnumerable<int>`。呼叫端就能直接知道可以用 `Count` 和索引。
