using CommonArticleLibrary;
using EventsPractice.Observer;

namespace EventsPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Author author = new("Nintendo", "Game Developer");
            User firstUser = new("Simon");
            User secondUser = new("Cindy");

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
    }
}
