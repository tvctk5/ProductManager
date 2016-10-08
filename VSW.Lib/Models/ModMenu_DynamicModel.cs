using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModMenu_DynamicEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ModMenuTypeID { get; set; }

        [DataInfo]
        public int ParentID { get; set; }

        [DataInfo]
        public int LangID { get; set; }

        [DataInfo]
        public int SysPageID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Url { get; set; }

        [DataInfo]
        public string IconUrl { get; set; }

        [DataInfo]
        public bool ShowPopup { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        #endregion      
  
        private ModMenu_TypeEntity _oModMenuType = null;
        public ModMenu_TypeEntity getModMenuType()
        {
            if (_oModMenuType == null && ModMenuTypeID > 0)
                _oModMenuType = ModMenu_TypeService.Instance.GetByID(ModMenuTypeID);

            if (_oModMenuType == null)
                _oModMenuType = new ModMenu_TypeEntity();

            return _oModMenuType;
        }

        private SysLangEntity _oLang = null;
        public SysLangEntity getLang()
        {
            if (_oLang == null && LangID > 0)
                _oLang = SysLangService.Instance.GetByID(LangID);

            if (_oLang == null)
                _oLang = new SysLangEntity();

            return _oLang;
        }      
  
        private SysPageEntity _oSysPage = null;
        public SysPageEntity getSysPage()
        {
            if (_oSysPage == null && SysPageID > 0)
                _oSysPage = SysPageService.Instance.GetByID_Cache(SysPageID);

            if (_oSysPage == null)
                _oSysPage = new SysPageEntity();

            return _oSysPage;
        }
        
        /// <summary>
        /// Lấy tên của cha
        /// CanTV
        /// </summary>
        /// <param name="lstData"></param>
        /// <param name="iParentId"></param>
        /// <returns></returns>
        public string getParentName(List<ModMenu_DynamicEntity> lstData, int iParentId)
        {
            if (lstData == null || lstData.Count <= 0)
                return string.Empty;

            if (iParentId == 0)
                return string.Empty;

            var data = lstData.Where(p => p.ID == iParentId).FirstOrDefault();
            if (data == null)
                return string.Empty;

            return data.Name;
        }
        
    }

    public class ModMenu_DynamicService : ServiceBase<ModMenu_DynamicEntity>
    {

        #region Autogen by VSW

        private ModMenu_DynamicService()
            : base("[Mod_Menu_Dynamic]")
        {

        }

        private static ModMenu_DynamicService _Instance = null;
        public static ModMenu_DynamicService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModMenu_DynamicService();

                return _Instance;
            }
        }

        #endregion

        public ModMenu_DynamicEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
         
        /// <summary>
        /// Lấy danh sách theo Loại Menu
        /// Create By CanTV
        /// </summary>
        /// <param name="iMenuTypeId"></param>
        /// <returns></returns>
        public List<ModMenu_DynamicEntity> GetListByMenuType(int iMenuTypeId)
        {
            return ModMenu_DynamicService.Instance.CreateQuery().Where(o => o.Activity == true && o.ModMenuTypeID == iMenuTypeId)
                .OrderByAsc(o=>o.Order)
                .ToList_Cache();
        }
    }
}