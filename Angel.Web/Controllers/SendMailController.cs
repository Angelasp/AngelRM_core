using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    public class SendMailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
