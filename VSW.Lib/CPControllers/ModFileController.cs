using System;
using System.Data;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "File tải lên", 
        Description = "Quản lý - File tải lên", 
        Code = "ModFile", 
        Access = 15, 
        Order = 9999,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 4)]
    public class ModFileController : CPController
    {
        public void ActionIndex()
        {
           
        }
    }
}
