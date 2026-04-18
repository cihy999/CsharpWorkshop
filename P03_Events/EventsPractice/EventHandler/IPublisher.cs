using CommonArticleLibrary;

namespace EventsPractice.EventHandler
{
    public class PublishEventArgs(string message) : EventArgs
    {
        public string Message { get; init; } = message;
    }

    internal interface IPublisher
    {
        // 有通知機制時，優先採用 EventHandler 比自訂 delegate 更妥
        // EventHandler<T> 為泛型：若要傳自訂資料給訂閱者，應定義 EventArgs 子類
        event EventHandler<PublishEventArgs> OnPublish;

        public void Publish(Article article);
    }
}
