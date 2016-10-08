using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModFeedbackEntity :EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Address { get; set; }

        [DataInfo]
        public string Phone { get; set; }

        [DataInfo]
        public string Mobile { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public string Title { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }

        #endregion
    }

    public class ModFeedbackService : ServiceBase<ModFeedbackEntity>
    {
        #region Autogen by VSW

        public ModFeedbackService()
            : base("[Mod_Feedback]")
        {

        }

        private static ModFeedbackService _Instance = null;
        public static ModFeedbackService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModFeedbackService();

                return _Instance;
            }
        }

        #endregion

        public ModFeedbackEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
    }
}
