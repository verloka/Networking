using ServiceInterface.Enums;
using System.ServiceModel;

namespace ServiceInterface.Models
{
    [MessageContract(WrapperNamespace = "https://verloka.com/humanrequest")]
    public class HumanRequest
    {
        [MessageHeader]
        public string SecretKet { get; set; }

        [MessageBodyMember]
        public HumanType Type { get; set; }

        public HumanRequest() : this("", HumanType.Male) { }
        public HumanRequest(string SecretKet, HumanType Type)
        {
            this.SecretKet = SecretKet;
            this.Type = Type;
        }
    }
}
