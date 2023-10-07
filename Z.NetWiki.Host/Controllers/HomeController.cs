using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Z.Ddd.Common.Attributes;
using Z.NetWiki.Application.UserModule;
using Z.NetWiki.Host.Models;

namespace Z.NetWiki.Host.Controllers
{
    [NoResult]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserAppService userRespo;

        public HomeController(ILogger<HomeController> logger,  IUserAppService userRespo)
        {
            _logger = logger;
            this.userRespo = userRespo;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("正在加载首页......");
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}