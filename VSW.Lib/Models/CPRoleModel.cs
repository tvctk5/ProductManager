using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class CPRoleEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public bool Lock { get; set; }

        [DataInfo]
        public int Order { get; set; }

        #endregion
    }

    public class CPRoleService : ServiceBase<CPRoleEntity>
    {
        #region Autogen by VSW

        public CPRoleService()
            : base("[CP_Role]")
        {

        }

        private static CPRoleService _Instance = null;
        public static CPRoleService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CPRoleService();

                return _Instance;
            }
        }

        #endregion

        public CPRoleEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public List<CPRoleEntity> GetByUserID(int user_id)
        {
            return base.CreateQuery()
                .WhereIn(o => o.ID, CPUserRoleService.Instance.CreateQuery()
                                                      .Select(o => o.RoleID)
                                                      .Where(o => o.UserID == user_id)
                        )
                .ToList();
        }
    }
}
