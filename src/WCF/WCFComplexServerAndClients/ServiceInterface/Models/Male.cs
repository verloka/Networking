using System.Runtime.Serialization;

namespace ServiceInterface.Models
{
    [DataContract]
    public class Male : Human
    {
        [DataMember]
        public string Job { get; set; }
    }
}
