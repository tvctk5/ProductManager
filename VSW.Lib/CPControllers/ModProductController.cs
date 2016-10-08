using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Product", 
        Description = "Quản lý - Product", 
        Code = "ModProduct", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = false, 
        CssClass = "icon-16-component")]
    public class ModProductController : CPController
    {
        public ModProductController()
        {
            //khoi tao Service
            DataService = ModProductService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProductModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProductService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Product", model.MenuID, model.LangID))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProductModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProductService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProductEntity();

                // khoi tao gia tri mac dinh khi insert
                item.MenuID = model.MenuID;
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProductModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProductModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProductModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProductEntity item = null;

        private bool ValidSave(ModProductModel model)
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

            //kiem tra chuyen muc
            if (item.MenuID < 1)
                CPViewPage.Message.ListMessage.Add("Chọn chuyên mục.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                 //neu khong nhap code -> tu sinh
                 if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModProductService.Instance.Save(item);
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

        private int GetMaxOrder(ModProductModel model)
        {
            return ModProductService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion
    }

    public class ModProductModel : DefaultModel
    {
        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }

        public int MenuID { get; set; }
        public string SearchText { get; set; }
    }
}

