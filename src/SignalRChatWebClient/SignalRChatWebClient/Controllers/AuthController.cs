using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalRChatShare;
using SignalRChatShare.Models;

namespace SignalRChatWebClient.Controllers
{
    public class AuthController : Controller
    {
        readonly ILogger<HomeController> _logger;
        readonly AuthService authService;

        public AuthController(ILogger<HomeController> logger, AuthService authService)
        {
            _logger = logger;
            this.authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Auth")]
        [HttpPost]
        public IActionResult Index(User model)
        {
            var response = authService.Authenticate(model);

            if (response == null)
            {
                ViewBag.Error = "Invalid login attempt.";
                return View(model);
            }

            Response.Cookies.Append("token", response.Token);
            return RedirectToAction("Index", "Home");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return RedirectToAction("Index", "Auth");
        }
    }
}
