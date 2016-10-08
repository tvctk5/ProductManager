using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_Info_SizeEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public string SizeCode { get; set; }

        [DataInfo]
        public string SizeName { get; set; }

        [DataInfo]
        public int CountNumber { get; set; }

        [DataInfo]
        public double Price { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_Info_SizeService : ServiceBase<ModProduct_Info_SizeEntity>
    {

        #region Autogen by VSW

        private ModProduct_Info_SizeService()
            : base("[Mod_Product_Info_Size]")
        {

        }

        private static ModProduct_Info_SizeService _Instance = null;
        public static ModProduct_Info_SizeService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_Info_SizeService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_Info_SizeEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}