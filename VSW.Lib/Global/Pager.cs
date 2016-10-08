using System;
using System.Web.UI;
using System.Web;
using System.Text;
using System.IO;

namespace VSW.Lib.Global
{
    public class Pager
    {
        public Pager()
        {

        }

        private int _TotalRecord = -1;
        private int _PageSize = 10;
        private int _PageMax = 10;
        private int _TotalBegin = 0;
        private int _TotalEnd = 0;
        private string _CssClass = string.Empty;
        private string _URL = string.Empty;
        private string _ParamName = "Page";
        private string _ActionName = "";
        private string _BackText = "<";
        private string _NextText = ">";
        private string _BackEndText = "<<";
        private string _NextEndText = ">>";
        private bool _IsCPLayout = false;
        private bool _DisableMode = false;


        public int TotalRecord
        {
            get { return _TotalRecord; }
            set
            {
                if (value > 0)
                    _TotalRecord = value;
            }
        }

        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                if (value > 0)
                    _PageSize = value;
            }
        }

        private int _PageIndex = 0;
        public int PageIndex
        {
            get
            {
                //int _PageIndex = PageViewState.GetValue(ParamName).ToInt(1) - 1;

                //return (_PageIndex > -1) ? _PageIndex : 0;

                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }
        }

        public int PageMax
        {
            get { return _PageMax; }
            set
            {
                if (value > 0)
                    _PageMax = value;
            }
        }

        public int Skip
        {
            get { return PageIndex * PageSize; }
        }

        public int TotalBegin
        {
            get { return _TotalBegin; }
        }

        public int TotalEnd
        {
            get { return _TotalEnd; }
        }

        public int TotalPage
        {
            get
            {
                if (PageSize == 0)
                    return 0;

                return (TotalRecord % PageSize == 0 ? 0 : 1) + (TotalRecord / PageSize);
            }
        }

        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        public string ParamName
        {
            get { return _ParamName; }
            set { _ParamName = value; }
        }

        public string ActionName
        {
            get { return _ActionName; }
            set { _ActionName = value; }
        }

        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }

        public string BackText
        {
            get { return _BackText; }
            set { _BackText = value; }
        }

        public string NextText
        {
            get { return _NextText; }
            set { _NextText = value; }
        }

        public string BackEndText
        {
            get { return _BackEndText; }
            set { _BackEndText = value; }
        }

        public string NextEndText
        {
            get { return _NextEndText; }
            set { _NextEndText = value; }
        }

        public bool IsCPLayout
        {
            get { return _IsCPLayout; }
            set { _IsCPLayout = value; }
        }

        public bool DisableMode
        {
            get { return _DisableMode; }
            set { _DisableMode = value; }
        }

        public string HtmlPage
        {
            get { return _HtmlPage; }
        }

        private string _HtmlPage = string.Empty;

        public void Update_Modified()
        {
            int _PageIndex = PageIndex;
            int MinPage = (int)(_PageIndex / _PageMax) * _PageMax;
            int MaxPage = MinPage + _PageMax;

            double MaxPageIndex = (double)_TotalRecord / ((double)_PageSize);
            _TotalBegin = _PageIndex * _PageSize;
            _TotalEnd = _TotalBegin + _PageSize;

            if (MaxPageIndex - _PageIndex < 1)
                _TotalEnd = _TotalRecord;

            string sURL = URL;

            if (sURL.EndsWith("/"))
                sURL = ParamName;
            else
                sURL += "/" + ParamName;

            if (IsCPLayout)
            {
                #region CP

                if (MaxPageIndex > 1)
                {
                    if (MaxPage > _PageMax)
                    {
                        _HtmlPage += "<div class=\"button2-right\"><div class=\"start\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + 1 + ", '" + ParamName + "');\">" + BackEndText + "</a></div></div>";
                        _HtmlPage += "<div class=\"button2-right\"><div class=\"prev\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + MinPage + ", '" + ParamName + "');\">" + BackText + "</a></div></div>";
                    }
                    else
                    {
                        _HtmlPage += "<div class=\"button2-right off\"><div class=\"start\"><span>" + BackEndText + "</span></div></div>";
                        _HtmlPage += "<div class=\"button2-right off\"><div class=\"prev\"><span>" + BackText + "</span></div></div>";
                    }

                    _HtmlPage += "<div class=\"button2-left\"><div class=\"page\">";
                    for (int i = MinPage; i < MaxPage; i++)
                    {
                        if (i != _PageIndex)
                        {
                            if (i < MaxPageIndex)
                                _HtmlPage += "<a href=\"javascript:VSWRedirect('" + ActionName + "', " + (i + 1) + ", '" + ParamName + "');\">" + (i + 1) + "</a>";
                        }
                        else
                        {
                            if (i < MaxPageIndex)
                                _HtmlPage += "<span>" + (i + 1) + "</span>";
                        }
                    }
                    _HtmlPage += "</div></div>";

                    if (MaxPage < MaxPageIndex)
                    {
                        _HtmlPage += "<div class=\"button2-left\"><div class=\"next\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + (MaxPage + 1) + ", '" + ParamName + "');\">" + NextText + "</a></div></div>";
                        _HtmlPage += "<div class=\"button2-left\"><div class=\"end\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + (MaxPageIndex > (int)MaxPageIndex ? (int)MaxPageIndex + 1 : MaxPageIndex) + ", '" + ParamName + "');\">" + NextEndText + "</a></div></div>";
                    }
                    else
                    {
                        _HtmlPage += "<div class=\"button2-left off\"><div class=\"next\"><span>" + NextText + "</span></div></div>";
                        _HtmlPage += "<div class=\"button2-left off\"><div class=\"end\"><span>" + NextEndText + "</span></div></div>";
                    }
                }

                #endregion
            }
            else
            {
                #region web

                _HtmlPage = string.Empty;
                if (MaxPageIndex > 1)
                {
                    if (MaxPage > _PageMax)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + 1 + "\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + MinPage + "\">" + BackText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackText + "</a> ";
                    }

                    for (int i = MinPage; i < MaxPage; i++)
                    {
                        if (i != _PageIndex)
                        {
                            if (i < MaxPageIndex)
                            {
                                _HtmlPage += " <a href=\"" + sURL + "/" + (i + 1) + "\">" + (i + 1) + "</a> ";
                            }
                        }
                        else
                        {
                            if (i < MaxPageIndex) _HtmlPage += " <a class=\"" + CssClass + " selected\" href=\"#\">" + (i + 1) + "</a> ";
                        }
                    }

                    if (MaxPage < MaxPageIndex)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPage + 1) + "\">" + NextText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPageIndex > (int)MaxPageIndex ? (int)MaxPageIndex + 1 : MaxPageIndex) + "\">" + NextEndText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextEndText + "</a> ";
                    }
                }

                #endregion
            }
        }

        public void Update_t()
        {
            int _PageIndex = PageIndex;
            int MinPage = (int)(_PageIndex / _PageMax) * _PageMax;
            int MaxPage = MinPage + _PageMax;

            // Tổng số trang
            int PageAddEnd = (TotalRecord % PageSize) == 0 ? 0 : 1;
            int iTotalPage = ((int)(TotalRecord / PageSize)) + PageAddEnd + MinPage;

            double MaxPageIndex = (double)_TotalRecord / ((double)_PageSize);
            _TotalBegin = _PageIndex * _PageSize;
            _TotalEnd = _TotalBegin + _PageSize;

            if (MaxPageIndex - _PageIndex < 1)
                _TotalEnd = _TotalRecord;

            string sURL = URL;

            if (sURL.EndsWith("/"))
                sURL = ParamName;
            else
                sURL += "/" + ParamName;

            if (IsCPLayout)
            {
                #region CP

                if (MaxPageIndex > 1)
                {
                    if (_PageIndex > MinPage)
                    {
                        int PagePreview = (_PageIndex == 1) ? 1 : _PageIndex - 1;
                        _HtmlPage += "<div class=\"button2-right\"><div class=\"start\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + PagePreview + ", '" + ParamName + "');\">" + BackEndText + "</a></div></div>";
                        _HtmlPage += "<div class=\"button2-right\"><div class=\"prev\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + MinPage + ", '" + ParamName + "');\">" + BackText + "</a></div></div>";
                    }
                    else
                    {
                        _HtmlPage += "<div class=\"button2-right off\"><div class=\"start\"><span>" + BackEndText + "</span></div></div>";
                        _HtmlPage += "<div class=\"button2-right off\"><div class=\"prev\"><span>" + BackText + "</span></div></div>";
                    }

                    _HtmlPage += "<div class=\"button2-left\"><div class=\"page\">";
                    for (int i = MinPage; i < MaxPage; i++)
                    {
                        if (i != _PageIndex)
                        {
                            if (i < MaxPageIndex)
                                _HtmlPage += "<a href=\"javascript:VSWRedirect('" + ActionName + "', " + (i + 1) + ", '" + ParamName + "');\">" + (i + 1) + "</a>";
                        }
                        else
                        {
                            if (i < MaxPageIndex)
                                _HtmlPage += "<span>" + (i + 1) + "</span>";
                        }
                    }
                    _HtmlPage += "</div></div>";

                    if (iTotalPage > (_PageIndex + 1))
                    {
                        int PageNext = (_PageIndex == 0) ? 2 : _PageIndex + 1;
                        _HtmlPage += "<div class=\"button2-left\"><div class=\"next\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + PageNext + ", '" + ParamName + "');\">" + NextText + "</a></div></div>";
                        _HtmlPage += "<div class=\"button2-left\"><div class=\"end\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + iTotalPage + ", '" + ParamName + "');\">" + NextEndText + "</a></div></div>";
                    }
                    else
                    {
                        _HtmlPage += "<div class=\"button2-left off\"><div class=\"next\"><span>" + NextText + "</span></div></div>";
                        _HtmlPage += "<div class=\"button2-left off\"><div class=\"end\"><span>" + NextEndText + "</span></div></div>";
                    }
                }

                #endregion
            }
            else
            {
                #region web

                _HtmlPage = string.Empty;
                if (MaxPageIndex > 1)
                {
                    if (MaxPage > _PageMax)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + 1 + "\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + MinPage + "\">" + BackText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackText + "</a> ";
                    }

                    for (int i = MinPage; i < MaxPage; i++)
                    {
                        if (i != _PageIndex)
                        {
                            if (i < MaxPageIndex)
                            {
                                _HtmlPage += " <a href=\"" + sURL + "/" + (i + 1) + "\">" + (i + 1) + "</a> ";
                            }
                        }
                        else
                        {
                            if (i < MaxPageIndex) _HtmlPage += " <a class=\"" + CssClass + " selected\" href=\"#\">" + (i + 1) + "</a> ";
                        }
                    }

                    if (MaxPage < MaxPageIndex)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPage + 1) + "\">" + NextText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPageIndex > (int)MaxPageIndex ? (int)MaxPageIndex + 1 : MaxPageIndex) + "\">" + NextEndText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextEndText + "</a> ";
                    }
                }

                #endregion
            }
        }

        public void Update()
        {
            PageIndex = PageIndex;
            TotalRecord = TotalRecord;
            PageSize = PageSize;

            // Tổng số trang
            int PageAddEnd = (TotalRecord % PageSize) == 0 ? 0 : 1;
            int iTotalPage = ((int)(TotalRecord / PageSize)) + PageAddEnd;

            int _PageIndex = PageIndex;
            int MinPage = (int)(_PageIndex / _PageMax) * _PageMax;
            int MaxPage = MinPage + _PageMax;

            double MaxPageIndex = (double)_TotalRecord / ((double)_PageSize);
            _TotalBegin = _PageIndex * _PageSize;
            _TotalEnd = _TotalBegin + _PageSize;

            if (MaxPageIndex - _PageIndex < 1)
                _TotalEnd = _TotalRecord;

            string sURL = URL;

            if (sURL.EndsWith("/"))
                sURL = ParamName;
            else
                sURL += "/" + ParamName;

            if (IsCPLayout)
            {
                #region CP
                if (_PageIndex > 0)// Nếu không phải trang đầu tiên
                {
                    int iPagePrev = (_PageIndex - 1) <= 0 ? 1 : _PageIndex;
                    _HtmlPage += "<div class=\"button2-right\"><div class=\"start\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + 0 + ", '" + ParamName + "');\">" + BackEndText + "</a></div></div>";
                    _HtmlPage += "<div class=\"button2-right\"><div class=\"prev\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + iPagePrev + ", '" + ParamName + "');\">" + BackText + "</a></div></div>";

                }
                // Nếu là trang đầu tiên
                else
                {
                    _HtmlPage += "<div class=\"button2-right off\"><div class=\"start\"><span>" + BackEndText + "</span></div></div>";
                    _HtmlPage += "<div class=\"button2-right off\"><div class=\"prev\"><span>" + BackText + "</span></div></div>";
                }

                // Nếu còn trang tiếp, sau trang hiện tại

                _HtmlPage += "<div class=\"button2-left\"><div class=\"page\">";
                for (int i = 0; i < iTotalPage; i++)
                {
                    if (i != _PageIndex)
                        _HtmlPage += "<a href=\"javascript:VSWRedirect('" + ActionName + "', " + (i + 1) + ", '" + ParamName + "');\">" + (i + 1) + "</a>";

                    else
                        _HtmlPage += "<span>" + (i + 1) + "</span>";

                }
                _HtmlPage += "</div></div>";

                if (PageIndex == iTotalPage - 1)
                {
                    _HtmlPage += "<div class=\"button2-left off\"><div class=\"next\"><span>" + NextText + "</span></div></div>";
                    _HtmlPage += "<div class=\"button2-left off\"><div class=\"end\"><span>" + NextEndText + "</span></div></div>";
                }
                else
                {
                    int iPageNext = PageIndex + 2;

                    _HtmlPage += "<div class=\"button2-left\"><div class=\"next\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + iPageNext + ", '" + ParamName + "');\">" + NextText + "</a></div></div>";
                    _HtmlPage += "<div class=\"button2-left\"><div class=\"end\"><a href=\"javascript:VSWRedirect('" + ActionName + "', " + iTotalPage + ", '" + ParamName + "');\">" + NextEndText + "</a></div></div>";
                }
                #endregion
            }
            else
            {
                #region web

                _HtmlPage = string.Empty;
                if (MaxPageIndex > 1)
                {
                    if (MaxPage > _PageMax)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + 1 + "\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + MinPage + "\">" + BackText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + (CssClass + " disabled").Trim() + "\" href=\"#\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a class=\"" + (CssClass + " disabled").Trim() + "\" href=\"#\">" + BackText + "</a> ";
                    }

                    for (int i = MinPage; i < MaxPage; i++)
                    {
                        if (i != _PageIndex)
                        {
                            if (i < MaxPageIndex)
                            {
                                _HtmlPage += " <a href=\"" + sURL + "/" + (i + 1) + "\">" + (i + 1) + "</a> ";
                            }
                        }
                        else
                        {
                            if (i < MaxPageIndex) _HtmlPage += " <a class=\"" + (CssClass + " selected").Trim() + "\" href=\"#\">" + (i + 1) + "</a> ";
                        }
                    }

                    if (MaxPage < MaxPageIndex)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPage + 1) + "\">" + NextText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPageIndex > (int)MaxPageIndex ? (int)MaxPageIndex + 1 : MaxPageIndex) + "\">" + NextEndText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + (CssClass + " disabled").Trim() + "\" href=\"#\">" + NextText + "</a> ";
                        _HtmlPage += " <a class=\"" + (CssClass + " disabled").Trim() + "\" href=\"#\">" + NextEndText + "</a> ";
                    }
                }

                #endregion
            }
        }

        public void Update(string sFunctionOnchangePage, string controllerName)
        {
            PageIndex = PageIndex;
            TotalRecord = TotalRecord;
            PageSize = PageSize;
            if (string.IsNullOrEmpty(controllerName))
                ActionName = "Add";
            else
                ActionName = controllerName;

            // Tổng số trang
            int PageAddEnd = (TotalRecord % PageSize) == 0 ? 0 : 1;
            int iTotalPage = ((int)(TotalRecord / PageSize)) + PageAddEnd;

            int _PageIndex = PageIndex;
            int MinPage = (int)(_PageIndex / _PageMax) * _PageMax;
            int MaxPage = MinPage + _PageMax;

            double MaxPageIndex = (double)_TotalRecord / ((double)_PageSize);
            _TotalBegin = _PageIndex * _PageSize;
            _TotalEnd = _TotalBegin + _PageSize;

            if (MaxPageIndex - _PageIndex < 1)
                _TotalEnd = _TotalRecord;

            string sURL = URL;

            if (sURL.EndsWith("/"))
                sURL = ParamName;
            else
                sURL += "/" + ParamName;

            if (IsCPLayout)
            {
                #region CP
                if (_PageIndex > 0)// Nếu không phải trang đầu tiên
                {
                    int iPagePrev = (_PageIndex - 1) <= 0 ? 1 : _PageIndex;
                    _HtmlPage += "<div class=\"button2-right\"><div class=\"start\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + 0 + ", '" + ParamName + "');\">" + BackEndText + "</a></div></div>";
                    _HtmlPage += "<div class=\"button2-right\"><div class=\"prev\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + iPagePrev + ", '" + ParamName + "');\">" + BackText + "</a></div></div>";

                }
                // Nếu là trang đầu tiên
                else
                {
                    _HtmlPage += "<div class=\"button2-right off\"><div class=\"start\"><span>" + BackEndText + "</span></div></div>";
                    _HtmlPage += "<div class=\"button2-right off\"><div class=\"prev\"><span>" + BackText + "</span></div></div>";
                }

                // Nếu còn trang tiếp, sau trang hiện tại

                _HtmlPage += "<div class=\"button2-left\"><div class=\"page\">";
                for (int i = 0; i < iTotalPage; i++)
                {
                    if (i != _PageIndex)
                        _HtmlPage += "<a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + (i + 1) + ", '" + ParamName + "');\">" + (i + 1) + "</a>";

                    else
                        _HtmlPage += "<span>" + (i + 1) + "</span>";

                }
                _HtmlPage += "</div></div>";

                if (PageIndex == iTotalPage - 1)
                {
                    _HtmlPage += "<div class=\"button2-left off\"><div class=\"next\"><span>" + NextText + "</span></div></div>";
                    _HtmlPage += "<div class=\"button2-left off\"><div class=\"end\"><span>" + NextEndText + "</span></div></div>";
                }
                else
                {
                    int iPageNext = PageIndex + 2;

                    _HtmlPage += "<div class=\"button2-left\"><div class=\"next\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + iPageNext + ", '" + ParamName + "');\">" + NextText + "</a></div></div>";
                    _HtmlPage += "<div class=\"button2-left\"><div class=\"end\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + iTotalPage + ", '" + ParamName + "');\">" + NextEndText + "</a></div></div>";
                }
                #endregion
            }
            else
            {
                #region web

                _HtmlPage = string.Empty;
                if (MaxPageIndex > 1)
                {
                    if (MaxPage > _PageMax)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + 1 + "\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + MinPage + "\">" + BackText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackText + "</a> ";
                    }

                    for (int i = MinPage; i < MaxPage; i++)
                    {
                        if (i != _PageIndex)
                        {
                            if (i < MaxPageIndex)
                            {
                                _HtmlPage += " <a href=\"" + sURL + "/" + (i + 1) + "\">" + (i + 1) + "</a> ";
                            }
                        }
                        else
                        {
                            if (i < MaxPageIndex) _HtmlPage += " <a class=\"" + CssClass + " selected\" href=\"#\">" + (i + 1) + "</a> ";
                        }
                    }

                    if (MaxPage < MaxPageIndex)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPage + 1) + "\">" + NextText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPageIndex > (int)MaxPageIndex ? (int)MaxPageIndex + 1 : MaxPageIndex) + "\">" + NextEndText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextEndText + "</a> ";
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Add by CanTV
        /// Update list Page but  postback on this Page and update PageIndex
        /// </summary> 
        public void Update(string controllerName, string sFunctionOnchangePage, bool PostbackOnPage)
        {
            PageIndex = PageIndex;
            TotalRecord = TotalRecord;
            PageSize = PageSize;
            if (string.IsNullOrEmpty(controllerName))
                ActionName = "Add";
            else
                ActionName = controllerName;

            // Tổng số trang
            int PageAddEnd = (TotalRecord % PageSize) == 0 ? 0 : 1;
            int iTotalPage = ((int)(TotalRecord / PageSize)) + PageAddEnd;

            int _PageIndex = PageIndex;
            int MinPage = (int)(_PageIndex / _PageMax) * _PageMax;
            int MaxPage = MinPage + _PageMax;

            double MaxPageIndex = (double)_TotalRecord / ((double)_PageSize);
            _TotalBegin = _PageIndex * _PageSize;
            _TotalEnd = _TotalBegin + _PageSize;

            if (MaxPageIndex - _PageIndex < 1)
                _TotalEnd = _TotalRecord;

            string sURL = URL;

            if (sURL.EndsWith("/"))
                sURL = ParamName;
            else
                sURL += "/" + ParamName;

            if (IsCPLayout)
            {
                #region CP
                if (_PageIndex > 0)// Nếu không phải trang đầu tiên
                {
                    int iPagePrev = (_PageIndex - 1) <= 0 ? 1 : _PageIndex;
                    _HtmlPage += "<div class=\"button2-right\"><div class=\"start\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + 0 + ");\">" + BackEndText + "</a></div></div>";
                    _HtmlPage += "<div class=\"button2-right\"><div class=\"prev\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + iPagePrev + ");\">" + BackText + "</a></div></div>";

                }
                // Nếu là trang đầu tiên
                else
                {
                    _HtmlPage += "<div class=\"button2-right off\"><div class=\"start\"><span>" + BackEndText + "</span></div></div>";
                    _HtmlPage += "<div class=\"button2-right off\"><div class=\"prev\"><span>" + BackText + "</span></div></div>";
                }

                // Nếu còn trang tiếp, sau trang hiện tại

                _HtmlPage += "<div class=\"button2-left\"><div class=\"page\">";
                for (int i = 0; i < iTotalPage; i++)
                {
                    if (i != _PageIndex)
                        _HtmlPage += "<a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + (i + 1) + ");\">" + (i + 1) + "</a>";

                    else
                        _HtmlPage += "<span>" + (i + 1) + "</span>";

                }
                _HtmlPage += "</div></div>";

                if (PageIndex == iTotalPage - 1)
                {
                    _HtmlPage += "<div class=\"button2-left off\"><div class=\"next\"><span>" + NextText + "</span></div></div>";
                    _HtmlPage += "<div class=\"button2-left off\"><div class=\"end\"><span>" + NextEndText + "</span></div></div>";
                }
                else
                {
                    int iPageNext = PageIndex + 2;

                    _HtmlPage += "<div class=\"button2-left\"><div class=\"next\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + iPageNext + ");\">" + NextText + "</a></div></div>";
                    _HtmlPage += "<div class=\"button2-left\"><div class=\"end\"><a href=\"#\" onclick=\"" + sFunctionOnchangePage + "('" + ActionName + "', " + iTotalPage + ");\">" + NextEndText + "</a></div></div>";
                }
                #endregion
            }
            else
            {
                #region web

                _HtmlPage = string.Empty;
                if (MaxPageIndex > 1)
                {
                    if (MaxPage > _PageMax)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + 1 + "\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + MinPage + "\">" + BackText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackEndText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + BackText + "</a> ";
                    }

                    for (int i = MinPage; i < MaxPage; i++)
                    {
                        if (i != _PageIndex)
                        {
                            if (i < MaxPageIndex)
                            {
                                _HtmlPage += " <a href=\"" + sURL + "/" + (i + 1) + "\">" + (i + 1) + "</a> ";
                            }
                        }
                        else
                        {
                            if (i < MaxPageIndex) _HtmlPage += " <a class=\"" + CssClass + " selected\" href=\"#\">" + (i + 1) + "</a> ";
                        }
                    }

                    if (MaxPage < MaxPageIndex)
                    {
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPage + 1) + "\">" + NextText + "</a> ";
                        _HtmlPage += " <a href=\"" + sURL + "/" + (MaxPageIndex > (int)MaxPageIndex ? (int)MaxPageIndex + 1 : MaxPageIndex) + "\">" + NextEndText + "</a> ";
                    }
                    else if (DisableMode)
                    {
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextText + "</a> ";
                        _HtmlPage += " <a class=\"" + CssClass + " disabled\" href=\"#\">" + NextEndText + "</a> ";
                    }
                }

                #endregion
            }
        }
    }
}
