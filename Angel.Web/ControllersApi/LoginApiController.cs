using Angel.Model;
using Angel.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Angel.Service;
using System.Web;


namespace Angel.Web.ControllersApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginApiController : BaseApiController
    {

        public LoginApiController(IDataService _queryService, IHttpContextAccessor accessor) : base(_queryService, accessor)
        {
        }

        // post api/loginapi/postLogin
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="value">实体信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> postLogin([FromBody] UserInfos value)
        {
            IEnumerable<KeyValuePair<string, string>> list;  // = ControllerContext.Request.GetQueryNameValuePairs();
            JObject obj = new JObject();
            var resultData = new MessageModel<string>();
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerAPI/LoginApiController/Get()方法");
                string mycode = value.mycode;
                string username = value.username;
                obj.Add("username", value.username);
                obj.Add("password", value.password);
                obj.Add("mycode", value.mycode);
                obj.Add("url", value.url);

                // 1. 验证码检查
                string a = HttpContext.Session.GetString("checkcode").ToString();
                if (HttpContext.Session.GetString("checkcode") == null)
                {
                    resultData.code = 0;
                    resultData.msg = "验证码错误";
                    return resultData;
                }

                if (mycode == "" || mycode == null)
                {
                    resultData.code = 0;
                    resultData.msg = "输入验证码错误";
                    return resultData;
                }
                string b = mycode.ToUpper();

                 if (String.Compare(a, b, true) != 0) {
                    resultData.code = 0;
                    resultData.msg = "验证码错误";
                    return resultData;
                }

                string msg = QueryService.GetDataBefor(obj, "1_6");
                if (msg.IndexOf("成功") != -1)
                {
                    JObject model = JObject.FromObject(JsonConvert.DeserializeObject(msg));
                    JToken UserId = model["user"][0]["id"];
                    JToken UserName = model["user"][0]["username"];
                    JToken CityId = model["user"][0]["cityid"];
                    JToken RoleId = model["role"][0]["roleid"];
                    JToken RoleName = model["role"][0]["rolename"];
                    JToken Level = model["role"][0]["level"];
                    int Rid = Convert.ToInt32(RoleId.ToString());
                    int Uid = int.Parse(UserId.ToString());
                    string UName = UserName.ToString();
                    WriteCookie("uid", Uid.ToString(), 14400); // 用户ID
                    WriteCookie("roleid", Rid.ToString(), 14400);  //角色ID
                    WriteCookie("rolename", HttpUtility.UrlEncode(RoleName.ToString()), 14400);    //角色名字
                    WriteCookie("uname", UName, 14400);    //用户名字
                    WriteCookie("cityid", HttpUtility.UrlEncode(CityId.ToString()), 14400);    //城市id
                    WriteCookie("level", Level.ToString(), 14400);  //级别ID
                    LoginLogService loginbll = new LoginLogService();
                    string clientip = GetIpAddress();
                    loginbll.SaveLog(Uid, UName, Rid, RoleName.ToString(), clientip, UName);
                }

                resultData.code = 1;
                resultData.data = "登录成功";
                resultData.msg = msg;
                return resultData;
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerAPI/LoginApiController/Get()方法," + er.ToString());
                resultData.code = 0;
                resultData.msg = "登录异常";
                return resultData;
            }
        }

        // POST api/userapi/post
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] string value)
        {
            var list = Newtonsoft.Json.Linq.JObject.Parse(value);
            //Newtonsoft.Json.Linq.JArray jArray = new JArray();
            Dictionary<string, JArray> dict = new Dictionary<string, JArray>();
            var resultData = new MessageModel<string>() { code = 1, msg = "" };

            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/UserApiController/Post([FromBody]string value)方法");
                string serverName = "";
                Newtonsoft.Json.Linq.JArray jArray = new JArray();

                if (list != null && list.Count > 0)
                {
                    foreach (var arry in list)
                    {
                        switch (arry.Key)
                        {
                            case "insert":
                                serverName = "1_1";
                                break;
                            case "update":
                                serverName = "1_4";
                                break;
                            case "delete":
                                serverName = "1_8";
                                break;
                            default:
                                break;
                        }

                        jArray = arry.Value as JArray;
                    }
                }

                resultData.data = QueryService.InsertBatchCheck(jArray, serverName);
                return resultData;
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用 Angel.ControllersApi/ControllerApi/UserApiController/Post([FromBody]string value)方法," + er.ToString());
                resultData.code = 0;
                resultData.msg = "请求异常！";
                return resultData;
         
            }
        }



        // GET api/userapi/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/userapi
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/userapi/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/userapi/5
        public void Delete(int id)
        {
        }

        // GET api/loginapi/getlogincode
        /// <summary>
        /// 获取登录验证码
        /// </summary>
        /// <param name="granularity"></param>
        /// <returns
        /// ></returns>
        /// 
        [HttpGet]
        public string getLoginCode()
        {
            // 3. 验证码检查
            string a = HttpContext.Session.GetString("checkcode").ToString();
            return (a == null || a == "") ? "" : a;
        }

    }
}
