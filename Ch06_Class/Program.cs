namespace Ch06_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"建立一個屬於ClassA的物件A");
            
            ClassA a = new ClassA("A");

            a.Number = 15;
            a.SayHello();
            a.SayHello("Hi");

            Console.WriteLine($"野生物件A已出現！");

            Console.WriteLine($"建立一個屬於IBM的Notebook");

            IBM.Notebook n1 = new IBM.Notebook();

            n1.SayHello();

            Console.WriteLine($"建立一個屬於Apple的Notebook");

            Apple.Notebook n2 = new Apple.Notebook();

            n2.SayHello();
        }
    }
}
