using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Đại lý",
        Description = "Quản lý - Đại lý",
        Code = "ModProduct_Agent",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_AgentController : CPController
    {
        public ModProduct_AgentController()
        {
            //khoi tao Service
            DataService = ModProduct_AgentService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_AgentModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_AgentService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_AgentModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_AgentService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update

                //// Lấy quốc gia
                //ModProduct_CityEntity objModProduct_CityEntity = ModProduct_CityService.Instance.GetByID(item.ProductCityId);

                //// Lấy danh sách tỉnh thành
                //model.DanhSachTinhThanh = model.ShowTinhThanh(objModProduct_CityEntity.ProductNationalId, objModProduct_CityEntity.ID);

                //// Lấy danh sách quốc gia
                //model.DanhSachQuocGia = model.ShowQuocGia(objModProduct_CityEntity.ProductNationalId);
            }
            else
            {
                item = new ModProduct_AgentEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                 
                // Lấy danh sách quốc gia: Mặc định là 1
                model.DanhSachQuocGia = model.ShowQuocGia(1);

                // Lấy danh sách tỉnh thành: Mặc định là 1
                model.DanhSachTinhThanh = model.ShowTinhThanh(1, 1);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_AgentModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_AgentModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_AgentModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_AgentEntity item = null;

        private bool ValidSave(ModProduct_AgentModel model)
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
                // Kiểm tra mã xem có trùng với mã nào khác đã có không
                string sMessError = string.Empty;
                if (ModProduct_AgentService.Instance.DuplicateCode(item.Code, model.RecordID, ref sMessError))
                {
                    if (string.IsNullOrEmpty(sMessError))
                        CPViewPage.Message.ListMessage.Add(CPViewControl.ShowMessDuplicate("Mã đại lý", item.Code));
                    else
                        CPViewPage.Message.ListMessage.Add("Lỗi phát sinh: " + sMessError);
                    return false;
                }

                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModProduct_AgentService.Instance.Save(item);
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

        public void ActionRefreshPage(ModProduct_AgentModel model)
        {
            TryUpdateModel(item);
        }
    }

    public class ModProduct_AgentModel : DefaultModel
    {
        public string SearchText { get; set; }

        private List<ModProduct_NationalEntity> lstModProduct_NationalEntity { get; set; }
        private List<ModProduct_CityEntity> lstModProduct_CityEntity { get; set; }

        public string DanhSachQuocGia { get; set; }
        public string DanhSachTinhThanh { get; set; }

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

        /// <summary>
        /// Hiển thị tỉnh thành
        /// </summary>
        /// <param name="QuocGiaID"></param>
        /// <param name="TinhThanhID"></param>
        /// <returns></returns>
        public string ShowTinhThanh(int QuocGiaID, int TinhThanhID)
        {
            lstModProduct_CityEntity = ModProduct_CityService.Instance.CreateQuery().Where(p => p.ProductNationalId == QuocGiaID && p.Activity == true).ToList();
            if (lstModProduct_CityEntity == null || lstModProduct_CityEntity.Count <= 0)
                return string.Empty;

            string sReturn = string.Empty;

            foreach (var item in lstModProduct_CityEntity)
            {
                if (item.ID == TinhThanhID)
                    sReturn += "<option value=\"" + item.ID + "\" selected=\"selected\">" + item.Name + "</option>";
                else
                    sReturn += "<option value=\"" + item.ID + "\">" + item.Name + "</option>";
            }

            return sReturn;
        }
    }
}

