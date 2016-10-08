using System;
using System.Collections.Generic;
using System.Linq;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModDT_DaiLyEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModProductAgentParentId { get; set; }

        [DataInfo]
        public int ModProductAgentId { get; set; }

        [DataInfo]
        public int ModDTLoaiDaiLyId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string ModLoaiDaiLyCode { get; set; }

        [DataInfo]
        public string ModLoaiDaiLyName { get; set; }

        [DataInfo]
        public int ModLoaiDaiLyType { get; set; }

        [DataInfo]
        public double ModLoaiDaiLyValue { get; set; }

        [DataInfo]
        public double Transfer { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

        private ModProduct_AgentEntity _oModProduct_AgentEntity = null;
        public ModProduct_AgentEntity getModProduct_AgentParent()
        {
            if (_oModProduct_AgentEntity == null && ModProductAgentParentId > 0)
                _oModProduct_AgentEntity = ModProduct_AgentService.Instance.GetByID(ModProductAgentParentId);

            if (_oModProduct_AgentEntity == null)
                _oModProduct_AgentEntity = new ModProduct_AgentEntity();

            return _oModProduct_AgentEntity;
        }

    }

    public class ModDT_DaiLyService : ServiceBase<ModDT_DaiLyEntity>
    {

        #region Autogen by VSW

        private ModDT_DaiLyService()
            : base("[Mod_DT_DaiLy]")
        {

        }

        private static ModDT_DaiLyService _Instance = null;
        public static ModDT_DaiLyService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModDT_DaiLyService();

                return _Instance;
            }
        }

        #endregion

        public ModDT_DaiLyEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public VSW.Lib.LinqToSql.Mod_DT_Ky_DaiLy GetByIDLazy(int id)
        {
            VSW.Lib.LinqToSql.DbDataContext db = VSW.Lib.LinqToSql.DbExecute.Create(true);

            VSW.Lib.LinqToSql.Mod_DT_Ky_DaiLy objMod_DT_Ky_DaiLy =
                db.Mod_DT_Ky_DaiLies.Where(o => o.ID == id).SingleOrDefault();

            return objMod_DT_Ky_DaiLy;
        }

    }
}