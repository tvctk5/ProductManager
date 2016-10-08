using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Bình luận sản phẩm",
        Description = "Quản lý - Bình luận sản phẩm",
        Code = "ModProduct_Comments",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component")]
    public class ModProduct_CommentsController : CPController
    {
        public ModProduct_CommentsController()
        {
            //khoi tao Service
            DataService = ModProduct_CommentsService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_CommentsModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_CommentsService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_CommentsModel model)
        {
            var ProductCurrent = new ModProduct_InfoEntity();

            if (model.RecordID > 0)
            {
                item = ModProduct_CommentsService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
                ProductCurrent = ModProduct_InfoService.Instance.GetByID(item.ProductInfoId);
            }
            else
            {
                item = new ModProduct_CommentsEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
            ViewBag.ProductCurrent = ProductCurrent;
        }

        public void ActionSave(ModProduct_CommentsModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_CommentsModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_CommentsModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionApprovedGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@Approved", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModProduct_CommentsEntity item = null;

        private bool ValidSave(ModProduct_CommentsModel model)
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
                    item.ModifiedDate = DateTime.Now;

                    //save
                    ModProduct_CommentsService.Instance.Save(item);
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

    public class ModProduct_CommentsModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

