using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.MVC
{
    public class ViewPage : VSW.Core.MVC.ViewPage
    {
        public ViewPage()
        {
            LangService = SysLangService.Instance;
            ModuleService = SysModuleService.Instance;
            SiteService = SysSiteService.Instance;
            TemplateService = SysTemplateService.Instance;
            PageService = SysPageService.Instance;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ResourceService = new IniSqlResourceService(CurrentLang);

            // khong thi bo di nhe
            Lib.Global.Utils.UpdateOnline();
        }

        protected override VSW.Core.Interface.IPageInterface PageNotFound()
        {
            Error404();

            return null;
        }

        public void Error404()
        {
            Response.Clear();
            Response.StatusCode = 404;
            Response.Write(Global.File.ReadText(Server.MapPath("~/Views/HttpError/404.html")));
            Response.End();
        }

        public new SysSiteEntity CurrentSite
        {
            get
            {
                return base.CurrentSite as SysSiteEntity;
            }
        }
        public new SysTemplateEntity CurrentTemplate
        {
            get
            {
                return base.CurrentTemplate as SysTemplateEntity;
            }
        }
        public new SysPageEntity CurrentPage
        {
            get
            {
                var PageCurrent = base.CurrentPage as SysPageEntity;
                string strCssFile = string.Empty;
                string strJsFile = string.Empty;

                if (PageCurrent.ChangeCss)
                    strCssFile = PageCurrent.CssFile;
                else
                    strCssFile = CurrentTemplate.CssFile + ";" + PageCurrent.CssFile;

                if (PageCurrent.ChangeCss)
                    strJsFile = PageCurrent.JsFile;
                else
                    strJsFile = CurrentTemplate.JsFile + ";" + PageCurrent.JsFile;

                strCssFile = strCssFile != null ? strCssFile.Replace("~", "").Trim(';') : "";
                strJsFile = strJsFile != null ? strJsFile.Replace("~", "").Trim(';') : "";

                VSW.Lib.Global.Session.SetValue("CssPage", strCssFile);
                VSW.Lib.Global.Session.SetValue("JsPage", strJsFile);
                //Cookies.SetValue("JsPage", strJsFile, 1);
                //Cookies.SetValue("CssPage", strCssFile, 1);
                return PageCurrent;
            }
        }

        // Get Info new
        public SysPageEntity GetCurrentPage()
        {
            var PageCurrent = base.CurrentPage as SysPageEntity;
            SysPageEntity objGetPage = SysPageService.Instance.GetByID(PageCurrent.ID);
            PageCurrent = objGetPage;
            return PageCurrent;
        }

        public new SysLangEntity CurrentLang { get { return base.CurrentLang as SysLangEntity; } }

        private string _CurrentURL = null;
        public string CurrentURL
        {
            get
            {
                if (_CurrentURL == null)
                    _CurrentURL = GetURL(SysPageService.Instance.GetMapCode_Cache(CurrentPage));

                return _CurrentURL;
            }
        }

        private Message _Message = new Message();
        public Message Message
        {
            get { return _Message; }
        }

        public string GetURLCurrentPage(string Code)
        {
            return GetURL(SysPageService.Instance.GetMapCode_Cache(CurrentPage), Code);
        }

        public string GetURL(int MenuID, string Code)
        {
            string Key_Cache = "Lib.App.ViewPage.GetURL." + MenuID;

            string _MapCode = null;
            object obj = VSW.Core.Web.Cache.GetValue(Key_Cache);
            if (obj != null)
            {
                _MapCode = obj.ToString();
            }
            else
            {
                SysPageEntity _NewPage = null;

                while (MenuID > 0)
                {
                    _NewPage = SysPageService.Instance.CreateQuery()
                       .Where(o => o.MenuID == MenuID && o.Activity == true)
                       .ToSingle_Cache();

                    if (_NewPage != null)
                        break;

                    WebMenuEntity _Menu = WebMenuService.Instance.GetByID_Cache(MenuID);

                    if (_Menu == null || (_Menu != null && _Menu.ParentID == 0))
                        break;

                    MenuID = _Menu.ParentID;
                }

                if (_NewPage != null)
                    _MapCode = SysPageService.Instance.GetMapCode_Cache(_NewPage);
                else
                    _MapCode = SysPageService.Instance.GetMapCode_Cache(CurrentPage);

                VSW.Core.Web.Cache.SetValue(Key_Cache, _MapCode);
            }

            return GetURL(_MapCode, Code);
        }

        /// <summary>
        /// Add by CanTV
        /// </summary>
        /// <param name="iPage"></param>
        /// <returns></returns>
        public string GetPageURL(int iPage)
        {
            var objPage = SysPageService.Instance.GetByID(iPage);

            return GetPageURL(objPage);
        }

        /// <summary>
        /// Add by CanTV
        /// </summary>
        /// <param name="iPage"></param>
        /// <returns></returns>
        public string GetPageURL(int iPage, List<SysPageEntity> lstSysPageEntity)
        {
            SysPageEntity objPage = null;
            if (lstSysPageEntity == null)
                objPage = SysPageService.Instance.GetByID(iPage);
            else
                objPage = lstSysPageEntity.Where(p => p.ID == iPage).FirstOrDefault();

            return GetPageURL(objPage);
        }

        public string GetPageURL(SysPageEntity Page)
        {
            if (Page.Items.GetValue("Type").ToString() == string.Empty)
            {
                string sUrl = GetURL(SysPageService.Instance.GetMapCode_Cache(Page));

                if (Page.LangID == 1) // Khi là ngôn ngữ VN
                    // Kiểm tra xem có ngôn ngữ hiện hành chưa. Nếu chưa có, mặc định là tiếng việt
                    sUrl = sUrl.Contains("vn/") ? sUrl : "vn/" + sUrl;

                // Nếu không có đuôi aspx thì thêm vào
                sUrl = sUrl.ToLower().Contains(".aspx") ? sUrl : (sUrl + ".aspx");
                return sUrl;
            }
            else if (Page.Items.GetValue("Type").ToString() == "HTTP")
            {
                string target = Page.Items.GetValue("Target").ToString();
                string url = Page.Items.GetValue("URL").ToString();

                if (url == string.Empty)
                    url = Page.Code;

                return url.Replace("{URLBase}/", URLBase).Replace("{PageExt}", PageExt) + (target == string.Empty ? string.Empty : "\" target=\"" + target);
            }

            return "#";
        }

        public bool IsPageActived(SysPageEntity page_to_check)
        {
            if (CurrentPage.ID == page_to_check.ID)
                return true;

            SysPageEntity _page = (SysPageEntity)CurrentPage.Clone();
            while (true)
            {
                _page = SysPageService.Instance.GetByID_Cache(_page.ParentID);

                if (_page == null || _page.ParentID == 0)
                    return false;

                if (_page.ID == page_to_check.ID)
                    return true;
            }
        }

        public bool IsPageActived(SysPageEntity page, int index)
        {
            return CurrentPage.ID == page.ID || CurrentVQS.Equals(index, page.Code);
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

        public void RefreshPage()
        {
            Response.Redirect(Request.RawUrl);
        }

    }
}
