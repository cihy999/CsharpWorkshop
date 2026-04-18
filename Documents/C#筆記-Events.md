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

## 練習觀察者模式

```csharp
using CommonArticleLibrary;

namespace EventsPractice.Observer
{
    internal interface IPublisher
    {
        public void AddSubscriber(ISubscriber subscriber);
        public void RemoveSubscriber(ISubscriber subscriber);
        public void Publish(Article article);
    }
}
```

```csharp
namespace EventsPractice.Observer
{
    internal interface ISubscriber
    {
        public void Subscribe(IPublisher publisher);
        public void Unsubscribe(IPublisher publisher);
        public void Update(string message);
    }
}
```

```csharp
using CommonArticleLibrary;

namespace EventsPractice.Observer
{
    internal record Author : DomainEntity, IPublisher
    {
        private readonly List<ISubscriber>? subscribers;

        public string Name { get; init; }
        public string Description { get; init; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
            subscribers = [];
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            subscribers?.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            subscribers?.Remove(subscriber);
        }

        public void Publish(Article article)
        {
            Article createdArticle = article.Create();
            string subscriberUpdateMessage = createdArticle.ToString();
            Notify(subscriberUpdateMessage);
        }

        private void Notify(string message)
        {
            subscribers?.ForEach(subscriber =>
            {
                subscriber.Update(message);
            });
        }
    }
}
```

```csharp
using CommonArticleLibrary;

namespace EventsPractice.Observer
{
    internal record User : DomainEntity, ISubscriber
    {
        public string Name { get; init; }

        public User(string name)
        {
            Name = name;
        }

        public void Subscribe(IPublisher publisher)
        {
            publisher.AddSubscriber(this);
        }

        public void Unsubscribe(IPublisher publisher)
        {
            publisher.RemoveSubscriber(this);
        }

        public void Update(string message)
        {
            Console.WriteLine(message);
        }
    }
}
```

```csharp
static void Main(string[] args)
{
   Observer.Author author = new("Nintendo", "Game Developer");
   Observer.User firstUser = new("Simon");
   Observer.User secondUser = new("Cindy");

   // 讓使用者訂閱作者
   firstUser.Subscribe(author);
   secondUser.Subscribe(author);

   // 作者寫新文章
   Article article = new("Tomodachi Life", "朋友收集 夢想生活", author.Id);
   author.Publish(article);

   // 新文章 + 退訂
   Console.WriteLine();
   Console.WriteLine("--------Changes in article-----------");
   article = article.WithTitle("Tomodachi Life is Goooood");
   author.RemoveSubscriber(secondUser);
   author.Publish(article);
}
```

## 練習Delegate取代ISubscriber

```csharp
using CommonArticleLibrary;

namespace EventsPractice.Delegate
{
    public delegate void SubscriberDelegate(string message);

    internal interface IPublisher
    {
        public void AddSubscriber(Guid subscriberId, SubscriberDelegate subscriber);
        public void RemoveSubscriber(Guid subscriberId);
        public void Publish(Article article);
    }
}
```

```csharp
using CommonArticleLibrary;

namespace EventsPractice.Delegate
{
    internal record Author : DomainEntity, IPublisher
    {
        private readonly Dictionary<Guid, SubscriberDelegate>? subscribers;

        public string Name { get; init; }
        public string Description { get; init; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
            subscribers = [];
        }

        public void AddSubscriber(Guid subscriberId, SubscriberDelegate subscriber)
        {
            subscribers?.Add(subscriberId, subscriber);
        }

        public void RemoveSubscriber(Guid subscriberId)
        {
            subscribers?.Remove(subscriberId);
        }

        public void Publish(Article article)
        {
            Article createdArticle = article.Create();
            Notify(createdArticle.ToString());
        }

        private void Notify(string message)
        {
            if (subscribers == null) return;

            foreach (var item in subscribers!.Values)
            {
                item(message);
            }
        }
    }
}
```

```csharp
static void Main(string[] args)
{
   Delegate.Author author = new("Nintendo", "Game Developer");
   Observer.User firstUser = new("Simon");
   Observer.User secondUser = new("Cindy");

   author.AddSubscriber(firstUser.Id, firstUser.Update);
   author.AddSubscriber(secondUser.Id, secondUser.Update);

   Article article = new("Tomodachi Life", "朋友收集 夢想生活", author.Id);
   author.Publish(article);

   Console.WriteLine();
   Console.WriteLine("--------Changes in article-----------");
   article = article.WithTitle("Tomodachi Life is Goooood");
   author.RemoveSubscriber(secondUser.Id);
   author.Publish(article);
}
```

### 小結：Delegate 與介面

- 若只有單一簽章、不需狀態，技術上可用 `delegate` 取代介面。
- 從架構角度，講者認為 **介面通常仍比 delegate 更合適**。

## 練習Event取代ISubscriber

