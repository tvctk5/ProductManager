using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_FilterEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int FilterGroupsId { get; set; }

        [DataInfo]
        public string Value { get; set; }

        [DataInfo]
        public string Note { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_FilterService : ServiceBase<ModProduct_FilterEntity>
    {

        #region Autogen by VSW

        private ModProduct_FilterService()
            : base("[Mod_Product_Filter]")
        {

        }

        private static ModProduct_FilterService _Instance = null;
        public static ModProduct_FilterService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_FilterService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_FilterEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}