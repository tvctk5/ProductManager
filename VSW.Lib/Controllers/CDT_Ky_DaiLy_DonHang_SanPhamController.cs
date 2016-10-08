using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : D t_ ky_ dai ly_ don hang_ san pham", Code = "CDT_Ky_DaiLy_DonHang_SanPham", IsControl = true, Order = 50)]
    public class CDT_Ky_DaiLy_DonHang_SanPhamController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        public override void OnLoad()
        {
            ViewBag.Data = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                            .OrderByDesc(o => o.ID)
                            .Take(PageSize)
                            .ToList_Cache();

            ViewBag.Title = Title;
        }
    }
}
