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
