using System;
using System.Collections.Generic;
using System.Linq;
using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Thông tin thuộc tính sản phẩm",
        Description = "Quản lý - Thông tin thuộc tính sản phẩm",
        Code = "ModProduct_Info_Details",
        Access = 31,
        Order = 200,
        ShowInMenu = false,
        CssClass = "icon-16-component")]
    public class ModProduct_Info_DetailsController : CPController
    {
        public ModProduct_Info_DetailsController()
        {
            //khoi tao Service
            DataService = ModProduct_Info_DetailsService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_Info_DetailsModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_Info_DetailsService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_Info_DetailsModel model)
        {
            if (model.ProductInfoId > 0)
            {
                // khoi tao gia tri mac dinh khi update
                // Lấy thông tin sản phẩm
                ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(model.ProductInfoId);

                // Lấy thông tin chủng loại
                WebMenuEntity objWebMenuEntity = WebMenuService.Instance.GetByID(objModProduct_InfoEntity.MenuID);

                // Lưu lại Nhóm thuộc tính ID
                model.PropertiesGroupsId = (int)objWebMenuEntity.ProductAreaId;

                if (objWebMenuEntity.ProductAreaId <= 0)
                {
                    CPViewPage.Message.ListMessage.Add("Sản phẩm chưa có nhóm thuộc tính nào. Yêu cầu chọn các nhóm cho Sản phẩm để có thể nhập thông tin");
                    ViewBag.Data = item;
                    ViewBag.Model = model;
                    return;
                }

                model.GetPropertiesList_Value = ModProduct_Info_DetailsService.Instance.CreateQuery()
                    .Where(o => o.PropertiesGroupsId == objWebMenuEntity.ProductAreaId && o.ProductInfoId == model.ProductInfoId)
                    .ToList();

                // Where In Lấy danh sách nhóm thuộc tính chứa thuộc tính SP
                model.PropertiesGroup = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                    .Where(o => o.ID == objWebMenuEntity.ProductAreaId && o.Activity == true)
                    .OrderByAsc(o => o.Order).ToSingle();

                // Lấy danh sách thuộc tính thuộc nhóm bên trên
                model.GetPropertiesList = ModProduct_PropertiesListService.Instance.CreateQuery()
                    .Where(o => o.PropertiesGroupsId == objWebMenuEntity.ProductAreaId && o.Activity == true)
                    .OrderByAsc(o => o.Order)
                    .ToList();

                // Lấy danh sách thứ tự thuộc tính để cập nhật dữ liệu theo thứ tự này
                string sThuTu = string.Empty;

                foreach (var itemProperties in model.GetPropertiesList)
                {
                    sThuTu += itemProperties.ID + ",";
                }
                model.DanhSachThuTuThuocTinh = sThuTu.Trim(',');

                // Lấy danh sách các giá trị của các thuộc tính có sẵn
                List<ModProduct_PropertiesList_ValuesEntity> lstModProduct_PropertiesList_Values= ModProduct_PropertiesList_ValuesService.Instance.CreateQuery().WhereIn(o => o.PropertiesListId, model.DanhSachThuTuThuocTinh).ToList();
                if (lstModProduct_PropertiesList_Values == null)
                    lstModProduct_PropertiesList_Values = new List<ModProduct_PropertiesList_ValuesEntity>();

                model.GetProduct_PropertiesList_Values = lstModProduct_PropertiesList_Values;
            }
            else
            {
                item = new ModProduct_Info_DetailsEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        /*
        public void ActionAdd(ModProduct_Info_DetailsModel model)
        {
            if (model.RecordID > 0)
            {

                // khoi tao gia tri mac dinh khi update

                item = ModProduct_Info_DetailsService.Instance.GetByID(model.RecordID);
                // Lấy danh sách SP - nhóm thuộc tính
                List<ModProduct_Groups_PropertiesGroupsEntity> lstInfo_PropertiesGroups = ModProduct_Groups_PropertiesGroupsService.Instance.CreateQuery()
                    //.Where(o => o.ProductInfoId == item.ID)
                      .ToList();

                if (lstInfo_PropertiesGroups == null || lstInfo_PropertiesGroups.Count <= 0)
                {
                    CPViewPage.Message.ListMessage.Add("Sản phẩm chưa có nhóm thuộc tính nào. Yêu cầu chọn các nhóm cho Sản phẩm để có thể nhập thông tin");
                    ViewBag.Data = item;
                    ViewBag.Model = model;
                    return;
                }
                // Lấy chuỗi Id
                string sPropertiesGroupsId = string.Empty;
                foreach (ModProduct_Groups_PropertiesGroupsEntity itemEntity in lstInfo_PropertiesGroups)
                {
                    if (!string.IsNullOrEmpty(sPropertiesGroupsId))
                        sPropertiesGroupsId += ",";

                    sPropertiesGroupsId += itemEntity.PropertiesGroupsId;
                }

                // Where In Lấy danh sách nhóm thuộc tính chứa thuộc tính SP
                model.GetGroupList = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                    .WhereIn(o => o.ID, sPropertiesGroupsId)
                    .Where(o => o.Activity == true)
                    .OrderByAsc(o => o.Order).ToList();

                // Lấy danh sách thuộc tính thuộc nhóm bên trên
                model.GetPropertiesList = ModProduct_PropertiesListService.Instance.CreateQuery()
                    .WhereIn(o => o.PropertiesGroupsId, sPropertiesGroupsId)
                    .Where(o => o.Activity == true)
                    //.OrderByAsc(o => o.Order)
                    .ToList();
            }
            else
            {
                item = new ModProduct_Info_DetailsEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }
        */
        public void ActionSave(ModProduct_Info_DetailsModel model)
        {
            if (ValidSave(model))
            {
                CPViewPage.SetMessage("Thông tin thuộc tính sản phẩm đã được cập nhật.");
                CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl);
            }
        }

        public void ActionApply(ModProduct_Info_DetailsModel model)
        {
            if (ValidSave(model))
            //ApplyRedirect(model.RecordID, item.ID);
            {
                CPViewPage.SetMessage("Thông tin thuộc tính sản phẩm đã được cập nhật.");
                CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl);
            }
        }

        public void ActionSaveNew(ModProduct_Info_DetailsModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public override void ActionCancel()
        {
            string sUrl = CPViewPage.Request.RawUrl.Replace("ModProduct_Info_Details", "ModProduct_Info");
            sUrl = sUrl.Substring(0, sUrl.LastIndexOf('/')) + "/";
            var model = ViewBag.Model as ModProduct_Info_DetailsModel;
            if (model == null)
                return;

            var dbQuery = ModProduct_Info_DetailsService.Instance.CreateQuery()
                .Where(o => o.ID == model.RecordID);

            if (dbQuery != null || dbQuery.ToList().Count > 0)
                CPViewPage.Response.Redirect(sUrl + dbQuery.ToList()[0].ProductInfoId);
        }

        #region private func

        private ModProduct_Info_DetailsEntity item = null;

        private bool ValidSave(ModProduct_Info_DetailsModel model)
        {
            //TryUpdateModel(item);

            // Lấy thông tin Thuộc tính
            string sThuTu = model.DanhSachThuTuThuocTinh;
            if (string.IsNullOrEmpty(sThuTu))
                return false;

            string[] ArrThuTuId = sThuTu.Split(',');
            ThuocTinh objThuocTinh = null;
            List<ThuocTinh> lstThuocTinh = new List<ThuocTinh>();

            for (int i = 0; i < ArrThuTuId.Length; i++)
            {
                objThuocTinh = new ThuocTinh();
                objThuocTinh.ProductInfoId = model.ProductInfoId;
                objThuocTinh.PropertiesGroupsId = model.PropertiesGroupsId;
                objThuocTinh.PropertiesListId = int.Parse(ArrThuTuId[i]);
                objThuocTinh.Content = CPViewPage.Request.Form["Value" + i];
                if (CPViewPage.Request.Form["chkSaveData" + i] == null || CPViewPage.Request.Form["chkSaveData" + i] != "on")
                    objThuocTinh.SaveData = false;
                else
                    objThuocTinh.SaveData = true;

                // Thêm vào danh sách
                lstThuocTinh.Add(objThuocTinh);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;

            try
            {
                // Xóa dữ liệu cũ
                ModProduct_Info_DetailsService.Instance.Delete("ProductInfoId=" + model.ProductInfoId);

                // Lấy danh sách insert 
                List<ModProduct_Info_DetailsEntity> lstModProduct_Info_DetailsEntity = new List<ModProduct_Info_DetailsEntity>();
                ModProduct_Info_DetailsEntity itemModProduct_Info_DetailsEntity = null;

                foreach (var itemThuocTinh in lstThuocTinh)
                {
                    itemModProduct_Info_DetailsEntity = new ModProduct_Info_DetailsEntity();
                    itemModProduct_Info_DetailsEntity.ProductInfoId = itemThuocTinh.ProductInfoId;
                    itemModProduct_Info_DetailsEntity.PropertiesGroupsId = itemThuocTinh.PropertiesGroupsId;
                    itemModProduct_Info_DetailsEntity.PropertiesListId = itemThuocTinh.PropertiesListId;
                    itemModProduct_Info_DetailsEntity.Content = itemThuocTinh.Content;

                    // Thêm vào list
                    lstModProduct_Info_DetailsEntity.Add(itemModProduct_Info_DetailsEntity);
                }

                // Insert vào DataBase
                ModProduct_Info_DetailsService.Instance.Save(lstModProduct_Info_DetailsEntity);

                // Thêm giá trị các thuộc tính vào bảng thuộc tính - giá trị
                List<ModProduct_PropertiesList_ValuesEntity> lstModProduct_PropertiesList_ValuesEntity = new List<ModProduct_PropertiesList_ValuesEntity>();
                ModProduct_PropertiesList_ValuesEntity itemPropertiesList_ValuesEntity = null;
                List<ModProduct_PropertiesList_ValuesEntity> lstModProduct_PropertiesList_ValuesEntity_Check = ModProduct_PropertiesList_ValuesService.Instance.CreateQuery().WhereIn(o => o.PropertiesListId, sThuTu).ToList();
                if (lstModProduct_PropertiesList_ValuesEntity_Check == null)
                    lstModProduct_PropertiesList_ValuesEntity_Check = new List<ModProduct_PropertiesList_ValuesEntity>();

                foreach (var itemThuocTinh in lstThuocTinh)
                {
                    if (itemThuocTinh.SaveData == false)
                        continue;

                    // Kiểm tra xem giá trị tồn tại chưa
                    itemPropertiesList_ValuesEntity = lstModProduct_PropertiesList_ValuesEntity_Check
                        .Where(p => p.PropertiesListId == itemThuocTinh.PropertiesListId && p.Content.ToUpper() == itemThuocTinh.Content.ToUpper()).SingleOrDefault();

                    // Nếu tồn tại rồi thì không thêm nữa
                    if (itemPropertiesList_ValuesEntity != null)
                        continue;

                    itemPropertiesList_ValuesEntity = new ModProduct_PropertiesList_ValuesEntity();

                    itemPropertiesList_ValuesEntity.PropertiesListId = itemThuocTinh.PropertiesListId;
                    itemPropertiesList_ValuesEntity.Content = itemThuocTinh.Content;

                    // Thêm vào danh sách
                    lstModProduct_PropertiesList_ValuesEntity.Add(itemPropertiesList_ValuesEntity);
                }

                // Insert vào DataBase
                ModProduct_PropertiesList_ValuesService.Instance.Save(lstModProduct_PropertiesList_ValuesEntity);
            }
            catch (Exception ex)
            {
                Global.Error.Write(ex);
                CPViewPage.Message.ListMessage.Add(ex.Message);
                return false;
            }

            return true;
        }

        #endregion
    }

    public class ModProduct_Info_DetailsModel : DefaultModel
    {
        public ModProduct_PropertiesGroupsEntity PropertiesGroup { get; set; }
        public List<ModProduct_PropertiesListEntity> GetPropertiesList { get; set; }
        public List<ModProduct_Info_DetailsEntity> GetPropertiesList_Value { get; set; }
        public List<ModProduct_PropertiesList_ValuesEntity> GetProduct_PropertiesList_Values { get; set; }
        public int ProductInfoId { get; set; }
        public int PropertiesGroupsId { get; set; }
        public string DanhSachThuTuThuocTinh { get; set; }
    }

    class ThuocTinh
    {
        public string Content { get; set; }
        public int ProductInfoId { get; set; }
        public int PropertiesGroupsId { get; set; }
        public int PropertiesListId { get; set; }
        public bool SaveData { get; set; }
    }
}

