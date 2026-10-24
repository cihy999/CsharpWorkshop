using CommonArticleLibrary;

namespace EventsPractice.Delegate
{
    internal record Author : DomainEntity, IPublisher
    {
        private readonly Dictionary<Guid, SubscriberDelegate>? subscribers;

        public string Name { get; init; }
        public string Description { get; init; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
            subscribers = [];
        }

        public void AddSubscriber(Guid subscriberId, SubscriberDelegate subscriber)
        {
            subscribers?.Add(subscriberId, subscriber);
        }

        public void RemoveSubscriber(Guid subscriberId)
        {
            subscribers?.Remove(subscriberId);
        }

        public void Publish(Article article)
        {
            Article createdArticle = article.Create();
            Notify(createdArticle.ToString());
        }

        private void Notify(string message)
        {
            if (subscribers == null) return;

            foreach (var item in subscribers!.Values)
            {
                item(message);
            }
        }
    }
}
