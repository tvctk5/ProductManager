using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Tỉnh thành[C]",
        Description = "Quản lý - Tỉnh thành[C]", 
        Code = "ModProvince", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProvinceController : CPController
    {
        public ModProvinceController()
        {
            //khoi tao Service
            DataService = ModProvinceService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProvinceModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProvinceService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProvinceModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProvinceService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProvinceEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProvinceModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProvinceModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProvinceModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProvinceEntity item = null;

        private bool ValidSave(ModProvinceModel model)
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
                    ModProvinceService.Instance.Save(item);
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

    public class ModProvinceModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

