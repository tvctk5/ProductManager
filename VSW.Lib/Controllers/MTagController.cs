using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Tag", Code = "MTag", Order = 3)]
    public class MTagController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public void ActionDetail(string tagCode)
        {
            MTagModel model = new MTagModel();

            TryUpdateModel(model);

            ModTagEntity _Tag = ModTagService.Instance.GetByCode(tagCode);

            if (_Tag != null)
            {
                var dbQuery = ModNewsService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true)
                                    .WhereIn(o => o.ID, ModNewsTagService.Instance.CreateQuery()
                                                            .Select(o => o.NewsID)
                                                            .Where(o => o.TagID == _Tag.ID)
                                            )
                                    .OrderByDesc(o => o.ID)
                                    .Take(PageSize)
                                    .Skip(PageSize * model.Page);

                ViewBag.Data = dbQuery.ToList();
                model.TotalRecord = dbQuery.TotalRecord;

                ViewPage.CurrentPage.PageTitle = !string.IsNullOrEmpty(_Tag.Title) ? _Tag.Title : _Tag.Name;

                if (!string.IsNullOrEmpty(_Tag.Description))
                    ViewPage.CurrentPage.PageDescription = _Tag.Description;

                if (!string.IsNullOrEmpty(_Tag.Keywords))
                    ViewPage.CurrentPage.PageKeywords = _Tag.Keywords;
            }
            else
            {
                ViewPage.Response.Redirect("~/");
                return;
            }

            ViewBag.Model = model;
            ViewBag.Tag = _Tag;
        }
    }

    public class MTagModel
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
