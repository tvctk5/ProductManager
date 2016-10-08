using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class CPAccessEntity :EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public string RefCode { get; set; }

        [DataInfo]
        public int RoleID { get; set; }

        [DataInfo]
        public int UserID { get; set; }

        [DataInfo]
        public string Type { get; set; }

        [DataInfo]
        public int Value { get; set; }
       
        #endregion
    }

    public class CPAccessService : ServiceBase<CPAccessEntity>
    {
        #region Autogen by VSW

        public CPAccessService()
            : base("[CP_Access]")
        {

        }

        private static CPAccessService _Instance = null;
        public static CPAccessService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CPAccessService();

                return _Instance;
            }
        }

        #endregion


        public CPAccessEntity GetByUser(string type, string ref_code, int user_id)
        {
            return base.CreateQuery()
                .Where(o => o.UserID == user_id && o.RefCode == ref_code && o.Type == type)
                .ToSingle();
        }

        public CPAccessEntity GetByRole(string type, string ref_code, int role_id)
        {
            return base.CreateQuery()
                .Where(o => o.RoleID == role_id && o.RefCode == ref_code && o.Type == type)
                .ToSingle();
        }
    }
}
