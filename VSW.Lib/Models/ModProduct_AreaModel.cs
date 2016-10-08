using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_AreaEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public bool Activity { get; set; }
        
        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_AreaService : ServiceBase<ModProduct_AreaEntity>
    {

        #region Autogen by VSW

        private ModProduct_AreaService()
            : base("[Mod_Product_Area]")
        {

        }

        private static ModProduct_AreaService _Instance = null;
        public static ModProduct_AreaService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_AreaService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_AreaEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}