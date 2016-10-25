using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Banner/ Slideshow/ Quảng cáo/ Liên kết", Code = "CAdv", IsControl = true, Order = 2)]
    public class CAdvController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Chuyên mục", "Type|Adv")]
        public int MenuID;

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        [VSW.Core.MVC.PropertyInfo("Slide Width")]
        public int Width;

        [VSW.Core.MVC.PropertyInfo("Slide Height")]
        public int Height;

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

            ViewBag.Data = ModAdvService.Instance.CreateQuery()
                                        .Where(o => o.Activity == true && o.MenuID == MenuID)
                                        .OrderByAsc(o => o.Order)
                                        .ToList_Cache();

            ViewBag.Title = Title;
            ViewBag.Width = Width;
            ViewBag.Height = Height;
        }
    }
}
