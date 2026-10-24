using CommonArticleLibrary;

namespace EventsPractice.Observer
{
    internal record User : DomainEntity, ISubscriber
    {
        public string Name { get; init; }

        public User(string name)
        {
            Name = name;
        }

        public void Subscribe(IPublisher publisher)
        {
            publisher.AddSubscriber(this);
        }

        public void Unsubscribe(IPublisher publisher)
        {
            publisher.RemoveSubscriber(this);
        }

        public void Update(string message)
        {
            Console.WriteLine(message);
        }
    }
}
