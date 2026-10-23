using CommonArticleLibrary;

namespace EventsPractice.EventHandler
{
    internal record User : DomainEntity
    {
        private EventHandler<PublishEventArgs>? _publishLambda;

        public string Name { get; init; }

        public User(string name)
        {
            Name = name;
        }

        public void Subscribe(IPublisher publisher)
        {
            //publisher.OnPublish += Publisher_OnPublish;

            // Lambda 版本
            // 一定要建一個EventHandler，確保都用同一個實例綁定、解除事件通知
            if (_publishLambda == null)
                _publishLambda = new ((sender, args) => Console.WriteLine(args.Message));
            publisher.OnPublish += _publishLambda;
        }

        public void Unsubscribe(IPublisher publisher)
        {
            //publisher.OnPublish -= Publisher_OnPublish;

            if (_publishLambda != null)
                publisher.OnPublish -= _publishLambda;
        }

        private void Publisher_OnPublish(object? sender, PublishEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
