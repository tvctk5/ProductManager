using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModParametersEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Description { get; set; }

        [DataInfo]
        public string Value { get; set; }

        [DataInfo]
        public int Type { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public DateTime Modified { get; set; }

        #endregion

    }

    public class ModParametersService : ServiceBase<ModParametersEntity>
    {

        #region Autogen by VSW

        private ModParametersService()
            : base("[Mod_Parameters]")
        {

        }

        private static ModParametersService _Instance = null;
        public static ModParametersService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModParametersService();

                return _Instance;
            }
        }

        #endregion

        public ModParametersEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}