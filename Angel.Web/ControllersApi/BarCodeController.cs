using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.ControllersApi
{
    public class BarCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
