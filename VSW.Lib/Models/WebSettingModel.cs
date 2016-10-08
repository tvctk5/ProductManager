using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class WebSettingEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string Value { get; set; }

        #endregion
    }

    public class WebSettingService : ServiceBase<WebSettingEntity>
    {
        #region Autogen by VSW

        public WebSettingService()
            : base("[Web_Setting]")
        {

        }

        private static WebSettingService _Instance = null;
        public static WebSettingService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new WebSettingService();

                return _Instance;
            }
        }

        #endregion
    }
}
