using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Nhà sản xuất",
        Description = "Quản lý - Nhà sản xuất", 
        Code = "ModProduct_Manufacturer", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_ManufacturerController : CPController
    {
        public ModProduct_ManufacturerController()
        {
            //khoi tao Service
            DataService = ModProduct_ManufacturerService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_ManufacturerModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_ManufacturerService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_ManufacturerModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_ManufacturerService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_ManufacturerEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_ManufacturerModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProduct_ManufacturerModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_ManufacturerModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_ManufacturerEntity item = null;

        private bool ValidSave(ModProduct_ManufacturerModel model)
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

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                // Kiểm tra mã xem có trùng với mã nào khác đã có không
                string sMessError = string.Empty;
                if (ModProduct_ManufacturerService.Instance.DuplicateCode(item.Code, model.RecordID, ref sMessError))
                {
                    if (string.IsNullOrEmpty(sMessError))
                        CPViewPage.Message.ListMessage.Add(CPViewControl.ShowMessDuplicate("Mã nhà sản xuất", item.Code));
                    else
                        CPViewPage.Message.ListMessage.Add("Lỗi phát sinh: " + sMessError);
                    return false;
                }

                 //neu khong nhap code -> tu sinh
                 if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModProduct_ManufacturerService.Instance.Save(item);
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

    public class ModProduct_ManufacturerModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

