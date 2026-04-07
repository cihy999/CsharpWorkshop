# C#筆記 - Events

## 教學摘要

這是一份針對影片 **《[Master C# Events Like a Senior Developer](http://www.youtube.com/watch?v=9H7PU-cy0Sw)》** 的深度技術摘要，旨在幫助開發者從資深工程師的視角理解 C# 事件（Events）的設計哲學與實作演進。

---

### 第一部分：核心價值 (High-Level Summary)

這部影片旨在解決 C# 開發中「如何實現物件間的鬆散耦合（Loose Coupling）」問題。核心結論是：透過從**介面實作**演進到**委派（Delegates）**，最終採用 **Microsoft 建議的事件處理標準**，開發者能建立一個高效、易維護且符合業界規範的訂閱者通知系統。

---

### 第二部分：深度解析 (Deep Dive)

### 🧠 核心觀念：發布者與訂閱者模式 [[00:30](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=30)]

- **機制說明**：事件允許物件（發布者）在特定事情發生時通知其他物件（訂閱者）。
- **鬆散耦合**：資深開發者傾向使用介面（I Publisher/I Subscriber）來解耦，讓發布者不需知道訂閱者的具體實作即可進行通訊 [[02:36](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=156)]。
- **1 對 N 關係**：一個發布者可以擁有零個或多個訂閱者；若無訂閱者，事件則不會被引發 [[00:55](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=55)]。

#### 🛠️ 經典實作：基於介面的通知 [[07:08](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=428)]

- **結構設計**：定義 `I Publisher`（含增加/刪除訂閱者方法）與 `I Subscriber`（含通知方法）。
- **手動管理**：發布者內部需維護一個 `List<ISubscriber>`，並在發布時使用 `foreach` 迴圈逐一調用訂閱者的通知方法 [[12:30](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=750)]。
- **缺點**：程式碼較為冗長，且需要手動處理列表的增刪邏輯。

#### ⚡ 進階演進：委派與自定義事件 [[23:00](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=1380)]

- **委派替代介面**：利用 `delegate` 取代強型別介面，簡化通訊契約。
- **事件包裝（Wrapper）**：事件本質上是委派的包裝器，它封裝了 `add` 和 `remove` 的行為 [[33:12](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=1992)]。
- **優勢**：不需要手動維護私有資料結構（如 Dictionary 或 List）來儲存訂閱者，語法更簡潔 [[33:56](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=2036)]。

#### 📊 業界標準：Microsoft 推薦實作 [[38:00](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=2280)]

- **使用 `EventHandler<T>`**：強烈建議使用內建的泛型 `EventHandler`，而非自定義委派 [[39:06](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=2346)]。
- **標準參數結構**：遵循 `(object sender, TEventArgs e)` 的簽署規範，其中 `sender` 代表發起者，`EventArgs` 承載數據 [[40:40](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=2440)]。
- **安全調用**：使用 `?.Invoke()` 確保在沒有訂閱者時不會拋出空參考異常。

---

### 第三部分：實踐建議 (Take Action)

- **總結重點**：
    1. **優先選用 `EventHandler<T>`**：在 .NET 開發中，這是最符合慣例且最具擴展性的方式。
    2. **注意取消訂閱**：在使用 `+=` 訂閱事件後，務必在不需要時使用 `-=` 取消訂閱，以防止記憶體洩漏 [[44:26](http://www.youtube.com/watch?v=9H7PU-cy0Sw&t=2666)]。

- **適用對象**：
  - 想要從「只會寫程式」晉升到「理解系統架構」的中高階 C# 開發者。
  - 準備進行 .NET 技術面試，需要掌握事件底層原理的求職者。

## 專有名詞

- **鬆散耦合(Loose Coupling)**：系統中的各個元件（例如類別、模組或物件）彼此之間的依賴程度很低，互相知道的細節越少越好。
- **緊密耦合(Tight Coupling)**：與**鬆散耦合**相反。
  - 以餐廳點餐為例，你必須走進廚房，找到負責做漢堡的廚師，然後直接告訴他漢堡的具體做法、要加多少鹽和醬料。如果廚師換人了，你就必須重新認識新廚師才能點餐。
  - 鬆散耦合的餐廳點餐：你只需要在座位上，對著服務鈴按一下，服務生就會過來問你要點什麼。你不需要知道廚房裡是誰在做漢堡，也不需要知道漢堡是怎麼做的。如果廚師換人了，也不會點不到漢堡。
