using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Thuộc tính lọc", 
        Description = "Quản lý - Thuộc tính lọc", 
        Code = "ModProduct_Filter", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_FilterController : CPController
    {
        public ModProduct_FilterController()
        {
            //khoi tao Service
            DataService = ModProduct_FilterService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_FilterModel model)
        {
            // sap xep tu dong
            string orderBy = string.Empty;
            if(model.Sort!=null)
                orderBy =  AutoSort(model.Sort);
            else
                orderBy = "[FilterGroupsId],[Order] ASC";
             
            // tao danh sach
            var dbQuery = ModProduct_FilterService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);


            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            model.ListFilterGroups = ModProduct_FilterGroupsService.Instance.CreateQuery().ToList();
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_FilterModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_FilterService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_FilterEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_FilterModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProduct_FilterModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_FilterModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_FilterEntity item = null;

        private bool ValidSave(ModProduct_FilterModel model)
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
                    ModProduct_FilterService.Instance.Save(item);
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

        private int GetMaxOrder(ModProduct_FilterModel model)
        {
            return ModProduct_FilterService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion
    }

    public class ModProduct_FilterModel : DefaultModel
    {
        public List<ModProduct_FilterGroupsEntity> ListFilterGroups { get; set; }

        public static string GetNameFilterGroup(int iFilterGroupId, List<ModProduct_FilterGroupsEntity> lstFilterGroups)
        { 
            string sData = string.Empty;
            if (lstFilterGroups == null || lstFilterGroups.Count <= 0)
                return sData;

            var Item = lstFilterGroups.Where(p => p.ID == iFilterGroupId).SingleOrDefault();
            if (Item == null)
                return sData;

            return Item.Name;

        }
    }
}

