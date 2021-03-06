using System;
using System.Web;

namespace VSW.Lib.Global
{
    public static class Error 
    {
        private static int Year
        {
            get { return DateTime.Now.Year; }
        }

        private static int Month
        {
            get { return DateTime.Now.Month; }
        }

        private static string PathCurrent 
        {
            get
            {
                string _Path = VSW.Core.Global.Application.BaseDirectory + "/Logs";

                // bo qua loi
                try
                {
                    VSW.Lib.Global.Directory.Create(_Path + "/" + Year);
                }
                catch { }

                return _Path;
            }
        }

        public static void Write(string func, string var, Exception exception)
        {
            Write(func + " : " + var + "\r\n", exception);
        }

        public static void Write(string func, Exception exception)
        {
            Write(func + " : \r\n", exception);
        }

        public static void Write(Exception exception)
        {
            string _Message = "Message : " + exception.Message + "\r\n";

            // neu la loi SQL
            if (exception is VSW.Core.Data.SQLException)
            {
                VSW.Core.Data.SQLException _SQLException = (VSW.Core.Data.SQLException)exception;
                _Message += "SQLText : " + _SQLException.CommandText + "\r\n";
                _Message += "SQLParameters : " + _SQLException.Parameters + "\r\n";
            }

            _Message += "Source : " + exception.Source 
                + "\r\nTargetSite : " + exception.TargetSite 
                + "\r\nStackTrace : " + exception.StackTrace;

            Write(_Message);
        }

        public static void Write(string message)
        {
            string _s = "Time : " + string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now) + "\r\n";
            _s += "IP : " + HttpContext.Current.Request.UserHostAddress + "\r\n";
            _s += "URL : " + HttpContext.Current.Request.Url + "\r\n";
            _s += message + "\r\n\r\n";

            // bo qua loi
            try
            {
                VSW.Lib.Global.File.WriteText(PathCurrent + "/" + Year + "/" + Month + ".err", _s);
            }
            catch { }
        }
    }
}
