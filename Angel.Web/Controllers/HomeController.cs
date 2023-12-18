using Angel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Angel.DBHelper;
using System.Data;

namespace Angel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            DataTable dt = MySqlHelpers.GetDataTable("select *  from city");
            //ViewData["Mydata"] = dt;
            return View(dt);
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