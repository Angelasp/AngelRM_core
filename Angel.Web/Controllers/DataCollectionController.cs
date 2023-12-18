using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    public class DataCollectionController : Controller
    {
        // 文件上传管理
        // GET: /DataCollection/DataImport
        public IActionResult DataImport()
        {
            ViewBag.Title = "文件上传管理";
            return View();
        }


        // 文件列表管理
        // GET: /DataCollection/FileBak
        public IActionResult FileBak()
        {
            ViewBag.Title = "文件列表管理";
            return View();
        }
        // 分析管理
        // GET: /DataCollection/ComplaintAnalysis
        public IActionResult ComplaintAnalysis()
        {
            ViewBag.Title = "分析管理";
            return View();
        }





    }
}
