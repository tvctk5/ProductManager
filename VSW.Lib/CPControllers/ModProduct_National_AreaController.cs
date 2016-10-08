using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Product_ national_ area", 
        Description = "Quản lý - Product_ national_ area", 
        Code = "ModProduct_National_Area", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = false, 
        CssClass = "icon-16-component")]
    public class ModProduct_National_AreaController : CPController
    {
        public ModProduct_National_AreaController()
        {
            //khoi tao Service
            DataService = ModProduct_National_AreaService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_National_AreaModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_National_AreaService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_National_AreaModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_National_AreaService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_National_AreaEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;

                item.CreateDate = DateTime.Now;
            }

            if (model.ProductNationalId>0)
            {
                ModProduct_NationalEntity objNationalEntity = ModProduct_NationalService.Instance.GetByID(model.ProductNationalId);
                if (objNationalEntity != null)
                    model.ProductNationalName = objNationalEntity.Name;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_National_AreaModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProduct_National_AreaModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_National_AreaModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_National_AreaEntity item = null;

        private bool ValidSave(ModProduct_National_AreaModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;

            model.Execute = 1;
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
                    ModProduct_National_AreaService.Instance.Save(item);
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

    public class ModProduct_National_AreaModel : DefaultModel
    {
        public string SearchText { get; set; }
        public int ProductNationalId { get; set; }
        public string ProductNationalName { get; set; }
        public int Execute { get; set; }
    }
}

