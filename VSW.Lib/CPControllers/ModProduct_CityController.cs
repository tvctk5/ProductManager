using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Tỉnh thành",
        Description = "Quản lý - Tỉnh thành",
        Code = "ModProduct_City",
        Access = 31,
        Order = 200,
        ShowInMenu = false,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_CityController : CPController
    {
        public ModProduct_CityController()
        {
            //khoi tao Service
            DataService = ModProduct_CityService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_CityModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_CityService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_CityModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_CityService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update

                // Lấy danh sách quốc gia
                model.DanhSachQuocGia = model.ShowQuocGia(item.ProductNationalId);
            }
            else
            {
                item = new ModProduct_CityEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;

                // Lấy danh sách quốc gia: Mặc định là 1
                model.DanhSachQuocGia = model.ShowQuocGia(1);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_CityModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_CityModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_CityModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_CityEntity item = null;

        private bool ValidSave(ModProduct_CityModel model)
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
                    ModProduct_CityService.Instance.Save(item);
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

    public class ModProduct_CityModel : DefaultModel
    {
        public string SearchText { get; set; }

        private List<ModProduct_NationalEntity> lstModProduct_NationalEntity { get; set; }

        public string DanhSachQuocGia { get; set; }

        /// <summary>
        /// Hiển thị quốc gia
        /// </summary>
        /// <param name="QuocGiaID"></param>
        /// <returns></returns>
        public string ShowQuocGia(int QuocGiaID)
        {
            lstModProduct_NationalEntity = ModProduct_NationalService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();
            string sReturn = string.Empty;

            foreach (var item in lstModProduct_NationalEntity)
            {
                if (item.ID == QuocGiaID)
                    sReturn += "<option value=\"" + item.ID + "\" selected=\"selected\">" + item.Name + "</option>";
                else
                    sReturn += "<option value=\"" + item.ID + "\">" + item.Name + "</option>";
            }

            return sReturn;
        }
    }
}

