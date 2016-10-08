using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_ProductGroupsEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int MenuID { get; set; }
        #endregion

    }

    public class ModProduct_Info_ProductGroupsService : ServiceBase<ModProduct_Info_ProductGroupsEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_ProductGroupsService()
            : base("[Mod_Product_Info_ProductGroups]")
        {

        }

        private static ModProduct_Info_ProductGroupsService _Instance = null;
        public static ModProduct_Info_ProductGroupsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_ProductGroupsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_ProductGroupsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}