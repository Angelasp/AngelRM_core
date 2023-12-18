using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angel.Model
{
    [Serializable]
    public class DepartMent
    {
        //实体
        private int _id; public int id { get { return _id; } set { _id = value; } }
        private string _dname; public string dname { get { return _dname; } set { _dname = value; } }
        private int _parent_id; public int parent_id { get { return _parent_id; } set { _parent_id = value; } }
        private int _seq; public int seq { get { return _seq; } set { _seq = value; } }
        private string _remark; public string remark { get { return _remark; } set { _remark = value; } }
        private string _isenabled; public string isenabled { get { return _isenabled; } set { _isenabled = value; } }
        private string _createuser; public string createuser { get { return _createuser; } set { _createuser = value; } }
        private string _createtime; public string createtime { get { return _createtime; } set { _createtime = value; } }

        private string _updateuser; public string updateuser { get { return _updateuser; } set { _updateuser = value; } }
        private string _updatetime; public string updatetime { get { return _updatetime; } set { _updatetime = value; } }

    }
}
