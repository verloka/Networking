using System;
using System.Windows.Forms;

namespace WCFServiceClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HelloService.HelloServiceClient client = new HelloService.HelloServiceClient();
            label1.Text = await client.GetMessageAsync(textBox1.Text);
        }
    }
}
