using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModDT_Ky_DaiLy_DonHang_SanPhamEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModDtKyId { get; set; }

        [DataInfo]
        public int ModDTKyDaiLyId { get; set; }

        [DataInfo]
        public int ModDTKyDaiLyDonHangId { get; set; }

        [DataInfo]
        public int ModProductId { get; set; }

        [DataInfo]
        public int SoLuong { get; set; }

        [DataInfo]
        public double DonGia { get; set; }

        [DataInfo]
        public double ChietKhau { get; set; }

        [DataInfo]
        public double TongTien { get; set; }

        [DataInfo]
        public double TongSauGiam { get; set; }

        #endregion

    }

    public class ModDT_Ky_DaiLy_DonHang_SanPhamService : ServiceBase<ModDT_Ky_DaiLy_DonHang_SanPhamEntity>
    {

        #region Autogen by VSW

        private ModDT_Ky_DaiLy_DonHang_SanPhamService()
            : base("[Mod_DT_Ky_DaiLy_DonHang_SanPham]")
        {

        }

        private static ModDT_Ky_DaiLy_DonHang_SanPhamService _Instance = null;
        public static ModDT_Ky_DaiLy_DonHang_SanPhamService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModDT_Ky_DaiLy_DonHang_SanPhamService();

                return _Instance;
            }
        }

        #endregion

        public ModDT_Ky_DaiLy_DonHang_SanPhamEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}