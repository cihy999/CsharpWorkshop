using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Ch21_Observer
{
    public class ConcretePullSubject : PullSubject
    {
        private string _message = "";

        public string Message { get { return _message; } }

        public void SetMessage(string msg)
        {
            _message = msg;
            Notify();
        }
    }

    public class ConcretePushSubject : PushSubject<string>
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

    public class ConcretePullObserver : IPullObserver
    {
        ConcretePullSubject? _subject = null;

        public ConcretePullObserver(ConcretePullSubject subject)
        {
            _subject = subject;
        }

        /// <summary>
        /// 採用拉訊息方式(Pull)，獲取通知
        /// </summary>
        /// <param name="subject"></param>
        public void Update()
        {
            Console.WriteLine($"Pull messgae: {_subject?.Message ?? ""}");
        }
    }

    public class ConcretePushObserver : IPushObserver<string>
    {
        /// <summary>
        /// 採用推訊息方式(Push)，獲取通知
        /// </summary>
        /// <param name="data"></param>
        public void Update(string data)
        {
            Console.WriteLine($"Push Message: {data}");
        }
    }
}
