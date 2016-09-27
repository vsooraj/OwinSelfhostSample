using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace miGuardClient.Utilities
{
    public static class Appsettings
    {
        public static string AppSetting(this string Key)
        {
            string ret = string.Empty;
            if (System.Configuration.ConfigurationManager.AppSettings[Key] != null)
                ret = ConfigurationManager.AppSettings[Key];
            return ret;
        }
    }

    //public  class Appsettings
    //{
    //    public string HostUrl {
    //        get
    //        {
    //            return ConfigurationManager.AppSettings["hostUrl"].ToString();
    //        }
    //    }
    //    //public  string AppSetting(string str)
    //    //{         
    //    //    return ConfigurationManager.AppSettings["hostUrl"].ToString();
    //    //}

    //}

    //interface IAppsettings
    //{
    //    string AppSetting(string str);       

    //}
}