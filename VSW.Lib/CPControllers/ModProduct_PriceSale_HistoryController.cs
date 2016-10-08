using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Product_ price sale_ history",
        Description = "Quản lý - Product_ price sale_ history",
        Code = "ModProduct_PriceSale_History",
        Access = 31,
        Order = 200,
        ShowInMenu = false,
        CssClass = "icon-16-component")]
    public class ModProduct_PriceSale_HistoryController : CPController
    {
        public ModProduct_PriceSale_HistoryController()
        {
            //khoi tao Service
            DataService = ModProduct_PriceSale_HistoryService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_PriceSale_HistoryModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            if (model.ProductInfoId > 0)
            {
                // tao danh sach
                var dbQuery = ModProduct_PriceSale_HistoryService.Instance.CreateQuery()
                                    .Where(o => o.ProductInfoId == model.ProductInfoId)
                                    .Take(model.PageSize)
                                    .OrderBy(orderBy)
                                    .Skip(model.PageIndex * model.PageSize);

                ViewBag.Data = dbQuery.ToList();
                model.TotalRecord = dbQuery.TotalRecord;
            }
            else
            {
                // tao danh sach
                var dbQuery = ModProduct_PriceSale_HistoryService.Instance.CreateQuery()
                                    .Take(model.PageSize)
                                    .OrderBy(orderBy)
                                    .Skip(model.PageIndex * model.PageSize);

                ViewBag.Data = dbQuery.ToList();
                model.TotalRecord = dbQuery.TotalRecord;
            }

            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_PriceSale_HistoryModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_PriceSale_HistoryService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_PriceSale_HistoryEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_PriceSale_HistoryModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_PriceSale_HistoryModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_PriceSale_HistoryModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionSaleOffTypeGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@SaleOffType", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModProduct_PriceSale_HistoryEntity item = null;

        private bool ValidSave(ModProduct_PriceSale_HistoryModel model)
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
                    ModProduct_PriceSale_HistoryService.Instance.Save(item);
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

    public class ModProduct_PriceSale_HistoryModel : DefaultModel
    {
        public int ProductInfoId { get; set; }
    }
}

