using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Groups_PropertiesGroupsEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductGroupsId { get; set; }

        [DataInfo]
        public int PropertiesGroupsId { get; set; }

        #endregion

    }

    public class ModProduct_Groups_PropertiesGroupsService : ServiceBase<ModProduct_Groups_PropertiesGroupsEntity>
    {

        #region Autogen by VSW

        private ModProduct_Groups_PropertiesGroupsService()
            : base("[Mod_Product_Groups_PropertiesGroups]")
        {

        }

        private static ModProduct_Groups_PropertiesGroupsService _Instance = null;
        public static ModProduct_Groups_PropertiesGroupsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Groups_PropertiesGroupsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Groups_PropertiesGroupsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}