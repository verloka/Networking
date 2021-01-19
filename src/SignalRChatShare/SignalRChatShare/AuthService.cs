using Microsoft.IdentityModel.Tokens;
using SignalRChatShare.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SignalRChatShare
{
    public class AuthService
    {
        const string TOKEN_KEY = "<mega secret shit>";

        public AuthenticateResponse Authenticate(User User)
        {
            bool b1 = string.IsNullOrWhiteSpace(User.Username);
            bool b2 = User.Username.Length < 5;
            bool b3 = !Enumerable.SequenceEqual(User.Username.AsEnumerable(), User.Password.Reverse());

            if (b1 || b2 || b3) return null;
             
            var token = GenerateJwtToken(User);
            return new AuthenticateResponse(User, token);
        }

        public User ValidateToken(string Token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(Token, GetTokenValidationParameters(), out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return new User { Username = jwtToken.Claims.First(x => x.Type == "Id").Value, Token = Token };
            }
            catch
            {
                return null;
            }
        }

        public static TokenValidationParameters GetTokenValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes(TOKEN_KEY);
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        }

        string GenerateJwtToken(User User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TOKEN_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, User.Username),
                    new Claim("Id", User.Username),
                    new Claim(ClaimTypes.Name, User.Username),
                    new Claim(ClaimTypes.Role, User.Username == "Verloka" ? "admin" : "user")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
