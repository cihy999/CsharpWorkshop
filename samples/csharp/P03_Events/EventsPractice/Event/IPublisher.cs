using CommonArticleLibrary;

namespace EventsPractice.Event
{
    public delegate void SubscriberDelegate(string message);

    internal interface IPublisher
    {
        public event SubscriberDelegate? OnPublish;

        public void Publish(Article article);
    }
}
