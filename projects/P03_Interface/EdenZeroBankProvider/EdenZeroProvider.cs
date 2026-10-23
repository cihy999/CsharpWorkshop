using DecodeBytes.Provider;

namespace EdenZeroBankProvider
{
    public class EdenZeroProvider : IBankProvider
    {
        private Dictionary<string, decimal> Cards;

        public string ProviderName => "EdenZero";

        public EdenZeroProvider()
        {
            Cards = LoadCards();
        }

        public void AddToBalance(CardNumber cardNumber, decimal amount)
        {
            if (!IsCardNumberExist(cardNumber.Number))
                throw new ArgumentException("Card number is not valid");
            Cards[cardNumber.Number] += amount;
        }

        public decimal GetBalance(CardNumber cardNumber)
        {
            if (!IsCardNumberExist(cardNumber.Number))
                throw new ArgumentException("Card number is not valid");
            return Cards[cardNumber.Number];
        }

        private static Dictionary<string, decimal> LoadCards()
        {
            return new Dictionary<string, decimal>
            {
                { "1111-2222-3333-4444", 500 },
                { "1111-2222-3333-5555", 700 }
            };
        }

        private bool IsCardNumberExist(string cardNumber)
        {
            return Cards.Where(x => x.Key == cardNumber).Any();
        }
    }
}
