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

## 專有名詞

- 基底類別 Base Class、父類別 Parent Class、超類別 Super Class
- 衍生類別 Derived Class、子類別 Child Class、次類別 Sub Class
