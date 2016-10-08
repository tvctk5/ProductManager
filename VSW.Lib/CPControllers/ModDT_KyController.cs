using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;
using VSW.Lib.LinqToSql;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Doanh thu - kỳ",
        Description = "Quản lý - Doanh thu - kỳ",
        Code = "ModDT_Ky",
        Access = 31,
        Order = 201,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 5)]
    public class ModDT_KyController : CPController
    {
        public ModDT_KyController()
        {
            //khoi tao Service
            DataService = ModDT_KyService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModDT_KyModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModDT_KyService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModDT_KyModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModDT_KyService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
                model.DaChotKy = item.Activity ? (int)EnumValue.Activity.FALSE : (int)EnumValue.Activity.TRUE;
            }
            else
            {
                // Kiểm tra xem có kỳ doanh thu nào đang tồn tại không. Nếu có thì không cho phép thêm
                var objCheck = ModDT_KyService.Instance.CreateQuery().Where(o => o.Activity == true).ToSingle();
                if (objCheck != null)
                {
                    CPViewPage.SetMessage("Tồn tại một kỳ doanh thu đang hoạt động. Yêu cầu chốt kỳ doanh thu để có thể thêm mới");
                    CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("Add.aspx", "Index.aspx"));
                    return;
                }

                item = new ModDT_KyEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                model.DaChotKy = (int)EnumValue.Activity.FALSE;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModDT_KyModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModDT_KyModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModDT_KyModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModDT_KyEntity item = null;

        private bool ValidSave(ModDT_KyModel model)
        {
            TryUpdateModel(item);

            bool bolThemMoi = false;
            if (model.RecordID <= 0)
                bolThemMoi = true;

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
                    ModDT_KyService.Instance.Save(item);

                    #region Chỉ thực hiện khi thêm mới
                    if (bolThemMoi)
                    {
                        // Lấy danh sách Đại lý giới thiệu
                        List<ModDT_DaiLyEntity> lstModDT_DaiLyEntity = ModDT_DaiLyService.Instance.CreateQuery()
                                                                       .Where(o => o.Activity == ConvertTool.ConvertToBoolean((int)EnumValue.Activity.TRUE)).ToList();

                        if (lstModDT_DaiLyEntity != null && lstModDT_DaiLyEntity.Count > 0)
                        {
                            List<ModDT_Ky_DaiLyEntity> lstModDT_Ky_DaiLyEntity = new List<ModDT_Ky_DaiLyEntity>();
                            foreach (var itemModDT_DaiLy in lstModDT_DaiLyEntity)
                            {
                                ModDT_Ky_DaiLyEntity objModDT_Ky_DaiLyEntity = new ModDT_Ky_DaiLyEntity();
                                objModDT_Ky_DaiLyEntity.ModDtKyId = item.ID;
                                if (itemModDT_DaiLy.ModProductAgentParentId != 0)
                                    objModDT_Ky_DaiLyEntity.ModProductAgentParentId = itemModDT_DaiLy.ModProductAgentParentId;
                                objModDT_Ky_DaiLyEntity.ModProductAgentId = itemModDT_DaiLy.ModProductAgentId;
                                objModDT_Ky_DaiLyEntity.Code = itemModDT_DaiLy.Code;
                                objModDT_Ky_DaiLyEntity.Name = itemModDT_DaiLy.Name;
                                //objModDT_Ky_DaiLyEntity.Type = itemModDT_DaiLy.Type;
                                //objModDT_Ky_DaiLyEntity.Value = itemModDT_DaiLy.Value;
                                //objModDT_Ky_DaiLyEntity.TotalFirst = itemModDT_DaiLy.TotalFirst;
                                //objModDT_Ky_DaiLyEntity.TotalLast = itemModDT_DaiLy.TotalLast;
                                objModDT_Ky_DaiLyEntity.ModLoaiDaiLyCode = itemModDT_DaiLy.ModLoaiDaiLyCode;
                                objModDT_Ky_DaiLyEntity.ModLoaiDaiLyName = itemModDT_DaiLy.ModLoaiDaiLyName;
                                objModDT_Ky_DaiLyEntity.ModLoaiDaiLyType = itemModDT_DaiLy.ModLoaiDaiLyType;
                                objModDT_Ky_DaiLyEntity.ModLoaiDaiLyValue = itemModDT_DaiLy.ModLoaiDaiLyValue;
                                objModDT_Ky_DaiLyEntity.Activity = true;
                                objModDT_Ky_DaiLyEntity.CreateDate = DateTime.Now;

                                lstModDT_Ky_DaiLyEntity.Add(objModDT_Ky_DaiLyEntity);

                            }

                            // Insert 
                            ModDT_Ky_DaiLyService.Instance.Save(lstModDT_Ky_DaiLyEntity);
                        }

                    }
                    #endregion
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

        /// <summary>
        /// Delete - Modified by CanTV
        /// </summary>
        /// <param name="arrID"></param>
        public override void ActionDelete(int[] arrID)
        {
            string sMess = string.Empty;
            try
            {
                if (CheckPermissions && !CPViewPage.UserPermissions.Delete)
                {
                    //thong bao
                    CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                    CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                    return;
                }

                int iXoaKy_DaiLy_DonHang_SanPham = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.Delete(@"ModDTKyDaiLyDonHangId IN 
                (SELECT ID FROM Mod_DT_Ky_DaiLy_DonHang 
                WHERE ModDTKyDaiLyId IN 
                (SELECT ID FROM Mod_DT_Ky_DaiLy
                WHERE ModDtKyId IN 
                (SELECT ID FROM Mod_DT_Ky WHERE ID IN (" + VSW.Core.Global.Array.ToString(arrID) + "))))");

                int iXoaKy_DaiLy_DonHang = ModDT_Ky_DaiLy_DonHangService.Instance.Delete(@"ModDTKyDaiLyId IN 
                (SELECT ID FROM Mod_DT_Ky_DaiLy
                WHERE ModDtKyId IN 
                (SELECT ID FROM Mod_DT_Ky WHERE ID IN (" + VSW.Core.Global.Array.ToString(arrID) + ")))");

                int iXoaKy_DaiLy = ModDT_Ky_DaiLyService.Instance.Delete(@"ModDtKyId IN 
                (SELECT ID FROM Mod_DT_Ky WHERE ID IN (" + VSW.Core.Global.Array.ToString(arrID) + "))");

                int iXoaKy = ModDT_KyService.Instance.Delete("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")");

                // thành công
                sMess = "Đã xóa thành công.";
            }
            catch (Exception ex)
            {
                // Lỗi phát sinh
                sMess = "Xóa thất bại. Lỗi phát sinh trong quá trình xóa hoặc tồn tại ràng buộc dữ liệu:" + ex.Message;
            }

            //thong bao
            CPViewPage.SetMessage(sMess);
            CPViewPage.RefreshPage();
        }

        /// <summary>
        /// Chốt kỳ kinh doanh
        /// </summary>
        /// <param name="model"></param>
        public void ActionChotKy(ModDT_KyModel model)
        {
            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            DbDataContext db = DbExecute.Create(true);
            Mod_DT_Ky objMod_DT_Ky = db.Mod_DT_Kies.Where(o => o.ID == model.RecordID).SingleOrDefault();
            if (objMod_DT_Ky == null)
            {
                CPViewPage.Message.ListMessage.Add("Không tìm thấy kỳ kinh doanh hiện tại.");
                return;
            }

            // Cập nhật trạng thái cho Kỳ
            objMod_DT_Ky.Activity = false; // Đóng kỳ

            // Tổng hợp lại các giá trị hoa hồng cho từng đại lý
            if (objMod_DT_Ky.Mod_DT_Ky_DaiLies == null || objMod_DT_Ky.Mod_DT_Ky_DaiLies.Count <= 0)
            {
                db.SubmitChanges();
                return;
            }

            List<Mod_DT_Ky_DaiLy> lstMod_DT_Ky_DaiLy = objMod_DT_Ky.Mod_DT_Ky_DaiLies.ToList();
            lstMod_DT_Ky_DaiLy = lstMod_DT_Ky_DaiLy.OrderBy(o => o.ModProductAgentParentId).ToList();
            
            // Tính tổng tiền lấy hàng
            foreach (var itemKyDaiLy in lstMod_DT_Ky_DaiLy)
            {
                List<Mod_DT_Ky_DaiLy_DonHang> lstMod_DT_Ky_DaiLy_DonHang = itemKyDaiLy.Mod_DT_Ky_DaiLy_DonHangs.ToList();
                double doubTongTienLayHang = 0;
                foreach (var itemDaiLy_DonHang in lstMod_DT_Ky_DaiLy_DonHang)
                    doubTongTienLayHang += getValueDouble(itemDaiLy_DonHang.TongSauGiam);

                // Cập nhật tổng tiền
                itemKyDaiLy.TongTienLayHang = doubTongTienLayHang;
            }

            // Lấy cấp tỷ lệ ăn chia hoa hồng
            List<Mod_DT_CapDaiLy_TyLe> lstMod_DT_CapDaiLy_TyLe = db.Mod_DT_CapDaiLy_TyLes.OrderBy(o => o.ID).ToList();

            // Lấy các cấp cha để duyệt
            List<Mod_DT_Ky_DaiLy> lstMod_DT_Ky_DaiLy_Parent = lstMod_DT_Ky_DaiLy
                .Where(o => o.ModProductAgentParentId == null || o.ModProductAgentParentId == 0).ToList();
            // TÍnh tổng hoa hồng trong kỳ
            double doubTongHoaHong = 0;

            foreach (var itemParent in lstMod_DT_Ky_DaiLy)
            {
                int iCapHoaHong = 0;
                double doubTongTienHoaHong = 0;
                // Cộng giá trị đầu kỳ nếu có
                double doubTotalLast = getValueDouble(itemParent.TotalFirst);

                // Lấy các đại lý con: Tính hoa hồng
                List<Mod_DT_Ky_DaiLy> lstMod_DT_Ky_DaiLy_Child = lstMod_DT_Ky_DaiLy
                                        .Where(o => o.ModProductAgentParentId != null && o.ModProductAgentParentId == itemParent.ModProductAgentId)
                                        .ToList();

                if (lstMod_DT_Ky_DaiLy_Child != null && lstMod_DT_Ky_DaiLy_Child.Count>0)
                    // Tính hoa hồng cho đại lý cấp đầu trước (Nếu tồn tại ít nhất một con)
                    doubTongTienHoaHong = (getValueDouble(itemParent.TongTienLayHang) * getValueDouble(lstMod_DT_CapDaiLy_TyLe[iCapHoaHong].Value)) / 100;

                // Duyệt các đại lý con
                foreach (var itemChild in lstMod_DT_Ky_DaiLy_Child)
                {
                    doubTongTienHoaHong += getTienHoaHong(ref lstMod_DT_Ky_DaiLy, itemChild, lstMod_DT_CapDaiLy_TyLe, iCapHoaHong + 1);
                }

                // Lưu lại tổng tiền hoa hồng
                itemParent.TongTienHoaHong = doubTongTienHoaHong;
                // Tổng tiền thu nhập cuối kỳ
                itemParent.TotalLast = doubTotalLast + doubTongTienHoaHong;
                // Tổng hoa hồng trong kỳ
                doubTongHoaHong += doubTongTienHoaHong;
            }

            // Tính tổng hoa hồng trong kỳ
            doubTongHoaHong += getValueDouble(objMod_DT_Ky.TotalFirst);
            
            // Tổng hoa hồng trong kỳ
            objMod_DT_Ky.TotalLast = doubTongHoaHong;

            // Cập nhật vào DB
            db.SubmitChanges();

            // Hiển thị thông báo
            CPViewPage.SetMessage("Đã chốt kỳ doanh thu thành công.");
            CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("Add.aspx", "Index.aspx"));
        }

        /// <summary>
        /// Tính tiền hoa hồng bằng đệ quy
        /// </summary>
        /// <param name="lstMod_DT_Ky_DaiLy"></param>
        /// <param name="objParent"></param>
        /// <param name="lstMod_DT_CapDaiLy_TyLe"></param>
        /// <param name="iCapHoaHong"></param>
        /// <returns></returns>
        private double getTienHoaHong(ref List<Mod_DT_Ky_DaiLy> lstMod_DT_Ky_DaiLy,Mod_DT_Ky_DaiLy objParent, 
                                        List<Mod_DT_CapDaiLy_TyLe> lstMod_DT_CapDaiLy_TyLe, int iCapHoaHong)
        {
            try
            {
                if (lstMod_DT_Ky_DaiLy == null || objParent == null)
                    return 0;

                // Tiền hoa hồng
                double doubHoaHong = (getValueDouble(objParent.TongTienLayHang) * getValueDouble(lstMod_DT_CapDaiLy_TyLe[iCapHoaHong].Value)) / 100;


                // Lấy các đại lý con: Tính hoa hồng
                List<Mod_DT_Ky_DaiLy> lstMod_DT_Ky_DaiLy_Child = lstMod_DT_Ky_DaiLy
                                        .Where(o => o.ModProductAgentParentId != null && o.ModProductAgentParentId == objParent.ModProductAgentId)
                                        .ToList();
                // Không còn con
                if (lstMod_DT_Ky_DaiLy_Child == null || lstMod_DT_Ky_DaiLy_Child.Count <= 0)
                    return doubHoaHong;

                // Duyệt các đại lý con để tính hoa hồng
                foreach (var itemChild in lstMod_DT_Ky_DaiLy_Child)
                {
                    doubHoaHong += getTienHoaHong(ref lstMod_DT_Ky_DaiLy, itemChild, lstMod_DT_CapDaiLy_TyLe, iCapHoaHong + 1);
                }

                //// Lưu lại tổng tiền hoa hồng
                //objParent.TongTienHoaHong = doubHoaHong;
                //// Tổng tiền thu nhập cuối kỳ
                //objParent.TotalLast = getValueDouble(objParent.TotalFirst) + doubHoaHong;

                return doubHoaHong;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// Lấy giá trị double value
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        private double getValueDouble(object objData)
        {
            if (objData == null)
                return 0;

            try
            {
                return (double)objData;
            }
            catch
            {
                return 0;
            }

        }
    }

    public class ModDT_KyModel : DefaultModel
    {
        public string SearchText { get; set; }
        public int DaChotKy { get; set; }
    }
}

