using System;
using System.Globalization;
using System.Web;

using VSW.Lib.Models;
using VSW.Core.Models;

namespace VSW.Lib.Global
{
    public static class WebResource 
    {
        private static string CurrentCode
        {
            get { return CultureInfo.CurrentCulture.Name; }
        }

        public static string GetValue(string code)
        {
            return GetValue(code, string.Empty);
        }

        public static string GetValue(string code, string defalt)
        {
            SysLangEntity _Lang = SysLangService.Instance.CreateQuery()
                .Where(o => o.Code == CurrentCode)
                .ToSingle_Cache();

            if (_Lang == null)
                return defalt;

            WebResourceEntity _Resource = WebResourceService.Instance.GetByCode_Cache(code, _Lang.ID);

            if (_Resource != null) 
                return _Resource.Value;

            IniResourceService _IniResourceService = new IniResourceService(HttpContext.Current.Server.MapPath("~/Views/Lang/" + _Lang.Code + ".ini"));
            return _IniResourceService.VSW_Core_GetByCode(code, defalt);
        }
    }
}