using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;
using System.Linq;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Doanh thu - kỳ - đại lý - đơn hàng",
        Description = "Quản lý - Doanh thu - kỳ - đại lý - đơn hàng",
        Code = "ModDT_Ky_DaiLy_DonHang",
        Access = 31,
        Order = 203,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 5)]
    public class ModDT_Ky_DaiLy_DonHangController : CPController
    {
        public ModDT_Ky_DaiLy_DonHangController()
        {
            //khoi tao Service
            DataService = ModDT_Ky_DaiLy_DonHangService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModDT_Ky_DaiLy_DonHangModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            if (model.ModDtKyId <= 0)
            {
                ModDT_KyEntity objModDT_KyEntity = ModDT_KyService.Instance.CreateQuery().OrderByDesc(o => o.ID).Take(1).ToSingle();
                if (objModDT_KyEntity != null)
                {
                    model.ModDtKyId = objModDT_KyEntity.ID;
                    model.DaChotKy = objModDT_KyEntity.Activity ? (int)EnumValue.Activity.FALSE : (int)EnumValue.Activity.TRUE;
                }
            }

            // tao danh sach
            var dbQuery = ModDT_Ky_DaiLy_DonHangService.Instance.CreateQuery()
                .Where(o => o.ModDtKyId == (model.ModDtKyId))
                .Where(model.ModDTKyDaiLyId > 0, o => o.ModDTKyDaiLyId == (model.ModDTKyDaiLyId))
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModDT_Ky_DaiLy_DonHangModel model)
        {
            ModDT_KyEntity objModDT_KyEntity = ModDT_KyService.Instance.GetByID(model.ModDtKyId);
            ModDT_Ky_DaiLyEntity objModDT_Ky_DaiLyEntity = new ModDT_Ky_DaiLyEntity();

            if (model.RecordID > 0)
            {
                item = ModDT_Ky_DaiLy_DonHangService.Instance.GetByID(model.RecordID);
                if (model.ModDTKyDaiLyId > 0)
                    model.ModDTKyDaiLyId = item.ModDTKyDaiLyId;
                if (model.ModDtKyId > 0)
                    model.ModDtKyId = item.ModDtKyId;
                if (model.ModDTKyDaiLyId > 0)
                    objModDT_Ky_DaiLyEntity = ModDT_Ky_DaiLyService.Instance.GetByID(model.ModDTKyDaiLyId);


            }
            else
            {
                // Kiểm tra xem kỳ còn hoạt động ko hay chốt kỳ rồi thì ko cho thêm
                if (objModDT_KyEntity.Activity == false)
                {
                    CPViewPage.SetMessage("Kỳ doanh thu bạn chọn đã được đóng, không thể thêm mới đơn hàng.");
                    CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("Add.aspx", "Index.aspx"));
                    return;
                }

                item = new ModDT_Ky_DaiLy_DonHangEntity();
                if (model.ModDTKyDaiLyId > 0)
                    objModDT_Ky_DaiLyEntity = ModDT_Ky_DaiLyService.Instance.GetByID(model.ModDTKyDaiLyId);

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                item.NgayTao = DateTime.Now;
                item.Code = objModDT_KyEntity.Code + "." + objModDT_Ky_DaiLyEntity.Code + "." + DateTime.Now.ToString("ddMMyyy.HHmmss");
                item.Name = item.Code;
            }

            //List<ModDT_Ky_DaiLy_DonHangEntity> lstModDT_Ky_DaiLy_DonHang = ModDT_Ky_DaiLy_DonHangService.Instance.CreateQuery().Where(o=>o.ModDTKyDaiLyId== model.ModDTKyDaiLyId).ToList_Cache();
            ViewBag.KyDaiLy = objModDT_Ky_DaiLyEntity;
            ViewBag.Ky = objModDT_KyEntity;
            // Đã chốt kỳ hay chưa
            model.DaChotKy = objModDT_KyEntity.Activity ? (int)EnumValue.Activity.FALSE : (int)EnumValue.Activity.TRUE;

            model.lstModProduct_InfoEntity = new List<ModProduct_InfoEntity>();
            model.lstModProduct_InfoEntity.Add(new ModProduct_InfoEntity());
            model.lstModProduct_InfoEntity.Add(new ModProduct_InfoEntity());

            string strDanhSachSanPhanTrongDonHang = string.Empty;
            if (model.RecordID > 0)
                strDanhSachSanPhanTrongDonHang = FormDkKyDaiLyDonHang_ReloadData(model.RecordID, model);

            ViewBag.DanhSachSanPhanTrongDon = strDanhSachSanPhanTrongDonHang;
            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionAddDonHang(ModDT_Ky_DaiLy_DonHangModel model)
        {
            ModDT_Ky_DaiLyEntity objModDT_Ky_DaiLyEntity = ModDT_Ky_DaiLyService.Instance.GetByID(model.ModDTKyDaiLyId);
            ModDT_KyEntity objModDT_KyEntity = ModDT_KyService.Instance.GetByID(objModDT_Ky_DaiLyEntity.ModDtKyId);
            // Lưu lại kỳ
            model.ModDtKyId = objModDT_Ky_DaiLyEntity.ModDtKyId;

            if (model.RecordID > 0)
                item = ModDT_Ky_DaiLy_DonHangService.Instance.GetByID(model.RecordID);
            else
            {
                item = new ModDT_Ky_DaiLy_DonHangEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                item.NgayTao = DateTime.Now;
                item.Code = objModDT_KyEntity.Code + "." + objModDT_Ky_DaiLyEntity.Code + "." + DateTime.Now.ToString("ddMMyyy.HHmmss");
                item.Name = item.Code;
            }
            //List<ModDT_Ky_DaiLy_DonHangEntity> lstModDT_Ky_DaiLy_DonHang = ModDT_Ky_DaiLy_DonHangService.Instance.CreateQuery().Where(o=>o.ModDTKyDaiLyId== model.ModDTKyDaiLyId).ToList_Cache();
            ViewBag.KyDaiLy = objModDT_Ky_DaiLyEntity;
            ViewBag.Ky = objModDT_KyEntity;
            // Đã chốt kỳ hay chưa
            model.DaChotKy = objModDT_KyEntity.Activity ? (int)EnumValue.Activity.FALSE : (int)EnumValue.Activity.TRUE;

            model.lstModProduct_InfoEntity = new List<ModProduct_InfoEntity>();
            model.lstModProduct_InfoEntity.Add(new ModProduct_InfoEntity());
            model.lstModProduct_InfoEntity.Add(new ModProduct_InfoEntity());

            string strDanhSachSanPhanTrongDonHang = string.Empty;
            if (model.RecordID > 0)
                strDanhSachSanPhanTrongDonHang = FormDkKyDaiLyDonHang_ReloadData(model.RecordID, model);

            ViewBag.DanhSachSanPhanTrongDon = strDanhSachSanPhanTrongDonHang;
            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        /// <summary>
        /// CanTv
        /// Lấy danh sách các sản phẩm trong đơn hàng
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string FormDkKyDaiLyDonHang_ReloadData(int iModDTKyDaiLyDonHangId, ModDT_Ky_DaiLy_DonHangModel model)
        {
            string sData = string.Empty;

            // Lấy danh sách các sản phẩm đã có trong đơn hàng
            string strListProductExists = string.Empty;
            List<ModDT_Ky_DaiLy_DonHang_SanPhamEntity> lstCacSanPhamDaCoTrongDonHang = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                                .Where(o => o.ModDTKyDaiLyDonHangId == iModDTKyDaiLyDonHangId).ToList();
            if (lstCacSanPhamDaCoTrongDonHang != null && lstCacSanPhamDaCoTrongDonHang.Count > 0)
            {
                strListProductExists = VSW.Core.Global.Array.ToString(lstCacSanPhamDaCoTrongDonHang.Select(o => o.ModProductId).ToList().ToArray());
            }

            if (strListProductExists == null || string.IsNullOrEmpty(strListProductExists))
                return sData;


            List<ModProduct_InfoEntity> lstProductInfo = ModProduct_InfoService.Instance.CreateQuery()
                                                    .WhereIn(p => p.ID, strListProductExists)
                                                    .OrderByAsc(o => o.Code)
                                                    .ToList();

            if (lstProductInfo == null || lstProductInfo.Count <= 0)
                return sData;

            // Lấy danh sách nhà sản xuất
            List<ModProduct_ManufacturerEntity> lstManufacturerEntity = ModProduct_ManufacturerService.Instance.CreateQuery().ToList();

            int iIndex = 0;
            foreach (ModProduct_InfoEntity item in lstProductInfo)
            {
                sData += "";
                sData += "<tr class='row" + iIndex % 2 + "'>";
                sData += "<td align='center'>" + (iIndex + 1) + "</td>";
                sData += "<td align='center'>" + item.ID + "</td>";
                sData += "<td class='text-right' align='center' nowrap='nowrap'>";

                if (item.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";

                sData += "</td>";

                sData += "<td align='center'>" + Utils.GetMedia(item.File, 60, 60) + "</td>";
                sData += "<td align='left'>" + item.Code + " </td>";
                sData += "<td align='left'>" + item.Name + "</td>";
                #region Lấy thông tin số lượng và đơn giá
                ModDT_Ky_DaiLy_DonHang_SanPhamEntity objSanPhan_DonHang = lstCacSanPhamDaCoTrongDonHang.Where(o => o.ModProductId == item.ID).SingleOrDefault();
                if (objSanPhan_DonHang == null)
                {
                    sData += "<td align='right' nowrap='nowrap'></td>";
                    sData += "<td align='right'></td>";
                    sData += "<td align='right'></td>";
                }
                else
                {
                    string stxtSoLuong_Value = string.Format("{0:#,##0}", objSanPhan_DonHang.SoLuong);
                    string stxtDonGia_Value = string.Format("{0:#,##0}", objSanPhan_DonHang.DonGia);
                    string stxtTongTien_Value = string.Format("{0:#,##0}", objSanPhan_DonHang.DonGia * objSanPhan_DonHang.SoLuong);

                    sData += "<td align='right' nowrap='nowrap'><input type='text' style='width: 75% !important;' class='text_input txtSoLuong' id=txtSoLuong_" + objSanPhan_DonHang.ID +
                        " value='" + stxtSoLuong_Value + "' disabled='disabled' Value_Old='" + stxtSoLuong_Value + "' /></td>";
                    sData += "<td align='right'><input type='text' style='width: 75% !important;' class='text_input txtDonGia' id=txtDonGia_" + objSanPhan_DonHang.ID +
                        " value='" + stxtDonGia_Value + "' disabled='disabled' Value_Old='" + stxtDonGia_Value + "'  /></td>";
                    sData += "<td align='right'><label type='text' style='width: 75% !important;' class='text_input' id=lblTongTien_" + objSanPhan_DonHang.ID +
                        " value='" + stxtTongTien_Value + "' disabled='disabled' Value_Old='" + stxtTongTien_Value + "' >" + stxtTongTien_Value + "</label></td>";
                }
                #endregion

                sData += "<td align='left' style='display:none;'>" + GetManufactureName(lstManufacturerEntity, item.ManufacturerId) + "</td>";
                sData += "<td align='center' style='display:none;'>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) + "</td>";
                if (model.DaChotKy == 0)
                {
                    sData += "<td align='center'>";
                    sData += "<a class='jgrid' edit href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamEdit(this);return false;' title='Chỉnh sửa'>";
                    sData += "<span class='jgrid'><span class='state edit'></span></span></a>";
                    sData += "<a class='jgrid hide' save href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamSave(urlSoLuong_DonGia_Save," + objSanPhan_DonHang.ID + ",txtSoLuong_" + objSanPhan_DonHang.ID + ",txtDonGia_" + objSanPhan_DonHang.ID + ", true);return false;' title='Lưu thay đổi'>";
                    sData += "<span class='jgrid'><span class='state save'></span></span></a>";
                    sData += "<a class='jgrid hide' cancel href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamCancel(this);return false;' title='Hủy cập nhật'>";
                    sData += "<span class='jgrid'><span class='state deny'></span></span></a>";
                    sData += "</td>";

                    sData += "<td align='center'>";
                    sData += "<a class='jgrid' href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamDelete_DeleteTr(urlSoLuong_DonGia_Delete,this,\"" + item.ID + "\", true);return false;'>";
                    sData += "<span class='jgrid'><span class='state delete'></span></span></a>";
                    sData += "</td>";
                }

                sData += "</tr> ";
                iIndex++;
            }

            return sData;
        }

        #region Lấy tên nhà sản xuất
        private string GetManufactureName(List<ModProduct_ManufacturerEntity> lstManufacturerEntity, int? iManufacturerID)
        {
            string sName = string.Empty;
            if (iManufacturerID == null || iManufacturerID <= 0)
                return sName;

            if (lstManufacturerEntity == null || lstManufacturerEntity.Count <= 0)
                return sName;

            ModProduct_ManufacturerEntity objModProduct_ManufacturerEntity = lstManufacturerEntity.Where(p => p.ID == iManufacturerID).SingleOrDefault();
            if (objModProduct_ManufacturerEntity == null)
                return sName;

            sName = objModProduct_ManufacturerEntity.Name;
            return sName;
        }

        #endregion

        public void ActionSave(ModDT_Ky_DaiLy_DonHangModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModDT_Ky_DaiLy_DonHangModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModDT_Ky_DaiLy_DonHangModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModDT_Ky_DaiLy_DonHangEntity item = null;

        private bool ValidSave(ModDT_Ky_DaiLy_DonHangModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;
            item.ModDTKyDaiLyId = model.ModDTKyDaiLyId;
            if (model.ModDtKyId>0)
                item.ModDtKyId = model.ModDtKyId;

            //ModDT_Ky_DaiLyEntity objModDT_Ky_DaiLyEntity = ModDT_Ky_DaiLyService.Instance.GetByID(model.ModDTKyDaiLyId);
            //item.ModDtKyId = objModDT_Ky_DaiLyEntity.ModDtKyId;

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
                    item.TongSauGiam = item.TongTien - item.ChietKhau;

                    //save
                    ModDT_Ky_DaiLy_DonHangService.Instance.Save(item);
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

    public class ModDT_Ky_DaiLy_DonHangModel : DefaultModel
    {
        public string SearchText { get; set; }
        public int ModDtKyId { get; set; }

        public int ModDTKyDaiLyId { get; set; }
        public int Id { get; set; }
        public List<ModProduct_InfoEntity> lstModProduct_InfoEntity { get; set; }
        public int DaChotKy { get; set; }
    }
}

