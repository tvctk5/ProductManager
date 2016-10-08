using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_TransportEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Note { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_TransportService : ServiceBase<ModProduct_TransportEntity>
    {

        #region Autogen by VSW

        private ModProduct_TransportService()
            : base("[Mod_Product_Transport]")
        {

        }

        private static ModProduct_TransportService _Instance = null;
        public static ModProduct_TransportService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_TransportService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_TransportEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}