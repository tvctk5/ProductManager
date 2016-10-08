using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_PaymentEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Note { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_PaymentService : ServiceBase<ModProduct_PaymentEntity>
    {

        #region Autogen by VSW

        private ModProduct_PaymentService()
            : base("[Mod_Product_Payment]")
        {

        }

        private static ModProduct_PaymentService _Instance = null;
        public static ModProduct_PaymentService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_PaymentService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_PaymentEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}