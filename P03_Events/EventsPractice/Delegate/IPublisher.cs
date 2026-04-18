using CommonArticleLibrary;

namespace EventsPractice.Delegate
{
    public delegate void SubscriberDelegate(string message);

    internal interface IPublisher
    {
        public void AddSubscriber(Guid subscriberId, SubscriberDelegate subscriber);
        public void RemoveSubscriber(Guid subscriberId);
        public void Publish(Article article);
    }
}
