using CommonArticleLibrary;

namespace EventsPractice.Observer
{
    internal record Author : DomainEntity, IPublisher
    {
        private readonly List<ISubscriber>? subscribers;

        public string Name { get; init; }
        public string Description { get; init; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
            subscribers = [];
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            subscribers?.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            subscribers?.Remove(subscriber);
        }

        public void Publish(Article article)
        {
            Article createdArticle = article.Create();
            string subscriberUpdateMessage = createdArticle.ToString();
            Notify(subscriberUpdateMessage);
        }

        private void Notify(string message)
        {
            subscribers?.ForEach(subscriber =>
            {
                subscriber.Update(message);
            });
        }
    }
}
