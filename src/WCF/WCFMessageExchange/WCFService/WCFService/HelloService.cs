using System;
using System.ServiceModel;
using System.Threading;
using WCFServiceDuplex;

namespace WCFService
{
    public class HelloService : IHelloService
    {
        public string GetResponse()
        {
            return "Response from server";
        }

        public void RunProcessing()
        {
            for (int i = 0; i < 101; i++)
            {
                Thread.Sleep(50);
                OperationContext.Current.GetCallbackChannel<IDuplexContract>().Processing(i);
            }
        }

        public void StartTimeProcessing()
        {
            while(true)
            {
                Thread.Sleep(500);
                OperationContext.Current.GetCallbackChannel<IDuplexContract>().Time($"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}");
            }
        }
    }
}
