using System;

using VSW.Core.Interface;

namespace VSW.Lib.Models
{
    public class SysModuleService : IModuleServiceInterface
    {
        private static SysModuleService _Instance = null;
        public static SysModuleService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SysModuleService();

                return _Instance;
            }
        }

        public IModuleInterface VSW_Core_GetByCode(string code)
        {
           return VSW.Lib.Web.Application.Modules.Find(o => o.Code == code);
        }
    }
}
