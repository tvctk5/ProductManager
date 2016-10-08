using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Tin tức", Code = "CNews", IsControl = true, Order = 4)]
    public class CNewsController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Chuyên mục", "Type|News")]
        public int MenuID;

        [VSW.Core.MVC.PropertyInfo("Loại tin")]
        public string Type = string.Empty;

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 5;

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

            ViewBag.Data = ModNewsService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByDesc(o => o.Order)
                                    .Take(PageSize)
                                    .ToList_Cache();
            ViewBag.Title = Title;
        }
    }
}
