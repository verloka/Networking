using System.ServiceModel;

namespace ServiceInterface
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        string GetMessage(string Name);
    }
}
