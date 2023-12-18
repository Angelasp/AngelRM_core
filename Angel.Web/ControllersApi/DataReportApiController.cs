using Angel.DataAccess;
using Angel.Service;
using Angel.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;

namespace Angel.Web.ControllersApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataReportApiController : BaseApiController
    {

        IHttpContextAccessor _accessor;
        private ICacheService _cache;

        public DataReportApiController(IDataService _queryService, IHttpContextAccessor accessor, ICacheService cache) : base(_queryService, accessor)
        {
            this._accessor = accessor;
            this._cache = cache;
        }

        string _directory = Directory.GetCurrentDirectory();
        string error_msg = "";
        public class Parameter
        {
            public string tablename { get; set; }
            public int id { get; set; }
            public string ids { get; set; }
            public string uFileName { get; set; }
            public string batch { get; set; } // 批次号
            public string indicatorid { get; set; }
            public string noP_NUMBER { get; set; }
            public string last_NOP_NUMBER { get; set; }
            public string citY_NO { get; set; }
            public string sourcename { get; set; }
            public string newValue { get; set; }
            public string iData { get; set; }
            public string source { get; set; }
            public string red_start { get; set; }
            public string red_end { get; set; }
            public int createUserId { get; set; }
            public string createUserName { get; set; }
            public int status { get; set; } // -1=未提交、0=提交审核、1=审核通过、2=审核不通过
            public int offset { get; set; }
            public int rows { get; set; }
            public int role { get; set; } // 0-审核人员、1=普通用户
            public string field { get; set; }  // 检索字段名
            public string search { get; set; } // 检索值
            public string isMust { get; set; }
            public string type { get; set; }
        }
        /// <summary>
        /// 文件上传  api/datareportapi/upload
        /// </summary>
        public Object Upload()
        {
            BaseService bs = new BaseService(_cache);
   
            IFormFileCollection hfc = HttpContext.Request.Form.Files;
            //HttpFileCollection hfc = _accessor1.HttpContext.Request.Files;
            string batch = "angelasp";                            // 用户标识
            DateTime date = DateTime.Now;                        // 当前时间毫秒数Millisecond
            string userid = GetCookie("uid");       // 当前登录用户ID
            string username = GetCookie("uname");   // 当前登录用户Name
            string city = HttpUtility.UrlDecode(GetCookie("cityid"));
            string filename = null;                              // 文件名
            string sysFileName = userid + "_" + batch + "_" + date.Millisecond + "_";  // 备份文件名
            string path = _directory + "//wwwroot//UploadFiles//";           // 文件备份路径

            DirectoryInfo info = new DirectoryInfo(path);
            // 去除文件夹的只读属性
            info.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            // 去除文件的只读属性
            System.IO.File.SetAttributes(path, FileAttributes.Normal);


            int count = 0;
            if (hfc.Count > 0)
            {
                IFormFile item = hfc[0];
                filename = item.FileName;
                sysFileName += filename;
                path += sysFileName;
                // 将文件备份到指定目录
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    item.CopyTo(fs);
                    fs.Flush();
                }
                count = 1;
            }
            //注意要写好后面的第二第三个参数
            string strmsg = null, strerr = null;
            if (count == -1)
            {
                // 如果文件中的数据已存在，则删除备份文件
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                strmsg = "";
                strerr = "读取《" + filename + "》文件异常！"; // 指标编号重复
            }
            else if (count == 0)
            {

                strmsg = "";
                strerr = "《" + filename + "》文件中的数据已存在，请勿重复上传文件！";
            }
            else if (count > 0)
            {
                // 如果文件中的数据有入库操作，则将该文件的备份信息入库
                List<Object> param = new List<Object>();
                Dictionary<string, List<Object>> map = new Dictionary<string, List<Object>>();
                param.Add(filename);
                param.Add(sysFileName);
                param.Add(date);
                param.Add(userid);
                param.Add(username);
                param.Add("1"); // 1-数据
                map.Add("insertFileBak", param);

                count += bs.InsUpdDelDataTableToParam(map);

                strmsg = "共导入" + count + "条记录！";
                strerr = "";
            }
            return new { msg = strmsg, error = strerr };
        }


    }
}
