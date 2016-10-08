using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Product_ properties list_ values", Code = "CProduct_PropertiesList_Values", IsControl = false, Order = 50)]
    public class CProduct_PropertiesList_ValuesController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            ViewBag.Data = ModProduct_PropertiesList_ValuesService.Instance.CreateQuery()
                            .OrderByDesc(o => o.ID)
                            .Take(PageSize)
                            .ToList_Cache();

            ViewBag.Title = Title;
        }
    }
}
