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
