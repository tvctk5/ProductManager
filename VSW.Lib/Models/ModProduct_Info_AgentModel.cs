using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_AgentEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int ProductAgeId { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_Info_AgentService : ServiceBase<ModProduct_Info_AgentEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_AgentService()
            : base("[Mod_Product_Info_Agent]")
        {

        }

        private static ModProduct_Info_AgentService _Instance = null;
        public static ModProduct_Info_AgentService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_AgentService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_AgentEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}