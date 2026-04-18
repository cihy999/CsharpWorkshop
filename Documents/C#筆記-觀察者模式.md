# C#筆記 - 觀察者模式

[設計模式與遊戲開發的完美結合](https://play.google.com/store/books/details?id=g98dDQAAQBAJ) 的學習筆記。

## [耦合] 冗長的遊戲事件通知

```csharp
// Enemy角色介面
public abstract class IEnemy : ICharacter
{
    // 被武器攻擊
    public override void UnderAttack(ICharacter attacker)
    {
        ...

        // 是否陣亡
        if (mAttribute.GetNowHP() <= 0)
        {
            Killed();

            // 通知成就系統
            AchievementSystem.NotifyGameEvent(
                ENUM_GameEvent.EnemyKilled,
                this,
                null);

            // 通知B系統
            ...

            // 通知C系統
            ...

            // 通知D系統
            ...
        }
    }
}
```

問題：

1. `IEnemy` 本來只應該負責「戰鬥邏輯」，但它現在還要知道遊戲裡有哪些系統需要被通知，已經是職責的越界。
    - *成就系統僅關注於某些遊戲事件的發生；而遊戲事件的發生，也不是只提供給成就系統使用。*
    - **違反開放封閉原則 Open/Closed Principle**：每次要新增一個系統需要被通知，都必須修改 `IEnemy` 類別。
2. **依賴方向錯誤**：`IEnemy` 是遊戲核心實體，`AchievementSystem` 是外圍功能，依賴方向應該是外圍依賴核心，而不是反過來。

**解決方案**：觀察者模式(Observer Pattern)！

## 觀察者模式(Observer Pattern)

> GoF對觀察者模式(Observer)的定義為：
>「在物件之間定義一個一對多的連接方法，當一個物件變換狀態時，其它關連的物件都會自動收到通知。」

```plantuml
@startuml Observer Pattern

abstract Subject 
{
    # observers: List<IObserver>
    + AddObserver(o: IObserver)
    + RemoveObserver(o: IObserver)
    + Notify()
}

interface IObserver 
{
    + Update(data: T)
}

Subject --> "0..*" IObserver

@enduml
```

```plantuml
@startuml Observer Pattern

abstract Subject
interface IObserver

ConcreteSubject ..|> Subject
ConcreteObserver ..|> IObserver

@enduml
```

## 實作觀察者模式(推送型 + 泛型)

觀察者、主題的基礎類別設計：

```csharp
public interface IPushObserver<T>
{
    public void Update(T data);
}

public abstract class PushSubject<T>
{
    protected List<IPushObserver<T>> _observers = new();

    public void AddObserver(IPushObserver<T> observer) => _observers.Add(observer);

    public void RemoveObserver(IPushObserver<T> observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
            observer.Update(GetData());
    }

    protected abstract T GetData();
}
```

實作觀察者、主題類別：

```csharp
public class ConcretePushSubject : PushSubject<string>
{
    private string _message = "";

    public string Message { get { return _message; } }

    public void SetMessage(string msg)
    {
        _message = msg;
        Notify();
    }

    protected override string GetData() => _message;
}

public class ConcretePushObserver : IPushObserver<string>
{
    /// <summary>
    /// 採用推訊息方式(Push)，獲取通知
    /// </summary>
    /// <param name="data"></param>
    public void Update(string data)
    {
        Console.WriteLine($"Push Message: {data}");
    }
}
```

使用觀察者、主題類別：

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        ConcretePullSubject pullSubject = new ();
        ConcretePullObserver pullObserver = new (pullSubject);
        pullSubject.AddObserver(pullObserver);
        pullSubject.SetMessage("Hello, World!");
    }
}
```

## 實作觀察者模式(拉訊息)

觀察者、主題的基礎類別設計：

```csharp
public interface IPullObserver
{
    public void Update();
}

public abstract class PullSubject
{
    protected List<IPullObserver> _observers = new();

    public void AddObserver(IPullObserver observer) => _observers.Add(observer);

    public void RemoveObserver(IPullObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
            observer.Update();
    }
}
```

實作觀察者、主題類別：

```csharp
public class ConcretePullSubject : PullSubject
{
    private string _message = "";

    public string Message { get { return _message; } }

    public void SetMessage(string msg)
    {
        _message = msg;
        Notify();
    }
}

public class ConcretePullObserver : IPullObserver
{
    ConcretePullSubject? _subject = null;

    public ConcretePullObserver(ConcretePullSubject subject)
    {
        _subject = subject;
    }

    /// <summary>
    /// 採用拉訊息方式(Pull)，獲取通知
    /// </summary>
    /// <param name="subject"></param>
    public void Update()
    {
        Console.WriteLine($"Pull messgae: {_subject?.Message ?? ""}");
    }
}
```

使用觀察者、主題類別：

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        ConcretePushSubject pushSubject = new();
        ConcretePushObserver pushObserver = new();
        pushSubject.AddObserver(pushObserver);
        pushSubject.SetMessage("Hi, World!");
    }
}
```

## Push & Pull 選擇

- Push 優點：推送所有內容給觀察者，省去觀察者查詢動作
- Push 缺點：推送的內容過多，會使觀察者收到不必要的資訊
- Pull 優點：觀察者只需要被通知更新、再查詢所需資訊
- Pull 缺點：主題必須提供查詢方式，容易造成主題類別的方法過多
