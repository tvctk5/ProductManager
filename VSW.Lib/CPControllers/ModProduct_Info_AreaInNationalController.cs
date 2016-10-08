using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Product_ info_ area in national", 
        Description = "Quản lý - Product_ info_ area in national", 
        Code = "ModProduct_Info_AreaInNational", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = false, 
        CssClass = "icon-16-component")]
    public class ModProduct_Info_AreaInNationalController : CPController
    {
        public ModProduct_Info_AreaInNationalController()
        {
            //khoi tao Service
            DataService = ModProduct_Info_AreaInNationalService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_Info_AreaInNationalModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_Info_AreaInNationalService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_Info_AreaInNationalModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_Info_AreaInNationalService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_Info_AreaInNationalEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_Info_AreaInNationalModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProduct_Info_AreaInNationalModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_Info_AreaInNationalModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_Info_AreaInNationalEntity item = null;

        private bool ValidSave(ModProduct_Info_AreaInNationalModel model)
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

                try
                {
                    //save
                    ModProduct_Info_AreaInNationalService.Instance.Save(item);
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

    public class ModProduct_Info_AreaInNationalModel : DefaultModel
    {
    }
}

