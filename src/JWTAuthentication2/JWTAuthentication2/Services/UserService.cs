using JWTAuthentication2.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication2.Services
{
    public class UserService
    {
        readonly Dictionary<int, User> MockUsers;

        public UserService()
        {
            MockUsers = new Dictionary<int, User>
            {
                { 12, new User { Id = 12, Username = "verloka", Password = "123" } },
                { 53, new User { Id = 53, Username = "ogy", Password = "321" } },
                { 66, new User { Id = 66, Username = "qwerty", Password = "qwerty" } },
                { 62, new User { Id = 62, Username = "test", Password = "!123" } }
            };
        }

        public User GetById(int Id)
        {
            return MockUsers.ContainsKey(Id) ? MockUsers[Id] : null;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = MockUsers.SingleOrDefault(x => x.Value.Username == model.Username && x.Value.Password == model.Password);

            if (user.Value == null) return null;
            var token = GenerateJwtToken(user.Value);
            return new AuthenticateResponse(user.Value, token);
        }

        string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
