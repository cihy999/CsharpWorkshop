# List 與 yield 共用 Console 範例

教材：[主線筆記](../yield筆記.md)，[單元 1](../yield筆記.md#unit-01)、[單元 2](../yield筆記.md#unit-02)。

開始上課：[單元 1 HTML 短課程](../lessons/0001-list-basics.html)、[單元 2 HTML 短課程](../../02-ienumerable-contract/lessons/0002-ienumerable-contract.html)；複習：[List 基礎速查](../reference/list-basics.html)、[IEnumerable 約定速查](../../02-ienumerable-contract/reference/ienumerable-contract.html)。

目標框架：`net10.0`。2026-10-24 確認本機 SDK 為 `10.0.402`，依可用 SDK 選用 .NET 10；未修改既有範例框架。無額外 NuGet 套件。

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01-answer
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 02
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 02-answer
```

在本資料夾內執行時，可改用 `dotnet run -- 02` 等較短指令。

省略選項時預設執行 `01`。`Program.cs` 負責選擇單元；`ListBasics.cs` 包含第一單元，`EnumerableContract.cs` 包含第二單元，各自附修改題參考解。後續單元沿用同一專案。

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
