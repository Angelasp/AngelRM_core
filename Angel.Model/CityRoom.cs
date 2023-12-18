using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angel.Model
{
    [Serializable]
    public class CityRoom
    {
        //实体
        private string _id; public string id { get { return _id; } set { _id = value; } }
        private string _proname; public string proname { get { return _proname; } set { _proname = value; } }
        private string _roomname; public string roomname { get { return _roomname; } set { _roomname = value; } }
        private string _name; public string name { get { return _name; } set { _name = value; } }


    }
}
