using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Hình thức thanh toán",
        Description = "Quản lý - Hình thức thanh toán", 
        Code = "ModProduct_Payment", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_PaymentController : CPController
    {
        public ModProduct_PaymentController()
        {
            //khoi tao Service
            DataService = ModProduct_PaymentService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_PaymentModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_PaymentService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_PaymentModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_PaymentService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_PaymentEntity();

                // khoi tao gia tri mac dinh khi insert
                item.CreateDate = DateTime.Now;
            }
            
            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_PaymentModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProduct_PaymentModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_PaymentModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_PaymentEntity item = null;

        private bool ValidSave(ModProduct_PaymentModel model)
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
                    //save
                    ModProduct_PaymentService.Instance.Save(item);
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

    public class ModProduct_PaymentModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

