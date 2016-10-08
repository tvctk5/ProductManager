using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProvinceEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModCountryId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Type { get; set; }

        [DataInfo]
        public string FullName { get; set; }

        [DataInfo]
        public string ModCountryName { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProvinceService : ServiceBase<ModProvinceEntity>
    {

        #region Autogen by VSW

        private ModProvinceService()
            : base("[Mod_Province]")
        {

        }

        private static ModProvinceService _Instance = null;
        public static ModProvinceService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProvinceService();

                return _Instance;
            }
        }

        #endregion

        public ModProvinceEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}