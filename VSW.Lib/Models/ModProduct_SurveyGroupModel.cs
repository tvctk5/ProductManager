using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_SurveyGroupEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public DateTime StartDate { get; set; }

        [DataInfo]
        public DateTime FinishDate { get; set; }

        [DataInfo]
        public int Type { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_SurveyGroupService : ServiceBase<ModProduct_SurveyGroupEntity>
    {

        #region Autogen by VSW

        private ModProduct_SurveyGroupService()
            : base("[Mod_Product_SurveyGroup]")
        {

        }

        private static ModProduct_SurveyGroupService _Instance = null;
        public static ModProduct_SurveyGroupService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_SurveyGroupService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_SurveyGroupEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}