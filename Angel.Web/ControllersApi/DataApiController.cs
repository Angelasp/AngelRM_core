using Angel.DataAccess;
using Angel.Model;
using Angel.Service;
using Angel.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Angel.Web.ControllersApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataApiController : BaseApiController
    {

        private ICacheService _cache;
        public DataApiController(IDataService _queryService, IHttpContextAccessor accessor, ICacheService cache) : base(_queryService, accessor)
        {
            this._cache = cache;
        }
        //获取语句返回json数据
        // GET /api/dataapi/get
        [HttpGet]
        public async Task<MessageModel<DataTable>> Get()
        {

            BaseService bs = new BaseService(_cache);
           UtilFunction uf = new UtilFunction();
           /// DataCache.SetCache("ssssss", "ttttttttttttt");
            //  DataTable tablelist = bs.GetDataTableToID("CityData");
            string value = "{ \"CityData1\": [{\"[@c0]\": \"PRONAME\", \"[@c1]\": \"CITYNAME\", \"[@c2]\": \"CITY_NO\",\"[@c3]\": \"where 1=1\" }]}";
            var list = Newtonsoft.Json.Linq.JObject.Parse(value);
            Dictionary<string, JArray> jarrays = new Dictionary<string, JArray>();

            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }
            DataTable tablelist = bs.GetDataTableToParamID(jarrays);

            return Success<DataTable>(tablelist);

        }

        // GET /api/dataapi/get/1
        [HttpGet("{id}")]
        public async Task<MessageModel<List<CityRoom>>> Get(int id)
        {

            string myjson = string.Empty;


            switch (id)
            {
                case 1:
                    myjson = QueryService.GetData(null, "1_13");
                    break;
                case 2:
                    myjson = "[{\"name\":\"2号\"}]";
                    break;
                case 3:
                    myjson = "[{\"name\":\"3号\"}]";
                    break;
                default:
                    myjson = QueryService.GetData(null, "room_list");
                    break;
            }
            List<CityRoom> model = JsonConvert.DeserializeObject<List<CityRoom>>(myjson);
            return Success<CityRoom>(model);
        }

        // POST api/dataapi
        public async Task<MessageModel<DataTable>> Post([FromBody] string value)
        {
            BaseService bs = new BaseService(_cache);
            var list = Newtonsoft.Json.Linq.JObject.Parse(value);
            Dictionary<string, JArray> jarrays = new Dictionary<string, JArray>();
            foreach (var arry in list)
            {
                jarrays.Add(arry.Key, arry.Value as JArray);
            }

            DataTable tablelist = bs.GetDataTableToParamID(jarrays);
            return Success<DataTable>(tablelist);

        }

        // DELETE api/dataapi/5
        public void Delete(int id)
        {
        }

    }
}
