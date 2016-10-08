using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class FormTextController : CPController
    {
        public void ActionIndex(FormTextModel model)
        {
            if (!CPViewPage.IsPostBack && !string.IsNullOrEmpty(model.TextID))
                model.Value = Global.Data.Base64Decode(Global.Session.GetValue(model.TextID).ToString().Replace(" ", "+"));

            ViewBag.Model = model;
        }

        public void ActionApply(FormTextModel model)
        {
            if (string.IsNullOrEmpty(model.Value))
            {
                CPViewPage.Alert("Nhập nội dung");
                return;
            }

            string s = Global.Data.Base64Encode(model.Value);

            VSW.Lib.Global.Session.SetValue(model.TextID, s);

            CPViewPage.Script("Close", "Close('" + s + "')");
        }

        public override void ActionCancel()
        {
            CPViewPage.Script("Cancel", "Cancel()");
        }
    }

    public class FormTextModel : DefaultModel
    {
        public string TextID { get; set; }
        public string Value { get; set; }
    }
}
