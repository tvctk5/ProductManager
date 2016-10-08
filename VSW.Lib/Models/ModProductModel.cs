using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProductEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string Summary { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public int Frice { get; set; }

        [DataInfo]
        public int Quantity { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion      
  
        private WebMenuEntity _oMenu = null;
        public WebMenuEntity getMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            if (_oMenu == null)
                _oMenu = new WebMenuEntity();

            return _oMenu;
        }

    }

    public class ModProductService : ServiceBase<ModProductEntity>
    {

        #region Autogen by VSW

        private ModProductService()
            : base("[Mod_Product]")
        {

        }

        private static ModProductService _Instance = null;
        public static ModProductService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProductService();

                return _Instance;
            }
        }

        #endregion

        public ModProductEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}