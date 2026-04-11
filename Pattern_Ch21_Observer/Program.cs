namespace Pattern_Ch21_Observer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConcreteSubject helloSubject = new();
            ConcreteObserver observer = new();
            helloSubject.AddObserver(observer);
            helloSubject.SetMessage("Hello, World!");

            ConcretePullSubject helloSubject2 = new();
            ConcretePullObserver observer2 = new();
            helloSubject2.AddObserver(observer2);
            helloSubject2.SetMessage("Hello, World!");
        }
    }
}
