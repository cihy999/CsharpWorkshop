using CommonArticleLibrary;

namespace EventsPractice.EventHandler
{
    internal record Author : DomainEntity, IPublisher
    {
        public event EventHandler<PublishEventArgs>? OnPublish;

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
            string subscriberUpdateMessage = createdArticle.ToString();
            // Sender: 傳自己當作 sender
            OnPublish?.Invoke(this, new PublishEventArgs(subscriberUpdateMessage));
        }
    }
}
