using System.Runtime.Serialization;

namespace WCFFaults.Exceptions
{
    [DataContract]
    public class DivideByZeroFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
