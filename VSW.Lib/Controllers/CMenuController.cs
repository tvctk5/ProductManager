using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Menu", Code = "CMenu", IsControl = true, Order = 1)]
    public class CMenuController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Default[Title-true],Left[Title-true],MenuLeft[Title-true]")]
        public string LayoutDefine;

        [VSW.Core.MVC.PropertyInfo("Trang")]
        public int PageID;

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title = string.Empty;

        [VSW.Core.MVC.PropertyInfo("Loại Menu", "List|Mod_Menu_Type#Name&ID#1=1#Name")]
        public int MenuType;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }
            
            // Thông tin cần thiết của module
            ViewBag.ModuleId = ModuleId;
            ViewBag.CssForModule = getCssForModule(ModuleId, CssForModule);
            ViewBag.JsForModule = getJsForModule(ModuleId, JsForModule);


            SysPageEntity _Page = SysPageService.Instance.GetByID_Cache(PageID);

            if (_Page != null && MenuType <= 0)
            {
                ViewBag.Data = SysPageService.Instance.GetByParent_Cache(_Page.ID);
                ViewBag.Page = _Page;
            }
            else
            {
                List<SysPageEntity> lstSysPageEntity = SysPageService.Instance.CreateQuery().ToList();

                ViewBag.Data = ModMenu_DynamicService.Instance.GetListByMenuType(MenuType);
                ViewBag.ListAllSysPage = lstSysPageEntity;
                ViewBag.Page = _Page;
            }

            ViewBag.Title = Title;
        }
    }
}
