namespace EventsPractice.Observer
{
    internal interface ISubscriber
    {
        public void Subscribe(IPublisher publisher);
        public void Unsubscribe(IPublisher publisher);
        public void Update(string message);
    }
}
