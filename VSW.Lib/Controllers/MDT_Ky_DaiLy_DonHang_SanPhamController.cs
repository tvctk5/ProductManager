using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : D t_ ky_ dai ly_ don hang_ san pham", Code = "MDT_Ky_DaiLy_DonHang_SanPham", Order = 50)]
    public class MDT_Ky_DaiLy_DonHang_SanPhamController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public void ActionIndex(MDT_Ky_DaiLy_DonHang_SanPhamModel model)
        {
            var dbQuery = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                            .OrderByDesc(o => o.ID)
                            .Take(PageSize)
                            .Skip(PageSize * model.Page);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            model.PageSize = PageSize;
            ViewBag.Model = model;
        }

        public void ActionDetail(int id)
        {
            var item = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                            .Where(o => o.ID == id)
                            .ToSingle();

            if (item != null)
            {
                ViewBag.Other = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                                        .Where(o => o.ID < item.ID)
                                        .OrderByDesc(o => o.ID)
                                        .Take(PageSize)
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
    }

    public class MDT_Ky_DaiLy_DonHang_SanPhamModel
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
