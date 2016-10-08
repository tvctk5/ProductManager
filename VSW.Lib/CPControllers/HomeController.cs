using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class HomeController : CPController
    {
        public void ActionIndex()
        {

        }

        public void ActionLogout()
        {
            CPViewPage.SetLog("Thoát khỏi hệ thống.");

            CPLogin.Logout();

            CPViewPage.CPRedirect("Login.aspx");
        }
    }
}
