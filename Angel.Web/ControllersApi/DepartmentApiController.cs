using Angel.DataAccess;
using Angel.Model;
using Angel.Service;
using Angel.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

namespace Angel.Web.ControllersApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentApiController : BaseApiController
    {

        private ICacheService dataCache;

        public DepartmentApiController(IDataService _queryService, IHttpContextAccessor accessor, ICacheService _cacheService) : base(_queryService, accessor)
        {
            dataCache = _cacheService;
        }

        // GET api/departmentapi/get
        [HttpGet]
        public async Task<MessageModel<List<DepartMent>>> Get()
        {
            var resultData = new MessageModel<string>();
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/RoomApiController/Get()方法");

                List<DepartMent> list = JsonConvert.DeserializeObject<List<DepartMent>>(QueryService.GetData(null, "7_1"));
                return Success<DepartMent>(list);
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/RoomApiController/Get()方法," + er.ToString());
                return Failed<List<DepartMent>>("查询异常");
            }
        }

        // GET api/departmentapi/getd
        public async Task<MessageModel<string>> GetD()
        {
            var resultData = new MessageModel<string>();
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/RoomApiController/GetD()方法");
                resultData.code = 1;
                resultData.data = QueryService.GetData(null, "7_2");
                resultData.msg = "查询成功";
                return resultData;
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/RoomApiController/GetD()方法," + er.ToString());
                return Failed<string>();
            }
        }

        /// <summary>
        /// 指标采集-数据导出-部门树
        /// </summary>
        /// <returns></returns>
        // GET /api/DepartmentApi/GetDepartment
        public async Task<MessageModel<List<Tree_t>>> GetDepartment()
        {
            BaseService bs_gp = new BaseService(dataCache);
            UtilFunction uf = new UtilFunction();
            // 部门TREE
            List<Tree_t> list = new List<Tree_t>();
            // 查询所有部门
            DataTable datatable = bs_gp.GetDataTableToID("findDepartment_To2GP");
            // 一级部门
            foreach (DataRow row1 in datatable.Select("parent_id=0"))
            {
                Tree_t tree1 = new Tree_t();
                tree1.id = row1["id"].ToString();
                tree1.text = row1["dname"].ToString();
                tree1.nodes = new List<Tree_t>();
                // 二级部门
                foreach (DataRow row2 in datatable.Select("parent_id=" + tree1.id))
                {
                    Tree_t tree2 = new Tree_t();
                    tree2.id = row2["id"].ToString();
                    tree2.text = row2["dname"].ToString();
                    tree2.nodes = new List<Tree_t>();
                    tree1.nodes.Add(tree2);
                }
                list.Add(tree1);
            }
            return Success<Tree_t>(list);
        }


        // Get api/departmentapi/GetRoommeul
        public async Task<MessageModel<string>> GetRoommeul()
        {
            var resultData = new MessageModel<string>();
            try
            {
                string roomid = GetCookie("roomid"); ;
                //string roomid = "46";
                string value = "{ \"RoomID\": " + roomid + "}";
                var list = Newtonsoft.Json.Linq.JObject.Parse(value);
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/RoomApiController/GetRoommeul()方法");
                resultData.code = 1;
                resultData.data = QueryService.GetData(list, "33_5");
                resultData.msg = "查询成功";
                return resultData;
            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用Angel.ControllersApi/ControllerApi/RoomApiController/GetRoommeul()方法," + er.ToString());
                return Failed<string>();
            }
        }





        // POST api/DepartmentApi/post
        public async Task<MessageModel<string>> Post([FromBody] string value)
        {
            var resultData = new MessageModel<string>();
            string username = GetCookie("uname");
            var list = Newtonsoft.Json.Linq.JObject.Parse(value.Replace("admin", username));
            //Newtonsoft.Json.Linq.JArray jArray = new JArray();
            Dictionary<string, JArray> dict = new Dictionary<string, JArray>();
            string serverName = "";
            try
            {
                FileLog.WriteLog("InfoApiTime：" + DateTime.Now.ToString() + ",调用：Angel.ControllersApi/ControllerApi/RoomApiController/Post([FromBody]string value)方法");
                if (list != null && list.Count > 0)
                {

                    foreach (var arry in list)
                    {
                        switch (arry.Key)
                        {
                            case "insert":
                                serverName = "7_3";
                                break;
                            case "update":
                                serverName = "7_4";
                                break;
                            case "delete":
                                serverName = "7_5";
                                break;
                            case "insertRoom":
                                serverName = "33_11";
                                break;
                            case "deleteroom":
                                serverName = "33_12";
                                break;
                            case "roommenulist":
                                serverName = "33_5";
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

                string data = string.Empty;

                if (serverName.Equals("33_1"))
                {
                    data = QueryService.ExecuteScalar(dict);
                }
                else
                {
                    data = QueryService.MulteBatch(dict);
                }


                resultData.code = 1;
                resultData.data = data;
                resultData.msg = "查询成功";
                return resultData;


            }
            catch (Exception er)
            {
                FileLog.WriteLog("Error：调用 Angel.ControllersApi/ControllerApi/RoomApiController/Post([FromBody]string value)方法," + er.ToString());
                return Failed<string>();
            }
        }



        //// GET api/roomapi
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/roomapi/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/roomapi
        //public void Post([FromBody]string value)
        //{
        //}
    
     }
}
