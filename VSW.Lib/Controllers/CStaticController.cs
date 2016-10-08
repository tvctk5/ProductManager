using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Static", Code = "CStatic", IsControl = true, Order = 3)]
    public class CStaticController : Controller
    {

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            //item = new ModProduct_InputEntity();
            //ViewBag.Data = item;
            ViewBag.Model = new StaticController_InputModel(); 
        }

        public void ActionAddNewsLetter1(string Name, string Email)
        {
            //TryUpdateModel()
            if (string.IsNullOrEmpty(Email))
                ViewPage.Alert("Ban can nhap email");
            else
            {
                //XU LY dua vao DB

                ViewPage.Alert("Cam on ban " + Name + " da dang ky....");
            }


        }

        public void ActionAddNewsLetter(StaticController_InputModel model)
        {
            //TryUpdateModel(item);
            ////XU LY dua vao DB

            //ViewPage.Alert("Cam on ban " + item.Code + "(" + model.Email + ")" + " da dang ky....");
        }
        
    }

    public class StaticController_InputModel : DefaultModel
    {
        public string Email { get; set; }
    }

}
