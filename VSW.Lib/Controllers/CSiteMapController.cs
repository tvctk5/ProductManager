using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : SiteMap", Code = "CSiteMap", IsControl = true, Order = 50)]
    public class CSiteMapController : Controller
    {
        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            SysPageEntity objCurrentPage = ViewPage.CurrentPage;
            ViewBag.SiteMap = SiteMap(objCurrentPage);
        }

        private string SiteMap(SysPageEntity objCurrentPage)
        {
            // Lấy danh sách các trang cha
            List<SysPageEntity> lstAllPage = SysPageService.get_all_cache();
            // Lấy đường dẫn trang hiện tại
            string sUrlCurrentPage = "<a href='" + ViewPage.GetPageURL(objCurrentPage) + "'>" + objCurrentPage.Name + "</a>";

            // Lấy các trang cha
            sUrlCurrentPage = GetLinkParent(objCurrentPage.ParentID, lstAllPage) + " > " + sUrlCurrentPage + " >";

            if (!string.IsNullOrEmpty(objCurrentPage.PageTitle))
                sUrlCurrentPage += " <span class='a-sitemap-activate'>" + objCurrentPage.PageTitle + "</span>";

            // Trả ra giá trị
            sUrlCurrentPage = sUrlCurrentPage.Trim();
            if (!sUrlCurrentPage.EndsWith("n>") && !sUrlCurrentPage.EndsWith("</a>"))
                return sUrlCurrentPage.Trim('>');

            return sUrlCurrentPage;
        }

        private string GetLinkParent(int iParent, List<SysPageEntity> lstAllPage)
        {
            if (iParent <= 0)
                return string.Empty;

            SysPageEntity objFilter = lstAllPage.Where(p => p.ID == iParent).FirstOrDefault();
            if (objFilter == null)
                return string.Empty;

            string sReturn = string.Empty;
            if (objFilter.ViewInSiteMap)
                sReturn = " > <a href='" + ViewPage.GetPageURL(objFilter) + "'>" + objFilter.Name + "</a>";
            else
                sReturn = string.Empty;

            string sReturn_Parent = GetLinkParent(objFilter.ParentID, lstAllPage);
            if (string.IsNullOrEmpty(sReturn_Parent))
                sReturn = "<a href='/'><img class='img-sitemap'/>Trang chủ</a>" + sReturn;
            else
                sReturn = sReturn_Parent + sReturn;

            return sReturn;
        }
    }
}
