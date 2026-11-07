# 03-enumerator-protocol 課堂筆記

## 課堂練習的回答

### 01 / 兩個角色

先猜猜看：剛呼叫 GetEnumerator() 時，列舉器在哪裡？

答：第一個元素之前。

### 02 / MoveNext 決定能不能讀 Current

序列有三個元素，第幾次 MoveNext() 會第一次回傳 False？

答：第四次。

假設元素為 1, 2, 3，則 MoveNext 的結果為：

- MoveNext: 1
- MoveNext: 2
- MoveNext: 3
- MoveNext: False

哪一行讀 Current 的時機合法？

答：只有 B 行。

### 03 / 用 using 收尾

手動列舉為什麼要放進 using？

答：離開時必呼叫 Dispose。

### 04 / 先預測，再執行

答：

```text
True
10
True
20
True
30
False
```

對空的 List 執行 foreach，迴圈主體會執行幾次？

答(錯)：一次，讀到 0 值。

修正答案：零次，直接結束。

### 05 / 自己改一次

```csharp
public static void RunExercise()
{
    IEnumerable<int> numbers = new List<int> { 2, 4, 6, 8 };
    int n = 0;

    using (IEnumerator<int> enumerator = numbers.GetEnumerator())
    {
        while (enumerator.MoveNext())
        {
            n++;
            Console.WriteLine($"第 {n} 個：{enumerator.Current}");
        }
    }

    Console.WriteLine($"共 {n} 個元素，MoveNext 呼叫 {n + 1} 次");
}
```

下面有兩處讀取 Current 的時機不合法。指出是哪兩處、為什麼，再說明手動列舉為什麼要放進 using。

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

答：

1. A 行不合法，因為 enumerator 紀錄的元素位置在第一個之前，Current 沒有合法的值。
2. C 行不合法，因為 enumerator 紀錄的元素位置在最後一個之後，Current 沒有合法的值。

加強答案：

A 和 C 行不合法，因為 enumerator 紀錄的元素位置不在合法的範圍內，Current 是未定義，實際可能讀到 0 或 null，也可能擲出例外，不一定是哪一種。
