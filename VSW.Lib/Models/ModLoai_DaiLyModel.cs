using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModLoai_DaiLyEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

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

    }

    public class ModLoai_DaiLyService : ServiceBase<ModLoai_DaiLyEntity>
    {

        #region Autogen by VSW

        private ModLoai_DaiLyService()
            : base("[Mod_Loai_DaiLy]")
        {

        }

        private static ModLoai_DaiLyService _Instance = null;
        public static ModLoai_DaiLyService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModLoai_DaiLyService();

                return _Instance;
            }
        }

        #endregion

        public ModLoai_DaiLyEntity GetByID(int id)
        {
            if (id <= 0)
                return new ModLoai_DaiLyEntity();

            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}