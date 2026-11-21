# 教材設計備註

- 此目錄為第四單元的教學工作區；lessons、reference 路徑相對於此目錄。樣式與互動元件復用 `../01-list-basics/assets/`，不另建副本；本單元未修改共用元件。
- 程式放在共用專案 `../01-list-basics/YieldTutorial/YieldSuspension.cs`，入口選項 `04`、`04-answer`；Markdown 主線為 `../01-list-basics/yield筆記.md#unit-04`。預設入口仍為 `01`。
- return 與 yield return 各寫成一個合法方法對照（`GetNumbersWithReturn`、`GetNumbersWithYield`），交出相同的值；混寫的 CS1622 與回傳型別 `List<int>` 的 CS1624 只在系統 TEMP 的臨時專案實測，臨時專案已刪除。
- 輸出以 `[產生端]`／`[消費端]` 前綴區分迭代器方法與使用端，沿用 spec 與 Ticket 06 的用語。
- `CountUp` 刻意寫成不含迴圈的三段，以註解「位置 1～3」標出 yield return，讓「從哪一行繼續」可直接對照輸出；迴圈版本放在 HTML 預測題，練習迴圈變數也會保留。
- 逐次要求段落沿用單元 3 的寫法：先存入 `bool moved` 再輸出，只在 MoveNext 回傳 True 後讀 Current；兩次要求之間插入一行消費端輸出，觀察產生端不會自行往下執行。
- 本單元在同一行呼叫迭代器並取得列舉器，不區分呼叫、GetEnumerator 與第一次 MoveNext 的時間點，留給單元 5；教材只說「第一次 MoveNext 從方法開頭執行」。
- 修改題插入 `yield return count + 100;`，用來說明 yield return 只交出運算式的值、不改寫區域變數。
- 不使用規格中的 before／running／suspended／after 狀態名稱，也不提編譯器產生的型別或欄位；狀態機實作列為選讀。
- 本機 SDK 自前三單元的 10.0.402 更新為 11.0.100；共用專案仍以 `net10.0` 建置，執行於 .NET Runtime 10.0.15，未修改目標框架。
- 專案規範優先：不建立 learning-records，瀏覽器練習不寫入儲存空間。
- teach 參照指紋於 2026-11-21 核對，與相容性基準一致。
