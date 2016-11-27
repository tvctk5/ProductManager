using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Category", Code = "CCategory", IsControl = true, Order = 2)]
    public class CCategoryController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Trang")]
        public int PageID;

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title = string.Empty;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }
            // Module Id
            if (string.IsNullOrEmpty(ModuleId))
                ModuleId = CreateModuleId();

            // Thông tin cần thiết của module
            ViewBag.ModuleId = ModuleId;
            ViewBag.Title = Title;
            ViewBag.ModuleId = ModuleId;

            SysPageEntity _Page = SysPageService.Instance.GetByID_Cache(PageID);

            if (_Page != null)
            {
                ViewBag.Data = SysPageService.Instance.GetByParent_Cache(_Page.ID);
                ViewBag.Page = _Page;
                ViewBag.AllCategory = WebMenuService.Instance.CreateQuery().ToList_Cache();
            }
        }
    }
}
