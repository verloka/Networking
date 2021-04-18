using System;
using System.ServiceModel;
using System.Windows.Forms;
using WCFFaultsClient.ComplexCalculatorService;
using WCFFaultsClient.SimpleCalculatorService;

namespace WCFFaultsClient
{
    public partial class Form1 : Form
    {
        SimpleCalculatorClient wsClient;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnDivide_Click(object sender, EventArgs e)
        {
            btnDivide.Enabled = false;

            switch (cbType.SelectedIndex)
            {
                case 0:
                default:
                    {
                        try
                        {
                            SimpleCalculatorClient client = new SimpleCalculatorClient("BasicHttpBinding_ISimpleCalculator");
                            lblResult.Text = $"Result: {await client.Divide_1Async(Convert.ToInt32(tbLeft.Text), Convert.ToInt32(tbRight.Text))}";
                        }
                        catch (FaultException fe)
                        {
                            lblResult.Text = $"Result: error, code - {fe.Code.Name}, message - {fe.Message}";
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = $"Result: {ex.Message}";
                        }
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            lblResult.Text = $"Result: {await wsClient.Divide_1Async(Convert.ToInt32(tbLeft.Text), Convert.ToInt32(tbRight.Text))}";
                        }
                        catch (FaultException fe)
                        {
                            lblResult.Text = $"Result: error, code - {fe.Code.Name}, message - {fe.Message}";
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = $"Result: {ex.Message}";
                        }
                        break;
                    }
                case 2:
                    {
                        try
                        {
                            SimpleCalculatorClient client = new SimpleCalculatorClient("BasicHttpBinding_ISimpleCalculator");
                            lblResult.Text = $"Result: {await client.Divide_2Async(Convert.ToInt32(tbLeft.Text), Convert.ToInt32(tbRight.Text))}";
                        }
                        catch (FaultException fe)
                        {
                            lblResult.Text = $"Result: error, code - {fe.Code.Name}, message - {fe.Message}";
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = $"Result: {ex.Message}";
                        }
                        break;
                    }
                case 3:
                    {
                        try
                        {
                            SimpleCalculatorClient client = new SimpleCalculatorClient("BasicHttpBinding_ISimpleCalculator");
                            lblResult.Text = $"Result: {await client.Divide_3Async(Convert.ToInt32(tbLeft.Text), Convert.ToInt32(tbRight.Text))}";
                        }
                        catch (FaultException fe)
                        {
                            lblResult.Text = $"Result: error, code - {fe.Code.Name}, message - {fe.Message}";
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = $"Result: {ex.Message}";
                        }
                        break;
                    }
                case 4:
                    {
                        try
                        {
                            SimpleCalculatorClient client = new SimpleCalculatorClient("BasicHttpBinding_ISimpleCalculator");
                            lblResult.Text = $"Result: {await client.Divide_4Async(Convert.ToInt32(tbLeft.Text), Convert.ToInt32(tbRight.Text))}";
                        }
                        catch (FaultException<DivideByZeroFault> fet)
                        {
                            lblResult.Text = $"Result: error, code - {fet.Code.Name}, message - {fet.Message}, details - {fet.Detail.Message}";
                        }
                        catch (FaultException fe)
                        {
                            lblResult.Text = $"Result: error, code - {fe.Code.Name}, message - {fe.Message}";
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = $"Result: {ex.Message}";
                        }
                        break;
                    }
                case 5:
                    {
                        try
                        {
                            ComplexCalculatorClient client = new ComplexCalculatorClient("BasicHttpBinding_IComplexCalculator");
                            lblResult.Text = $"Result: {await client.Divide_1Async(Convert.ToInt32(tbLeft.Text), Convert.ToInt32(tbRight.Text))}";
                        }
                        catch (FaultException fe)
                        {
                            lblResult.Text = $"Result: error, code - {fe.Code.Name}, message - {fe.Message}";
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = $"Result: {ex.Message}";
                        }
                        break;
                    }
                case 6:
                    {
                        try
                        {
                            ComplexCalculatorClient client = new ComplexCalculatorClient("BasicHttpBinding_IComplexCalculator");
                            lblResult.Text = $"Result: {await client.Divide_2Async(Convert.ToInt32(tbLeft.Text), Convert.ToInt32(tbRight.Text))}";
                        }
                        catch (FaultException fe)
                        {
                            lblResult.Text = $"Result: error, code - {fe.Code.Name}, message - {fe.Message}";
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = $"Result: {ex.Message}";
                        }
                        break;
                    }
            }

            btnDivide.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wsClient = new SimpleCalculatorClient("WSHttpBinding_ISimpleCalculator");
            cbType.SelectedIndex = 0;
        }

        private void btnResession_Click(object sender, EventArgs e)
        {
            wsClient = new SimpleCalculatorClient("WSHttpBinding_ISimpleCalculator");
        }
    }
}
