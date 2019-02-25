using System.Web;
using APP.Utility.Extension;

namespace APP.Utility
{
    public static class Globals
    {
        /// <summary>
        /// 获得当前客户端的IP
        /// </summary>
        /// <returns>当前客户端的IP</returns>
        public static string ClientIP
        {
            get {
                string result = HttpContext.Current?.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrWhiteSpace(result))
                    result = HttpContext.Current?.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrWhiteSpace(result))
                    result = HttpContext.Current?.Request.UserHostAddress;

                if (string.IsNullOrWhiteSpace(result) || !ObjectExtensions.IsIP(result))
                    return "127.0.0.1";

                return result;
            }
        }

        /// <summary>
        /// 获取服务端的IP
        /// </summary>
        public static string ServerIP
        {
            get {
                string result = string.Empty;

                result = HttpContext.Current?.Request.ServerVariables["LOCAL_ADDR"];

                if (!result.IsIP())
                    result = "127.0.0.1";

                return result;
            }
        }
    }
}
