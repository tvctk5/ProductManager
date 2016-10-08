using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Price_HistoryEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int UserId { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public bool Type { get; set; }

        [DataInfo]
        public double BeforePrice { get; set; }

        [DataInfo]
        public double AfterPrice { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_Price_HistoryService : ServiceBase<ModProduct_Price_HistoryEntity>
    {

        #region Autogen by VSW

        private ModProduct_Price_HistoryService()
            : base("[Mod_Product_Price_History]")
        {

        }

        private static ModProduct_Price_HistoryService _Instance = null;
        public static ModProduct_Price_HistoryService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Price_HistoryService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Price_HistoryEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}