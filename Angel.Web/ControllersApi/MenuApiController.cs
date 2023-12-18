using Angel.Utils;
using Angel.Service;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Angel.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Angel.Web.ControllersApi
{
    /*************************************************************************
    * 文件名称 ：MenuApiController.cs                          
    * 描述说明 ：系统导航菜单API控制器
    * 创建信息 : create by QQ：815657032、709047174 Email:Angel_asp@126.com on 2018-05-09
    * 修订信息 : modify by (person) on (date) for (reason)
    * 
    * 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
    **************************************************************************/
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuApiController : BaseApiController
    {

        public MenuApiController(IDataService _queryService, IHttpContextAccessor accessor): base(_queryService, accessor)
        {
        }


        // GET api/menuapi
        [HttpGet]
        public async Task<List<Menus>> Get()
        {

            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/MenuApiController/Get()方法");
                List<Menus> list = JsonConvert.DeserializeObject<List<Menus>>(QueryService.GetData(null, "0_1"));
                return list;
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/MenuApiController/Get()方法," + er.ToString());
                return null;
            }
        }

        // GET api/menuapi/Getroleid
        [HttpGet]
        public async Task<MessageModel<string>> Getroleid(int roleid)
        {
            var resultData = new MessageModel<string>();
            try
            {
                var list = Newtonsoft.Json.Linq.JObject.Parse("{roleid:"+roleid+"}");
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/MenuApiController/Getroleid()方法");

                resultData.code = 1;
                resultData.data = QueryService.GetData(list, "0_8");
                resultData.msg ="查询成功";
                return resultData;

            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/MenuApiController/Getroleid()方法," + er.ToString());
                return Failed<string>("查询异常");
            }
        }

        // POST api/menuapi/post
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] string value)
        {

            var resultData = new MessageModel<string>();

            string username = GetCookie("uname");
            var list = Newtonsoft.Json.Linq.JObject.Parse(value.Replace("admin", username));
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

                resultData.code = 1;
                resultData.data = "提交成功";
                resultData.msg = QueryService.MulteBatch(dict);
                return resultData;
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用 Angel.ControllersApi/ControllerApi/MenuApiController/Post([FromBody]string value)方法," + er.ToString());
                resultData.code = 0;
                resultData.msg = "提交异常";
                return resultData;
            }
        }



        //// GET api/menuapi
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/menuapi/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/menuapi
        //public void Post([FromBody]string value)
        //{
        //}

        // DELETE api/menuapi/5
        public void Delete(int id)
        {
        }

    }
}
/*************************************************************************
* 文件名称 ：MenuApiController.cs                          
* 描述说明 ：系统导航菜单API控制器
* 创建信息 : create by QQ：815657032、709047174 Email:Angel_asp@126.com on 2018-05-09
* 修订信息 : modify by (person) on (date) for (reason)
* 
* 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
**************************************************************************/
