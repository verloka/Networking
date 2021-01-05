using Grpc.Core;
using GrpcServer;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GrpcClient.Windows
{
    public partial class Chat : Window
    {
        public StreamData Value { get; set; } = new StreamData();

        readonly Greeter.GreeterClient Client;
        readonly Action<string> WriteLine;
        readonly int ID;

        CancellationTokenSource CancellationToken;
        AsyncDuplexStreamingCall<HelloReply, HelloReply> StreamCall;

        public Chat(Greeter.GreeterClient Client, Action<string> WriteLine, int ID)
        {
            InitializeComponent();
            DataContext = Value;
            this.Client = Client;
            this.WriteLine = WriteLine;
            this.ID = ID;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            btnOk.IsEnabled = false;
            btnSend.IsEnabled = true;

            CancellationToken = new CancellationTokenSource();

            WriteLine($"    Chat started (id: {ID})");


            try
            {
                StreamCall = Client.Chat(cancellationToken: CancellationToken.Token);
            }
            catch (Exception ex)
            {
                btnOk.IsEnabled = true;
                btnSend.IsEnabled = false;
                WriteLine(ex.Message);
            }
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await StreamCall.RequestStream.WriteAsync(new HelloReply { Message = tbMessage.Text });

                var responseReaderTask = Task.Run(async () =>
                {
                    while (await StreamCall.ResponseStream.MoveNext() && !CancellationToken.IsCancellationRequested)
                        Value.Value = StreamCall.ResponseStream.Current.Message;
                });
            }
            catch (Exception ex)
            {
                btnOk.IsEnabled = true;
                btnSend.IsEnabled = false;
                WriteLine(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void windowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CancellationToken != null)
            {
                if(StreamCall!= null)
                {
                    await StreamCall.RequestStream.CompleteAsync();
                    StreamCall.Dispose();
                }

                CancellationToken.Cancel();
                WriteLine($"    Chat cancelled (id: {ID})");
            }
        }
    }
}
