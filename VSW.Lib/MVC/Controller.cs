using System;

namespace VSW.Lib.MVC
{
    public class Controller : VSW.Core.MVC.Controller
    {
        public new ViewPage ViewPage { get { return base.ViewPageBase as ViewPage; } }
        public new ViewControl ViewControl { get { return base.ViewControl as ViewControl; } }

        [VSW.Core.MVC.PropertyInfo("Module Id")]
        public string ModuleId;

        [VSW.Core.MVC.PropertyInfo("Hiển thị Module", "ConfigKey|Mod.YesNo")]
        public int ShowModule = 1;

        //[VSW.Core.MVC.PropertyInfo("Css cho Module", "Mutiline|TRUE")]
        //public string CssForModule;

        //[VSW.Core.MVC.PropertyInfo("Js cho Module", "Mutiline|TRUE")]
        //public string JsForModule;

        public Controller()
        {
            ViewBag.ShowModule = true;
            if (string.IsNullOrEmpty(ModuleId))
                ModuleId = CreateModuleId();
        }

        public string CreateModuleId()
        {
            string sModuleId = "ModuleId" + DateTime.Now.ToString("yyMMddHHmmssff");
            return sModuleId;
        }

        public string getCssForModule(string sModuleId, string sCssForModule)
        {
            string sCssWriteToPage = string.Empty;
            if (string.IsNullOrEmpty(sCssForModule))
                return string.Empty;

            sCssWriteToPage = "<style type='text/css' title='Css for Module " + sModuleId + "'>\r\n" + sCssForModule + "\r\n</style>";

            return sCssWriteToPage;
        }

        public string getJsForModule(string sModuleId, string sJsForModule)
        {
            string sJsWriteToPage = string.Empty;
            if (string.IsNullOrEmpty(sJsForModule))
                return string.Empty;

            sJsWriteToPage = "<script type='text/javascript' charset='UTF-8'>\r\n" + sJsForModule + "\r\n</script>";

            return sJsWriteToPage;
        }
    }
}
