using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Tham số hệ thống",
        Description = "Quản lý - Tham số hệ thống", 
        Code = "ModParameters", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = false, 
        CssClass = "icon-16-component")]
    public class ModParametersController : CPController
    {
        public ModParametersController()
        {
            //khoi tao Service
            DataService = ModParametersService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModParametersModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModParametersService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModParametersModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModParametersService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModParametersEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModParametersModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModParametersModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModParametersModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModParametersEntity item = null;

        private bool ValidSave(ModParametersModel model)
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
                 //neu khong nhap code -> tu sinh
                 if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModParametersService.Instance.Save(item);
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

    public class ModParametersModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

