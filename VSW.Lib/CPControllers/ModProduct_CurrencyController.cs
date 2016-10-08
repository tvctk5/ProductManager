using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Tỷ giá",
        Description = "Quản lý - Tỷ giá",
        Code = "ModProduct_Currency",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_CurrencyController : CPController
    {
        public ModProduct_CurrencyController()
        {
            //khoi tao Service
            DataService = ModProduct_CurrencyService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_CurrencyModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_CurrencyService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_CurrencyModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_CurrencyService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_CurrencyEntity();

                // khoi tao gia tri mac dinh khi insert
                item.VND = 1;
                item.Activity = CPViewPage.UserPermissions.Approve;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_CurrencyModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_CurrencyModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_CurrencyModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_CurrencyEntity item = null;

        private bool ValidSave(ModProduct_CurrencyModel model)
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

            if (item.Code.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Yêu cầu nhập mã tỷ giá.");

            if (item.Code.Trim().Length < 3)
                CPViewPage.Message.ListMessage.Add("Mã tỷ giá phải có từ 3 ký tự trở lên.");

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Yêu cầu nhập tên tỷ giá.");

            if (item.VND == 0)
                CPViewPage.Message.ListMessage.Add("Tỷ giá so với VNĐ phải lớn hơn 0.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                // Kiểm tra mã xem có trùng với mã nào khác đã có không
                string sMessError = string.Empty;
                if (ModProduct_CurrencyService.Instance.DuplicateCode(item.Code, model.RecordID, ref sMessError))
                {
                    if (string.IsNullOrEmpty(sMessError))
                        CPViewPage.Message.ListMessage.Add(CPViewControl.ShowMessDuplicate("Mã tỷ giá", item.Code));
                    else
                        CPViewPage.Message.ListMessage.Add("Lỗi phát sinh: " + sMessError);
                    return false;
                }

                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    item.Code = item.Code.Trim();
                    //save
                    ModProduct_CurrencyService.Instance.Save(item);
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

    public class ModProduct_CurrencyModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

