# C#筆記 - ch07 - 繼承、多型、介面

[Visual C#程式設計經典-邁向Azure雲端、AI影像辨識與OpenAI API服務開發 - Ch07 繼承、多型、介面](https://play.google.com/store/books/details?id=-Nr9EAAAQBAJ) 的學習筆記。

## 繼承

```csharp
namespace Ch07_Inherit
{
    internal class Employee
    {
        private int _salary = 0;

        public int Salary 
        {
            get 
            { 
                return _salary;
            }
            set 
            {
                if (value < 20000)
                    _salary = 20000;
                else if (value > 40000)
                    _salary = 40000;
                else
                    _salary = value;
            }
        }
    }

    internal class Manager : Employee 
    { 
        public int Bonus { get; set; }

        public int TotalPayment { get { return Salary + Bonus; } }
    }
}
```

## 靜態成員 & 靜態方法

```csharp
namespace Ch07_Inherit
{
    internal class Car
    {
        public static int Total { get; set; }
        public int No { get; set; }
        public string Name { get; set; }

        public Car() 
        {
            Total++;
            No = Total;
            Name = "";
        }

        public Car(string name)
        {
            Total++;
            No = Total;
            Name = name;
        }

        ~Car() 
        {
            Total--;
        }

        public static string GetTotalCarString() 
        {
            return $"現在共有 {Total} 部車";
        }

        public string GetCarNoString()
        {
            return $"{Name} 是第 {No} 部車";
        }
    }
}
```

### 補充

在類別中宣告，其生命週期是程式執行開始到結束。靜態變數會被放到全域變數區，因此類別不需要建立物件就能使用，且物件都共用同一份靜態變數。

宣告C# 靜態變數會根據型別給初始值。

靜態方法(static method)：不需要建立物件就能使用，且物件都共用同一份靜態方法。

```text
[Code / Type Metadata]
Car type
 ├─ methods:
 │   Car::.ctor()
 │   Car::.ctor(string)
 │   Car::Finalize()        // ~Car
 │   Car::GetTotalCarString()
 │   Car::GetCarNoString()
 └─ static field slot:
     Total  ----------------------+
                                  |
[Static Area]                     |
Car.Total = 2  <------------------+

[Heap]
Object Car#1 (c1)
 ├─ No = 1
 └─ Name -> "BMW"

Object Car#2 (c2)
 ├─ No = 2
 └─ Name -> "Toyota"

[Stack / Local references]
c1 ----> Car#1
c2 ----> Car#2
```

## 多型

```csharp
namespace Ch07_Inherit
{
    internal class Employee
    {
        protected int _salary = 0;

        public virtual int Salary
        {
            get 
            { 
                return _salary;
            }
            set 
            {
                if (value < 20000)
                    _salary = 20000;
                else if (value > 40000)
                    _salary = 40000;
                else
                    _salary = value;
            }
        }

        public virtual int TotalPayment { get { return _salary; } }
    }

    internal class Manager : Employee 
    { 
        public int Bonus { get; set; }

        public override int Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                if (value < 30000)
                    _salary = 30000;
                else if (value > 60000)
                    _salary = 60000;
                else
                    _salary = value;
            }
        }

        public override int TotalPayment { get { return base.TotalPayment + Bonus; } }
    }
}
```

### 動態繫結

```csharp
namespace Ch07_Inherit
{
    internal abstract class Traffic
    {
        protected static int _miles = 0;

        public virtual void SpeedUp()
        { 
            
        }
    }

    internal class Boat : Traffic
    {
        public override void SpeedUp()
        {
            _miles += 5;
            Console.WriteLine($"駕駛船，加速中，前進至{_miles}公里");
        }
    }

    internal class Airplane : Traffic
    {
        public override void SpeedUp()
        {
            _miles += 10;
            Console.WriteLine($"駕駛飛機，加速中，前進至{_miles}公里");
        }
    }
}
```

```csharp
namespace Ch07_Inherit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowSpeedUp();
        }

        private static void ShowSpeedUp()
        {
            // 抽象類別不能建立實例
            //Traffic r = new Traffic();
            Traffic r1 = new Boat();
            Traffic r2 = new Airplane();

            r1.SpeedUp();
            r2.SpeedUp();
        }
    }
}
```

## 專有名詞

- 基底類別 Base Class、父類別 Parent Class、超類別 Super Class
- 衍生類別 Derived Class、子類別 Child Class、次類別 Sub Class
- 多型 Polymorphism
- 動態繫結 Dynamic Binding：程式進入執行階段時，物件參考才決定所要執行的方法。
