# 02-ienumerable-contract 課堂筆記

## 課堂練習的回答

### 02 / 賦值不是複製

`IEnumerable<int> sequence = list;` 做了什麼？

答：讓另一個變數參照同一清單。

### 03 / 宣告型別決定能寫什麼

`sequence 宣告為 IEnumerable<int>`，哪一行可以直接編譯？

答：`foreach (int n in sequence)`。

呼叫 GetNumbers(list) 拿到的值，宣告型別是什麼？

答：`IEnumerable<int>`。

### 04 / 先預測，再執行

答：

```text
10
20
30
40
```

### 05 / 自己改一次

```csharp
public static void RunExercise()
{
    List<int> list = new List<int> { 1, 2, 3 };
    IEnumerable<int> sequence = GetNumbers(list);

    list.Add(4);

    Console.WriteLine("sequence 列舉：");
    foreach (int number in sequence)
    {
        Console.WriteLine(number);
    }
}
```

為什麼 list.Add(40) 會讓 sequence 也列出 40？又為什麼 sequence.Add(40) 不能編譯？

答：

1. sequence 是紀錄 list 的位址、是參考，所以 list.Add(40) 會讓 sequence 也列出 40。
2. 因為 sequence 是 `IEnumerable<int>`，不具備 Add 的方法。

修正說法：

1. **list 和 sequence 兩個變數保存同一個 List 物件的參考**，所以 list.Add(40) 會讓 sequence 也列出 40。
   - 理由是 C# 的參考不等於固定的記憶體位址，垃圾回收器（GC）可能搬移物件。

## 補充：`IEnumerable` 英文解釋

`IEnumerable` 可以拆成三段：

| 部分 | 意思 |
|---|---|
| `I` | Interface，介面（.NET 的命名慣例，介面名稱以 I 開頭） |
| `Enumer` | 來自動詞 **enumerate**：列舉、逐一數出來 |
| `-able` | 英文字尾「能夠被……的」，例如 readable（可讀的）、washable（可洗的） |

合起來，`IEnumerable` 就是「**可被列舉的**」介面。實作它的型別等於承諾：「我裡面的元素可以一個一個依序拿出來。」所以 `foreach` 能用在 `IEnumerable<int>` 上，而 `Add`、索引這類功能不在這個承諾裡。

這種「`I` + 形容能力的 `-able` 字」在 .NET 很常見，看名字就能猜出介面承諾什麼：

- `IComparable`：可被比較的
- `IDisposable`：可被釋放（資源）的
- `IEquatable<T>`：可判斷相等的
