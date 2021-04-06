using System.Runtime.Serialization;

namespace ServiceInterface.Models
{
    [DataContract]
    public class Shemale : Human
    {
        [DataMember]
        public int ChildrensCount { get; set; }
    }
}
