using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class CPUserLogEntity :EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int UserID { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public string Note { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }

        #endregion

        private CPUserEntity _oUser = null;
        public CPUserEntity getUser()
        {
            if (_oUser == null && UserID > 0)
                _oUser = CPUserService.Instance.GetByID(UserID);

            return _oUser;
        }
    }

    public class CPUserLogService : ServiceBase<CPUserLogEntity>
    {
        #region Autogen by VSW

        public CPUserLogService()
            : base("[CP_UserLog]")
        {

        }

        private static CPUserLogService _Instance = null;
        public static CPUserLogService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CPUserLogService();

                return _Instance;
            }
        }

        #endregion
    }
}
