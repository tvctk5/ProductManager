using System;

namespace VSW.Lib.MVC
{
    public class ViewControl : VSW.Core.MVC.ViewControl
    {
        public ViewPage ViewPage
        {
            get { return this.Page as ViewPage; }
        }

        protected string GetPagination(int pageIndex, int pageSize, int totalRecord)
        {
            return GetPagination(ViewPage.CurrentURL, pageIndex, pageSize, totalRecord);
        }

        protected string GetPagination(string url, int pageIndex, int pageSize, int totalRecord)
        {
            Global.Pager _Pager = new Global.Pager();

            _Pager.URL = url;
            _Pager.PageIndex = pageIndex;
            _Pager.PageSize = pageSize;
            _Pager.TotalRecord = totalRecord;

            _Pager.Update();

            return _Pager.HtmlPage;
        }
    }
}
