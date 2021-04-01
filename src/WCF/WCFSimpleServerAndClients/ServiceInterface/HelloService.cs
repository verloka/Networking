namespace ServiceInterface
{
    public class HelloService : IHelloService
    {
        public string GetMessage(string Name) => $"Hello, {Name}!";
    }
}
