using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_OfficeEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int ProductOfficeId { get; set; }

        [DataInfo]
        public int CountNumber { get; set; }

        #endregion

    }

    public class ModProduct_Info_OfficeService : ServiceBase<ModProduct_Info_OfficeEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_OfficeService()
            : base("[Mod_Product_Info_Office]")
        {

        }

        private static ModProduct_Info_OfficeService _Instance = null;
        public static ModProduct_Info_OfficeService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_OfficeService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_OfficeEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}