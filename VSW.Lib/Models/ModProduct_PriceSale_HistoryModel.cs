using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_PriceSale_HistoryEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int UserId { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public double Price { get; set; }

        [DataInfo]
        public bool SaleOffType { get; set; }

        [DataInfo]
        public double SaleOffValue { get; set; }

        [DataInfo]
        public double PriceSale { get; set; }

        [DataInfo]
        public DateTime? StartDate { get; set; }

        [DataInfo]
        public DateTime? FinishDate { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_PriceSale_HistoryService : ServiceBase<ModProduct_PriceSale_HistoryEntity>
    {

        #region Autogen by VSW

        private ModProduct_PriceSale_HistoryService()
            : base("[Mod_Product_PriceSale_History]")
        {

        }

        private static ModProduct_PriceSale_HistoryService _Instance = null;
        public static ModProduct_PriceSale_HistoryService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_PriceSale_HistoryService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_PriceSale_HistoryEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}