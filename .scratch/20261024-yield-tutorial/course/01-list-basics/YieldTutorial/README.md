# List 與 yield 共用 Console 範例

教材：[主線筆記](../yield筆記.md)，[單元 1](../yield筆記.md#unit-01)、[單元 2](../yield筆記.md#unit-02)、[單元 3](../yield筆記.md#unit-03)、[單元 4](../yield筆記.md#unit-04)。

開始上課：[單元 1 HTML 短課程](../lessons/0001-list-basics.html)、[單元 2 HTML 短課程](../../02-ienumerable-contract/lessons/0002-ienumerable-contract.html)、[單元 3 HTML 短課程](../../03-enumerator-protocol/lessons/0003-enumerator-protocol.html)、[單元 4 HTML 短課程](../../04-yield-suspension/lessons/0004-yield-suspension.html)；複習：[List 基礎速查](../reference/list-basics.html)、[IEnumerable 約定速查](../../02-ienumerable-contract/reference/ienumerable-contract.html)、[列舉器協定速查](../../03-enumerator-protocol/reference/enumerator-protocol.html)、[yield 暫停與繼續速查](../../04-yield-suspension/reference/yield-suspension.html)。

目標框架：`net10.0`。2026-10-24 確認本機 SDK 為 `10.0.402`，依可用 SDK 選用 .NET 10；未修改既有範例框架。2026-11-21 本機 SDK 已更新為 `11.0.100`，仍以 `net10.0` 建置，並於 .NET Runtime 10.0.15 執行；目標框架未變更。無額外 NuGet 套件。

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01-answer
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 02
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 02-answer
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 03
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 03-answer
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 04
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 04-answer
```

在本資料夾內執行時，可改用 `dotnet run -- 02` 等較短指令。

省略選項時預設執行 `01`。`Program.cs` 負責選擇單元；`ListBasics.cs` 包含第一單元，`EnumerableContract.cs` 包含第二單元，`EnumeratorProtocol.cs` 包含第三單元，`YieldSuspension.cs` 包含第四單元，各自附修改題參考解。後續單元沿用同一專案。

`01` 固定輸出：

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

`01-answer` 固定輸出：

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

`02` 固定輸出：

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

`02-answer` 固定輸出：

```text
sequence 列舉：
1
2
3
4
```

`03` 固定輸出：

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

`03-answer` 固定輸出：

```text
第 1 個：2
第 2 個：4
第 3 個：6
第 4 個：8
共 4 個元素，MoveNext 呼叫 5 次
```

`04` 固定輸出：

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

`04-answer` 固定輸出：

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
