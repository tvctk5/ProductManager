using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_ColorEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public string ColorCode { get; set; }

        [DataInfo]
        public string ColorName { get; set; }

        [DataInfo]
        public int CountNumber { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_Info_ColorService : ServiceBase<ModProduct_Info_ColorEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_ColorService()
            : base("[Mod_Product_Info_Color]")
        {

        }

        private static ModProduct_Info_ColorService _Instance = null;
        public static ModProduct_Info_ColorService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_ColorService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_ColorEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}