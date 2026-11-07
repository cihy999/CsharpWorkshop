# 教材設計備註

- 此目錄為第三單元的教學工作區；lessons、reference 路徑相對於此目錄。樣式與互動元件復用 `../01-list-basics/assets/`，不另建副本。
- 程式放在共用專案 `../01-list-basics/YieldTutorial/EnumeratorProtocol.cs`，入口選項 `03`、`03-answer`；Markdown 主線為 `../01-list-basics/yield筆記.md#unit-03`。
- 示範的序列刻意宣告為 `IEnumerable<int>`，讓手動版本的 `IEnumerator<int>` 與 foreach 的規格展開對得上；宣告為 `List<int>` 時編譯器改用 `List<int>.Enumerator`，只在說明中標示適用範圍。
- 「逐次呼叫」段落先存入 `bool moved` 再輸出，避免學生需要知道同一運算式內由左到右求值，才能判斷 MoveNext 先於 Current。
- Current 未定義的實測（List<int> 為 0、List<string> 為 null、int[] 與非泛型列舉器擲出 InvalidOperationException）於系統 TEMP 的臨時專案執行後即刪除；共用專案只在合法時點讀取 Current。
- `quiz.js` 的預測題比對改為不分大小寫，讓 True／true 都算正確；前兩課的預期答案只有數字，不受影響。
- 既有正式筆記「列舉器與集合」寫「開頭列舉器會拿到 null」，與實測不符；已記入 Ticket 10，於正式歸檔時修正，本次不修改正式筆記。
- Dispose 在 List 列舉器上看不出效果；可觀察的清理留給單元 6 的 yield finally 示範。
- 專案規範優先：不建立 learning-records，瀏覽器練習不寫入儲存空間。
- teach 參照指紋於 2026-11-07 核對，與相容性基準一致。
