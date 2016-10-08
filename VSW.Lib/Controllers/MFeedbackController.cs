using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Liên hệ", Code = "MFeedback", Order = 99)]
    public class MFeedbackController : Controller
    {
        public void ActionIndex()
        {
           
        }

        public void ActionAddPOST(ModFeedbackEntity item)
        {
            if (item.Name.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("{RS:Web_FB_Err_Name}.");

            if (item.Title.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("{RS:Web_FB_Err_Title}.");

            if (item.Content.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("{RS:Web_FB_Err_Content}.");

            //hien thi thong bao loi
            if (ViewPage.Message.ListMessage.Count > 0)
            {
                string message = @"{RS:Web_FB_Err_Mess}: \r\n";

                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                    message += @"\r\n + " + ViewPage.Message.ListMessage[i];

                ViewPage.Alert(message);
            }
            else
            {
                item.ID = 0;
                item.IP = VSW.Core.Web.HttpRequest.IP;
                item.Created = DateTime.Now;

                ModFeedbackService.Instance.Save(item);

                // xoa trang
                item = new ModFeedbackEntity();

                ViewPage.Alert("{RS:Web_FB_Succ_Mess}");
            }

            ViewBag.Data = item;
        }
    }
}
