using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    public class PrintController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