```csharp
using CommonArticleLibrary;

namespace EventsPractice.Event
{
    public delegate void SubscriberDelegate(string message);

    internal interface IPublisher
    {
        public event SubscriberDelegate? OnPublish;

        public void Publish(Article article);
    }
}
```

```csharp
using CommonArticleLibrary;

namespace EventsPractice.Event
{
    internal record Author : DomainEntity, IPublisher
    {
        public event SubscriberDelegate? OnPublish;

        public string Name { get; init; }
        public string Description { get; init; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Publish(Article article)
        {
            Article createdArticle = article.Create();
            string subscriberUpdateMessage = article.ToString();
            OnPublish?.Invoke(subscriberUpdateMessage);
        }
    }
}
```

```csharp
static void Main(string[] args)
{
   Event.Author author = new("Nintendo", "Game Developer");
   Observer.User firstUser = new("Simon");
   Observer.User secondUser = new("Cindy");

   // 讓使用者訂閱作者
   author.OnPublish += firstUser.Update;
   author.OnPublish += secondUser.Update;

   // 作者寫新文章
   Article article = new("Tomodachi Life", "朋友收集 夢想生活", author.Id);
   author.Publish(article);

   // 新文章 + 退訂
   Console.WriteLine();
   Console.WriteLine("--------Changes in article-----------");
   article = article.WithTitle("Tomodachi Life is Goooood");
   author.OnPublish -= secondUser.Update;
   author.Publish(article);
}
```

### 觀念對照：Event vs.「當參數的 Delegate」

- **使用時機**：在有某件事發生、要通知訂閱者時，用 event 較直覺；delegate 也能做，但先前那種自己存清單、foreach 通知的寫法比較繁瑣。
- **一般慣例**：delegate 常當方法參數（例如 callback）；若 API 要求傳入 delegate，語意上往往是必填，一定要在某處被呼叫，系統才算完整。
- **Event**：可以有 0 個或多個訂閱者；沒人訂閱時，程式仍可正常執行（註解掉訂閱程式碼也沒問題）。
- **對比**：若把語意上應該是 callback 的 delegate 當成必傳參數（例如某 LINQ 風格方法接受 delegate），不傳可能讓流程無法合理完成；event 則可不訂閱、不通知任何部分。

## 練習EventHandler取代Event

```csharp
using CommonArticleLibrary;

namespace EventsPractice.EventHandler
{
    public class PublishEventArgs(string message) : EventArgs
    {
        public string Message { get; init; } = message;
    }

    internal interface IPublisher
    {
        // 有通知機制時，優先採用 EventHandler 比自訂 delegate 更妥
        // EventHandler<T> 為泛型：若要傳自訂資料給訂閱者，應定義 EventArgs 子類
        event EventHandler<PublishEventArgs> OnPublish;

        public void Publish(Article article);
    }
}
```

```csharp
using CommonArticleLibrary;

namespace EventsPractice.EventHandler
{
    internal record Author : DomainEntity, IPublisher
    {
        public event EventHandler<PublishEventArgs>? OnPublish;

        public string Name { get; init; }
        public string Description { get; init; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Publish(Article article)
        {
            Article createdArticle = article.Create();
            string subscriberUpdateMessage = createdArticle.ToString();
            // Sender: 傳自己當作 sender
            OnPublish?.Invoke(this, new PublishEventArgs(subscriberUpdateMessage));
        }
    }
}
```

```csharp
using CommonArticleLibrary;

namespace EventsPractice.EventHandler
{
    internal record User : DomainEntity
    {
        private EventHandler<PublishEventArgs>? _publishLambda;

        public string Name { get; init; }

        public User(string name)
        {
            Name = name;
        }

        public void Subscribe(IPublisher publisher)
        {
            //publisher.OnPublish += Publisher_OnPublish;

            // Lambda 版本
            // 一定要建一個EventHandler，確保都用同一個實例綁定、解除事件通知
            if (_publishLambda == null)
                _publishLambda = new ((sender, args) => Console.WriteLine(args.Message));
            publisher.OnPublish += _publishLambda;
        }

        public void Unsubscribe(IPublisher publisher)
        {
            //publisher.OnPublish -= Publisher_OnPublish;

            if (_publishLambda != null)
                publisher.OnPublish -= _publishLambda;
        }

        private void Publisher_OnPublish(object? sender, PublishEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
```

```csharp
static void Main(string[] args)
{
   EventHandler.Author author = new("Nintendo", "Game Developer");
   EventHandler.User firstUser = new("Simon");
   EventHandler.User secondUser = new("Cindy");

   // 讓使用者訂閱作者
   firstUser.Subscribe(author);
   secondUser.Subscribe(author);

   // 作者寫新文章
   Article article = new("Tomodachi Life", "朋友收集 夢想生活", author.Id);
   author.Publish(article);

   // 新文章 + 退訂
   Console.WriteLine();
   Console.WriteLine("--------Changes in article-----------");
   article = article.WithTitle("Tomodachi Life is Goooood");
   secondUser.Unsubscribe(author);
   author.Publish(article);
}
```

# 補充

## 專有名詞

