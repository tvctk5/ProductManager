using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_SlideShowEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string UrlReSize { get; set; }

        [DataInfo]
        public string UrlFull { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion

    }

    public class ModProduct_SlideShowService : ServiceBase<ModProduct_SlideShowEntity>
    {

        #region Autogen by VSW

        private ModProduct_SlideShowService()
            : base("[Mod_Product_SlideShow]")
        {

        }

        private static ModProduct_SlideShowService _Instance = null;
        public static ModProduct_SlideShowService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_SlideShowService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_SlideShowEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}