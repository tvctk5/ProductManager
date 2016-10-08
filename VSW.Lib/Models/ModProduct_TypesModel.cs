using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_TypesEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

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

    public class ModProduct_TypesService : ServiceBase<ModProduct_TypesEntity>
    {

        #region Autogen by VSW

        private ModProduct_TypesService()
            : base("[Mod_Product_Types]")
        {

        }

        private static ModProduct_TypesService _Instance = null;
        public static ModProduct_TypesService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_TypesService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_TypesEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}