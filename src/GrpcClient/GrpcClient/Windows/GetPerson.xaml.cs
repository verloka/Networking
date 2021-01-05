using System.Windows;

namespace GrpcClient.Windows
{
    public partial class GetPerson : Window
    {
        public int Value { get; set; }

        public GetPerson()
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
