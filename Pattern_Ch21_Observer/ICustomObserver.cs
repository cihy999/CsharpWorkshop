using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Ch21_Observer
{
    public interface ICustomObserver<T>
    {
        public void Update(T data);
    }

    public abstract class CustomSubject<T>
    {
        protected List<ICustomObserver<T>> _observers = new();

        public void AddObserver(ICustomObserver<T> observer) => _observers.Add(observer);

        public void RemoveObserver(ICustomObserver<T> observer) => _observers.Remove(observer);

        public void Notify() 
        {
            foreach (var observer in _observers)
                observer.Update(GetData());
        }

        protected abstract T GetData();
    }
}
