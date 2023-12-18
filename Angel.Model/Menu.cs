using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angel.Model
{
    [Serializable]
    public class Menu
    {
        //菜单按钮实体
        private string _id; public string id { get { return _id; } set { _id = value; } }
        private string _menuname; public string menuname { get { return _menuname; } set { _menuname = value; } }
        private string _parentid; public string parentid { get { return _parentid; } set { _parentid = value; } }
        private string _orderid; public string orderid { get { return _orderid; } set { _orderid = value; } }
        private string _menutype; public string menutype { get { return _menutype; } set { _menutype = value; } }
        private string _menuo; public string menuo { get { return _menuo; } set { _menuo = value; } }

        private string _menuicon; public string menuicon { get { return _menuicon; } set { _menuicon = value; } }
        private string _menuurl; public string menuurl { get { return _menuurl; } set { _menuurl = value; } }
        private string _isenabled; public string isenabled { get { return _isenabled; } set { _isenabled = value; } }

        private string _isvisible; public string isvisible { get { return _isvisible; } set { _isvisible = value; } }
        private string _createuser; public string createuser { get { return _createuser; } set { _createuser = value; } }

        private string _createtime; public string createtime { get { return _createtime; } set { _createtime = value; } }

        private string _updateuser; public string updateuser { get { return _updateuser; } set { _updateuser = value; } }
        private string _updatetime; public string updatetime { get { return _updatetime; } set { _updatetime = value; } }
        private string _remark; public string remark { get { return _remark; } set { _remark = value; } }

    }
}
