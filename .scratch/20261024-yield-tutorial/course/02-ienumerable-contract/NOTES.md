# 教材設計備註

- 此目錄為第二單元的教學工作區；lessons、reference 路徑相對於此目錄。樣式與互動元件復用 `../01-list-basics/assets/`，不另建副本。
- 程式放在共用專案 `../01-list-basics/YieldTutorial/EnumerableContract.cs`，入口選項 `02`、`02-answer`；Markdown 主線為 `../01-list-basics/yield筆記.md#unit-02`。
- 無法編譯的操作（Add、索引、Count 屬性、隱含轉回 List）於系統 TEMP 的臨時專案實測錯誤代碼後即刪除，不放入共用專案。
- 修改題刻意讓學生先觸發 CS1061，再改由 List 變數新增；參考解以註解保留那一行。
- 列舉期間修改集合只在邊界提及，留給單元 7；LINQ `Count()` 只說明為何可以編譯。
- 專案規範優先：不建立 learning-records，瀏覽器練習不寫入儲存空間。
- teach 參照指紋於 2026-11-07 核對，與相容性基準一致。
