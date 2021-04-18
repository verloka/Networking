using System.ServiceModel;

namespace WCFFaults
{
    [ServiceContract]
    public interface IComplexCalculator
    {
        [OperationContract]
        int Divide_1(int Left, int Right);

        [OperationContract]
        int Divide_2(int Left, int Right);
    }
}
