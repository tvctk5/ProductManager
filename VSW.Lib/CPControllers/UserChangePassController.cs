using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class UserChangePassController : CPController
    {
        public void ActionIndex()
        {

        }

        public void ActionSave(UserChangePassModel model)
        {
            if (ValidSave(model))
            {
                CPViewPage.SetMessage("Mật khẩu đã thay đổi thành công.");
                CPViewPage.CPRedirectHome();
            }
        }

        public void ActionApply(UserChangePassModel model)
        {
            if (ValidSave(model))
            {
                CPViewPage.Message.Clear();
                CPViewPage.Message.ListMessage.Add("Mật khẩu đã thay đổi thành công.");
            }
        }

        public override void ActionCancel()
        {
            CPViewPage.CPRedirectHome();
        }

        private bool ValidSave(UserChangePassModel model)
        {
            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            if (model.CurrentPassword == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập mật khẩu cũ.");
            else if (CPViewPage.CurrentUser.Password != VSW.Lib.Global.Security.MD5(model.CurrentPassword))
                CPViewPage.Message.ListMessage.Add("Mật khẩu cũ không đúng.");

            if (model.NewPassword == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập mật khẩu mới.");
            else if (model.NewPassword != model.ConfirmPassword)
                CPViewPage.Message.ListMessage.Add("Xác nhận lại mật khẩu không đúng.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                CPViewPage.CurrentUser.Password = VSW.Lib.Global.Security.MD5(model.NewPassword);

                //save
                CPUserService.Instance.Save(CPViewPage.CurrentUser, o => o.Password);

                return true;
            }

            return false;
        }
    }

    public class UserChangePassModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
