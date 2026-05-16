namespace DecodeBytes.WinForm
{
    public partial class mainForm : Form
    {
        private FormProviderVisualizer _formProviderVisualizer;

        public mainForm()
        {
            InitializeComponent();
            _formProviderVisualizer = new(groupBoxProviders);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            _formProviderVisualizer.RenderProviders();
        }

        private void btn_relaod_Click(object sender, EventArgs e)
        {
            _formProviderVisualizer.RenderProviders();
        }
    }
}
