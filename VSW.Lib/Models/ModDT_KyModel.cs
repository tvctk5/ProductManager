using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModDT_KyEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public DateTime StartDate { get; set; }

        [DataInfo]
        public DateTime FinishDate { get; set; }

        [DataInfo]
        public double TotalFirst { get; set; }

        [DataInfo]
        public double TotalLast { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime UpdateDate { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModDT_KyService : ServiceBase<ModDT_KyEntity>
    {

        #region Autogen by VSW

        private ModDT_KyService()
            : base("[Mod_DT_Ky]")
        {

        }

        private static ModDT_KyService _Instance = null;
        public static ModDT_KyService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModDT_KyService();

                return _Instance;
            }
        }

        #endregion

        public ModDT_KyEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}