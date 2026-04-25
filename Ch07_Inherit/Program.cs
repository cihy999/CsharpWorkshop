namespace Ch07_Inherit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowCar();
        }

        private static void ShowEmployeeSalary() 
        { 
            Employee tom = new Employee();
            tom.Salary = 50000;
            Console.WriteLine($"Tom的薪水: {tom.Salary:0,0}");
            Console.WriteLine($"=============================");
            Console.WriteLine();

            Manager peter = new Manager();
            peter.Salary = 50000;
            Console.WriteLine($"Peter的薪水: {peter.Salary:0,0}");
            peter.Bonus = 30000;
            Console.WriteLine($"Peter的獎金: {peter.Bonus:0,0}");
            Console.WriteLine($"Peter的實領薪水: {peter.TotalPayment:0,0}");
        }

        private static void ShowCar()
        {
            Console.WriteLine($"{Car.GetTotalCarString()}");
            Console.WriteLine("==========================");

            Car benz = new Car("Benz");
            Console.WriteLine($"{benz.GetCarNoString()}");
            Console.WriteLine($"{Car.GetTotalCarString()}");
            Console.WriteLine("==========================");

            Car bmw = new Car("BMW");
            Car ford = new Car("Ford");
            Console.WriteLine($"{bmw.GetCarNoString()}");
            Console.WriteLine($"{ford.GetCarNoString()}");
            Console.WriteLine($"{Car.GetTotalCarString()}");
            Console.WriteLine("==========================");
        }
    }
}
