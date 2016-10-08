using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Phản hồi - Popup", Code = "CFeedback", IsControl = true, Order = 50)]
    public class CFeedbackController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        [VSW.Core.MVC.PropertyInfo("Cookie Name")]
        public string CookieName;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            ViewBag.Title = Title;
            ViewBag.CookieName = CookieName;
        }
    }
}
