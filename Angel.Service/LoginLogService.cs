using Angel.DBHelper;
using Angel.Model;
using Angel.Utils;
using System.Data;

namespace Angel.Service
{
    public class LoginLogService
    {
        /// <summary>
        /// 写入登录日志的方法
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="username">用户名称</param>
        /// <param name="roleid">权限ID</param>
        /// <param name="rolename">权限名称</param>
        /// <param name="logintdate">登录时间</param>
        public void SaveLog(int userid, string username, int roleid, string rolename, string clientip, string createuser)
        {
            var values = XmlHelper.ReadXml("SqlQuery", "insertloginlog");
            string sql = string.Format(values.First().Value, userid, username, roleid, rolename, createuser, clientip, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            MySqlHelpers.ExecuteNonQuery(sql);
        }

        ///// <summary>
        ///// 写入登录日志的方法
        ///// </summary>
        ///// <param name="userid">用户ID</param>
        ///// <param name="username">用户名称</param>
        ///// <param name="roleid">权限ID</param>
        ///// <param name="rolename">权限名称</param>
        ///// <param name="logintdate">登录时间</param>
        //public void SaveLog(int userid, string username, int roleid, string rolename, string clientip, string createuser, string type)
        //{
        //    var values = XmlHelper.ReadXml("SqlQuery", "insertloginlog");
        //    string sql = string.Format(values.First().Value, userid, username, roleid, rolename, createuser, clientip, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type);
        //    MySqlHelpers.ExecuteNonQuery(sql);
        //}

        /// <summary>
        /// 获取总条数
        /// </summary>
        public object counttotal()
        {
            string sql = " select count(*) from angel_sys_loginlog";
            return MySqlHelpers.ExecuteScalar(sql);
        }

        /// <summary>
        /// 登录日志列表
        /// </summary>
        /// <param name="time">查询条件</param>
        /// <returns></returns>
        public List<LoginLogModel> SelectLoginLOG(string time, int PageIndex, int PageSize)
        {
            List<LoginLogModel> logs = new List<LoginLogModel>();
            try
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                string sql;

                if (time != null)
                {
                    values = XmlHelper.ReadXml("SqlQuery", "getqueryloginlog");
                    sql = string.Format(values.First().Value, PageIndex, PageIndex * (PageSize - 1), time);
                }
                else
                {
                    values = XmlHelper.ReadXml("SqlQuery", "selectloginloglist");
                    sql = string.Format(values.First().Value, PageIndex, PageSize);
                }

                DataTable dt = MySqlHelpers.GetDataTable(sql);
                foreach (DataRow row in dt.Rows)
                {
                    LoginLogModel log = new LoginLogModel();
                    log.ID = Convert.ToInt32(row["id"].ToString());
                    log.UserID = Convert.ToInt32(row["userid"].ToString());
                    log.UserName = row["username"].ToString();
                    log.RoleID = Convert.ToInt32(row["roleid"].ToString());
                    log.RoleName = row["rolename"].ToString();
                    log.ClientIP = row["clientip"].ToString();
                    log.CreateUser = row["createuser"].ToString();
                    log.LoginTime = Convert.ToDateTime(row["logintime"].ToString());
                    log.CreateTime = Convert.ToDateTime(row["createtime"].ToString());
                    logs.Add(log);
                }

            }
            catch (Exception er)
            { FileLog.WriteLog("异常捕捉：Angel.Service.LoginLogService" + er.ToString()); }
            return logs;
        }
    }
}
