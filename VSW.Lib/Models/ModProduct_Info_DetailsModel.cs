using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_DetailsEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int PropertiesGroupsId { get; set; }

        [DataInfo]
        public int PropertiesListId { get; set; }
        
        [DataInfo]
        public string Content { get; set; }
        #endregion

    }

    public class ModProduct_Info_DetailsService : ServiceBase<ModProduct_Info_DetailsEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_DetailsService()
            : base("[Mod_Product_Info_Details]")
        {

        }

        private static ModProduct_Info_DetailsService _Instance = null;
        public static ModProduct_Info_DetailsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_DetailsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_DetailsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public ModProduct_Info_DetailsEntity GetByProductID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ProductInfoId == id)
               .ToSingle();
        }
    }
}