using Angel.DataAccess;
using Angel.Model;
using Angel.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Angel.Web.Controllers
{
        /*************************************************************************
         * 文件名称 ：SysManagerController.cs                          
         * 描述说明 ：用户权限控制器类
         * 
         * 创建信息 : create by QQ：815657032、Angel_asp@126.com on 2018-02-10
         * 修订信息 : modify by (person) on (date) for (reason)
         * 
         * 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
         **************************************************************************/
        public class SysManagerController : BaseController
    {


        private IHostingEnvironment _hostingEnvironment;
        private ICacheService dataCache;
        private List<Menu> menulist;

        public SysManagerController(IHostingEnvironment hostingEnvironment, ICacheService _cacheService, IDataService _queryService, IHttpContextAccessor accessor) : base(_queryService, accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            dataCache = _cacheService;
            menulist = (List<Menu>)dataCache.Get("roledatabtn");
        }

            /// <summary>
            /// 其他数据下载
            /// </summary>
            /// <param name="FileName"></param>
            public async Task<IActionResult> DownFile(string filename)
            {

            string webRootPath = _hostingEnvironment.ContentRootPath;
            string path = Path.Combine(webRootPath, "wwwroot", "OtherData", filename);

            if (System.IO.File.Exists(path))
                {
                    //System.IO.FileStream fs = new System.IO.FileStream(@path, FileMode.Open);
                    //byte[] bytes = new byte[(int)fs.Length];
                    //fs.Read(bytes, 0, bytes.Length);
                    //fs.Close();
                    //Response.ContentType = "application/octet-stream";
                    ////通知浏览器下载文件而不是打开
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
                    //Response.BinaryWrite(bytes);
                    //Response.Flush();
                    //Response.End();
                var fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);

                // var buffer = new byte[1024]; // 每次读取的缓冲区大小
                //int bytesRead;
                //do
                //{
                //    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                //    await Response.Body.WriteAsync(buffer, 0, bytesRead);
                //} while (bytesRead > 0);

                byte[] bytes = new byte[(int)fileStream.Length];
                await Response.Body.WriteAsync(bytes, 0, bytes.Length);
                fileStream.Close();
                Response.Body.Flush();
                Response.Body.Close();
                return View();
                }
                else
                {
                    ContentResult cr = new ContentResult();
                    cr.Content = string.Format("<script type='text/javascript'>alert('找不到文件《" + filename + "》,请联系管理员！');{0}</script>", "history.go(-1);");
                    return cr;
                }
            }
        //数据下载
        //get: /sysmanager/downloaddatamanager
        public ActionResult downloaddatamanager()
            {

                return View();
            }

        //
        // GET: /SysManager/MenuManager
        public ActionResult MenuManager()
            {
                ViewBag.Title = "菜单管理";
                var query = from a in menulist
                            where (a.menuo.StartsWith("sys:menu"))
                            select a;
                List<Menu> menulists = (List<Menu>)query.ToList();
                ViewBag.Menulist = menulists;
                return View();
            }
        // 用户管理
        // GET: /SysManager/UserManager
        public ActionResult UserManager()
            {
                ViewBag.Title = "用户管理";
                var query = from a in menulist
                            where (a.menuo.StartsWith("sys:user"))
                            select a;
                List<Menu> menulists = (List<Menu>)query.ToList();
                ViewBag.Menulist = menulists;
                return View();
            }
            // 角色管理
            // GET: /SysManager/RoleManager
            public ActionResult RoleManager()
            {
                ViewBag.Title = "角色管理";
                var query = from a in menulist
                            where (a.menuo.StartsWith("sys:role"))
                            select a;
                List<Menu> menulists = (List<Menu>)query.ToList();
                ViewBag.Menulist = menulists;
                return View();
            }
            // 部门管理
            // GET: /SysManager/DepartmentManager
            public ActionResult DepartmentManager()
            {
                ViewBag.Title = "部门管理";
                var query = from a in menulist
                            where (a.menuo.StartsWith("sys:department"))
                            select a;
                List<Menu> menulists = (List<Menu>)query.ToList();
                ViewBag.Menulist = menulists;
                return View();
            }


            //下载列表管理
            //GET:/SysManager/DownloadListManager
            public ActionResult DownloadListManager()
            {
                ViewBag.Title = "下载列表管理";
                ViewBag.userid = GetCookie("uid");
                return View();
            }

            // 登录日志管理
            // GET: /SysManager/LoginLogManager
            public ActionResult LoginLogManager()
            {
                ViewBag.Title = "登录日志管理";
                return View();
            }
            // 操作日志管理
            // GET: /SysManager/OperLogManager
            public ActionResult OperLogManager()
            {
                ViewBag.Title = "操作日志管理";
                return View();
            }

            // 字典管理
            // GET: /SysManager/DictionaryManager
            public ActionResult DictionaryManager()
            {
                ViewBag.Title = "字典管理";
                var query = from a in menulist
                            where (a.menuo.StartsWith("sys:dictionary"))
                            select a;
                List<Menu> menulists = (List<Menu>)query.ToList();
                ViewBag.Menulist = menulists;
                return View();
            }
        // 字典数据管理
        // GET: /SysManager/DictionaryDataManager
        public ActionResult DictionaryDataManager(int id)
            {

                ViewBag.Title = "字典数据管理";
                ViewBag.tpyeid = id;
                string where = " where id=" + id + "";
                //按条件查询
               // BLLService QueryService = new BLLService();
                DataTable tablelist = QueryService.GetWhereDataTable("query_dicttypelist", where);
                if (tablelist != null && tablelist.Rows.Count > 0)
                {
                    ViewBag.dicttype = tablelist.Rows[0]["dicttype"].ToString();
                }
                else
                {
                    ViewBag.dicttype = "";
                }

                return View();
            }







        }
    }
