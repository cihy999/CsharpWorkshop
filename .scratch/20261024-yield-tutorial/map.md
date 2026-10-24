# yield 學習地圖

目前階段：完整地圖與九張課程 Ticket 拆分皆已確認；[教材 spec](spec.md) 已發布。Ticket 01 進行中（claimed），已有教材草稿與共用 Console 專案，其餘課程 Ticket 尚待製作。課程內容先集中在本暫存工作區。

## Notes

- 流程：確認學習地圖 → 編寫教材 spec → 拆分課程 Ticket。
- 地圖描述教材的概念與相依關係，不記錄個人學習進度。

## Decisions-so-far

2026-10-24 教材準備與作法調整：[01 List 基礎](issues/01-list-basics.md) 仍在進行，採 `net10.0` 共用 Console 入口，示範與修改題解答已實測。依學生要求，教材草稿放在 [yield 筆記](course/01-list-basics/yield筆記.md)，共用專案放在 [YieldTutorial](course/01-list-basics/YieldTutorial/README.md)，後續課程內容也先留在本工作區；正式筆記保留原文，暫不列入正式查詢索引。整理完成後再歸檔。

2026-10-24 第一輪：使用者確認 Q1、Q2、Q3 全部採建議。

- Q1／學習終點：讀懂回合事實案例、寫出類似 yield 方法，並判斷何時使用 List。編譯器產生的狀態機型別、欄位與 MoveNext 實作列為選讀延伸。
- Q2／先備範圍：納入 List<T>、泛型基本讀法、介面、IEnumerable<T> 與列舉器；限於理解 yield 所需的部分。
- Q3／驗收能力：用自己的話解釋、預測程式輸出、修改小範例。此為教材設計標準，不建立學生能力紀錄。

2026-10-24 第二輪：使用者確認 Q4、Q5、Q6 全部採建議。

- Q4／概念順序：最小數字範例 → List → 介面與泛型基本讀法 → IEnumerable<T> → 列舉器 → yield → 回合事實案例。各步只引入少量新概念。
- Q5／列舉器深度：練習 GetEnumerator()、MoveNext() 與 Current；完整手寫列舉器類別列為選讀。
- Q6／核心行為：yield return 的暫停與繼續、延遲執行、yield break 與消費端 break、提前停止時的基本清理、重複列舉與資料變動、List 與 ToList() 取捨。完整 LINQ、非同步串流與編譯器實作留作延伸。

2026-10-24 第三輪：使用者確認 Q7、Q8、Q9 全部採建議。

- Q7／課程 Ticket：一個主要問題一張，不綁定上課時間。包含目標、先備知識、講解範圍、示範、練習、驗收條件與相依 Ticket。
- Q8／教學形式：簡短說明 → 範例 → 預測輸出 → 執行觀察 → 修改練習。題目少量且目的明確，解答置於題目後的獨立區段；清理操作提供完整示範。
- Q9／教材組織：一篇主線筆記與一個 Console 示範專案，依單元區分範例。重編既有 yield 筆記，保留原案例與來源脈絡；選讀內容先列入口，有實際內容再拆文。Ticket 追蹤教材工作，不記錄個人學習進度。

## 完整學習地圖（已確認）

學習終點：讀懂原回合事實案例、能撰寫類似 yield 方法，並能依需求選擇 yield 或 List。

| 順序 | 主要問題 | 概念與示範範圍 |
|---|---|---|
| 1 | List 如何保存與提供多個值？ | 最小整數範例、List<int>、新增、索引、foreach |
| 2 | 為什麼 List 可以交給 IEnumerable？ | 泛型基本讀法、介面的能力約定、List<T> 與 IEnumerable<T> 的關係；基本方法回傳型別讀法 |
| 3 | foreach 如何取得下一個值？ | GetEnumerator、IEnumerator<T>、MoveNext、Current；少量手動列舉與必要清理示範 |
| 4 | yield 如何分次提供元素？ | return 與 yield return、暫停與繼續、區域變數的保留 |
| 5 | 迭代器方法何時真正執行？ | 呼叫、取得列舉器、逐次要求元素的輸出時間線 |
| 6 | 序列如何結束或提前停止？ | 方法結尾、yield break、消費端 break；Dispose 與 finally 的基本清理示範 |
| 7 | 為什麼再次列舉可能得到不同結果？ | 重複計算、副作用與可變輸入；與列舉期間修改集合的情境區分 |
| 8 | 何時選 yield，何時保存成 List？ | 結果保存、ToList、重複使用、記憶體與執行成本的條件化比較 |
| 9 | 如何整理一個回合的多項事實？ | 完整最小型別、零／一／多個結果與順序；List 與 yield 版本對照 |

先備主線依表中順序安排；列舉器完整實作、編譯器狀態機、完整 LINQ 與非同步串流為選讀分支，不阻擋主線。

各單元的教材驗收包括解釋、預測與修改；課程 Ticket 以本表的主要問題切分，驗收教材是否支持這些能力。

## 決策樹

- 學習目標與驗收能力（已確認）
  - 先備概念範圍（已確認）
    - 概念順序與示範起點（已確認，Q4）
    - 列舉器實作深度（已確認，Q5）
  - yield 實務能力（已確認）
    - 核心行為與延伸的邊界（已確認，Q6）
  - 教材組織（已確認）
    - 單元切分與教學形式（Q7、Q8）
    - 正式文章與範例的組織（Q9）

## Fog

- 完整學習地圖與 Ticket 拆分均已確認，沒有待決的主線範圍問題。教材工作可從 01 開始；06 與 07 共同依賴 05，08 需等待兩者。

## 課程 Ticket 索引

| Ticket | 主要問題 | Blocked by |
|---|---|---|
| [01](issues/01-list-basics.md) | List 如何保存與提供多個值？ | 無 |
| [02](issues/02-ienumerable-contract.md) | 為什麼 List 可以交給 IEnumerable？ | 01 |
| [03](issues/03-enumerator-protocol.md) | foreach 如何取得下一個值？ | 02 |
| [04](issues/04-yield-suspension.md) | yield 如何分次提供元素？ | 03 |
| [05](issues/05-deferred-execution.md) | 迭代器方法何時真正執行？ | 04 |
| [06](issues/06-termination-and-cleanup.md) | 序列如何結束或提前停止？ | 05 |
| [07](issues/07-repeated-enumeration.md) | 為什麼再次列舉可能得到不同結果？ | 05 |
| [08](issues/08-yield-or-list.md) | 何時選 yield，何時保存成 List？ | 06、07 |
| [09](issues/09-round-facts-integration.md) | 如何整理一個回合的多項事實？ | 08 |

## Comments

- 2026-10-24：採 grill-with-docs 訪談，記錄已確認共識；尚未建立課程 Ticket。
- 2026-10-24：第二輪 Q4–Q6 全部採建議；繼續確認教材組織。
- 2026-10-24：第三輪 Q7–Q9 全部採建議；整理完整地圖，等待整體確認。
- 2026-10-24：使用者確認完整地圖並指定 to-spec；已發布教材 spec，狀態 ready-for-agent，尚未建立課程 Ticket。
- 2026-10-24：使用者確認 to-tickets 提出的九張拆分與阻擋關係；已各自發布獨立 Ticket，狀態 ready-for-agent。教材 spec 保持不變，尚未開始實作。
