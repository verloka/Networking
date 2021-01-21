using SignalRChatShare;
using SignalRChatShare.Models;
using SignalRChatWPFClient.Controllers;
using SignalRChatWPFClient.Core;
using SignalRChatWPFClient.Events;
using System.Windows;
using System.Windows.Controls;

namespace SignalRChatWPFClient
{
    public partial class MainWindow : Window
    {
        Chat Chat;
        AuthService authService;
        User User;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            authService = new AuthService();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var result = authService.Authenticate(new User { Username = tbUsername.Text, Password = tbPassword.Password });

            if (result == null)
            {
                MessageBox.Show(this, "Invalid login attempt.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Chat = new Chat("http://localhost:3000/chat", result.Token);
                User = result.User;

                tbGreetings.Text = $"Welcome, {User.Username}";

                Chat.Init();

                Chat.OnGroups += ChatOnGroups;
                Chat.OnMessage += ChatOnMessage;
                Chat.OnMessages += ChatOnMessages;

                await Chat.Start();

                gridLogin.Visibility = Visibility.Collapsed;
                gridChat.Visibility = Visibility.Visible;
            }
        }

        private async void btnLogOutClick(object sender, RoutedEventArgs e)
        {
            await Chat.DisposeAsync();
            Chat = null;
            User = null;

            gridChat.Visibility = Visibility.Collapsed;
            gridLogin.Visibility = Visibility.Visible;
        }

        private void ChatOnMessages(object sender, MessagesEventArgs e)
        {
            spMessages.Children.Clear();

            foreach (var msg in e.Messages)
            {
                MessageControll mc = new MessageControll(msg.Username, msg.Date, msg.Text, msg.Username == User.Username, msg.To == User.Username);
                mc.OnPM += McOnPM;

                spMessages.Children.Add(mc);
            }

            svMessages.ScrollToEnd();
        }

        private void ChatOnMessage(object sender, MessageEventArgs e)
        {
            MessageControll mc = new MessageControll(e.Message.Username, e.Message.Date, e.Message.Text, e.Message.Username == User.Username, e.Message.To == User.Username);
            mc.OnPM += McOnPM;
            spMessages.Children.Add(mc);
            svMessages.ScrollToEnd();
        }

        private void ChatOnGroups(object sender, GroupsArgs e)
        {
            cbGroups.Items.Clear();
            e.Groups.ForEach(x => cbGroups.Items.Add(x));
            cbGroups.SelectedIndex = 0;
        }

        private async void btnSendClick(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbMessage.Text))
            {
                await Chat.SendMessage(tbMessage.Text, cbGroups.SelectedItem as string);
                tbMessage.Text = string.Empty;
            }
        }

        private async void cbGroupsSelectionChnaged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGroups.SelectedIndex != -1)
                await Chat.OpenGroup(cbGroups.SelectedItem as string);
        }

        private void McOnPM(object sender, PMMessageArgs e)
        {
            tbPrivateUsername.Text = $"PM:{e.Username}";

            gridMessage.Visibility = Visibility.Collapsed;
            gridPrivateMessage.Visibility = Visibility.Visible;
        }

        private void btnCancelPMClick(object sender, RoutedEventArgs e)
        {
            gridPrivateMessage.Visibility = Visibility.Collapsed;
            gridMessage.Visibility = Visibility.Visible;
        }

        private async void btnSendPrivateClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbPrivateMessage.Text))
            {
                await Chat.SendMessage(tbPrivateMessage.Text, tbPrivateUsername.Text.Split(':')[1], cbGroups.SelectedItem as string);
                tbPrivateMessage.Text = string.Empty;
            }
        }
    }
}
