using Angel.DataAccess;
using Angel.DBHelper;
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
    public class DownloadListManagerApiController : BaseApiController
    {
        private ICacheService dataCache;

        public DownloadListManagerApiController(IDataService _queryService, IHttpContextAccessor accessor, ICacheService _cacheService) : base(_queryService, accessor)
        {
            dataCache = _cacheService;
        }

        [HttpGet]
        // GET api/DownloadListManagerApi/get
        public async Task<MessageModel<DataTable>> Get(string userid)
        {
            try
            {
                string value = "{\"userid\": \"'" + userid + " '\"}";
                var list = JObject.Parse(value);

                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/DownloadListManagerApiController/Get()方法");
                DataTable rtable;
                if (userid == "1")
                    rtable = QueryService.GetDataTable(null, "alluserdownload");
                else
                    rtable = QueryService.GetDataTable(list, "oneuserdownload");

                return Success<DataTable>(rtable);

            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/DownloadListManagerApiController/Get()方法," + er.ToString());
                return Failed<DataTable>();
            }
        }

        [HttpGet]

        // GET api/DownloadListManagerApi/get
        public async Task<MessageModel<DataTable>> GetOneUserDownloadList(string userid)
        {
            try
            {
                string value = "{\"userid\": \"'" + userid + " '\"}";
                var list = Newtonsoft.Json.Linq.JObject.Parse(value);
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/DownloadListManagerApiController/Get()方法");

                DataTable rTable = QueryService.GetDataTable(list, "downloadList");

                return Success<DataTable>(rTable);
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/DownloadListManagerApiController/Get()方法," + er.ToString());
                return Failed<DataTable>();
            }
        }

        // POST api/userapi/post
        public async Task<MessageModel<string>> Post([FromBody] string value)
        {
            string username = GetCookie("uname");
            string userid = GetCookie("uid");
            var list = Newtonsoft.Json.Linq.JObject.Parse(value.Replace("{admin}", username).Replace("{1}", userid));
            Dictionary<string, JArray> dict = new Dictionary<string, JArray>();
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/DownloadListManagerApiController/Post([FromBody]string value)方法");
                string serverName = "";
                Newtonsoft.Json.Linq.JArray jArray = new JArray();

                if (list != null && list.Count > 0)
                {
                    foreach (var arry in list)
                    {
                        switch (arry.Key)
                        {
                            case "insert":
                                serverName = "insertDownloadList";
                                break;
                            case "delete7daysBefore":
                                serverName = "delete7daysBefore";
                                break;
                            default:
                                break;
                        }

                        jArray = arry.Value as JArray;
                    }
                }
                if (serverName == "delete7daysBefore")
                {
                    string selectSql = "SELECT username, filename FROM angel_downloadlist where datediff('" + value.Split('"')[7] + "', createtime)>=" + value.Split('"')[4].Split(':')[1].Split(',')[0] + " and username = '" + value.Split('"')[11] + "';";
                    DataTable dt = MySqlHelpers.GetDataTable(selectSql);
                    string uname = string.Empty, filename = string.Empty, filepath = string.Empty;
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            uname = row["username"].ToString();
                            filename = row["filename"].ToString();
                            //从磁盘上删除文件
                            filepath = AppDomain.CurrentDomain.BaseDirectory + "DownFile\\import\\" + uname + "\\" + filename;
                            System.IO.File.Delete(filepath);
                        }
                    }
                    else
                        return Failed<string>("数据库不存在要删除的数据！"); 
                }
                return Success<string>(QueryService.InsertBatchCheck(jArray, serverName));
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用 Angel.ControllersApi/ControllerApi/DownloadListManagerApiController/Post([FromBody]string value)方法," + er.ToString());
                return Failed();
            }
        }



    }
}
