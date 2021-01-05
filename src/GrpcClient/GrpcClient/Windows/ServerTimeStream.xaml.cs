using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServer;
using System;
using System.Threading;
using System.Windows;

namespace GrpcClient.Windows
{
    public partial class ServerTimeStream : Window
    {
        public StreamData Value { get; set; } = new StreamData();

        readonly Greeter.GreeterClient Client;
        readonly Action<string> WriteLine;
        readonly int ID;

        CancellationTokenSource CancellationToken;

        public ServerTimeStream(Greeter.GreeterClient Client, Action<string> WriteLine, int ID)
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

            using var streamCall = Client.ServerTimeStream(new Empty(), cancellationToken: CancellationToken.Token);

            try
            {
                await foreach (var result in streamCall.ResponseStream.ReadAllAsync(cancellationToken: CancellationToken.Token))
                    Value.Value = DateTime.FromBinary(result.Tikcs).ToString("o");
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
