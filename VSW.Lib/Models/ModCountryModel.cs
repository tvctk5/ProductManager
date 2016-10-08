using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModCountryEntity : EntityBase
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

    public class ModCountryService : ServiceBase<ModCountryEntity>
    {

        #region Autogen by VSW

        private ModCountryService()
            : base("[Mod_Country]")
        {

        }

        private static ModCountryService _Instance = null;
        public static ModCountryService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModCountryService();

                return _Instance;
            }
        }

        #endregion

        public ModCountryEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}