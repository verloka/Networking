using System.ServiceModel;
using WCFFaults.Exceptions;

namespace WCFFaults
{
    [ServiceContract]
    public interface ISimpleCalculator
    {
        [OperationContract]
        int Divide_1(int Left, int Right);

        [OperationContract]
        int Divide_2(int Left, int Right);

        [OperationContract]
        int Divide_3(int Left, int Right);

        [FaultContract(typeof(DivideByZeroFault))]
        [OperationContract]
        int Divide_4(int Left, int Right);
    }
}
