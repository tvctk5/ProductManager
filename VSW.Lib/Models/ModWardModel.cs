using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModWardEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModDistrictId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Type { get; set; }

        [DataInfo]
        public string FullName { get; set; }

        [DataInfo]
        public string DistrictFullName { get; set; }

        [DataInfo]
        public string Location { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModWardService : ServiceBase<ModWardEntity>
    {

        #region Autogen by VSW

        private ModWardService()
            : base("[Mod_Ward]")
        {

        }

        private static ModWardService _Instance = null;
        public static ModWardService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModWardService();

                return _Instance;
            }
        }

        #endregion

        public ModWardEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}