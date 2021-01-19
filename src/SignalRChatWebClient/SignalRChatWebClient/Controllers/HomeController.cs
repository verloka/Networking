using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalRChatShare.Models;
using SignalRChatWebClient.Filter;
using SignalRChatWebClient.Models;
using System.Diagnostics;

namespace SignalRChatWebClient.Controllers
{
    [UserAuthorize]
    public class HomeController : Controller
    {
        new public User User { get; set; }

        readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;

            User = (User)contextAccessor.HttpContext.Items["User"];
        }

        public IActionResult Index()
        {
            ViewBag.Username = User.Username;
            ViewBag.Token = User.Token;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
