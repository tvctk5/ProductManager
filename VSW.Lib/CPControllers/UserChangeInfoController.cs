﻿using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class UserChangeInfoController : CPController
    {
        public void ActionIndex()
        {

        }

        public void ActionSave()
        {
            if (ValidSave())
            {
                CPViewPage.SetMessage("Thay đổi thông tin thành công.");
                CPViewPage.CPRedirectHome();
            }
        }

        public void ActionApply()
        {
            if (ValidSave())
            {
                CPViewPage.Message.Clear();
                CPViewPage.Message.ListMessage.Add("Thay đổi thông tin thành công.");
            }
        }

        public override void ActionCancel()
        {
            CPViewPage.CPRedirectHome();
        }

        private bool ValidSave()
        {
            TryUpdateModel(CPViewPage.CurrentUser);

            CPViewPage.CurrentUser.ID = CPLogin.UserID;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            CPViewPage.CurrentUser.Email = Utils.GetEmailAddress(CPViewPage.CurrentUser.Email);

            if (CPViewPage.CurrentUser.Name == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập họ và Tên.");

            if (CPViewPage.CurrentUser.Email == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập chính xác địa chỉ email.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                //save
                CPUserService.Instance.Save(CPViewPage.CurrentUser);

                return true;
            }

            return false;
        }
    }
}
