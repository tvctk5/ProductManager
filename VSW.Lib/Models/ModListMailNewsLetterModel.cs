using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModListMailNewsLetterEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public bool Sex { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public string CodeRemoveList { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModListMailNewsLetterService : ServiceBase<ModListMailNewsLetterEntity>
    {

        #region Autogen by VSW

        private ModListMailNewsLetterService()
            : base("[Mod_ListMailNewsLetter]")
        {

        }

        private static ModListMailNewsLetterService _Instance = null;
        public static ModListMailNewsLetterService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModListMailNewsLetterService();

                return _Instance;
            }
        }

        #endregion

        public ModListMailNewsLetterEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}