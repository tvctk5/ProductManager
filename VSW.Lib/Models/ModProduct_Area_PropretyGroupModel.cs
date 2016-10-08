using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Area_PropretyGroupEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductAreaId { get; set; }

        [DataInfo]
        public int PropertiesGroupId { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_Area_PropretyGroupService : ServiceBase<ModProduct_Area_PropretyGroupEntity>
    {

        #region Autogen by VSW

        private ModProduct_Area_PropretyGroupService()
            : base("[Mod_Product_Area_PropretyGroup]")
        {

        }

        private static ModProduct_Area_PropretyGroupService _Instance = null;
        public static ModProduct_Area_PropretyGroupService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Area_PropretyGroupService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Area_PropretyGroupEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}