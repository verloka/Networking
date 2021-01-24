using ADAuthentication.Enums;
using ADAuthentication.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ADAuthentication.Services
{
    public class UserService
    {
        readonly ActiveDirectoryService activeDirectoryService;
        readonly Dictionary<int, User> MockUsers;

        public UserService(ActiveDirectoryService activeDirectoryService)
        {
            MockUsers = new Dictionary<int, User>
            {
                { 12, new User { Id = 12, Username = "vadym@verloka.com", Password = "123" } },
                { 53, new User { Id = 53, Username = "ogy@verloka.com", Password = "321" } },
                { 66, new User { Id = 66, Username = "qwerty@verloka.com", Password = "qwerty" } },
                { 62, new User { Id = 62, Username = "test@verloka.com", Password = "!123" } }
            };

            this.activeDirectoryService = activeDirectoryService;
        }

        public User GetById(int Id)
        {
            return MockUsers.ContainsKey(Id) ? MockUsers[Id] : null;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var result = activeDirectoryService.Authenticate("verloka.com/ad", model.Username, model.Password);

            if(result == AccountAuthenticationResponse.Success)
            {
                var user = MockUsers.SingleOrDefault(x => x.Value.Username == model.Username && x.Value.Password == model.Password);

                if (user.Value != null)
                {
                    var token = GenerateJwtToken(user.Value);
                    return new AuthenticateResponse(user.Value, token);
                }
            }

            return null;
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
