# C#筆記 - ch06 - 物件與類別

[Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch06 物件與類別](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ) 的學習筆記。

```csharp
namespace Ch06_Class
{
    internal class ClassA
    {
        public void SayHello() 
        {
            Console.WriteLine("Object A: Hello");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"建立一個屬於ClassA的物件A");
            
            ClassA a = new ClassA();

            a.SayHello();

            Console.WriteLine($"野生物件A已出現！");
        }
    }
}
```

## 專有名詞

- **屬性(Properties)**
- **方法(Methods)**
- **類別(Class)**
- **物件實體(Instance)**
- **抽象化(Abstraction)**
- **封裝(Encapsulation)**
- **繼承(Inheritance)**
- **多型(Polymorphism)**
- **靜態繫結(Static Binding)**：編譯器在編譯時期就能確定要呼叫的方法位址。
- **動態繫結(Dynamic Binding)**：編譯器在執行時期才確定要呼叫的方法位址。
  - **虛擬表格(Virtual Table)**：物件方法位址表，執行時期從表格中得知要呼叫的方法位址。
- **抽象資料型態(Abstract Data Type, ADT)**
- **父類別(Super Class)、基礎類別(Base Class)**
- **子類別(Subclass)、衍生類別(Derived Class)**
