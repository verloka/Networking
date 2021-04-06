using ServiceInterface.Enums;
using ServiceInterface.Models;
using System.ServiceModel;

namespace ServiceInterface
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        string ReverseString(string ToReverse);

        [OperationContract]
        Human GetHuman(HumanType Type);

        [OperationContract]
        HumanResponse RequestHuman(HumanRequest request);
    }
}
