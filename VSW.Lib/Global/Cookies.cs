using System;
using System.Web;

namespace VSW.Lib.Global
{
    public class Cookies
    {
        private static string SiteID
        {
            get { return VSW.Core.Web.Setting.Sys_SiteID + "_"; }
        }

        public static bool Exists(string Key)
        {
            Key = SiteID + Key;

            if (HttpContext.Current.Request.Cookies[Key] != null)
                return true;
            else 
                return false;
        }

        public static void SetValue(string Key, string Value, int Minutes, bool Secure)
        {
            Key = SiteID + Key;

            string IP = HttpContext.Current.Request.UserHostAddress;

            if (IP != "127.0.0.1" && Setting.Mod_DomainCookies != string.Empty)
                HttpContext.Current.Response.Cookies[Key].Domain = Setting.Mod_DomainCookies;

            if (Secure)
                HttpContext.Current.Response.Cookies[Key].Value = VSW.Core.Global.CryptoString.Encrypt(IP + "_VSW_" + Key + Value);
            else 
                HttpContext.Current.Response.Cookies[Key].Value = Value;

            if (Minutes > 0)
                HttpContext.Current.Response.Cookies[Key].Expires = System.DateTime.Now.AddMinutes(Minutes);
        }

        public static void SetValue(string Key, string Value, int Minutes)
        {
            SetValue(Key, Value, Minutes, false);
        }

        public static void SetValue(string Key, string Value, bool Secure)
        {
            SetValue(Key, Value, 0, Secure);
        }

        public static void SetValue(string Key, string Value)
        {
            SetValue(Key, Value, 0, false);
        }

        public static string GetValue(string Key, bool Secure)
        {  
            if (Exists(Key))
            {
                string _Key = SiteID + Key;

                if (Secure)
                {
                    string IP = HttpContext.Current.Request.UserHostAddress;

                    string s = VSW.Core.Global.CryptoString.Decrypt(HttpContext.Current.Request.Cookies[_Key].Value).Replace(IP + "_VSW_" + _Key, string.Empty);

                    if (s.IndexOf("_VSW_" + _Key) > -1)
                    {
                        Remove(Key);

                        return string.Empty;
                    }

                    return s;
                }
                else
                    return HttpContext.Current.Request.Cookies[_Key].Value;
            }
            else 
                return string.Empty;
        }

        public static string GetValue(string Key)
        {
            return GetValue(Key, false);
        }

        public static void Remove(string Key)
        {
            if (Exists(Key))
            {
                Key = SiteID + Key;

                //ip
                string IP = HttpContext.Current.Request.UserHostAddress;

                //local
                if (IP != "127.0.0.1" && Setting.Mod_DomainCookies != string.Empty)
                    HttpContext.Current.Response.Cookies[Key].Domain = Setting.Mod_DomainCookies;

                //remove
                HttpContext.Current.Response.Cookies[Key].Value = string.Empty;
                HttpContext.Current.Response.Cookies[Key].Expires = System.DateTime.Now.AddDays(-1);
            }
        }
    }
}
