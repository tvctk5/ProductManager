using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Sản phẩm", Code = "MProduct", Order = 50)]
    public class MProductController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Chuyên mục", "Type|Product")]
        public int MenuID;

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public void ActionIndex(MProductModel model)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
               MenuID = ViewPage.CurrentPage.MenuID;

            var dbQuery = ModProductService.Instance.CreateQuery()
                            .Where(o => o.Activity == true)
                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                            .OrderByDesc(o => o.Order)
                            .Take(PageSize)
                            .Skip(PageSize * model.Page);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            model.PageSize = PageSize;
            ViewBag.Model = model;
        }

        public void ActionDetail(string endCode)
        {
            var item = ModProductService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.Code == endCode)
                            //.WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                            .ToSingle();

            if (item != null)
            {
                ViewBag.Other = ModProductService.Instance.CreateQuery()
                                        .Where(o => o.Activity == true)
                                        .Where(o => o.Order < item.Order)
                                        .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                                        .OrderByDesc(o => o.Order)
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

    public class MProductModel
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
