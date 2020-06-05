using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using JWTAuthentication.Models;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] Credentials creds)
        {
            if (UserVaults.ContainsCredentials(creds.UserName, creds.Password))
            {
                var key = SecurityService.GetSecurityKey();
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var identity = new ClaimsIdentity(new GenericIdentity(creds.UserName, "username"));
                identity.AddClaims(SecurityService.GetClaims());

                var handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(new SecurityTokenDescriptor()
                {
                    Issuer = SecurityService.GetIssuer(),
                    Audience = SecurityService.GetAudience(),
                    SigningCredentials = signingCredentials,
                    Subject = identity,
                    Expires = DateTime.Now.AddMinutes(10),
                    NotBefore = DateTime.Now
                });
                return handler.WriteToken(token);
            }
            else
            {
                return StatusCode(401);
            }
        }
    }
}
