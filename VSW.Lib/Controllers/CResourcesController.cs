using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Tài nguyên", Code = "CResources", IsControl = true, Order = 1)]
    public class CResourcesController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title = string.Empty;

        [VSW.Core.MVC.PropertyInfo("Chọn tài nguyên", "List|Web_Resource#Code&ID#1=1#Code")]
        public int WebResourceId;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            if (WebResourceId <= 0)
                return;

            var objData = WebResourceService.Instance.GetByID(WebResourceId);
            if (objData == null)
                return;

            ViewBag.Data = objData;
            ViewBag.Title = Title;
        }
    }
}
