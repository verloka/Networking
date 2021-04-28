using System;
using System.Windows.Forms;
using WCFClient.HelloService;

namespace WCFClient
{
    public partial class Form1 : Form, IHelloServiceCallback
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Processing(int percentage)
        {
            pg.Value = percentage;
            if(percentage == 100)
                btnStartProcessing.Enabled = true;
        }

        public void Time(string Date)
        {
            lblDt.Text = $"Date: {Date}";
        }

        private async void btnInit_Click(object sender, EventArgs e)
        {
            HelloServiceClient Client = new HelloServiceClient(new System.ServiceModel.InstanceContext(this));
            await Client.StartTimeProcessingAsync();
            btnInit.Enabled = false;
            btnGetResponse.Enabled = true;
            btnStartProcessing.Enabled = true;
        }

        private async void btnGetResponse_Click(object sender, EventArgs e)
        {
            HelloServiceClient Client = new HelloServiceClient(new System.ServiceModel.InstanceContext(this));
            btnGetResponse.Enabled = false;
            lblResponse.Text = await Client.GetResponseAsync();
            btnGetResponse.Enabled = true;
        }

        private async void btnStartProcessing_Click(object sender, EventArgs e)
        {
            HelloServiceClient Client = new HelloServiceClient(new System.ServiceModel.InstanceContext(this));
            btnStartProcessing.Enabled = false;
            await Client.RunProcessingAsync();
        }
    }
}
