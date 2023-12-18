using Angel.Service;
using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    public class MainController : Controller
    {


        private IHttpContextAccessor _accessor;

        public MainController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }


        public IActionResult Index()
        {
            string userName = GetCookie("uname");
            ViewData["userName"] = userName;
            return PartialView();
        }

        //
        // GET: /Main/Details/5

        public IActionResult Details(int id)
        {
            return View();
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public string GetCookie(string strName)
        {
            if (_accessor != null && _accessor.HttpContext.Request.Cookies[strName] != null)
            {
                return _accessor.HttpContext.Request.Cookies[strName];
            }
            else
            {
                return "";
            }

        }


    }
}
