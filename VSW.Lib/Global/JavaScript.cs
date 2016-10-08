using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web;

namespace VSW.Lib.Global
{
   public class JavaScript
    {
        public static void Alert(string Message, Page Page)
        {
            if (Message != null)
            {
                Script("AlertScript", string.Format("alert({0})", EscapeQuote(Message)), Page);
            }
        }

        public static void Close(Page Page)
        {
            Script("CloseScript", "window.close()", Page);
        }

        public static void Back(int Step, Page Page)
        {
            Script("BackScript", "history.go(" + Step + ")", Page);
        }

        public static void Confirm(string Message, string ifTrue, string ifFalse, Page Page)
        {
            if (Message != null)
            {
                string script = string.Format("\r\nif (window.confirm({0}))\r\n{{{1}}}\r\nelse\r\n{{{2}}}\r\n", EscapeQuote(Message), ifTrue, ifFalse);
                Script("ConfirmScript", script, Page);
            }
        }

        private static string EscapeQuote(string S)
        {
            return VSW.Lib.Global.Data.EscapeQuote(S);
        }

        public static void Navigate(string Href, Page Page)
        {
            Script("NavigateScript", "location.href='" + Href + "';", Page);
        }

        public static void RegisterClientScriptBlock(string Key, string Script, Page Page)
        {
            if (!string.IsNullOrEmpty(Script))
            {
                //Script = Script.Replace("''", "|");
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), Key, Script);
            }
        }

        public static void Script(string Key, string Script, Page Page)
        {
            if (!string.IsNullOrEmpty(Script))
            {
                string text = string.Format("<script type=\"text/javascript\">{0}</script>", Script);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Key, text);
            }
        }
    }
}
