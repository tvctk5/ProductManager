using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_SurveyGroup_DetailEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int SurveyGroupId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public int Vote { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_SurveyGroup_DetailService : ServiceBase<ModProduct_SurveyGroup_DetailEntity>
    {

        #region Autogen by VSW

        private ModProduct_SurveyGroup_DetailService()
            : base("[Mod_Product_SurveyGroup_Detail]")
        {

        }

        private static ModProduct_SurveyGroup_DetailService _Instance = null;
        public static ModProduct_SurveyGroup_DetailService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_SurveyGroup_DetailService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_SurveyGroup_DetailEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}