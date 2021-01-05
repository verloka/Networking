using GrpcServer;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GrpcClient.Windows
{
    /// <summary>
    /// Interaction logic for ClientTimeStream.xaml
    /// </summary>
    public partial class ClientTimeStream : Window
    {
        public StreamData Value { get; set; } = new StreamData();

        readonly Greeter.GreeterClient Client;
        readonly Action<string> WriteLine;
        readonly int ID;

        CancellationTokenSource CancellationToken;

        public ClientTimeStream(Greeter.GreeterClient Client, Action<string> WriteLine, int ID)
        {
            InitializeComponent();
            DataContext = Value;
            this.Client = Client;
            this.WriteLine = WriteLine;
            this.ID = ID;
        }

        private async void btnOk_Click(object sender, RoutedEventArgs e)
        {
            btnOk.IsEnabled = false;

            CancellationToken = new CancellationTokenSource();

            WriteLine($"    Stream started (id: {ID})");

            using var streamCall = Client.ClientTimeStream(cancellationToken: CancellationToken.Token);

            try
            {
                Value.Value = "evaluating...";

                for (int i = 0; i < 10; i++)
                {
                    await streamCall.RequestStream.WriteAsync(new GrpcServer.StreamData { Tikcs = DateTime.Now.Ticks });
                    await Task.Delay(1000);
                }

                await streamCall.RequestStream.CompleteAsync();

                var result = await streamCall.ResponseAsync;

                Value.Value = result.Message;

                WriteLine($"    Stream completed (id: {ID})");
                btnOk.IsEnabled = true;
            }
            catch (Exception ex)
            {
                btnOk.IsEnabled = true;
                WriteLine(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CancellationToken != null)
            {
                CancellationToken.Cancel();
                WriteLine($"    Stream cancelled (id: {ID})");
            }
        }
    }
}
