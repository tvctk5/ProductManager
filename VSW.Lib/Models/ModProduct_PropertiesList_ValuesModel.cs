using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_PropertiesList_ValuesEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int PropertiesListId { get; set; }

        [DataInfo]
        public string Content { get; set; }

        #endregion

    }

    public class ModProduct_PropertiesList_ValuesService : ServiceBase<ModProduct_PropertiesList_ValuesEntity>
    {

        #region Autogen by VSW

        private ModProduct_PropertiesList_ValuesService()
            : base("[Mod_Product_PropertiesList_Values]")
        {

        }

        private static ModProduct_PropertiesList_ValuesService _Instance = null;
        public static ModProduct_PropertiesList_ValuesService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_PropertiesList_ValuesService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_PropertiesList_ValuesEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}