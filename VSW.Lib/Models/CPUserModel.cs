using System;
using System.Collections.Generic;

using VSW.Core.Models;
using VSW.Core.Interface;
using VSW.Lib.Global;

namespace VSW.Lib.Models
{
    public class CPUserEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string LoginName { get; set; }

        [DataInfo]
        public string Password { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Address { get; set; }

        [DataInfo]
        public string Phone { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion


        List<CPRoleEntity> _GetRole = null;
        public List<CPRoleEntity> GetRole()
        {
            if (_GetRole == null)
                _GetRole = CPRoleService.Instance.GetByUserID(ID);

            if (_GetRole == null)
                _GetRole = new List<CPRoleEntity>();

            return _GetRole;
        }

        public bool HasRole(string role_code)
        {
            return GetRole().Find(o => o.Code == role_code) != null;
        }

        public bool HasRoleAdministrator()
        {
            return HasRole("Administrator");
        }

        public bool IsCPDesign
        {
            get { return IsAdministrator; }
        }

        public bool IsAdministrator
        {
            get { return GetPermissionsByModule("SysAdministrator").Any; }
        }

        private Dictionary<string, Permissions> _dicModulePermissions = null;
        public Permissions GetPermissionsByModule(string module_code)
        {
            if (_dicModulePermissions == null)
                _dicModulePermissions = new Dictionary<string, Permissions>();

            if (_dicModulePermissions.ContainsKey(module_code))
                return _dicModulePermissions[module_code];

            return GetPermissionsByRef("CP.MODULE", module_code);
        }

        private Dictionary<string, Permissions> _dicRefPermissions = null;
        public Permissions GetPermissionsByRef(string type, string module_code)
        {
            if (_dicRefPermissions == null)
                _dicRefPermissions = new Dictionary<string, Permissions>();

            string cache_key = type + "." + module_code;

            if (_dicRefPermissions.ContainsKey(cache_key))
                return _dicRefPermissions[cache_key];

            Permissions _Permissions = new Permissions();

            CPAccessEntity _Access = CPAccessService.Instance.GetByUser(type, module_code, ID);

            if (_Access != null)
                _Permissions = new Permissions(_Access.Value);

            if (_Permissions.Full)
            {
                _dicRefPermissions[cache_key] = _Permissions;

                return _Permissions;
            }

            List<CPRoleEntity> _GetRole = GetRole();
            for (int i = 0; _GetRole != null && i < _GetRole.Count; i++)
            {
                _Access = CPAccessService.Instance.GetByRole(type, module_code, _GetRole[i].ID);

                if (_Access != null)
                    _Permissions.ORAccess(_Access.Value);

                if (_Permissions.Full)
                    break;
            }

            _dicRefPermissions[cache_key] = _Permissions;

            return _Permissions;
        }
    }

    public class CPUserService : ServiceBase<CPUserEntity>
    {
        #region Autogen by VSW

        public CPUserService()
            : base("[CP_User]")
        {

        }

        private static CPUserService _Instance = null;
        public static CPUserService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CPUserService();

                return _Instance;
            }
        }

        #endregion

        public CPUserEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public CPUserEntity GetByEmail(string email)
        {
            return base.CreateQuery()
                .Where(o => o.Email == email)
                .ToSingle();
        }

        public CPUserEntity GetLogin(int id)
        {
            return base.CreateQuery()
                .Where(o => o.ID == id && o.Activity == true)
                .ToSingle();
        }

        public CPUserEntity GetLogin(string login_name, string password)
        {
            return GetLoginMd5(login_name, VSW.Lib.Global.Security.MD5(password));
        }

        public CPUserEntity GetLoginMd5(string login_name, string password)
        {
            return base.CreateQuery()
                .Where(o => o.LoginName == login_name && o.Password == password && o.Activity == true)
                .ToSingle();
        }
    }
}
