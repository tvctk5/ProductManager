using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_CustomersEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string UserName { get; set; }

        [DataInfo]
        public string Pass { get; set; }

        [DataInfo]
        public string FullName { get; set; }

        [DataInfo]
        public DateTime? Birthday { get; set; }

        [DataInfo]
        public bool Sex { get; set; }

        [DataInfo]
        public string Address { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public string PhoneNumber { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public DateTime ModifiedDate { get; set; }

        [DataInfo]
        public int PointTotal { get; set; }

        [DataInfo]
        public DateTime DateRequestReset { get; set; }

        [DataInfo]
        public string KeyReset { get; set; }

        [DataInfo]
        public DateTime DateReset { get; set; }

        [DataInfo]
        public bool Reseted { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        /// <summary>
        /// Danh sách ID của nhóm khách hàng chứa Khách hàng đang xét.
        /// Cách nhau bằng dấu ','
        /// </summary>
        public string CustomGroupInId { get; set; }

        /// <summary>
        /// Danh sách ID của nhóm khách hàng KHÔNG chứa Khách hàng đang xét.
        /// Cách nhau bằng dấu ','
        /// </summary>
        public string CustomGroupOutId { get; set; }
        #endregion

    }

    public class ModProduct_CustomersService : ServiceBase<ModProduct_CustomersEntity>
    {

        #region Autogen by VSW

        private ModProduct_CustomersService()
            : base("[Mod_Product_Customers]")
        {

        }

        private static ModProduct_CustomersService _Instance = null;
        public static ModProduct_CustomersService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_CustomersService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_CustomersEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}