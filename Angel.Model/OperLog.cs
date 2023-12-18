using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angel.Model
{
    [Serializable]
    public class OperLog
    {
        //菜单按钮实体
        private int _id; public int id { get { return _id; } set { _id = value; } }
        private int _userid; public int userid { get { return _userid; } set { _userid = value; } }
        private string _username; public string username { get { return _username; } set { _username = value; } }
        private int _roleid; public int roleid { get { return _roleid; } set { _roleid = value; } }
        private string _rolename; public string rolename { get { return _rolename; } set { _rolename = value; } }
        private string _logposition; public string logposition { get { return _logposition; } set { _logposition = value; } }
        private string _operationtype; public string operationtype { get { return _operationtype; } set { _operationtype = value; } }
        private string _operationparam; public string operationparam { get { return _operationparam; } set { _operationparam = value; } }
        private string _createuser; public string createuser { get { return _createuser; } set { _createuser = value; } }
        private string _createdate; public string createdate { get { return _createdate; } set { _createdate = value; } }

    }
}
