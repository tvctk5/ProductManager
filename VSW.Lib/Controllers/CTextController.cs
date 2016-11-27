using System;

using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Text", Code = "CText", IsControl = true, Order = 99)]
    public class CTextController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Chọn văn bản")]
        public string Text;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            if (!string.IsNullOrEmpty(Text))
                ViewBag.Text = Global.Data.Base64Decode(Text);
            else
                ViewBag.Text = string.Empty;

            // Thông tin cần thiết của module
            ViewBag.ModuleId = ModuleId;
        }
    }
}
