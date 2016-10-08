﻿using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Quảng cáo/Liên kết", 
        Description = "Quản lý  - Quảng cáo/Liên kết",
        Code = "ModAdv", 
        Access = 31, 
        Order = 50, 
        ShowInMenu = true,
        CssClass = "icon-16-media",
        MenuGroupId = 4)]
    public class ModAdvController : CPController
    {
        public ModAdvController()
        {
            //khoi tao Service
            DataService = ModAdvService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModAdvModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort, "[Order]");

            // tao danh sach
            var dbQuery = ModAdvService.Instance.CreateQuery()
                                .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Adv", model.MenuID, model.LangID))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModAdvModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModAdvService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModAdvEntity();

                // khoi tao gia tri mac dinh khi insert
                item.MenuID = model.MenuID;
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModAdvModel model)
        {
            if (ValidSave(model)) 
                SaveRedirect();
        }

        public void ActionApply(ModAdvModel model)
        {
            if (ValidSave(model)) 
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModAdvModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModAdvEntity item = null;

        private bool ValidSave(ModAdvModel model)
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

            //kiem tra chuyen muc
            if (item.MenuID < 1)
                CPViewPage.Message.ListMessage.Add("Chọn chuyên mục.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                try
                {
                    //save
                    ModAdvService.Instance.Save(item);
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

        private int GetMaxOrder(ModAdvModel model)
        {
            return ModAdvService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion
    }

    public class ModAdvModel : DefaultModel
    {
        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }

        public int MenuID { get; set; }
    }
}
