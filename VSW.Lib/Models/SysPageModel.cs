using System;
using System.Collections.Generic;
using System.Linq;
using VSW.Core.Interface;
using VSW.Core.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Models
{
    public class SysPageEntity : EntityBase, IPageInterface
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int TemplateID { get; set; }

        [DataInfo]
        public string ModuleCode { get; set; }

        [DataInfo]
        public int LangID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int ParentID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string LinkTitle { get; set; }

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
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public bool ViewInMenu { get; set; }

        [DataInfo]
        public bool ViewInSiteMap { get; set; }

        [DataInfo]
        public bool ChangeCss { get; set; }

        [DataInfo]
        public string CssFile { get; set; }

        [DataInfo]
        public bool ChangeJs { get; set; }

        [DataInfo]
        public string JsFile { get; set; }
        
        [DataInfo]
        public string CssContent { get; set; }

        [DataInfo]
        public string JsContent { get; set; }
        #endregion

        public bool HasEnd { get; set; }

        public bool Root { get { return (ParentID == 0); } }

        public bool End { get { return Items.GetValue("End").ToBool(); } }
    }

    public class SysPageService : ServiceBase<SysPageEntity>, IPageServiceInterface
    {
        #region Autogen by VSW

        public SysPageService()
            : base("[Sys_Page]")
        {

        }

        private static SysPageService _Instance = null;
        public static SysPageService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SysPageService();

                return _Instance;
            }
        }

        #endregion

        public SysPageEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public SysPageEntity GetByID_Cache(int id)
        {
            //return base.CreateQuery()
            //   .Where(o => o.ID == id)
            //   .ToSingle_Cache();

            return _get_all_cache().Find(o => o.ID == id);
        }

        public List<SysPageEntity> GetByParent_Cache(int parent_id)
        {
            //return base.CreateQuery()
            //   .Where(o => o.Activity == true && o.ParentID == parent_id)
            //   .OrderByAsc(o => o.Order)
            //   .ToList_Cache();

            var list = _get_all_cache().FindAll(o => o.ParentID == parent_id);

            if (list.Count == 0)
                return null;

            if (list != null)
                list.Sort((o1, o2) => o1.Order.CompareTo(o2.Order));
            return list;
        }

        public string GetMapCode_Cache(SysPageEntity page)
        {
            string Key_Cache = "Lib.App.SysPage.GetMapCode." + page.ID;

            string _MapCode = null;
            object obj = VSW.Core.Web.Cache.GetValue(Key_Cache);
            if (obj != null)
            {
                _MapCode = obj.ToString();
            }
            else
            {
                SysPageEntity _Page = page;

                _MapCode = _Page.Code;
                while (_Page.ParentID > 0)
                {
                    int _parent_id = _Page.ParentID;

                    _Page = base.CreateQuery()
                           .Where(o => o.ID == _parent_id)
                           .ToSingle_Cache();

                    if (_Page == null || _Page.Root)
                        break;

                    _MapCode = _Page.Code + "/" + _MapCode;
                }

                VSW.Core.Web.Cache.SetValue(Key_Cache, _MapCode);
            }

            return _MapCode;
        }

        private List<SysPageEntity> _get_all_cache()
        {
            return base.CreateQuery()
              .Where(o => o.Activity == true)
              .ToList_Cache();
        }

        public static List<SysPageEntity> get_all_cache()
        {
            return SysPageService.Instance.CreateQuery()
              .Where(o => o.Activity == true)
              .ToList_Cache();
        }

        public static List<SysPageEntity> get_all()
        {
            return SysPageService.Instance.CreateQuery()
              .Where(o => o.Activity == true)
              .ToList();
        }

        /// <summary>
        ///  Lấy danh sách các Item con có ID cha
        /// </summary>
        /// <param name="ListAll"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        private static List<SysPageEntity> GetListItemByParentId(List<SysPageEntity> ListAll, int ParentId)
        {
            return ListAll.Where(o => o.ParentID == ParentId).ToList();
        }

        public static void BuildingMenu(List<SysPageEntity> ListAll, int ParentId, ref string sReturn, string HostPort)
        {
            List<SysPageEntity> ListFilter = GetListItemByParentId(ListAll, ParentId);

            if (ListFilter == null || ListFilter.Count <= 0)
                return;

            sReturn += " <ul>";
            string sClassCurrent = string.Empty;
            ViewPage objViewPage = new ViewPage();

            foreach (SysPageEntity item in ListFilter)
            {
                if (item.Activity)
                    sClassCurrent = "class='current'";

                sReturn += " <li " + sClassCurrent + "> <a href='" + HostPort + objViewPage.GetPageURL(item) + "'><span>" + item.Name + "</span></a> ";
                BuildingMenu(ListAll, item.ID, ref sReturn, HostPort);
                sReturn += "</li>";
            }

            sReturn += " </ul> ";
        }

        public static void BuildingMenu_Bak(List<SysPageEntity> ListAll, int ParentId, ref string sReturn, string HostPort)
        {
            List<SysPageEntity> ListFilter = GetListItemByParentId(ListAll, ParentId);

            if (ListFilter == null || ListFilter.Count <= 0)
                return;

            sReturn += " <ul class='subMenu' style=\" background:#0f77c8; color:White !important; border-color:#6E9F05;border-style:solid;border-width:1px;\"> ";//opacity:0.80;
            foreach (SysPageEntity item in ListFilter)
            {
                sReturn += " <li style='border-bottom-color:White;border-bottom-style:solid;border-bottom-width:1px;'> <a href=\"#\" style=\"background:#0f77c8;\">" + item.Name + "</a> ";
                BuildingMenu(ListAll, item.ID, ref sReturn, HostPort);
                sReturn += "</li>";
            }

            sReturn += " </ul> ";
        }

        #region IPageServiceInterface Members

        public IPageInterface VSW_Core_GetByID(int id)
        {
            //return base.CreateQuery()
            //      .Where(o => o.ID == id)
            //      .ToSingle_Cache();

            return _get_all_cache().Find(o => o.ID == id);
        }

        public IPageInterface VSW_Core_CurrentPage(VSW.Core.MVC.ViewPage viewPage)
        {
            string code = viewPage.CurrentVQS.GetString(0);

            if (code == string.Empty)
                return null;

            SysPageEntity _Page = null, _PageNew = null;

            List<SysPageEntity> ArrRootPage = SearchByLangID(viewPage.CurrentLang.ID);

            if (ArrRootPage == null)
                return null;

            for (int i = 0; i < ArrRootPage.Count; i++)
            {
                //int _SiteID = ArrRootPage[i].Items.GetInt("SiteID");

                //if (_SiteID > 0 && CurrentSite.ID != _SiteID)
                //    continue;

                _PageNew = GetByCodeAndParentID(code, ArrRootPage[i].ID);

                if (_PageNew != null)
                {
                    _Page = _PageNew;
                    if (_Page.End)
                    {
                        _Page.HasEnd = true;

                        return _Page;
                    }

                    break;
                }
            }

            if (_Page == null)
                return null;

            bool hasEnd = false;
            int mvc_index = 0;
            for (int i = 1; i < viewPage.CurrentVQS.Count; i++)
            {
                mvc_index = i - 1;

                code = viewPage.CurrentVQS.GetString(i);

                _PageNew = GetByCodeAndParentID(code, _Page.ID);

                if (_PageNew != null)
                {
                    _Page = _PageNew;

                    if (_Page.End)
                    {
                        if (i != viewPage.CurrentVQS.Count - 1)
                            _Page.HasEnd = true;

                        break;
                    }
                }
                else
                {
                    hasEnd = true;

                    if (_Page != null)
                        _Page.HasEnd = true;

                    break;
                }
            }

            mvc_index++;

            if (!hasEnd)
                mvc_index++;

            if (mvc_index < viewPage.CurrentVQS.Count)
                viewPage.CurrentVQSMVC.Trunc(viewPage.CurrentVQS, mvc_index);

            return _Page;
        }

        private SysPageEntity GetByCodeAndParentID(string code, int parent_id)
        {
            //return base.CreateQuery()
            //   .Where(o => o.Activity == true && o.ParentID == parent_id && o.Code == code)
            //   .ToSingle_Cache();

            return _get_all_cache().Find(o => o.ParentID == parent_id && o.Code.ToLower() == code.ToLower());
        }

        private List<SysPageEntity> SearchByLangID(int lang_id)
        {
            //return base.CreateQuery()
            //   .Where(o => o.Activity == true && o.ParentID == 0 && o.LangID == lang_id)
            //   .OrderByAsc(o => o.Order)
            //   .ToList_Cache();

            return _get_all_cache().FindAll(o => o.ParentID == 0 && o.LangID == lang_id);
        }

        public void VSW_Core_CPSave(IPageInterface item)
        {
            base.Save(item as SysPageEntity);
        }

        #endregion
    }
}
