using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    public class ProvinceReportController : Controller
    {
        // 报表采集
        // GET: /ProvinceReport/reportCollect
        public IActionResult reportCollect()
        {
            ViewBag.Title = "报表采集";
            return View();
        }
        // 编辑器测试
        // GET: /ProvinceReport/jdCollect
        public IActionResult jdCollect()
        {
            ViewBag.Title = "编辑器测试";
            return View();
        }

        // 组织结构
        // GET: /ProvinceReport/organization
        public IActionResult organization()
        {
            ViewBag.Title = "组织结构";
            return View();
        }



    }
}
