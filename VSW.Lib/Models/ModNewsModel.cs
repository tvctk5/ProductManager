using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModNewsEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public string Type { get; set; }
        
        [DataInfo]
        public int SlideType { get; set; }

        [DataInfo]
        public int State { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string Tags { get; set; }

        [DataInfo]
        public string Summary { get; set; }

        [DataInfo]
        public string PageTitle { get; set; }

        [DataInfo]
        public string PageDescription { get; set; }

        [DataInfo]
        public string PageKeywords { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public string Custom { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public DateTime Published { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public int CountViewed { get; set; }

        [DataInfo]
        public int CountComment { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

        public ModNewsEntity()
        {
            //Items.SetValue("IsName", true);
            //Items.SetValue("IsSummary", true);
        }

        private WebMenuEntity _oMenu = null;
        public WebMenuEntity getMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            if (_oMenu == null)
                _oMenu = new WebMenuEntity();

            return _oMenu;
        }


        private int count_comment = -1;
        public int getCountComment()
        {
            if (count_comment > -1)
                return count_comment;

            return count_comment = ModCommentService.Instance.CreateQuery()
                    .Where(o => o.Activity == true && o.NewsID == ID)
                    .Count()
                    .ToValue().ToInt();
        }
    }

    public class ModNewsService : ServiceBase<ModNewsEntity>
    {
        #region Autogen by VSW

        public ModNewsService()
            : base("[Mod_News]")
        {

        }

        private static ModNewsService _Instance = null;
        public static ModNewsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModNewsService();

                return _Instance;
            }
        }

        #endregion

        public ModNewsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
    }
}
