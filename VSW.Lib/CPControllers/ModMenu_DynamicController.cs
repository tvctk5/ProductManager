using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Quản lý Menu",
        Description = "Quản lý - Menu",
        Code = "ModMenu_Dynamic",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModMenu_DynamicController : CPController
    {
        public ModMenu_DynamicController()
        {
            //khoi tao Service
            DataService = ModMenu_DynamicService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModMenu_DynamicModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);
            if (model.LangID == 0)
                model.LangID = 1;

            // tao danh sach
            var dbQuery = ModMenu_DynamicService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText) && o.LangID == model.LangID)
                                .Where(o => o.LangID == model.LangID)
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModMenu_DynamicModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModMenu_DynamicService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
                if (item.CreateDate == null)
                    item.CreateDate = DateTime.Now;
            }
            else
            {
                item = new ModMenu_DynamicEntity();

                // khoi tao gia tri mac dinh khi insert
                item.CreateDate = DateTime.Now;
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModMenu_DynamicModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModMenu_DynamicModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModMenu_DynamicModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionGetParentIdByMenuType(ModMenu_DynamicModel model)
        {
            TryUpdateModel(item);
        }

        #region private func

        private ModMenu_DynamicEntity item = null;

        private bool ValidSave(ModMenu_DynamicModel model)
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
                    if (item.ID <= 0)
                        item.CreateDate = DateTime.Now;

                    item.LangID = model.LangID;

                    //save
                    ModMenu_DynamicService.Instance.Save(item);
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

        private int GetMaxOrder(ModMenu_DynamicModel model)
        {
            return ModMenu_DynamicService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion
    }

    public class ModMenu_DynamicModel : DefaultModel
    {
        public string SearchText { get; set; }
        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }

    }
}

