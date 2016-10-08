using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModMenu_TypeEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModMenu_TypeService : ServiceBase<ModMenu_TypeEntity>
    {

        #region Autogen by VSW

        private ModMenu_TypeService()
            : base("[Mod_Menu_Type]")
        {

        }

        private static ModMenu_TypeService _Instance = null;
        public static ModMenu_TypeService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModMenu_TypeService();

                return _Instance;
            }
        }

        #endregion

        public ModMenu_TypeEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
    }
}