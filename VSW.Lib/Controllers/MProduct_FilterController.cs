using System;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Product_ filter", Code = "MProduct_Filter", Order = 50)]
    public class MProduct_FilterController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string TieuDe { get; set; }

        public void ActionIndex(MProduct_FilterModel model)
        {
            //var dbQuery = ModProduct_FilterService.Instance.CreateQuery()
            //                .Where(o => o.Activity == true)
            //                .OrderByDesc(o => o.Order)
            //                .Take(PageSize)
            //                .Skip(PageSize * model.Page);

            //ViewBag.Data = dbQuery.ToList();
            //model.TotalRecord = dbQuery.TotalRecord;
            //model.PageSize = PageSize;
            //ViewBag.Model = model;
            ViewBag.Title = TieuDe;
            ViewBag.ListManufacturer = NhaSanXuat();
            ViewBag.ListFilterGroups = CacNhomThuocTinh();
        }

        public void ActionDetail(int id)
        {
            var item = ModProduct_FilterService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.ID == id)
                            .ToSingle();

            if (item != null)
            {
                ViewBag.Other = ModProduct_FilterService.Instance.CreateQuery()
                                        .Where(o => o.Activity == true)
                                        .Where(o => o.Order < item.Order)
                                        .OrderByDesc(o => o.Order)
                    //.Take(PageSize)
                                        .ToList();

                ViewBag.Data = item;

                ViewPage.CurrentPage.PageTitle = item.Name;

                //for SEO
                //ViewPage.CurrentPage.PageTitle = string.IsNullOrEmpty(item.PageTitle) ? item.Name : item.PageTitle;
                //ViewPage.CurrentPage.PageDescription = string.IsNullOrEmpty(item.PageDescription) ? item.Summary : item.PageDescription;
                //ViewPage.CurrentPage.PageKeywords = item.PageKeywords;
            }
            else
            {
                ViewPage.Error404();
            }
        }

        private string NhaSanXuat()
        {
            string sData = string.Empty;

            var lstNSX = ModProduct_ManufacturerService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();

            if (lstNSX == null || lstNSX.Count <= 0)
                return sData;


            sData += "<ul class='product-filter-ul'>";
            foreach (ModProduct_ManufacturerEntity itemManufacturer in lstNSX)
            {
                sData += "<li class='product-filter-ul-li'>";
                sData += "<span class='action-sub'> <input type='checkbox' name='Manufacturer' value='" + itemManufacturer.ID + "' /><span class='action-sub-checkbox-label'>&nbsp;" + itemManufacturer.Name + "</span></span>";
                sData += "</li>";
            }

            sData += "</ul>";

            return sData;
        }

        private string CacNhomThuocTinh()
        {
            string sData = string.Empty;
            string sType = string.Empty;
            string sStyle = string.Empty;

            var lstFilterGroups = ModProduct_FilterGroupsService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();

            if (lstFilterGroups == null || lstFilterGroups.Count <= 0)
                return sData;

            var lstFilters = ModProduct_FilterService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();
            if (lstFilters == null)
                lstFilters = new System.Collections.Generic.List<ModProduct_FilterEntity>();

            // Duyệt từng nhóm
            foreach (var itemFilterGroup in lstFilterGroups)
            {
                // Style view control
                sStyle = string.Empty;

                // Không hiển thị control
                if (itemFilterGroup.ShowControl == false)
                    sStyle = "class='hide'";

                #region Tạo từng giá trị lọc
                var lstFilters_Sub = lstFilters.Where(o => o.FilterGroupsId == itemFilterGroup.ID).OrderBy(o => o.Order).ToList();
                if (lstFilters_Sub == null || lstFilters_Sub.Count <= 0)
                    continue;

                sData += "<fieldset>";
                sData += "<legend>" + itemFilterGroup.Name + "</legend>";
                sData += "<div class='div-product-filter-left-content-detail'>";

                sData += "<ul class='product-filter-ul'>";

                // Kiểu check box
                if (itemFilterGroup.Type == (int)VSW.Lib.Global.EnumValue.TypeFilterGroup.CHECKBOX)
                    sType = "checkbox";
                else
                    if (itemFilterGroup.Type == (int)VSW.Lib.Global.EnumValue.TypeFilterGroup.RADIO)
                        sType = "radio";

                foreach (ModProduct_FilterEntity itemFilter in lstFilters_Sub)
                {
                    sData += "<li class='product-filter-ul-li'>";
                    sData += "<span class='action-sub' group='" + itemFilterGroup.Code + "'> <input " + sStyle + " type='" + sType + "' group='" + itemFilterGroup.Code + "' name='Filter' value='" + itemFilter.ID + "'/>";
                    sData += "<span class='action-sub-" + sType + "-label'><label-value value_base='&nbsp;" + itemFilter.Value + "' title='Bấm để chọn/ hủy chọn'>&nbsp;" + itemFilter.Value + "</label-value></span></span>";
                    sData += "</li>";
                }

                sData += "</ul>";
                #endregion

                sData += "</div>";
                sData += "</fieldset>";
            }

            return sData;
        }
    }

    public class MProduct_FilterModel
    {
        private int _Page = 0;
        public int Page
        {
            get { return _Page; }
            set { _Page = value - 1; }
        }

        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
    }
}
