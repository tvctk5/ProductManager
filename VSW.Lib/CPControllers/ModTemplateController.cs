using System;
using System.Data;
using System.IO;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global; 
 

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "[Giao diện] - Kiểu giao diện hiển thị", 
        Description = "Quản lý - Kiểu Giao diện",
        Code = "ModTemplate", 
        Access = 15, 
        Order = 9999,
        ShowInMenu = false,
        CssClass = "icon-16-component",
        MenuGroupId = 4)]
    public class ModTemplateController : CPController
    {
        public void ActionIndex()
        {
           // Template Active
            var StyleActivate = ModParametersService.Instance.GetByID((int)EnumValue.Parameter.TEMPLATE_ACTIVATE).Value;
            
            DirectoryInfo objDirectoryInfo = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Content/html/styles"));
            var ListTemplateStyle = objDirectoryInfo.GetDirectories("style*", SearchOption.TopDirectoryOnly);

            ViewBag.StyleActivate = StyleActivate;
            ViewBag.ListTemplateStyle = ListTemplateStyle;
        }

        public void ActionActivateStyle(ModParameterModel model)
        {
            if (model == null )
                return;

            string NameStyle = model.NameStyle;

            // Cập nhật
            var objStyleActivate = ModParametersService.Instance.GetByID((int)EnumValue.Parameter.TEMPLATE_ACTIVATE);
            if (objStyleActivate == null)
                return;

            objStyleActivate.Value = NameStyle;

            // Lưu lại
            ModParametersService.Instance.Save(objStyleActivate);

            CPViewPage.RefreshPage();
        }
    }

    public class ModParameterModel : DefaultModel
    {
        public string NameStyle { get; set; }
    }
}
