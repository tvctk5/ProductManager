using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Product_ customers_ groups", Code = "MProduct_Customers_Groups", Order = 50)]
    public class MProduct_Customers_GroupsController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public void ActionIndex(MProduct_Customers_GroupsModel model)
        {
            var dbQuery = ModProduct_Customers_GroupsService.Instance.CreateQuery()
                            .Where(o => o.Activity == true)
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
            var item = ModProduct_Customers_GroupsService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.ID == id)
                            .ToSingle();

            if (item != null)
            {
                ViewBag.Other = ModProduct_Customers_GroupsService.Instance.CreateQuery()
                                        .Where(o => o.Activity == true)
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

    public class MProduct_Customers_GroupsModel
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
