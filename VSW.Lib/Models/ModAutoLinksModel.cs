using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModAutoLinksEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Title { get; set; }

        [DataInfo]
        public string Link { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModAutoLinksService : ServiceBase<ModAutoLinksEntity>
    {

        #region Autogen by VSW

        private ModAutoLinksService()
            : base("[Mod_AutoLinks]")
        {

        }

        private static ModAutoLinksService _Instance = null;
        public static ModAutoLinksService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModAutoLinksService();

                return _Instance;
            }
        }

        #endregion

        public ModAutoLinksEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}