using Angel.DataAccess;
using Angel.Model;
using Angel.Service;
using Angel.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Angel.Web.ControllersApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InPutApiController : BaseApiController
    {
        private ICacheService _cache;
        public InPutApiController(IDataService _queryService, IHttpContextAccessor accessor, ICacheService cache) : base(_queryService, accessor)
        {
            this._cache = cache;
        }


        string _directory = System.AppDomain.CurrentDomain.BaseDirectory;


        /// <summary>删除数据模板</summary>
        /// <param name="param"></param>
        /// <returns></returns>
        // REMOVE api/inputapi/remove
        [HttpPost]
        public async Task<MessageModel<int>> remove([FromBody] List<FileInfo> list)
        {
            List<int> ids = new List<int>();
            // 删除文件
            foreach (FileInfo obj in list)
            {
                string filepath = _directory + obj.downLoadLink + "/" + obj.fileName;
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                ids.Add(obj.id);
            }
            // 删除数据库记录
            BaseService bs = new BaseService(_cache);
            List<Object> parames = new List<Object>();
            Dictionary<string, List<Object>> map = new Dictionary<string, List<Object>>();
            parames.Add(string.Join(",", ids));
            map.Add("deleteFileInfo", parames);
            int count = bs.InsUpdDelDataTableToParam(map);
            return Success<int>(count);
        }


        /// <summary>获取当前用户存档管理列表</summary>
        /// <param name="param"></param>
        /// <returns></returns>
        // GET api/inputapi/postarchive
        [HttpPost]
        public async Task<Dictionary<string, object>> PostArchive([FromBody] FileInfo param)
        {
            BaseService bs = new BaseService(_cache);
            UtilFunction uf = new UtilFunction();
            string userid = GetCookie("uid");
            // 获取检索输入框的值(文件名)
            string filename = "";
            if (param.fileName != null && param.fileName.Trim().Length > 0)
            {
                filename = param.fileName.Trim();
            }
            // 总记录数
            string sql = "{\"getFileBakTotal\":[{\"[@c0]\":" + userid + ",\"[@c1]\":\"" + filename + "\",\"[@c2]\":\"" + param.type + "\"}]}";
            var list = Newtonsoft.Json.Linq.JObject.Parse(sql);
            Dictionary<string, JArray> jarrays = new Dictionary<string, JArray>();
            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }
            DataTable tabletotal = bs.GetDataTableToParamID(jarrays);

            // 当前页显示的信息集合
            sql = "{\"selectFileBak\":[{\"[@c0]\":" + userid + ",\"[@c1]\":\"" + filename + "\",\"[@c2]\":" + param.offset + ",\"[@c3]\":" + param.rows + ",\"[@c4]\":\"" + param.type + "\"}]}";
            list = Newtonsoft.Json.Linq.JObject.Parse(sql);
            jarrays.Clear();
            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }
            DataTable tablelist = bs.GetDataTableToParamID(jarrays);

            // 返回查询结果
            Dictionary<string, object> map = new Dictionary<string, object>();
            if (tabletotal == null)
            {
                map.Add("total", 0);
            }
            else
            {
                map.Add("total", tabletotal.Rows[0]["total"]);
            }
            map.Add("rows", tablelist);
            return map;
        }

        /// <summary>上传文件存档管理列表</summary>
        /// <param name="param"></param>
        /// <returns></returns>
        // GET api/inputapi/postfilebak
        [HttpPost]
        public async Task<Dictionary<string, object>> postFileBak([FromForm] FileInfo filebak)
        {
            BaseService bs = new BaseService(_cache);
            UtilFunction uf = new UtilFunction();
            string where = "";
            if (!string.IsNullOrEmpty(filebak.fileName))
            {
                where += " AND FileName LIKE '%" + filebak.fileName.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(filebak.createUserName))
            {
                where += " AND CreateUserName='" + filebak.createUserName.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(filebak.type))
            {
                where += " AND Type=" + filebak.type;
            }
            // 总记录数
            string sql = "{\"getAllFileBakTotal\":[{\"[@c0]\":\"" + where + "\"}]}";
            var list = Newtonsoft.Json.Linq.JObject.Parse(sql);
            Dictionary<string, JArray> jarrays = new Dictionary<string, JArray>();
            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }
            DataTable tabletotal = bs.GetDataTableToParamID(jarrays);
            // 结果集
            sql = "{\"selectAllFileBak\":[{\"[@c0]\":\"" + where + "\",\"[@c1]\":" + filebak.offset + ",\"[@c2]\":" + filebak.rows + "}]}";
            list = Newtonsoft.Json.Linq.JObject.Parse(sql);
            jarrays.Clear();
            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }
            DataTable tablelist = bs.GetDataTableToParamID(jarrays);

            // 返回查询结果
            Dictionary<string, object> map = new Dictionary<string, object>();
            if (tabletotal == null)
            {
                map.Add("total", 0);
            }
            else
            {
                map.Add("total", tabletotal.Rows[0]["total"]);
            }
            map.Add("rows", tablelist);
            return map;
        }


        [HttpPost]
        public async Task<MessageModel<int>> removeFileBak([FromBody] List<FileInfo> data)
        {
            BaseService bs = new BaseService(_cache);
            List<int> ids = new List<int>();
            string path = Directory.GetCurrentDirectory() + "//wwwroot//UploadFiles//";
            foreach (FileInfo fileinfo in data)
            {
                ids.Add(fileinfo.id);
                // 删除原始文件
                string filepath = path + fileinfo.sysName;
                DirectoryInfo info = new DirectoryInfo(path);
                // 去除文件夹的只读属性
                info.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                // 去除文件的只读属性
                System.IO.File.SetAttributes(path, FileAttributes.Normal);
                // 判断文件是否存在
                if (System.IO.File.Exists(filepath))
                {

                    System.IO.File.Delete(filepath);
                }
            }
            List<Object> parames = new List<Object>();
            Dictionary<string, List<Object>> map = new Dictionary<string, List<Object>>();
            parames.Add(string.Join(",", ids));
            map.Add("deleteFileBak", parames);
            int count = bs.InsUpdDelDataTableToParam(map);
            return Success<int>(count);
        }


        /// <summary>获取季报管理列表</summary>
        /// <param name="param"></param>
        /// <returns></returns>
        // GET api/inputapi/Quarter
        [HttpPost]
        public async Task<Dictionary<string, object>> postQuarter([FromBody] FileInfo param)
        {
            BaseService bs = new BaseService(_cache);
            UtilFunction uf = new UtilFunction();
            // 获取检索输入框的值(文件名)
            string filename = "";
            if (param.fileName != null && param.fileName.Trim().Length > 0)
            {
                filename = param.fileName.Trim();
            }
            // 组合分页SQL
            string limit = " LIMIT " + param.offset + "," + param.rows;
            // 总记录数
            string c0 = "{\"getQuarterFilesTotal\":[{\"[@c0]\":\"" + filename + "\"}]}";
            var list = Newtonsoft.Json.Linq.JObject.Parse(c0);
            Dictionary<string, JArray> jarrays = new Dictionary<string, JArray>();
            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }
            DataTable tabletotal = bs.GetDataTableToParamID(jarrays);

            // 当前页显示的信息集合
            c0 = "{\"getQuarterFiles\":[{\"[@c0]\":\"" + (limit) + "\",\"[@c1]\":\"" + filename + "\"}]}";
            list = Newtonsoft.Json.Linq.JObject.Parse(c0);
            jarrays.Clear();
            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }
            DataTable tablelist = bs.GetDataTableToParamID(jarrays);

            // 返回查询结果
            Dictionary<string, object> map = new Dictionary<string, object>();
            if (tabletotal == null)
            {
                map.Add("total", 0);
            }
            else
            {
                map.Add("total", tabletotal.Rows[0]["total"]);
            }
            map.Add("rows", tablelist);
            return map;
        }
        public void FolderCreate(string Path)
        {
            // 判断目标目录是否存在如果不存在则新建 
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }
    }


    public class FileInfo
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string sysName { get; set; }
        public string downLoadLink { get; set; }
        public string createUserName { get; set; }
        public string modifyUserName { get; set; }
        public string type { get; set; }
        // 每页多少条数据
        public int rows { get; set; }
        // 请求第几页
        public int offset { get; set; }
    }



}
