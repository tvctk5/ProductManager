using System;
using System.Collections.Generic;

using VSW.Lib.Models;
using VSW.Core.Interface;
using VSW.Lib.Global;

namespace VSW.Lib.MVC
{
    public class CPViewPage : VSW.Core.MVC.CPViewPage
    {
        private Message _Message = new Message();
        public Message Message
        {
            get { return _Message; }
        }

        public CPUserEntity CurrentUser { get; private set; }
        public Permissions UserPermissions { get; private set; }

        private string ModuleCode = null;

        public CPViewPage()
        {
            string lang_code = Cookies.GetValue("CP.Lang", true);
            //ngon ngu mac dinh neu chua co
            if (lang_code == string.Empty)
                lang_code = "vi-VN";

            CurrentLang = new SysLangEntity();
            CurrentLang.Code = lang_code;

            ResourceService = new IniResourceService(Server.MapPath("~/" + VSW.Core.Web.Setting.Sys_CPDir + "/Views/Lang/" + lang_code + ".ini"));
        }

        protected override void OnPreInit(EventArgs e)
        {
            if (ModuleCode != null && ModuleCode.ToLower() == "login")
            {
                this.MasterPageFile = "Views/Shared/Login.Master";
            }
            else
            {
                if (ModuleCode != null && ModuleCode.ToLower().StartsWith("form"))
                    this.MasterPageFile = "Views/Shared/Form.Master";
                else
                    this.MasterPageFile = "Views/Shared/Main.Master";

                CurrentUser = CPLogin.CurrentUser;

                if (CurrentUser == null)
                {
                    CPRedirect("Login.aspx?ReturnPath=" + Server.UrlEncode(Request.RawUrl));
                    return;
                }

                if (ModuleCode != null)
                {
                    ModuleCode = ModuleCode.ToLower();

                    if (ModuleCode.StartsWith("mod") || ModuleCode.StartsWith("sys"))
                    {
                        if (ModuleCode.StartsWith("sys"))
                            UserPermissions = CurrentUser.GetPermissionsByModule("SysAdministrator");
                        else
                            UserPermissions = CurrentUser.GetPermissionsByModule(ModuleCode);
                    }
                    else
                        UserPermissions = new Permissions(31);
                }
                else
                    UserPermissions = new Permissions(31);

                if (UserPermissions == null || ( UserPermissions != null && !UserPermissions.Any))
                {
                    CurrentModule = AccessDeniedModule();
                }
            }
        }

        public override IModuleInterface DefaultModule()
        {
            return new ModuleInfo()
            { 
                Code = "Home",
                ModuleType = typeof(CPControllers.HomeController) 
            };
        }

        public IModuleInterface AccessDeniedModule()
        {
            return new ModuleInfo()
            {
                Code = "AccessDenied",
                ModuleType = typeof(CPControllers.AccessDeniedController) 
            };
        }

        public override IModuleInterface FindModule(string moduleCode)
        {
            ModuleCode = moduleCode;

            return new ModuleInfo()
            {
                Code = moduleCode,
                ModuleType = Type.GetType("VSW.Lib.CPControllers." + moduleCode + "Controller")
            };
        }

        public void Alert(string content)
        {
            JavaScript.Alert(content, this.Page);
        }

        public void Back(int step)
        {
            JavaScript.Back(step, this.Page);
        }

        public void Navigate(string url)
        {
            JavaScript.Navigate(url, this.Page);
        }

        public void Close()
        {
            JavaScript.Close(this.Page);
        }

        public void Confirm(string message, string if_true, string if_false)
        {
            JavaScript.Confirm(message, if_true, if_false, this.Page);
        }

        public void Script(string key, string script)
        {
            JavaScript.Script(key, script, this.Page);
        }

        public void CPRedirectHome()
        {
            Response.Redirect("~/" + VSW.Core.Web.Setting.Sys_CPDir + "/Default.aspx");
        }

        public void CPRedirect(string path)
        {
            Response.Redirect("~/" + VSW.Core.Web.Setting.Sys_CPDir + "/" + path);
        }

        public void RefreshPage()
        {
            Response.Redirect(Request.RawUrl);
        }

        public void SetLog(string message)
        {
            CPUserLogEntity log = new CPUserLogEntity();
            log.UserID = CPLogin.UserID;
            log.IP = VSW.Core.Web.HttpRequest.IP;
            log.Created = DateTime.Now;
            log.Note = message;
            CPUserLogService.Instance.Save(log);
        }

        public void SetMessage(string message)
        {
            Cookies.SetValue("message", Data.Base64Encode(message));
        }
    }
}
