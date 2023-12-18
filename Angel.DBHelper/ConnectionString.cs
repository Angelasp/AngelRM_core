using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Json;

namespace Angel.DBHelper
{

    /// <summary>
    /// 配置类
    /// </summary>
    public class ConnectionString
    {

        private static readonly object objLock = new object();
        private static ConnectionString instance = null;
        IConfigurationRoot config = default(IConfigurationRoot);

        public ConnectionString()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            //读取配置文件
            //configuration.Addctory.GetCurrentDirectory()).AddJsonFile(file =>
            //{
            //    file.Path = "/appsettings.json";
            //    file.Optional = false;
            //    file.ReloadOnChange = true;

            //}).Build();

            //path=文件路径；optional=true文件不存在不报错，false文件不存在报错；reloadOnChange文件修改了是否重新加载
            configBuilder.AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);
            config = configBuilder.Build();

        }

        public static ConnectionString GetInstance()
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new ConnectionString();
                    }
                }
            }

            return instance;
        }

        public static string GetConfig(string section, string key)
        {
            return (string)GetInstance().config.GetSection(section).GetValue(typeof(string), key);
        }
    }
}
