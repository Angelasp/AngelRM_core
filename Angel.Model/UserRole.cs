using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angel.Model
{
    [Serializable]
    public class UserRole
    {
        private int _id; public int id { get { return _id; } set { _id = value; } }
        private string _rolename; public string rolename { get { return _rolename; } set { _rolename = value; } }
        private int _level; public int level { get { return _level; } set { _level = value; } }
        private string _remark; public string remark { get { return _remark; } set { _remark = value; } }
        private string _isenabled; public string isenabled { get { return _isenabled; } set { _isenabled = value; } }
        private string _createuser; public string createuser { get { return _createuser; } set { _createuser = value; } }
        private string _createtime; public string createtime { get { return _createtime; } set { _createtime = value; } }
        private string _updateuser; public string updateuser { get { return _updateuser; } set { _updateuser = value; } }
    }
}
