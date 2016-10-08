using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class SysSiteController : CPController
    {
        public SysSiteController()
        {
            //khoi tao Service
            DataService = SysSiteService.Instance;
            //CheckPermissions = false;
        }

        public void ActionIndex(SysSiteModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort, "[Order]");

            // tao danh sach
            var dbQuery = SysSiteService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(SysSiteModel model)
        {
            if (model.RecordID > 0)
            {
                item = SysSiteService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new SysSiteEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(SysSiteModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(SysSiteModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(SysSiteModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionDefaultGX(int id)
        {
            //update for id
            SysSiteService.Instance.Update(o => o.ID == id,
                "@Default", 1);

            //update for != id
            SysSiteService.Instance.Update(o => o.ID != id,
                "@Default", 0);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private SysSiteEntity item = null;

        private bool ValidSave(SysSiteModel model)
        {
            TryUpdateModel(item);

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tên site.");

            //kiem tra ma 
            if (item.Code.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập mã.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                try
                {
                    //save
                    SysSiteService.Instance.Save(item);
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

        private int GetMaxOrder(SysSiteModel model)
        {
            return SysSiteService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion
    }

    public class SysSiteModel : DefaultModel
    {
        
    }
}
