using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModTestEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Column1 { get; set; }

        [DataInfo]
        public string Column2 { get; set; }

        #endregion

    }

    public class ModTestService : ServiceBase<ModTestEntity>
    {

        #region Autogen by VSW

        private ModTestService()
            : base("[Mod_Test]")
        {

        }

        private static ModTestService _Instance = null;
        public static ModTestService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModTestService();

                return _Instance;
            }
        }

        #endregion

        public ModTestEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == 1)
               .ToSingle();
        }

    }
}