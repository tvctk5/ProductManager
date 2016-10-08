using System;

using HL.Lib.Models;

namespace HL.Lib.Global
{
    public class WebLogin
    {
        public static ModWebUserEntity CurrentUser
        {
            get
            {
                HL.Core.MVC.ViewPage _WebPage = HL.Core.Web.Application.CurrentViewPage;

                if (_WebPage != null)
                {
                    object obj = _WebPage.PageViewState["Web.CurrentUser"];
                    if (obj != null)
                        return (ModWebUserEntity)obj;
                }

                ModWebUserEntity _WebUser = null;

                if (WebUserID > 0)
                    _WebUser = ModWebUserService.Instance.GetForLogin(WebUserID);

                if (_WebUser == null)
                    Logout();

                if (_WebPage != null)
                    _WebPage.PageViewState["Web.CurrentUser"] = _WebUser;

                return _WebUser;
            }
        }

        public static string Name { get { return CurrentUser.Name; } }

        public static int WebUserID
        {
            get
            {
                return HL.Core.Global.Convert.ToInt(Cookies.GetValue("Web.Login", true));
            }
        }

        public static bool IsLogin()
        {
            return CurrentUser != null;
        }

        public static void Logout()
        {
            Cookies.Remove("Web.Login");
        }

        public static void SetLogin(int webUserID, bool savepass)
        {
            Cookies.SetValue("Web.Login", webUserID.ToString(), savepass ? 0 : 120, true);
        }
    }
}
