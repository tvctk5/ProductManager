using System;
using System.Linq;
using VSW.Lib.MVC;
using VSW.Lib.Models;
using System.Collections.Generic;
using VSW.Lib.Global;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : [Sản phẩm] So sánh sản phẩm", Code = "CProduct_Info_Compare", IsControl = true, Order = 50)]
    public class CProduct_Info_ComapreController : Controller
    {
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

            ViewBag.Title = Title;
        }
    }
}
