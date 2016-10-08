using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_AreaInNationalEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int ProductNationalAreaId { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_Info_AreaInNationalService : ServiceBase<ModProduct_Info_AreaInNationalEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_AreaInNationalService()
            : base("[Mod_Product_Info_AreaInNational]")
        {

        }

        private static ModProduct_Info_AreaInNationalService _Instance = null;
        public static ModProduct_Info_AreaInNationalService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_AreaInNationalService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_AreaInNationalEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}