using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class AjaxController : CPController
    {
        public void ActionIndex()
        {

        }

        public void ActionSiteGetPage(SiteGetPageModel model)
        {
            List<Global.ListItem.Item> list_page = Global.ListItem.List.GetList(SysPageService.Instance, model.LangID);

            for (int i = 0; list_page != null && i < list_page.Count; i++)
            {
                ajaxModel.Params += "<option " + (model.PageID.ToString() == list_page[i].Value ? "selected" : "") + " value='" + list_page[i].Value + "'>&nbsp; " + list_page[i].Name + "</option>";
            }

            EndResponse();
        }

        public void ActionTemplateGetCustom(int templateID)
        {
            ajaxModel.Html = SysTemplateService.Instance.GetByID(templateID).Custom;

            EndResponse();
        }

        public void ActionPageGetCustom(int pageID)
        {
            ajaxModel.Html = SysPageService.Instance.GetByID(pageID).Custom;

            EndResponse();
        }

        public void ActionPageGetControl(PageGetControlModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.ModuleCode))
                {
                    SysPageEntity _CurrentPage = null;
                    var _CurrentModule = SysModuleService.Instance.VSW_Core_GetByCode(model.ModuleCode);

                    if (model.PageID > 0)
                        _CurrentPage = SysPageService.Instance.GetByID(model.PageID);

                    if (_CurrentModule != null)
                    {
                        VSW.Core.Global.Class _CurrentObject = new VSW.Core.Global.Class(_CurrentModule.ModuleType);

                        string filePath = (System.IO.File.Exists(CPViewPage.Server.MapPath("~/Views/Design/" + _CurrentModule.Code + ".ascx")) ? 
                            "~/Views/Design/" + _CurrentModule.Code + ".ascx" : "~/" + VSW.Core.Web.Setting.Sys_CPDir + "/Design/EditModule.ascx");

                        string sHtml = Utils.GetHtmlControl(CPViewPage, filePath,
                                            "CurrentObject", _CurrentObject,
                                            "CurrentPage", _CurrentPage,
                                            "CurrentModule", _CurrentModule,
                                            "LangID", model.LangID);

                        if (_CurrentObject.ExistsField("MenuID"))
                        {
                            System.Reflection.FieldInfo _FieldInfo = _CurrentObject.GetFieldInfo("MenuID");
                            object[] attributes = _FieldInfo.GetCustomAttributes(typeof(VSW.Core.MVC.PropertyInfo), true);
                            if (attributes != null && attributes.GetLength(0) > 0)
                            {
                                VSW.Core.MVC.PropertyInfo propertyInfo = (VSW.Core.MVC.PropertyInfo)attributes[0];
                                List<VSW.Lib.Global.ListItem.Item> listMenu = VSW.Lib.Global.ListItem.List.GetListByText(propertyInfo.Value.ToString());

                                string menuType = VSW.Lib.Global.ListItem.List.FindByName(listMenu, "Type").Value;

                                listMenu = VSW.Lib.Global.ListItem.List.GetList(WebMenuService.Instance, model.LangID, menuType);
                                listMenu.Insert(0, new VSW.Lib.Global.ListItem.Item(string.Empty, string.Empty));

                                for (int j = 1; listMenu != null && j < listMenu.Count; j++)
                                {
                                    ajaxModel.Params += "<option " + (_CurrentPage != null && _CurrentPage.MenuID.ToString() == listMenu[j].Value ? "selected" : "") + " value='" + listMenu[j].Value + "'>&nbsp; " + listMenu[j].Name + "</option>";
                                }
                            }
                        }

                        ajaxModel.Html = sHtml.Replace("{CPPath}", CPViewPage.CPPath);
                    }
                }
            }
            catch (Exception ex)
            {
                VSW.Lib.Global.Error.Write(ex);
            }

            EndResponse();
        }

        #region private func

        private void EndResponse()
        {
            string s = @"<Xml>
  <Item>
    <Html><![CDATA[" + ajaxModel.Html + @"]]></Html>
    <Params><![CDATA[" + ajaxModel.Params + @"]]></Params>
    <JS><![CDATA[" + ajaxModel.JS + @"]]></JS>
  </Item>
</Xml>";
            CPViewPage.Response.ContentType = "text/xml";
            CPViewPage.Response.Write(s);
            CPViewPage.Response.End();
        }

        AjaxModel ajaxModel = new AjaxModel() { Params = string.Empty, Html = string.Empty, JS = string.Empty };

        #endregion
    }

    public class PageGetControlModel
    {
        public int LangID { get; set; }
        public int PageID { get; set; }
        public string ModuleCode { get; set; }
    }

    public class SiteGetPageModel
    {
        public int LangID { get; set; }
        public int PageID { get; set; }
    }

    public class AjaxModel
    {
        public string Html { get; set; }
        public string Params { get; set; }
        public string JS { get; set; }
    }
}
