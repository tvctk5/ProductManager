using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Order_DetailsEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int OrderId { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int LangID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public int Quantity { get; set; }

        [DataInfo]
        public double FriceInput { get; set; }

        [DataInfo]
        public double Frice { get; set; }

        [DataInfo]
        public double PriceSale { get; set; }

        [DataInfo]
        public bool ShowVAT { get; set; }

        [DataInfo]
        public bool VAT { get; set; }

        [DataInfo]
        public bool SaleOffType { get; set; }

        [DataInfo]
        public string PriceTextSaleView { get; set; }

        [DataInfo]
        public double TotalFrice { get; set; }

        [DataInfo]
        public string Gifts { get; set; }

        [DataInfo]
        public bool Attach { get; set; }

        [DataInfo]
        public string Note { get; set; }

        #endregion

    }

    public class ModProduct_Order_DetailsService : ServiceBase<ModProduct_Order_DetailsEntity>
    {

        #region Autogen by VSW

        private ModProduct_Order_DetailsService()
            : base("[Mod_Product_Order_Details]")
        {

        }

        private static ModProduct_Order_DetailsService _Instance = null;
        public static ModProduct_Order_DetailsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Order_DetailsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Order_DetailsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}