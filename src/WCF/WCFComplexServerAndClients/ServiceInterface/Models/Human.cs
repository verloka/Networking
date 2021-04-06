using System.Runtime.Serialization;

namespace ServiceInterface.Models
{
    [KnownType(typeof(Male))]
    [KnownType(typeof(Shemale))]
    [DataContract(Namespace = "https://verloka.com/human")]
    public class Human
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public int Age { get; set; }
    }
}
