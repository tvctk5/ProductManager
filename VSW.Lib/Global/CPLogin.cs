using System;

using VSW.Lib.Models;

namespace VSW.Lib.Global
{
    public class CPLogin
    {
        public static void Logout()
        {
            Cookies.Remove("CP.UserID");
        }

        public static bool CheckLogin(string login_name, string password)
        {
             CPUserEntity _User = CPUserService.Instance.GetLogin(login_name, password);

             if (_User != null)
             {
                 SetLogin(_User.ID);

                 return true;
             }

             return false;
        }

        private static void SetLogin(int user_id)
        {
            Cookies.SetValue("CP.UserID", user_id.ToString(), Setting.Mod_CPTimeout, true);
        }

        public static bool IsLogin()
        {
            return (UserID > 0);
            //return (CurrentUser != null);
        }

        public static int UserID
        {
            get
            {
                int _UserID = VSW.Core.Global.Convert.ToInt(Cookies.GetValue("CP.UserID", true));

                if (_UserID > 0) 
                    SetLogin(_UserID);

                return _UserID;
            }
        }

        public static CPUserEntity CurrentUser
        {
            get
            {
                if (UserID > 0)
                    return CPUserService.Instance.GetLogin(UserID);

                return null;
            }
        }
    }
}
