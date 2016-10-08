using System;

namespace VSW.Lib.Global
{
    public class Setting : VSW.Core.Web.Setting
    {
        public static string Mod_DomainCookies = VSW.Core.Global.Config.GetValue("Mod.DomainCookies").ToString();
        public static bool Mod_WriteError = VSW.Core.Global.Config.GetValue("Mod.WriteError").ToBool(true);
        public static int Mod_CPTimeout = VSW.Core.Global.Config.GetValue("Mod.CPTimeout").ToInt();

        public static bool Mod_LangUnABC = VSW.Core.Global.Config.GetValue("Mod.LangUnABC").ToBool();
    }
}
