using CommonArticleLibrary;

namespace EventsPractice.Event
{
    internal record Author : DomainEntity, IPublisher
    {
        public event SubscriberDelegate? OnPublish;

        public string Name { get; init; }
        public string Description { get; init; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Publish(Article article)
        {
            Article createdArticle = article.Create();
            string subscriberUpdateMessage = article.ToString();
            OnPublish?.Invoke(subscriberUpdateMessage);
        }
    }
}
