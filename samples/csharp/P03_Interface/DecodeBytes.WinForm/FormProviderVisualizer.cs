using DecodeBytes.Common.Contracts;
using DecodeBytes.Common.Implementations;
using DecodeBytes.Provider;

namespace DecodeBytes.WinForm
{
    public class FormProviderVisualizer(Control control) : IProviderVisualizer
    {
        private const int _startLocationX = 36;
        private const int _startLocationY = 45;
        private const int _sizeX = 200;
        private const int _sizeY = 120;
        private readonly Control _control = control;
        private int _locationX = 0;
        private int _locationY = 0;
        private ProviderService _providerService = new ProviderService();

        public void RenderProviders()
        {
            ClearProviders();
            var providers = _providerService.GetProviders();
            foreach (var provider in providers)
            {
                AddProvider(provider);
            }
        }

        private void InitializeDefaultParams()
        {
            _locationX = _startLocationX;
            _locationY = _startLocationY;
        }

        private void ClearProviders()
        {
            _control.Controls.Clear();
            InitializeDefaultParams();
        }

        private void AddProvider(IBankProvider provider)
        {
            Button button = new()
            {
                Text = provider.ProviderName,
                Size = new Size(_sizeX, _sizeY),
                Location = new Point(_locationX, _locationY)
            };
            button.Click += (sender, args) =>
            {
                new BankForm(provider).ShowDialog();
            };
            _control.Controls.Add(button);
            _locationX += 236;
        }
    }
}
