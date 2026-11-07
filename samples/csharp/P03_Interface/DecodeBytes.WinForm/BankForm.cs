using DecodeBytes.Provider;

namespace DecodeBytes.WinForm
{
    public partial class BankForm : Form
    {
        private readonly IBankProvider _bankProvider;

        public BankForm(IBankProvider bankProvider)
        {
            InitializeComponent();
            _bankProvider = bankProvider;
        }

        private void btn_checkBalance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbox_cardNumber.Text))
            {
                MessageBox.Show("Not allowed empty input");
                return;
            }
            CardNumber cardNumber = new CardNumber(tbox_cardNumber.Text);
            decimal balance = _bankProvider.GetBalance(cardNumber);
            MessageBox.Show(balance.ToString());
        }

        private void btn_addToBalance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbox_amount.Text) || String.IsNullOrEmpty(tbox_cardNumber.Text))
            {
                MessageBox.Show("Not allowed empty input");
                return;
            }
            CardNumber cardNumber = new CardNumber(tbox_cardNumber.Text);
            decimal amount = Convert.ToDecimal(tbox_amount.Text);
            _bankProvider.AddToBalance(cardNumber, amount);
        }
    }
}
