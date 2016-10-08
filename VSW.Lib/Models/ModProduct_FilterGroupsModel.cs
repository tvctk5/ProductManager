using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_FilterGroupsEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public int Type { get; set; }

        [DataInfo]
        public bool ShowControl { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_FilterGroupsService : ServiceBase<ModProduct_FilterGroupsEntity>
    {

        #region Autogen by VSW

        private ModProduct_FilterGroupsService()
            : base("[Mod_Product_FilterGroups]")
        {

        }

        private static ModProduct_FilterGroupsService _Instance = null;
        public static ModProduct_FilterGroupsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_FilterGroupsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_FilterGroupsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}