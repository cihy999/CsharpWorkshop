using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Ch21_Observer
{
    public interface IPullObserver
    {
        public void Update();
    }

    public interface IPushObserver<T>
    {
        public void Update(T data);
    }

    public abstract class PullSubject
    {
        protected List<IPullObserver> _observers = new();

        public void AddObserver(IPullObserver observer) => _observers.Add(observer);

        public void RemoveObserver(IPullObserver observer) => _observers.Remove(observer);

        public void Notify()
        {
            foreach (var observer in _observers)
                observer.Update();
        }
    }

    public abstract class PushSubject<T>
    {
        protected List<IPushObserver<T>> _observers = new();

        public void AddObserver(IPushObserver<T> observer) => _observers.Add(observer);

        public void RemoveObserver(IPushObserver<T> observer) => _observers.Remove(observer);

        public void Notify()
        {
            foreach (var observer in _observers)
                observer.Update(GetData());
        }

        protected abstract T GetData();
    }
}
