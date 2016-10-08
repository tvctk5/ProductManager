using System;
using System.Collections.Generic;
using System.Linq;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModDT_Ky_DaiLy_DonHangEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModDtKyId { get; set; }

        [DataInfo]
        public int ModDTKyDaiLyId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public DateTime NgayTao { get; set; }

        [DataInfo]
        public double ChietKhau { get; set; }

        [DataInfo]
        public int TongSanPham { get; set; }

        [DataInfo]
        public double TongTien { get; set; }

        [DataInfo]
        public double TongSauGiam { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModDT_Ky_DaiLy_DonHangService : ServiceBase<ModDT_Ky_DaiLy_DonHangEntity>
    {

        #region Autogen by VSW

        private ModDT_Ky_DaiLy_DonHangService()
            : base("[Mod_DT_Ky_DaiLy_DonHang]")
        {

        }

        private static ModDT_Ky_DaiLy_DonHangService _Instance = null;
        public static ModDT_Ky_DaiLy_DonHangService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModDT_Ky_DaiLy_DonHangService();

                return _Instance;
            }
        }

        #endregion

        public ModDT_Ky_DaiLy_DonHangEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public VSW.Lib.LinqToSql.Mod_DT_Ky_DaiLy_DonHang GetByIDLazy(int id)
        {
            VSW.Lib.LinqToSql.DbDataContext db = VSW.Lib.LinqToSql.DbExecute.Create(true);

            VSW.Lib.LinqToSql.Mod_DT_Ky_DaiLy_DonHang objMod_DT_Ky_DaiLy_DonHang = 
                db.Mod_DT_Ky_DaiLy_DonHangs.Where(o => o.ID == id).SingleOrDefault();

            return objMod_DT_Ky_DaiLy_DonHang;
        }

    }
}