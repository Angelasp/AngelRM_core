using Angel.DataAccess;
using Angel.DBHelper;
using Angel.Model;
using Angel.Service;
using Angel.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Angel.Web.ControllersApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginlogApiController : BaseApiController
    {

        private ICacheService dataCache;

        public LoginlogApiController(IDataService _queryService, IHttpContextAccessor accessor, ICacheService _cacheService) : base(_queryService, accessor)
        {
            dataCache = _cacheService;
        }
        public class Page
        {
            // 每页多少条数据
            public int pageSize { get; set; }
            // 从多少行开始
            public int offset { get; set; }
        }


        // post api/loginlogapi/postlist
        [HttpPost]
        public async Task<Dictionary<string, object>> postList([FromForm] Page page)
        {
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/LoginlogApiController/PostList()方法");
                int pagenumber = page.offset;//从多少行开始查询
                int pSize = page.pageSize;
                UtilFunction utf1 = new UtilFunction();
                LoginLogService loginbll = new LoginLogService();
                string total = loginbll.counttotal().ToString();
                // 返回查询结果
                List<Model.LoginLogModel> listmodel = loginbll.SelectLoginLOG(null, pagenumber, pSize);
                Dictionary<string, object> objectlist = new Dictionary<string, object>();
                if (total == null)
                {
                    objectlist.Add("total", 0);
                }
                else
                {
                    objectlist.Add("total", total);
                }
                objectlist.Add("rows", listmodel);
                return  objectlist;
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/LoginlogApiController/PostList()方法," + er.ToString());
                return null;
            }
        }


        // POST api/loginlogapi/post
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] string value)
        {
            var list = Newtonsoft.Json.Linq.JObject.Parse(value);
            //Newtonsoft.Json.Linq.JArray jArray = new JArray();
            Dictionary<string, JArray> dict = new Dictionary<string, JArray>();
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/MenuApiController/Post([FromBody]string value)方法");
                if (list != null && list.Count > 0)
                {
                    string serverName = "";
                    foreach (var arry in list)
                    {
                        switch (arry.Key)
                        {
                            case "insert":
                                serverName = "0_5";
                                break;
                            case "update":
                                serverName = "0_4";
                                break;
                            case "delete":
                                serverName = "0_6";
                                break;
                            default:
                                break;
                        }

                        //jArray.Add( arry.Value as JArray);
                        if (serverName.Equals("") == false)
                        {
                            dict.Add(serverName, arry.Value as JArray);
                        }
                    }
                }
                return Success<string>(QueryService.MulteBatch(dict));
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用 Angel.ControllersApi/ControllerApi/MenuApiController/Post([FromBody]string value)方法," + er.ToString());
                return Failed<string>();
            }
        }


        // GET /api/loginlogapi/GetExcel
        [HttpGet]
        public async Task<MessageModel<string>> GetExcel()
        {

            string TempName = "用户登录日志表.xls";
            string value = "{ \"logloginexcel\": [{\"[@c0]\": \"\"}]}";
            ExportExcel(TempName, value);
            string myjson = "{\"success\":\"导出成功,请到下载中心进行下载\"}";
            return Success<string>("导出成功,请到下载中心进行下载");
        }

        //增加操作日志
        [HttpPost]
        public async Task<MessageModel<string>> Post_Log([FromBody] string value)
        {
            string userid = GetCookie("uid");
            string username = GetCookie("uname");
            string roleid = GetCookie("roleid");
            string rolename = Convert.ToString(MySqlHelpers.ExecuteScalar("select rolename from angel_sys_role where id = '" + roleid + "'"));
            string logposition = value.Split('"')[5];
            string operationtype = value.Split('"')[9];
            try
            {
                int count = Convert.ToInt32(MySqlHelpers.ExecuteScalar("SELECT COUNT(*) FROM angel_sys_operationlog"));
                int ID = 0;
                if (count == 0)
                {
                    ID = 1;
                }
                else
                {
                    ID = Convert.ToInt32(MySqlHelpers.ExecuteScalar("SELECT MAX(ID) FROM angel_sys_operationlog")) + 1;
                }
                string sql = "INSERT INTO angel_sys_operationlog VALUES (" + ID + ", '" + userid + "', '" + username + "', '" + roleid + "','" + rolename + "','" + logposition + "','" + operationtype + "','" + value.ToString() + "','" + username + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                MySqlHelpers.ExecuteNonQuery(sql);
                return Success<string>("成功");
            }
            catch (Exception er)
            {
                return Failed<string>();
            }
        }

        [HttpGet]
        public async Task<MessageModel<List<OperLog>>> GetOperLog()
        {
            try
            {
                List<OperLog> model;
                string uname = GetCookie("uname");
                if (uname != "admin")
                {
                    string value = "{\"uname\": \"'" + uname + "'\"}";
                    var list = Newtonsoft.Json.Linq.JObject.Parse(value);
                    model = JsonConvert.DeserializeObject<List<OperLog>>(QueryService.GetData(list, "selectoperationloglist"));                }
                else
                {
                    model = JsonConvert.DeserializeObject<List<OperLog>>(QueryService.GetData(null, "selectoperationloglist_admin"));
                } 
                return Success<List<OperLog>>(model);
            }
            catch (Exception er)
            {
                return Failed<List<OperLog>>();
            }
        }


        /// <summary>
        /// 按模板方式导出无返回值
        /// </summary>
        /// <param name="TempName"></param>
        /// <param name="Value"></param>
        /// <param name="sp"></param>
        public void ExportExcel(string TempName, string Value)
        {

            //var list = Newtonsoft.Json.Linq.JObject.Parse(Value);
            //Dictionary<string, JArray> jarrays = new Dictionary<string, JArray>();
            //string username = UtilFunction.GetCookie("uname");
            //foreach (var arry in list)
            //{
            //    jarrays.Add(arry.Key, arry.Value as JArray);
            //}
            //SaveParameter sp = new SaveParameter(jarrays, TempName, username);
            //SaveExcel SaveExcel = new SaveExcel();
            //SaveExcel.RunSave(sp);

            //while (true)
            //{
            //    if (SaveExcel.State > 1) return;
            //}
        }
    }
}
