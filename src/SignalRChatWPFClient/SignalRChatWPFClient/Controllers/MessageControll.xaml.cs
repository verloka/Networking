using SignalRChatWPFClient.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SignalRChatWPFClient.Controllers
{
    public partial class MessageControll : UserControl
    {
        public event PMMessageEventHandler OnPM;

        public MessageControll(string Username, DateTime Date, string Text, bool IsMy, bool IsPM)
        {
            InitializeComponent();

            tbUsername.Text = Username;
            tbDate.Text = $"{Date.ToShortDateString()} {Date.ToShortTimeString()}";
            tbMessage.Text = Text;

            if (IsMy)
                gridHead.Background = new SolidColorBrush(Color.FromRgb(3, 80, 255));

            if (IsPM)
                gridHead.Background = new SolidColorBrush(Color.FromRgb(120, 160, 255));
        }

        private void btnPMClick(object sender, RoutedEventArgs e) => OnPM?.Invoke(this, new PMMessageArgs(tbUsername.Text));
    }
}
