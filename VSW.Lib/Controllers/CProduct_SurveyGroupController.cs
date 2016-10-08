using System;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Khảo sát thông tin", Code = "CProduct_SurveyGroup", IsControl = true, Order = 50)]
    public class CProduct_SurveyGroupController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Số lượng khảo sát")]
        public int PageSize = 1;

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        [VSW.Core.MVC.PropertyInfo("Luôn mở popup", "ConfigKey|Mod.AlwayOpenPopupSurvey")]
        public int AlwayOpenPopupSurvey = 0;
        
        public override void OnLoad()
        {

            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            var Data = ModProduct_SurveyGroupService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.StartDate != null && o.StartDate <= DateTime.Now && (o.FinishDate == null || o.FinishDate >= DateTime.Now))
                            .OrderByDesc(o => o.StartDate)
                            .Take(PageSize)
                            .ToList_Cache();

            dynamic Data_Detail = null;
            if (Data != null && Data.Count > 0)
            {
                var arrListIdGroup = VSW.Core.Global.Array.ToString(Data.Select(o => o.ID).ToArray());
                Data_Detail = ModProduct_SurveyGroup_DetailService.Instance.CreateQuery()
                   .WhereIn(o => o.SurveyGroupId, arrListIdGroup)
                   .ToList_Cache();

                if (Data_Detail == null)
                    Data_Detail = new ModProduct_SurveyGroup_DetailEntity();
            }

            ViewBag.Data = Data;
            ViewBag.Data_Detail = Data_Detail;
            ViewBag.Title = Title;
            ViewBag.AlwayOpenPopupSurvey = AlwayOpenPopupSurvey;
        }
    }
}
