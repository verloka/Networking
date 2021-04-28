using System.ServiceModel;

namespace WCFServiceDuplex
{
    public interface IDuplexContract
    {
        [OperationContract(IsOneWay = true)]
        void Processing(int percentage);

        [OperationContract(IsOneWay = true)]
        void Time(string Date);
    }
}
