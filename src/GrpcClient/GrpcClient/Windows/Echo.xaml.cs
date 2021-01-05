using System.Windows;

namespace GrpcClient.Windows
{
    public partial class Echo : Window
    {
        public string Value { get; set; }

        public Echo()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
