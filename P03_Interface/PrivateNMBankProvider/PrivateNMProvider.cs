using DecodeBytes.Provider;

namespace PrivateNMBankProvider
{
    public class PrivateNMProvider : IBankProvider
    {
        private Dictionary<string, decimal> Cards;
        private const decimal Comission = 1.2m;

        public string ProviderName => "PrivateNM Bank";

        public PrivateNMProvider()
        {
            Cards = LoadCards();
        }

        public void AddToBalance(CardNumber cardNumber, decimal amount)
        {
            if (!IsCardNumberExist(cardNumber.Number))
                throw new ArgumentException("Card number is not valid");
            var totalAmount = CalculateComission(amount);
            Cards[cardNumber.Number] += totalAmount;
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
                { "1000-2222-3333-4444", 1500 },
                { "1000-2222-3333-5555", 2700 }
            };
        }

        private bool IsCardNumberExist(string cardNumber)
        {
            return Cards.Where(x => x.Key == cardNumber).Any();
        }

        private static decimal CalculateComission(decimal amount)
        {
            return amount - (amount * Comission / 100);
        }
    }
}
