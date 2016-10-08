using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Test Action", Code = "MTestAction", Order = 99)]
    public class MActionController : Controller
    {
        public void ActionIndex()
        {

        }

        public void ActionA_CanTV()
        {
            ViewPage.Alert("Gọi Action A");
        }

        public void ActionB_CanTV(string Name, string Address)
        {
            ViewPage.Alert("Name : " + Name  + " Address: " + Address);
        }
    }
}
