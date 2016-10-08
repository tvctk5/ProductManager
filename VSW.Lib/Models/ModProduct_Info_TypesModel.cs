using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_TypesEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int ProductTypesId { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_Info_TypesService : ServiceBase<ModProduct_Info_TypesEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_TypesService()
            : base("[Mod_Product_Info_Types]")
        {

        }

        private static ModProduct_Info_TypesService _Instance = null;
        public static ModProduct_Info_TypesService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_TypesService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_TypesEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}