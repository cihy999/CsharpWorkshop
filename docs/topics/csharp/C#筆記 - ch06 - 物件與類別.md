---
title: C#筆記 - ch06 - 物件與類別
tags: [C#]

---

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

## 命名空間 Namespace

有效分類類別，甚至避免重名問題。

```csharp
namespace Ch06_Class
{
    namespace IBM
    {
        internal class Notebook
        {
            public void SayHello()
            {
                Console.WriteLine("IBM Notebook: Hello");
            }
        }
    }

    namespace Apple 
    {
        internal class Notebook
        {
            public void SayHello()
            {
                Console.WriteLine("Apple Notebook: Hello");
            }
        }
    }
}
```

## 屬性

```csharp
namespace Ch06_Class
{
    internal class ClassA
    {
        private int _number = 0;

        public int Number 
        { 
            get 
            { 
                return _number; 
            } 
            set 
            { 
                if (value < 0) _number = 0;
                _number = value;
            } 
        }

        public string Name { get; set; } = "";
    }
}
```

## 方法

```csharp
namespace Ch06_Class
{
    internal class ClassA
    {
        // 省略屬性...

        public void SayHello() 
        {
            Console.WriteLine($"{Name}: Hello, my number = {Number}");
        }

        public void SayHello(string greeting)
        {
            Console.WriteLine($"{Name}: {greeting}, my number = {Number}");
        }
    }
}
```

## 建構函式與解構函式

```csharp
namespace Ch06_Class
{
    internal class ClassA
    {
        public string Name { get; private set; } = "";

        // 建構函式
        public ClassA()
        {
            Name = "Object A";
        }

        public ClassA(string name)
        {
            Name = name;
        }

        // 解構函式
        ~ClassA() 
        { 
            
        }
    }
}
```

## 存取權限

### protected internal

protected internal 在 C# 是聯集（OR），不是交集（AND）——這是最容易搞混的地方。

可以存取的範圍 = 「同組件的任何人」∪「任何組件的衍生類別」。

具體拆開看：

| 存取者 | 同組件 | 跨組件 |
| -------- | -------- | -------- |
| 一般類別（非衍生類別） | ✅ 可存取（靠 internal） | ❌ 不可存取 |
| 衍生類別 | ✅ 可存取 | ✅ 可存取（靠 protected） |

補充：若寫成 private protected（C# 7.2+）則是交集（AND）——必須「同組件 且 是衍生類別」才行，範圍比兩者都更窄，這次不適用。

## 物件初始設定式

```csharp
ClassA a = new ClassA("A") { Number = 15 };
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
- **方法多載(Method Overloading)**：方法名稱相同，但參數個數或型態不同。
- **建構函式(Constructor Function)**
- **解構函式(Destructor Function)**
