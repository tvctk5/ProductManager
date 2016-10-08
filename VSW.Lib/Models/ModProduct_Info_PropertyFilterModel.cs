using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_PropertyFilterEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int ProductFilterId { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_Info_PropertyFilterService : ServiceBase<ModProduct_Info_PropertyFilterEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_PropertyFilterService()
            : base("[Mod_Product_Info_PropertyFilter]")
        {

        }

        private static ModProduct_Info_PropertyFilterService _Instance = null;
        public static ModProduct_Info_PropertyFilterService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_PropertyFilterService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_PropertyFilterEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}