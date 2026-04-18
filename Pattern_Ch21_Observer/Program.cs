namespace Pattern_Ch21_Observer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConcretePullSubject pullSubject = new ();
            ConcretePullObserver pullObserver = new (pullSubject);
            pullSubject.AddObserver(pullObserver);
            pullSubject.SetMessage("Hello, World!");

            ConcretePushSubject pushSubject = new();
            ConcretePushObserver pushObserver = new();
            pushSubject.AddObserver(pushObserver);
            pushSubject.SetMessage("Hi, World!");
        }
    }
}
