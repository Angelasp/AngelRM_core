using Angel.Service;
using Microsoft.AspNetCore.Mvc;

namespace Angel.Web.Controllers
{
    public class BaseController : Controller
    {
        public IDataService QueryService;

        private IHttpContextAccessor _accessor;

        public BaseController(IDataService _queryService, IHttpContextAccessor accessor)
        {
            QueryService = _queryService;
            _accessor = accessor;
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


    }
}
