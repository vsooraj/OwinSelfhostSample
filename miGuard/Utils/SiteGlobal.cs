using System.Configuration;

namespace miGuard
{
    public static class SiteGlobal
    {
        static public string BaseUrl { get; set; }
        static SiteGlobal()
        {
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        }
    }
}
