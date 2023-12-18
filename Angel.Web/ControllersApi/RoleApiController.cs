using Microsoft.AspNetCore.Mvc;
using Angel.Utils;
using Angel.Model;
using Angel.DataAccess;
using Newtonsoft.Json;
using System.Data;
using Angel.Service;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Angel.Web.ControllersApi
{
    /*************************************************************************
    * 文件名称 ：RoleApiController.cs                          
    * 描述说明 ：用户角色API控制器
    * 创建信息 : create by QQ：815657032、Angel_asp@126.com on 2018-05-09
    * 修订信息 : modify by (person) on (date) for (reason)
    * 
    * 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
    **************************************************************************/
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleApiController : BaseApiController
    {

        private ICacheService _cache;

        public RoleApiController(IDataService _queryService, IHttpContextAccessor accessor, ICacheService cache) : base(_queryService, accessor)
        {
            _cache = cache;
        }


        // GET api/roleapi/get
        [HttpGet]
        public async Task<MessageModel<List<UserRole>>> Get()
        {

            try
            {
                string level = GetCookie("level");
                int levels = 0;
                if (Convert.ToInt32(level) == 9)
                {
                    levels = Convert.ToInt32(level) + 1;
                }
                else
                {
                    levels = Convert.ToInt32(level);
                }
                var list = Newtonsoft.Json.Linq.JObject.Parse("{level:" + levels + "}");
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/RoleApiController/Get()方法");
                List<UserRole> model = JsonConvert.DeserializeObject<List<UserRole>>(QueryService.GetData(list, "2_2"));
                return Success<UserRole>(model);
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/RoleApiController/Get()方法," + er.ToString());
                return Failed<List<UserRole>>("查询异常");
            }
        }


        // Get api/roleapi/GetRolemeul
        [HttpGet]
        public async Task<MessageModel<List<Menu>>> GetRolemeul()
        {
            var resultData = new MessageModel<string>();
            try
            {
                string roleid = GetCookie("roleid");
                string value = "{ \"RoleID\": " + roleid + "}";
                var list = Newtonsoft.Json.Linq.JObject.Parse(value);
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/RoleApiController/GetRolemeul()方法");
                //缓存当前角色按钮权限
                DataTable btdt = QueryService.GetWhereDataTable("query_rolebtnlist", roleid);
                List<Menu> menulist = new List<Menu>();
                foreach(DataRow row in btdt.Rows){
                    Menu menu = new Menu();
                    menu.id = row["id"].ToString();
                    menu.menuname = row["menuname"].ToString();
                    menu.parentid = row["parentid"].ToString();
                    menu.orderid = row["orderid"].ToString();
                    menu.menutype = row["menutype"].ToString();
                    menu.menuo = row["menuo"].ToString();
                    menulist.Add(menu);
                }
                _cache.Set("roledatabtn", menulist);
                //结束按钮权限操作
                return Success<List<Menu>>(JsonConvert.DeserializeObject<List<Menu>>(QueryService.GetData(list, "2_5")));
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/RoleApiController/GetRolemeul()方法," + er.ToString());

                return Failed<List<Menu>>();
            }
        }

        // POST api/roleapi/post
        public async Task<MessageModel<string>> Post([FromBody]string value)
        {
            var resultData = new MessageModel<string>();
            string username = GetCookie("uname");
            var list = Newtonsoft.Json.Linq.JObject.Parse(value.Replace("admin", username));
            //Newtonsoft.Json.Linq.JArray jArray = new JArray();
            Dictionary<string, JArray> dict = new Dictionary<string, JArray>();
            string serverName = "";
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/RoleApiController/Post([FromBody]string value)方法");
                if (list != null && list.Count > 0)
                {

                    foreach (var arry in list)
                    {
                        switch (arry.Key)
                        {
                            case "insert":
                                serverName = "2_1";
                                break;
                            case "update":
                                serverName = "2_3";
                                break;
                            case "delete":
                                serverName = "2_4";
                                break;
                            case "insertRoleMenu":
                                serverName = "2_11";
                                break;
                            case "deleterole":
                                serverName = "2_12";
                                break;
                            case "rolemenulist":
                                serverName = "2_5";
                                break;
                            default:
                                break;
                        }

                        if (serverName.Equals("") == false)
                        {
                            dict.Add(serverName, arry.Value as JArray);
                        }
                    }

                }
                if (serverName.Equals("2_1"))
                {
                    resultData.code = 1;
                    resultData.data = "操作成功";
                    resultData.msg = QueryService.ExecuteScalar(dict);
                    return resultData;
                }
                else
                {
                    resultData.code = 1;
                    resultData.data = "操作成功";
                    resultData.msg = QueryService.MulteBatch(dict);
                    return resultData;
                }

            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用 Angel.ControllersApi/ControllerApi/RoleApiController/Post([FromBody]string value)方法," + er.ToString());
                resultData.code = 0;
                resultData.msg = "操作异常";
                return resultData;
            }
        }


        // GET api/roleapi/5
        public string Get(int id)
        {
            return "value";
        }


        // PUT api/roleapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/roleapi/5
        public void Delete(int id)
        {
        }



    }
}

/*************************************************************************
* 文件名称 ：RoleApiController.cs                          
* 描述说明 ：系统导航菜单API控制器
* 创建信息 : create by QQ：815657032、709047174 Email:Angel_asp@126.com on 2018-05-09
* 修订信息 : modify by (person) on (date) for (reason)
* 
* 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
**************************************************************************/