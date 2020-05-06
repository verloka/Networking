using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Text;

namespace WebRequestTest
{
    class Program
    {
        private static readonly string FINANCE_CONN_GROUP = "financial_connection";
        private static readonly string REAL_ESTATE_CONN_GROUP = "real_estate_connection";

        static void Main(string[] args)
        {
            SubmitRealEstateRequest();
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        public static void SubmitRealEstateRequest()
        {
            WebRequest req = WebRequest.Create("https://real-estate-detail.com/market/api");
            req.ConnectionGroupName = FINANCE_CONN_GROUP;
            var noCachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            req.CachePolicy = noCachePolicy;
            req.AuthenticationLevel = AuthenticationLevel.MutualAuthRequired;
            req.Credentials = new NetworkCredential("test_user", "secure_and_safe_password");
            Stream reqStream = req.GetRequestStream();
            var messageString = "test";
            var messageBytes = Encoding.UTF8.GetBytes(messageString);
            reqStream.Write(messageBytes, 0, messageBytes.Length);
        }
    }
}
