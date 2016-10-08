using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModDT_Ky_DaiLyEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModDtKyId { get; set; }

        [DataInfo]
        public int? ModProductAgentParentId { get; set; }

        [DataInfo]
        public int ModProductAgentId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public int Type { get; set; }

        [DataInfo]
        public double Value { get; set; }

        [DataInfo]
        public double TotalFirst { get; set; }

        [DataInfo]
        public double TotalLast { get; set; }

        [DataInfo]
        public double TongTienLayHang { get; set; }

        [DataInfo]
        public double TongTienHoaHong { get; set; }

        [DataInfo]
        public string ModLoaiDaiLyCode { get; set; }

        [DataInfo]
        public string ModLoaiDaiLyName { get; set; }

        [DataInfo]
        public int ModLoaiDaiLyType { get; set; }

        [DataInfo]
        public double ModLoaiDaiLyValue { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion
        private ModDT_KyEntity _oModDT_KyEntity = null;
        public ModDT_KyEntity getModDT_KyEntity()
        {
            if (_oModDT_KyEntity == null && ModDtKyId > 0)
                _oModDT_KyEntity = ModDT_KyService.Instance.GetByID(ModDtKyId);

            if (_oModDT_KyEntity == null)
                _oModDT_KyEntity = new ModDT_KyEntity();

            return _oModDT_KyEntity;
        }

        private ModProduct_AgentEntity _oModProduct_AgentEntity = null;
        public ModProduct_AgentEntity getModProduct_AgentParent()
        {
            if (_oModProduct_AgentEntity == null && ModProductAgentParentId > 0)
                _oModProduct_AgentEntity = ModProduct_AgentService.Instance.GetByID(ModProductAgentParentId.HasValue?ModProductAgentParentId.Value : 0);

            if (_oModProduct_AgentEntity == null)
                _oModProduct_AgentEntity = new ModProduct_AgentEntity();

            return _oModProduct_AgentEntity;
        }

    }

    public class ModDT_Ky_DaiLyService : ServiceBase<ModDT_Ky_DaiLyEntity>
    {

        #region Autogen by VSW

        private ModDT_Ky_DaiLyService()
            : base("[Mod_DT_Ky_DaiLy]")
        {

        }

        private static ModDT_Ky_DaiLyService _Instance = null;
        public static ModDT_Ky_DaiLyService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModDT_Ky_DaiLyService();

                return _Instance;
            }
        }

        #endregion

        public ModDT_Ky_DaiLyEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}