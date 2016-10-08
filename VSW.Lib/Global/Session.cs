using System;
using System.Web;

namespace VSW.Lib.Global
{
    public class Session
    {
        public static bool Exists(string Key)
        {
            if (HttpContext.Current.Session[Key] != null) 
                return true;
            else
                return false;
        }

        public static void SetValue(string Key, object Value)
        {
            HttpContext.Current.Session[Key] = Value;
        }

        public static object GetValue(string Key)
        {
            if (Exists(Key)) 
                return HttpContext.Current.Session[Key];
            else 
                return string.Empty;
        }

        public static void Remove(string Key)
        {
            if (Exists(Key)) 
                HttpContext.Current.Session[Key] = null;
        }
    }
}
