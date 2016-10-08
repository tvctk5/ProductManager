using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSW.Lib.Models
{
    class Common
    {
        #region Autogen by VSW

        private static Common _Instance = null;
        public static Common Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Common();

                return _Instance;
            }
        }

        #endregion
    }
}
