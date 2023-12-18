using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    public class EchartsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "统计报表";
            return View();
        }
    }
}
