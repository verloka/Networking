using Microsoft.AspNetCore.Http;
using SignalRChatShare;
using System.Threading.Tasks;

namespace SignalRChatWebClient.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;

        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, AuthService authService)
        {
            string token = context.Request.Cookies["token"];
            if(!string.IsNullOrWhiteSpace(token))
                AttachUserToContext(context, authService, token);

            await next(context);
        }

        private void AttachUserToContext(HttpContext context, AuthService authService, string token)
        {
            try
            {
                var user = authService.ValidateToken(token);
                context.Items["User"] = user;
            }
            catch { }
        }
    }
}
