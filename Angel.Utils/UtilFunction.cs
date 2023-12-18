
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Angel.Model;
using System.Net.Http;

namespace Angel.Utils
{
    /*************************************************************************
       * 文件名称 ：UtilFunction.cs                          
       * 描述说明 ：共有通用方法
       * 
       * 创建信息 : create by QQ：815657032、709047174  E-mail:Angel_asp@126.com on 2016-06-10
       * 修订信息 : modify by (person) on (date) for (reason)
       * 
       * 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
       **************************************************************************
       */
    public class UtilFunction
    {


        /// <summary>
        /// 返回json
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string ToJson(object o)
        {
            string myjson = string.Empty;
            string datas = string.Empty;
            //名称和日期都进行格式
            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //空值处理
                //setting.NullValueHandling = NullValueHandling.Ignore;
                return setting;
            });

            myjson = JsonConvert.SerializeObject(o, Newtonsoft.Json.Formatting.None, setting);

            return myjson;
        }

        /// <summary>
        /// 查询文件列表
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public List<FileList> DataFileName(string UserName, string FilePath)
        {
            string Folder = FilePath + UserName;
            //判断目录是否存在
            FolderCreate(Folder);
            //var resultlist = new List<String>();
            List<FileList> resultlist = new List<FileList>();
            DirectoryInfo di = new DirectoryInfo(Folder);
            FileInfo[] arrFi = di.GetFiles("*.*");
            SortAsFileCreationTime(ref arrFi);
            if (arrFi.Length > 0)
            {
                foreach (var file in arrFi)
                {
                    FileList fi = new FileList();
                    fi.FileName = file.Name;
                    fi.FilePath = file.FullName;
                    fi.FileTime = file.CreationTime.ToString();
                    resultlist.Add(fi);
                }
            }

            return resultlist;
        }

        /// <summary>
        /// 新增文件按创建时间倒叙
        /// </summary>
        /// <param name="arrFi"></param>
        private void SortAsFileCreationTime(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return y.CreationTime.CompareTo(x.CreationTime); });
        }
        /// <summary>  
        /// 创建文件夹  
        /// </summary>  
        /// <param name="Path"></param>  
        public void FolderCreate(string Path)
        {
            // 判断目标目录是否存在如果不存在则新建 
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }



        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(string expression)
        {
            if (expression != null)
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");

            return false;
        }



    }

    /*************************************************************************
     * 文件名称 ：UtilFunction.cs                          
     * 描述说明 ：共有通用方法
     * 
     * 创建信息 : create by QQ：815657032、709047174  E-mail:Angel_asp@126.com on 2016-06-10
     * 修订信息 : modify by (person) on (date) for (reason)
     * 
     * 版权信息 : Copyright (c) 2009 Angel工作室 www.angelasp.com
     **************************************************************************
     */
}
