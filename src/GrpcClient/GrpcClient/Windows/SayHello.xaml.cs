using System.Windows;

namespace GrpcClient.Windows
{
    public partial class SayHello : Window
    {
        public string Value { get; set; }

        public SayHello()
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
