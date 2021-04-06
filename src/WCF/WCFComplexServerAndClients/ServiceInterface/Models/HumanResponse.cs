using ServiceInterface.Enums;
using System.ServiceModel;

namespace ServiceInterface.Models
{
    [MessageContract(WrapperNamespace = "https://verloka.com/humanresponse")]
    public class HumanResponse
    {
        [MessageBodyMember(Order = 1)]
        public bool IsSuccess { get; set; }

        [MessageBodyMember(Order = 2)]
        public int Id { get; set; }

        [MessageBodyMember(Order = 3)]
        public string Name { get; set; }

        [MessageBodyMember(Order = 4)]
        public int Age { get; set; }

        [MessageBodyMember(Order = 5)]
        public HumanType Type { get; set; }

        [MessageBodyMember(Order = 6)]
        public string Job { get; set; }

        [MessageBodyMember(Order = 7)]
        public int ChildrensCount { get; set; }

        public HumanResponse() { IsSuccess = false; }
        public HumanResponse(HumanType Type, Human Human)
        {
            IsSuccess = true;
            Id = Human.Id;
            Name = Human.Name;
            Age = Human.Age;

            switch (Type)
            {
                case HumanType.Male:
                    Job = ((Male)Human).Job;
                    break;
                case HumanType.Shemale:
                    ChildrensCount = ((Shemale)Human).ChildrensCount;
                    break;
                default:
                    break;
            }
        }
    }
}
