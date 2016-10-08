using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Danh sách đăng ký nhận tin",
        Description = "Quản lý - Danh sách đăng ký nhận tin", 
        Code = "ModListMailNewsLetter", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 4)]
    public class ModListMailNewsLetterController : CPController
    {
        public ModListMailNewsLetterController()
        {
            //khoi tao Service
            DataService = ModListMailNewsLetterService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModListMailNewsLetterModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModListMailNewsLetterService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModListMailNewsLetterModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModListMailNewsLetterService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModListMailNewsLetterEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModListMailNewsLetterModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModListMailNewsLetterModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModListMailNewsLetterModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionSexGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@Sex", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModListMailNewsLetterEntity item = null;

        private bool ValidSave(ModListMailNewsLetterModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tên.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {

                try
                {
                    //save
                    ModListMailNewsLetterService.Instance.Save(item);
                }
                catch (Exception ex)
                {
                    Global.Error.Write(ex);
                    CPViewPage.Message.ListMessage.Add(ex.Message);
                    return false;
                }

                return true;
            }

            return false;
        }

        #endregion
    }

    public class ModListMailNewsLetterModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

