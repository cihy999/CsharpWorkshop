using CommonArticleLibrary;
using Delegate = EventsPractice.Delegate;
using Observer = EventsPractice.Observer;

namespace EventsPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DoDelegate();
        }

        private static void DoObserver()
        {
            Observer.Author author = new("Nintendo", "Game Developer");
            Observer.User firstUser = new("Simon");
            Observer.User secondUser = new("Cindy");

            // 讓使用者訂閱作者
            firstUser.Subscribe(author);
            secondUser.Subscribe(author);

            // 作者寫新文章
            Article article = new("Tomodachi Life", "朋友收集 夢想生活", author.Id);
            author.Publish(article);

            // 新文章 + 退訂
            Console.WriteLine();
            Console.WriteLine("--------Changes in article-----------");
            article = article.WithTitle("Tomodachi Life is Goooood");
            author.RemoveSubscriber(secondUser);
            author.Publish(article);
        }

        private static void DoDelegate() 
        {
            Delegate.Author author = new("Nintendo", "Game Developer");
            Observer.User firstUser = new("Simon");
            Observer.User secondUser = new("Cindy");

            author.AddSubscriber(firstUser.Id, firstUser.Update);
            author.AddSubscriber(secondUser.Id, secondUser.Update);

            Article article = new("Tomodachi Life", "朋友收集 夢想生活", author.Id);
            author.Publish(article);

            Console.WriteLine();
            Console.WriteLine("--------Changes in article-----------");
            article = article.WithTitle("Tomodachi Life is Goooood");
            author.RemoveSubscriber(secondUser.Id);
            author.Publish(article);
        }
    }
}
