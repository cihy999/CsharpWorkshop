using CommonArticleLibrary;

namespace EventsPractice.Observer
{
    internal interface IPublisher
    {
        public void AddSubscriber(ISubscriber subscriber);
        public void RemoveSubscriber(ISubscriber subscriber);
        public void Publish(Article article);
    }
}
