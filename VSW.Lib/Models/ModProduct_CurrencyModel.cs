using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_CurrencyEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public double VND { get; set; }

        [DataInfo]
        public string Description { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_CurrencyService : ServiceBase<ModProduct_CurrencyEntity>
    {

        #region Autogen by VSW

        private ModProduct_CurrencyService()
            : base("[Mod_Product_Currency]")
        {

        }

        private static ModProduct_CurrencyService _Instance = null;
        public static ModProduct_CurrencyService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_CurrencyService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_CurrencyEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// 
        /// CanTV       2012/09/21      Tạo mới
        /// </summary>
        /// <param name="sCode">Mã tỷ giá</param>
        /// <param name="IdUpdate">Id bản ghi: Nếu thêm mới thì Id=0</param>
        /// <returns>True: Nếu Duplicate | False: nếu không Duplicate</returns>
        public bool DuplicateCode(string sCode, int IdUpdate, ref string sMess)
        {
            try
            {
                // Có mã trùng
                List<ModProduct_CurrencyEntity> lstCurrencyEntity =
                base.CreateQuery()
                        .Where(o => o.ID != IdUpdate && o.Code == sCode)
                        .ToList();

                if (lstCurrencyEntity == null)
                    return false;

                if (lstCurrencyEntity.Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                sMess = ex.ToString();
                return true;
            }
        }
    }
}