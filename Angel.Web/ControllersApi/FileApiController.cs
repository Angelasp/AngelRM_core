using Angel.Model;
using Angel.Service;
using Angel.Utils;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Angel.Web.ControllersApi
{

    /*************************************************************************
    * 文件名称 ：FileApiController.cs                          
    * 描述说明 ：文件模板下载API控制器
    * 创建信息 : create by QQ：815657032、709047174  E-mail:Angel_asp@126.com on 2018-02-10
    * 修订信息 : modify by (person) on (date) for (reason)
    * 
    * 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
    **************************************************************************/
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileApiController : BaseApiController
    {


        private readonly IHostingEnvironment _hostingEnvironment;

        public FileApiController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor) : base(null, accessor)
        {
            _hostingEnvironment = hostingEnvironment;
        }



        [HttpGet]
        public async Task<MessageModel<List<FileList>>>  Get()
        {
            string username1 = GetCookie("cityid");
            //string username = "admin";
            string username = GetCookie("uname");
            string webRootPath = _hostingEnvironment.ContentRootPath;
            string FilePath = Path.Combine(webRootPath, "wwwroot", "DownFile", "import");
            UtilFunction uf = new UtilFunction();
            List<FileList> tablelist = uf.DataFileName(username, FilePath);
           // string myjson = uf.ToJson(tablelist);
            return Success<FileList>(tablelist,"查询成功");
        }


    }
}
