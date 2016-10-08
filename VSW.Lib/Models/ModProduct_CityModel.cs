using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_CityEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductNationalId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_CityService : ServiceBase<ModProduct_CityEntity>
    {

        #region Autogen by VSW

        private ModProduct_CityService()
            : base("[Mod_Product_City]")
        {

        }

        private static ModProduct_CityService _Instance = null;
        public static ModProduct_CityService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_CityService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_CityEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}