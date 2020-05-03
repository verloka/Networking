using System;

namespace URITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetVerlokaURI());
            Console.WriteLine(GetVerlokaURICtor());
            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }

        static Uri GetVerlokaURI()
        {
            var builder = new UriBuilder
            {
                Scheme = "https",
                Host = "verloka.com"
            };

            return builder.Uri;
        }

        static Uri GetVerlokaURICtor() => new UriBuilder("https", "verloka.com").Uri;
    }
}
