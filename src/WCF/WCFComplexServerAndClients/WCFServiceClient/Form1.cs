using Newtonsoft.Json;
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

        private async void btnReverse_Click(object sender, EventArgs e)
        {
            btnReverse.Enabled = false;
            HelloService.HelloServiceClient client = new HelloService.HelloServiceClient();
            lblResult.Text = await client.ReverseStringAsync(tbReverse.Text);
            btnReverse.Enabled = true;
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            if (cbGet.SelectedIndex < 0)
                return;

            btnGet.Enabled = false;
            HelloService.HelloServiceClient client = new HelloService.HelloServiceClient();
            var result = await client.GetHumanAsync((HelloService.HumanType)cbGet.SelectedIndex);
            rtbGetHumanResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
            btnGet.Enabled = true;
        }

        private async void btnRequest_Click(object sender, EventArgs e)
        {
            if (cbRequest.SelectedIndex < 0)
                return;

            cbRequest.Enabled = false;
            HelloService.HelloServiceClient client = new HelloService.HelloServiceClient();
            var result = await client.RequestHumanAsync(new HelloService.HumanRequest(tbSecret.Text, (HelloService.HumanType)cbRequest.SelectedIndex));
            rtbRequestResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
            cbRequest.Enabled = true;
        }
    }
}
