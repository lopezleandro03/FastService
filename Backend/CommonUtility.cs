using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Backend
{
    public static class CommonUtility
    {
        public static bool IsDevelopmentServer()
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            return (host == "localhost");
        }

        public static string GetConfigVal(string key)
        {
            var configFile = File.ReadAllText(ConfigurationManager.AppSettings["DEVCONFIGPATH"]);
            String[] items = configFile.Split(';');
            foreach (var item in items)
            {
                String[] keyVal = item.Split(':');
                if (keyVal[0] == key)
                    return keyVal[1];
            }
            return string.Empty;
        }
    }
}
