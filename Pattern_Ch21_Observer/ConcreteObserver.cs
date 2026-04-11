using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Ch21_Observer
{
    public class ConcreteSubject : CustomSubject<string>
    {
        private string _message = "";

        public string Message { get { return _message; } }

        public void SetMessage(string msg)
        {
            _message = msg;
            Notify();
        }

        protected override string GetData() => _message;
    }

    public class ConcreteObserver : ICustomObserver<string>
    {
        /// <summary>
        /// 採用推訊息方式(Push)，獲取通知
        /// </summary>
        /// <param name="msg"></param>
        public void Update(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public class ConcretePullSubject : CustomSubject<ConcretePullSubject>
    {
        private string _message = "";

        public string Message { get { return _message; } }

        public void SetMessage(string msg)
        {
            _message = msg;
            Notify();
        }

        protected override ConcretePullSubject GetData() => this;
    }

    public class ConcretePullObserver : ICustomObserver<ConcretePullSubject>
    {
        /// <summary>
        /// 採用拉訊息方式(Pull)，獲取通知
        /// </summary>
        /// <param name="subject"></param>
        public void Update(ConcretePullSubject subject)
        {
            Console.WriteLine(subject.Message);
        }
    }
}
