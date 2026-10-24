# List 與 yield 共用 Console 範例

教材：[主線筆記](../yield筆記.md#unit-01)。

目標框架：`net10.0`。2026-10-24 確認本機 SDK 為 `10.0.402`，依可用 SDK 選用 .NET 10；未修改既有範例框架。無額外 NuGet 套件。

從 repo 根目錄執行：

```powershell
dotnet build .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial/YieldTutorial.csproj
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01
dotnet run --project .scratch/20261024-yield-tutorial/course/01-list-basics/YieldTutorial -- 01-answer
```

省略選項時預設執行 `01`。`Program.cs` 負責選擇單元，`ListBasics.cs` 包含第一單元與修改題參考解，後續單元沿用同一專案。

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
