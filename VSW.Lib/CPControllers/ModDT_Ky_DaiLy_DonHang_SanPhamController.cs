using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Doanh thu - kỳ - đại lý - đơn hàng - sản phẩm",
        Description = "Quản lý - Doanh thu - kỳ - đại lý - đơn hàng - sản phẩm", 
        Code = "ModDT_Ky_DaiLy_DonHang_SanPham", 
        Access = 31, 
        Order = 204, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 5)]
    public class ModDT_Ky_DaiLy_DonHang_SanPhamController : CPController
    {
        public ModDT_Ky_DaiLy_DonHang_SanPhamController()
        {
            //khoi tao Service
            DataService = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModDT_Ky_DaiLy_DonHang_SanPhamModel model)
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
            var dbQuery = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                                .Where(o => o.ModDtKyId == (model.ModDtKyId))
                                .Where(model.ModDTKyDaiLyId > 0, o => o.ModDTKyDaiLyId == (model.ModDTKyDaiLyId))                
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);


            var dbExec = Lib.LinqToSql.DbExecute.Create(true);
            var DataExec = (from o in dbExec.Mod_DT_Ky_DaiLy_DonHang_SanPhams
                           where o.ModDtKyId == model.ModDtKyId && (model.ModDTKyDaiLyId==0 || o.ModDTKyDaiLyId == model.ModDTKyDaiLyId)
                           orderby o.ID descending 
                            select o)
                           .Take(model.PageSize)
                           .Skip(model.PageIndex * model.PageSize).ToList();

            ViewBag.Data = dbQuery.ToList();
            ViewBag.DataLazy = DataExec;
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModDT_Ky_DaiLy_DonHang_SanPhamModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModDT_Ky_DaiLy_DonHang_SanPhamEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModDT_Ky_DaiLy_DonHang_SanPhamModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModDT_Ky_DaiLy_DonHang_SanPhamModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModDT_Ky_DaiLy_DonHang_SanPhamModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModDT_Ky_DaiLy_DonHang_SanPhamEntity item = null;

        private bool ValidSave(ModDT_Ky_DaiLy_DonHang_SanPhamModel model)
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
                    ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.Save(item);
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

    public class ModDT_Ky_DaiLy_DonHang_SanPhamModel : DefaultModel
    {
        public int ModDtKyId { get; set; }
        public int ModDTKyDaiLyId { get; set; }
        public int DaChotKy { get; set; }
        public int ModDTKyDaiLyDonHangId { get; set; }
        
    }
}

