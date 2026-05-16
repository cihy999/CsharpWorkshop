namespace DecodeBytes.Provider
{
    public interface IBankProvider
    {
        string ProviderName { get; }
        void AddToBalance(CardNumber cardNumber, decimal amount);
        decimal GetBalance(CardNumber cardNumber);
    }
}
