# Mission: 說明 yield 如何分次提供元素

## Why
依已確認的 [教材 spec](../../spec.md)，課程最終要協助讀者讀懂、改寫回合事實的 yield 方法。原案例的 `EnumerateRoundFacts` 以多個 yield return 依條件逐一交出事實；本單元建立 yield return 的產出、暫停與繼續，以及區域變數保留的推演方式，接上單元 3 的 MoveNext／Current，作為單元 5 延遲執行時間線的基礎。

## Success looks like
- 對照一般 return 與 yield return：前者一次交出整份結果並結束方法，後者每次交出一個元素後暫停。
- 推演每次 MoveNext 時產生端從哪一行繼續、執行到哪個 yield return，以及當下區域變數的值。
- 修改迭代器方法的計算或插入新的 yield return，先預測再執行核對。
- 辨認 return 與 yield return 混寫在同一方法中為何無法編譯。

## Constraints
- 程式沿用 [共用 Console 專案](../01-list-basics/YieldTutorial/README.md)，主線文字沿用 [主線筆記](../01-list-basics/yield筆記.md#unit-04)。
- 短篇說明、先預測再揭答、立即回饋與可執行 C# 範例；無法編譯的寫法只作實測說明，不放入共用專案。
- 只記錄教材設計與來源，不保存個人作答、能力或學習進度。

## Out of scope
- 呼叫方法、GetEnumerator 與第一次 MoveNext 的時間線（單元 5）；yield break、消費端 break 與 finally 清理（單元 6）；重複列舉（單元 7）；編譯器產生的狀態機、IL 與非同步迭代器。
