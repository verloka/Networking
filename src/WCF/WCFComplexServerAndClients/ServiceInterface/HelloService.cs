using ServiceInterface.Enums;
using ServiceInterface.Models;
using System.Linq;

namespace ServiceInterface
{
    public class HelloService : IHelloService
    {
        const string SECRET_KEY = "super_secreet_key";

        public Human GetHuman(HumanType Type)
        {
            switch (Type)
            {
                default:
                case HumanType.Male:
                    return new Male
                    {
                        Id = 777,
                        Age = 25,
                        Name = "Vadym",
                        Job = ".Net Developer"
                    };
                case HumanType.Shemale:
                    return new Shemale
                    {
                        Id = 666,
                        Name = "B2",
                        Age = 27,
                        ChildrensCount = -1
                    };
            }
        }

        public HumanResponse RequestHuman(HumanRequest request)
        {
            return request.SecretKet == SECRET_KEY ? new HumanResponse(request.Type, GetHuman(request.Type)) : new HumanResponse();
        }

        public string ReverseString(string ToReverse) => new string(ToReverse.Reverse().ToArray());
    }
}
