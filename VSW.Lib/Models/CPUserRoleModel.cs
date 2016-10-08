using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class CPUserRoleEntity :EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public int UserID { get; set; }

        [DataInfo]
        public int RoleID { get; set; }

        #endregion
    }

    public class CPUserRoleService : ServiceBase<CPUserRoleEntity>
    {
        #region Autogen by VSW

        public CPUserRoleService()
            : base("[CP_UserRole]")
        {

        }

        private static CPUserRoleService _Instance = null;
        public static CPUserRoleService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CPUserRoleService();

                return _Instance;
            }
        }

        #endregion
    }
}
