using System.ServiceModel;
using WCFServiceDuplex;

namespace WCFService
{
    [ServiceContract(CallbackContract = typeof(IDuplexContract))]
    public interface IHelloService
    {
        [OperationContract]
        string GetResponse();

        [OperationContract(IsOneWay = true)]
        void StartTimeProcessing();

        [OperationContract(IsOneWay = true)]
        void RunProcessing();
    }
}
