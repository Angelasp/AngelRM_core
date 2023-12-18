using Angel.Model;
using Angel.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Angel.Web.ControllersApi
{
    public class BaseApiController : ControllerBase
    {


        public IDataService QueryService;

        private IHttpContextAccessor _accessor;

        public BaseApiController(IDataService _queryService, IHttpContextAccessor accessor)
        {
            QueryService = _queryService;
            _accessor = accessor;
        }


        [NonAction]
        public MessageModel<T> Success<T>(T data, string msg = "成功")
        {
            return new MessageModel<T>()
            {
                code = 1,
                msg = msg,
                data = data,
            };
        }
        [NonAction]
        public MessageModel<List<T>> Success<T>(List<T> data, string msg = "成功")
        {
            return new MessageModel<List<T>>()
            {
                code = 1,
                msg = msg,
                data = data,
            };
        }



        [NonAction]
        public MessageModel Success(string msg = "成功")
        {
            return new MessageModel()
            {
                code = 1,
                msg = msg,
                data = null,
            };
        }

        [NonAction]
        public MessageModel<string> Failed(string msg = "失败", int status = 500)
        {
            return new MessageModel<string>()
            {
                code = 0,
                status = status,
                msg = msg,
                data = null,
            };
        }

        [NonAction]
        public MessageModel<T> Failed<T>(string msg = "失败", int status = 500)
        {
            return new MessageModel<T>()
            {
                code = 0,
                status = status,
                msg = msg,
                data = default,
            };
        }

        [NonAction]
        public MessageModel<PageModel<T>> SuccessPage<T>(int page, int dataCount, int pageSize, List<T> data,
            int pageCount, string msg = "获取成功")
        {
            return new MessageModel<PageModel<T>>()
            {
                code = 1,
                msg = msg,
                data = new PageModel<T>(page, dataCount, pageSize, data)
            };
        }

        [NonAction]
        public MessageModel<PageModel<T>> SuccessPage<T>(PageModel<T> pageModel, string msg = "获取成功")
        {
            return new MessageModel<PageModel<T>>()
            {
                code = 1,
                msg = msg,
                data = pageModel
            };
        }


        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public void WriteCookie(string strName, string strValue, int expires)
        {
            CookieOptions options = new CookieOptions();
            // 设置过期时间
            options.Expires = DateTime.Now.AddMinutes(expires);
            if (_accessor != null)
            {
                _accessor.HttpContext.Response.Cookies.Append(strName, strValue, options);
            }
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public string GetCookie(string strName)
        {
            if (_accessor != null && _accessor.HttpContext.Request.Cookies[strName] != null)
            {
                return _accessor.HttpContext.Request.Cookies[strName];
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public string GetIpAddress()
        {
            string ipAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return ipAddress;
        }



    }
}
