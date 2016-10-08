using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_OrderEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int TransportId { get; set; }

        [DataInfo]
        public int PaymentId { get; set; }

        [DataInfo]
        public string CustomersCode { get; set; }

        [DataInfo]
        public int CustomersId { get; set; }

        [DataInfo]
        public int UserId { get; set; }

        [DataInfo]
        public int UserModifiedId { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public int QuantityProduct { get; set; }

        [DataInfo]
        public int QuantityTotal { get; set; }

        [DataInfo]
        public double Discount { get; set; }

        [DataInfo]
        public double TotalFriceFirst { get; set; }

        [DataInfo]
        public double TotalFrice { get; set; }

        [DataInfo]
        public string Note { get; set; }

        [DataInfo]
        public int Status { get; set; }

        [DataInfo]
        public string NguoiDat_FullName { get; set; }

        [DataInfo]
        public bool NguoiDat_Sex { get; set; }

        [DataInfo]
        public string NguoiDat_Address { get; set; }

        [DataInfo]
        public string NguoiDat_Email { get; set; }

        [DataInfo]
        public string NguoiDat_PhoneNumber { get; set; }

        [DataInfo]
        public string NguoiNhan_FullName { get; set; }

        [DataInfo]
        public bool NguoiNhan_Sex { get; set; }

        [DataInfo]
        public string NguoiNhan_Address { get; set; }

        [DataInfo]
        public string NguoiNhan_Email { get; set; }

        [DataInfo]
        public string NguoiNhan_PhoneNumber { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public DateTime ModifiedDate { get; set; }

        #endregion

    }

    public class ModProduct_OrderService : ServiceBase<ModProduct_OrderEntity>
    {

        #region Autogen by VSW

        private ModProduct_OrderService()
            : base("[Mod_Product_Order]")
        {

        }

        private static ModProduct_OrderService _Instance = null;
        public static ModProduct_OrderService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_OrderService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_OrderEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}