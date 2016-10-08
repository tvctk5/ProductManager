using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_National_AreaEntity : EntityBase
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

    public class ModProduct_National_AreaService : ServiceBase<ModProduct_National_AreaEntity>
    {

        #region Autogen by VSW

        private ModProduct_National_AreaService()
            : base("[Mod_Product_National_Area]")
        {

        }

        private static ModProduct_National_AreaService _Instance = null;
        public static ModProduct_National_AreaService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_National_AreaService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_National_AreaEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}