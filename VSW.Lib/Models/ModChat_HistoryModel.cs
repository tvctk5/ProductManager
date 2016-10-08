using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModChat_HistoryEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string From_Name { get; set; }

        [DataInfo]
        public string From_UserName { get; set; }

        [DataInfo]
        public int From_Id { get; set; }

        [DataInfo]
        public string To_Name { get; set; }

        [DataInfo]
        public string To_UserName { get; set; }

        [DataInfo]
        public int To_Id { get; set; }

        [DataInfo]
        public string Message { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModChat_HistoryService : ServiceBase<ModChat_HistoryEntity>
    {

        #region Autogen by VSW

        private ModChat_HistoryService()
            : base("[Mod_Chat_History]")
        {

        }

        private static ModChat_HistoryService _Instance = null;
        public static ModChat_HistoryService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModChat_HistoryService();

                return _Instance;
            }
        }

        #endregion

        public ModChat_HistoryEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}