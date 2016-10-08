using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_RegisterEmailEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string FullName { get; set; }

        [DataInfo]
        public int Sex { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public bool Allow { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_RegisterEmailService : ServiceBase<ModProduct_RegisterEmailEntity>
    {

        #region Autogen by VSW

        private ModProduct_RegisterEmailService()
            : base("[Mod_Product_RegisterEmail]")
        {

        }

        private static ModProduct_RegisterEmailService _Instance = null;
        public static ModProduct_RegisterEmailService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_RegisterEmailService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_RegisterEmailEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}