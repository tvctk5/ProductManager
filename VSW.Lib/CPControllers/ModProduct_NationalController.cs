using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Quản lý quốc gia",
        Description = "Quản lý - Quốc gia", 
        Code = "ModProduct_National", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = false,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_NationalController : CPController
    {
        public ModProduct_NationalController()
        {
            //khoi tao Service
            DataService = ModProduct_NationalService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_NationalModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_NationalService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_NationalModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_NationalService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update

                model.ListArea = ModProduct_National_AreaService.Instance.CreateQuery().Where(p => p.ProductNationalId == model.RecordID).ToList();

            }
            else
            {
                item = new ModProduct_NationalEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;

            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_NationalModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProduct_NationalModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_NationalModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_NationalEntity item = null;

        private bool ValidSave(ModProduct_NationalModel model)
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
                    ModProduct_NationalService.Instance.Save(item);
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

    public class ModProduct_NationalModel : DefaultModel
    {
        public string SearchText { get; set; }

        public List<ModProduct_National_AreaEntity> ListArea { get; set; }
    }
}