- **鬆散耦合(Loose Coupling)**：系統中的各個元件（例如類別、模組或物件）彼此之間的依賴程度很低，互相知道的細節越少越好。
- **緊密耦合(Tight Coupling)**：與**鬆散耦合**相反。
  - 以餐廳點餐為例，你必須走進廚房，找到負責做漢堡的廚師，然後直接告訴他漢堡的具體做法、要加多少鹽和醬料。如果廚師換人了，你就必須重新認識新廚師才能點餐。
  - 鬆散耦合的餐廳點餐：你只需要在座位上，對著服務鈴按一下，服務生就會過來問你要點什麼。你不需要知道廚房裡是誰在做漢堡，也不需要知道漢堡是怎麼做的。如果廚師換人了，也不會點不到漢堡。

## C# `record` 是什麼？

`record` 是 C# 9 起引入的一種**參考型別**（預設是 `record class`，與 `class` 一樣在堆積上配置），語法上用來表示「**以資料為中心、以值語意比較為主**」的型別。

常見寫法：

```csharp
public record Person(string Name, int Age);
```

編譯器會幫你產生：

- 屬性（上例的 `Name`、`Age`）
- **以值相等**為主的 `Equals` / `GetHashCode`（會比對各欄位，而不是只比參考）
- `ToString()`（會印出有意義的內容）
- `with` 表達式用的複製語意（`with` 會產生新實例並只改指定欄位）

也有 `record struct`（C# 10），是**實值型別**的 record，同樣強調值相等與簡潔語法。

---

### 主要用途

1. **DTO / 唯讀資料模型**  
   API 回傳、訊息、設定片段等「一組欄位」的載體，需要**相等性依內容**而不是依物件身分。
2. **不可變（immutable）資料**  
   搭配 `init` 或 positional record，容易做出「建立後不變」的物件，並用 `with` 做**非破壞性更新**。
3. **模式比對與分解**  
   `record` 與 `switch` / `is` 模式比對、分解（deconstruction）搭配得很好，適合表達「這種形狀的資料」。
4. **減少樣板程式**  
   少寫手動實作的 `Equals`、`GetHashCode`、`ToString`、複製建構子邏輯。

---

### 和 `class` 的直覺差異

| 面向 | 典型 `class` | `record`（預設） |
|------|----------------|------------------|
| 相等性 | 常預設為**參考相等** | 預設為**值相等**（依欄位） |
| 常見用途 | 行為 + 狀態、領域物件 | 資料載體、值語意模型 |

若你需要**可變狀態、繼承階層複雜、以身分識別為主**的物件，仍可能用一般 `class`；若以**內容相同即視為相同、不可變與複製更新**為主，`record` 很合適。

## `init` 是什麼？

在 C# 裡，`init` 是**屬性存取子**的一種（和 `get`、`set` 並列），從 **C# 9** 開始提供。

```csharp
public string Description { get; init; }
```

意思是：**這個屬性只能在「物件建立／初始化」的那段期間被賦值，建立完成後就不能再從外面改。**

- 物件初始設定式（object initializer）裡：`new Author { Description = "..." }`
- 建構函式裡：`this.Description = ...`

建立好之後，若再寫 `author.Description = "別的"`，**編譯會失敗**（除非在型別內部有特殊設計）。

## `with` 表達式

```csharp
public Article Create()
{
   return this with { IsPublished = true };
}
```

`this with { ... }` 是 **`with` 表達式**（C# 9+）：用**目前這個物件當範本**，**複製出一個新的 `Article`**，並且只把大括號裡列出的屬性改成新值。

所以 `Create()` 的意思是：

- **不會**改動原本的 `this`（`IsPublished` 還是建構／初始設定時的值，預設是 `false`）。
- **會回傳另一個** `Article`，內容和 `this` 一樣，但 **`IsPublished = true`**。

也就是「以不可變／以複製為主的更新」：常搭配 `init` 屬性，做出「新版本」而不是當場修改欄位。

---

### 若沒有 `with` 要怎麼想？

概念上接近：「做一個跟我一樣的新物件，但把 `IsPublished` 設成 `true`。」`record` 會幫你處理複製各欄位；手寫 `class` 時通常要自己 `new Article(...)` 把所有欄位再傳一遍。

---

## event 深入解析

`event` 的底層是委派，委派是 `multicast` 宣告這行：

```csharp
public event SubscriberDelegate? OnPublish;
```

這時 `OnPublish` 的初始值是 `null`，還不是任何實例。

當你第一次 += 時：

```csharp
author.OnPublish += firstUser.Update;
```

C# 編譯器會把這行展開成類似：

```csharp
author.OnPublish = (SubscriberDelegate)Delegate.Combine(author.OnPublish, firstUser.Update);
```

`Delegate.Combine` 的行為：

- 若左側是 `null`，就直接回傳右側，這時才建立第一個委派實例
- 若左側已有實例，就把兩個合併成一個新的 `multicast` 委派實例（舊的被取代）
