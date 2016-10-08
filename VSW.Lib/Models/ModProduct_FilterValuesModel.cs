using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_FilterValuesEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductFilterId { get; set; }

        [DataInfo]
        public string Value { get; set; }

        #endregion

    }

    public class ModProduct_FilterValuesService : ServiceBase<ModProduct_FilterValuesEntity>
    {

        #region Autogen by VSW

        private ModProduct_FilterValuesService()
            : base("[Mod_Product_FilterValues]")
        {

        }

        private static ModProduct_FilterValuesService _Instance = null;
        public static ModProduct_FilterValuesService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_FilterValuesService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_FilterValuesEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}