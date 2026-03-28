namespace Ch06_Class
{
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
