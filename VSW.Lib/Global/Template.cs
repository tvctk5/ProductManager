using System;
using System.Web;

namespace VSW.Lib.Global
{
    public class Template
    {
        public static string GetHTML(string Template, params object[] Params)
        {
            string sHtml = VSW.Lib.Global.File.ReadText(HttpContext.Current.Server.MapPath("~/Views/Content/" + Template + ".html"));

            for (int i = 0; i < Params.Length - 1; i = i + 2)
            {
                sHtml = sHtml.Replace("{" + Params[i] + "}", Params[i + 1].ToString());
            }

            //sHtml = sHtml.Replace("{EMPTY}", string.Empty);

            return sHtml;
        }
    }
}