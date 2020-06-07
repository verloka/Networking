using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace TestWebSite.Services
{
    public class SecurityService
    {
        public static SymmetricSecurityKey GetSecurityKey()
        {
            string key = "0125eb1b-0251-4a86-8d43-8ebeeeb29d9a";
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        public static string GetIssuer()
        {
            return "https://verloka.com/oauth";
        }

        public static string GetAudience()
        {
            return "we_the_audience";
        }

        public static IEnumerable<Claim> GetClaims()
        {
            return new List<Claim>() {
                new Claim("secret_access", "true"),
                new Claim("excellent_code", "true")
            };
        }
    }
}
