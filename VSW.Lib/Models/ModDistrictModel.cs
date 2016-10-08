using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModDistrictEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModProvinceId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Type { get; set; }

        [DataInfo]
        public string FullName { get; set; }

        [DataInfo]
        public string ProvinceFullName { get; set; }

        [DataInfo]
        public string Location { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModDistrictService : ServiceBase<ModDistrictEntity>
    {

        #region Autogen by VSW

        private ModDistrictService()
            : base("[Mod_District]")
        {

        }

        private static ModDistrictService _Instance = null;
        public static ModDistrictService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModDistrictService();

                return _Instance;
            }
        }

        #endregion

        public ModDistrictEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}