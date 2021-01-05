using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Windows;
using System.Windows.Input;

namespace GrpcClient
{
    public partial class MainWindow : Window
    {
        readonly ConsoleContent Console = new ConsoleContent();
        readonly Greeter.GreeterClient Client;
        readonly GrpcChannel Channel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = Console;

            Channel = GrpcChannel.ForAddress("http://localhost:5000");
            Client = new Greeter.GreeterClient(Channel);
        }

        void WriteLine(string Text, params string[] args)
        {
            Console.ConsoleInput = string.Format(Text, args);
            Console.RunCommand();
            InputBlock.Focus();
            Scroller.ScrollToBottom();
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            InputBlock.KeyDown += InputBlockKeyDown;
            InputBlock.Focus();
        }

        private void InputBlockKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) WriteLine(InputBlock.Text);
        }

        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Channel.Dispose();
        }

        private async void btnSayHello_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.SayHello { Owner = this };
            if (window.ShowDialog() ?? false)
            {
                try
                {
                    WriteLine("Method: SayHello");
                    WriteLine("Request: {0}", window.Value);
                    var result = await Client.SayHelloAsync(new HelloRequest { Name = window.Value });
                    WriteLine("Response: {0}", result.Message);
                }
                catch (System.Exception ex)
                {
                    WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private async void btnEcho_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.Echo { Owner = this };
            if (window.ShowDialog() ?? false)
            {
                try
                {
                    WriteLine("Method: Echo");
                    WriteLine("Request: {0}", window.Value);
                    var result = await Client.EchoAsync(new EchoData { Message = window.Value });
                    WriteLine("Response: {0}", result.Message);
                }
                catch (System.Exception ex)
                {
                    WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private async void btnTestTypes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteLine("Method: TestTypes");
                WriteLine("Request: {0}", "Empty");
                var result = await Client.TestTypesAsync(new Empty());

                WriteLine("Response:");
                foreach (var prop in typeof(TypesData).GetProperties())
                    if (prop.Name != "Parser" && prop.Name != "Descriptor")
                        WriteLine("    {0}: {1}", prop.Name, prop.GetValue(result, null).ToString());
            }
            catch (System.Exception ex)
            {
                WriteLine("Error: {0}", ex.Message);
            }
        }

        private async void btnTestNullableTypes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteLine("Method: TestNullableTypes");
                WriteLine("Request: {0}", "Empty");
                var result = await Client.TestNullableTypesAsync(new Empty());

                WriteLine("Response:");
                foreach (var prop in typeof(NullableTypesData).GetProperties())
                    if (prop.Name != "Parser" && prop.Name != "Descriptor")
                        WriteLine("    {0}: {1}", prop.Name, prop.GetValue(result, null)?.ToString() ?? "null");
            }
            catch (System.Exception ex)
            {
                WriteLine("Error: {0}", ex.Message);
            }
        }

        private async void btnTestCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteLine("Method: TestCollection");
                WriteLine("Request: {0}", "Empty");
                var result = await Client.TestCollectionAsync(new Empty());

                WriteLine("Response:");
                WriteLine("    Names:");
                foreach (var n in result.Names)
                    WriteLine("        {0}", n);

                WriteLine("    Numbers:");
                foreach (var n in result.Numbers)
                    WriteLine("        {0}", n.ToString());

                WriteLine("    IdName:");
                foreach (var n in result.IdName)
                    WriteLine("        {0} - {1}", n.Key.ToString(), n.Value);

                WriteLine("    GuidValue:");
                foreach (var n in result.GuidValue)
                    WriteLine("        {0} - {1}", n.Key, n.Value.ToString());
            }
            catch (System.Exception ex)
            {
                WriteLine("Error: {0}", ex.Message);
            }
        }

        private async void btnTestAny_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteLine("Method: TestAny");
                WriteLine("Request: {0}", "Empty");
                var result = await Client.TestAnyAsync(new Empty());

                WriteLine("Response: {0}", result.Message);

                if (result.Message == "HelloRequest") WriteLine("    Name - {0}", result.Detail.Unpack<HelloRequest>().Name);
                else WriteLine("    Message - {0}", result.Detail.Unpack<HelloReply>().Message);
            }
            catch (System.Exception ex)
            {
                WriteLine("Error: {0}", ex.Message);
            }
        }

        private async void btnGetPerson_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.GetPerson { Owner = this };
            if (window.ShowDialog() ?? false)
            {
                try
                {
                    WriteLine("Method: GetPerson");
                    WriteLine("Request: {0}", window.Value.ToString());
                    var result = await Client.GetPersonAsync(new PersonRequest { Id = window.Value });

                    switch (result.ResultCase)
                    {
                        default:
                        case PersonResponse.ResultOneofCase.None:
                            WriteLine("Response: none");
                            break;
                        case PersonResponse.ResultOneofCase.Error:
                            WriteLine("Response: Error (code: {0}, message: {1})", result.Error.Code.ToString(), result.Error.Message);
                            break;
                        case PersonResponse.ResultOneofCase.Person:
                            WriteLine("Response: Person (name: {0}, address: {1}, age: {2})", result.Person.Name, result.Person.Address, result.Person.Age.ToString());
                            break;
                    }

                    
                }
                catch (Exception ex)
                {
                    WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void btnServerTimeStream_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int id = rnd.Next(1, 1000);

            WriteLine("Method: ServerTimeStream");
            WriteLine("Request: {0}", "Empty");

            try
            {
                new Windows.ServerTimeStream(Client, x => WriteLine(x), id) { Owner = this }.Show();
            }
            catch (Exception ex)
            {
                WriteLine("Error: {0}", ex.Message);
            }
        }

        private void btnClientTimeStream_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int id = rnd.Next(1, 1000);

            WriteLine("Method: ClientTimeStream");
            WriteLine("Request: {0}", "Empty");

            try
            {
                new Windows.ClientTimeStream(Client, x => WriteLine(x), id) { Owner = this }.Show();
            }
            catch (Exception ex)
            {
                WriteLine("Error: {0}", ex.Message);
            }
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int id = rnd.Next(1, 1000);

            WriteLine("Method: Chat");
            WriteLine("Request: {0}", "Empty");

            try
            {
                new Windows.Chat(Client, x => WriteLine(x), id) { Owner = this }.Show();
            }
            catch (Exception ex)
            {
                WriteLine("Error: {0}", ex.Message);
            }
        }
    }
}
