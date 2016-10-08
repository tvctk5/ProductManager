using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Doanh thu - Cấp đại lý - Hoa hồng",
        Description = "Quản lý - Doanh thu - Cấp đại lý - Hoa hồng",
        Code = "ModDT_CapDaiLy_TyLe",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 5)]
    public class ModDT_CapDaiLy_TyLeController : CPController
    {
        public ModDT_CapDaiLy_TyLeController()
        {
            //khoi tao Service
            DataService = ModDT_CapDaiLy_TyLeService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModDT_CapDaiLy_TyLeModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModDT_CapDaiLy_TyLeService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModDT_CapDaiLy_TyLeModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModDT_CapDaiLy_TyLeService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModDT_CapDaiLy_TyLeEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                item.Value = 1;

                // LẤy cấp cha nếu có.

                ModDT_CapDaiLy_TyLeEntity objModDT_CapDaiLy_TyLe = ModDT_CapDaiLy_TyLeService.Instance.CreateQuery().OrderByDesc(o => o.ID).Take(1).ToSingle();
                if (objModDT_CapDaiLy_TyLe != null)
                {
                    model.ParentID = objModDT_CapDaiLy_TyLe.ID;
                    item.ParentID = objModDT_CapDaiLy_TyLe.ID;
                }
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModDT_CapDaiLy_TyLeModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModDT_CapDaiLy_TyLeModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModDT_CapDaiLy_TyLeModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModDT_CapDaiLy_TyLeEntity item = null;

        private bool ValidSave(ModDT_CapDaiLy_TyLeModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;
            item.ParentID = model.ParentID;

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
                    if (item.ParentID <= 0)
                        item.ParentID = null;

                    //save
                    ModDT_CapDaiLy_TyLeService.Instance.Save(item);
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

    public class ModDT_CapDaiLy_TyLeModel : DefaultModel
    {
        public string SearchText { get; set; }
        public int ParentID { get; set; }
    }
}

