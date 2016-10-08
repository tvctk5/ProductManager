using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Customers_GroupsEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int CustomersGroupsId { get; set; }

        [DataInfo]
        public int CustomersId { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_Customers_GroupsService : ServiceBase<ModProduct_Customers_GroupsEntity>
    {

        #region Autogen by VSW

        private ModProduct_Customers_GroupsService()
            : base("[Mod_Product_Customers_Groups]")
        {

        }

        private static ModProduct_Customers_GroupsService _Instance = null;
        public static ModProduct_Customers_GroupsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Customers_GroupsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Customers_GroupsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}