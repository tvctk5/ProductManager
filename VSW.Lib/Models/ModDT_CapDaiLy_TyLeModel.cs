using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModDT_CapDaiLy_TyLeEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int? ParentID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public int Type { get; set; }

        [DataInfo]
        public double Value { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

        public ModDT_CapDaiLy_TyLeEntity getParent()
        {
            return ModDT_CapDaiLy_TyLeService.Instance.CreateQuery()
               .Where(o => o.ID == ParentID)
               .ToSingle();
        }

    }

    public class ModDT_CapDaiLy_TyLeService : ServiceBase<ModDT_CapDaiLy_TyLeEntity>
    {

        #region Autogen by VSW

        private ModDT_CapDaiLy_TyLeService()
            : base("[Mod_DT_CapDaiLy_TyLe]")
        {

        }

        private static ModDT_CapDaiLy_TyLeService _Instance = null;
        public static ModDT_CapDaiLy_TyLeService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModDT_CapDaiLy_TyLeService();

                return _Instance;
            }
        }

        #endregion

        public ModDT_CapDaiLy_TyLeEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
    }
}