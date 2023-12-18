using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angel.Model
{
    [Serializable]
    public class UserInfo
    {
        private int _id; public int id { get { return _id; } set { _id = value; } }
        private string _username; public string username { get { return _username; } set { _username = value; } }
        private string _password; public string password { get { return _password; } set { _password = value; } }
        private string _isenabled; public string isenabled { get { return _isenabled; } set { _isenabled = value; } }
        private string _lastlogintime; public string lastlogintime { get { return _lastlogintime; } set { _lastlogintime = value; } }
        private string _createuser; public string createuser { get { return _createuser; } set { _createuser = value; } }
        private string _createtime; public string createtime { get { return _createtime; } set { _createtime = value; } }
        private int _logincount; public int logincount { get { return _logincount; } set { _logincount = value; } }
        private string _updateuser; public string updateuser { get { return _updateuser; } set { _updateuser = value; } }
        private string _updatetime; public string updatetime { get { return _updatetime; } set { _updatetime = value; } }
        private string _cityid; public string cityid { get { return _cityid; } set { _cityid = value; } }
        private int _roleid; public int roleid { get { return _roleid; } set { _roleid = value; } }
        private string _rolename; public string rolename { get { return _rolename; } set { _rolename = value; } }
        private string _roomid; public string roomid { get { return _roomid; } set { _roomid = value; } }
        private string _roomname; public string roomname { get { return _roomname; } set { _roomname = value; } }


    }
}
