using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_CityEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int ProductCityId { get; set; }

        #endregion

    }

    public class ModProduct_Info_CityService : ServiceBase<ModProduct_Info_CityEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_CityService()
            : base("[Mod_Product_Info_City]")
        {

        }

        private static ModProduct_Info_CityService _Instance = null;
        public static ModProduct_Info_CityService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_CityService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_CityEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}