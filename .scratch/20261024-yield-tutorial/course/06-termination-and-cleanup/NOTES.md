# 教材設計備註

- 此目錄為第六單元的教學工作區；`lessons/`、`reference/` 路徑相對於此目錄。樣式與互動元件復用 `../01-list-basics/assets/`，不另建副本；本單元未修改共用元件。
- 程式放在共用專案 `../01-list-basics/YieldTutorial/TerminationAndCleanup.cs`，入口選項 `06`、`06-answer`；Markdown 主線為 `../01-list-basics/yield筆記.md#unit-06`。預設入口仍為 `01`。
- 依 Ticket 06 規範，`try`／`finally` 清理語法由教材直接提供完整程式 `ReadNumbers(int stopAt)`，修改練習只請學生撰寫消費端輔助方法與調整結束條件，不要求學生自行手寫 `try`／`finally`。
- 完整示範 `ReadNumbers(int stopAt)` 刻意將 `Console.WriteLine("[產生端] 走到方法結尾");` 放在 `finally` 區塊之後：
  - 路徑 1（正常走完）：印出 `迴圈正常走完` → `finally：釋放資源` → `走到方法結尾`，顯示自然耗盡會離開 `try` 並繼續走到方法結尾。
  - 路徑 2（`stopAt = 2`，產生端 `yield break`）：印出 `遇到 n = 2，執行 yield break` → `finally：釋放資源`，不印 `迴圈正常走完` 與 `走到方法結尾`，區分 `yield break` 與一般迴圈 `break`。
  - 路徑 3（消費端取 1 項後 `break`）：印出 `執行 break，離開 foreach` → `finally：釋放資源`，不印 `從產出 1 之後繼續`、`迴圈正常走完` 與 `走到方法結尾`，證明消費端 `break` 不會繼續執行迭代器剩餘的一般程式。
  - 對照 1（手動列舉取 1 項後離開 `using`）：在離開 `using` 前後加上標記，讓讀者直接看到 `finally` 發生在 `using` 收尾呼叫 `Dispose()` 的那一刻，連結單元 3 的 `using` 規則與 `foreach` 隱藏 `finally` 的關係。
  - 對照 2（取得列舉器但從未 `MoveNext` 就離開 `using`）：銜接單元 5，證明尚未執行第一次 `MoveNext()` 時控制流還沒進入 `try`，`Dispose()` 不會執行主體內的 `finally`。
- 修改題讓產生端固定 `stopAt = 3`，比較消費端在 `4` 才 `break`（練習 A）與在 `2` 就 `break`（練習 B）：兩者取得的數字同為 `1`、`2`，也都會執行一次 `finally`，但練習 A 由產生端第 3 次 `MoveNext()` 遇到 `yield break` 結束，練習 B 則由消費端拿到 `2` 後 `break` 並透過 `Dispose()` 清理，幫助讀者從事件順序而非僅看產出值來區分兩種 `break`。
- HTML 預測題使用較短的 `Items(int stopAt)` 連續跑三條路徑，輸出均為無空白單字標記以配合共用 `quiz.js`；已在系統 TEMP 的臨時專案實測並刪除臨時目錄。三題選擇題選項字元數分別對齊為 8、9、8 字，正解位置錯開為 `c`、`a`、`b`。
- 不引入規格內部的 `before`／`running`／`suspended`／`after` 狀態名稱作為學生必背術語，只用「尚未開始列舉」「正暫停在 `try` 內」「已經結束」描述外部可觀察行為。
- 專案規範優先：不建立 `learning-records`，瀏覽器練習不寫入儲存空間。
- `teach` 參照指紋於 2026-11-23 核對，三個檔案之 SHA-256 皆與相容性基準一致。
