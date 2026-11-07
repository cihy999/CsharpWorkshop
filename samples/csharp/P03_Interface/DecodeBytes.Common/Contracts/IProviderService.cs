using DecodeBytes.Provider;

namespace DecodeBytes.Common.Contracts
{
    /// <summary>
    /// 專門負責「找尋銀行」的服務，讓 UI 層不直接依賴於特定的銀行類別
    /// </summary>
    public interface IProviderService
    {
        IEnumerable<IBankProvider> GetProviders();
    }
}
