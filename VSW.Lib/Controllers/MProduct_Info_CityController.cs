using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Product_ info_ city", Code = "MProduct_Info_City", Order = 50)]
    public class MProduct_Info_CityController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public void ActionIndex(MProduct_Info_CityModel model)
        {
            var dbQuery = ModProduct_Info_CityService.Instance.CreateQuery()
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
            var item = ModProduct_Info_CityService.Instance.CreateQuery()
                            .Where(o => o.ID == id)
                            .ToSingle();

            if (item != null)
            {
                ViewBag.Other = ModProduct_Info_CityService.Instance.CreateQuery()
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

    public class MProduct_Info_CityModel
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
