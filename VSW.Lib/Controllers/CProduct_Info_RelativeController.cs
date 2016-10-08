using System;
using System.Linq;
using VSW.Lib.MVC;
using VSW.Lib.Models;
using System.Collections.Generic;
using VSW.Lib.Global;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : [Sản phẩm] Sản phẩm liên quan, cùng danh mục", Code = "CProduct_Info_Relative", IsControl = true, Order = 50)]
    public class CProduct_Info_RelativeController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        [VSW.Core.MVC.PropertyInfo("Loại", "ConfigKey|Mod.TypeProductSub")]
        public int TypeProductSub;

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            //int ProductId = Global.ConvertTool.ConvertToInt32(Global.Cookies.GetValue("Product.Detail.ProductId"));
            int ProductId = Global.ConvertTool.ConvertToInt32(Session.GetValue("ProductId"));
            // Không tìm thấy sản phẩm nào
            if (ProductId <= 0)
                return;

            // Mặc định lấy tất cả
            if (PageSize <= 0)
                PageSize = int.MaxValue;

            var ProductInfo = ModProduct_InfoService.Instance.GetByID(ProductId);
            if (ProductInfo == null)
                return;

            switch (TypeProductSub)
            {
                case (int)Global.EnumValue.TypeProductSub.Relative:

                    #region Lấy các sản phẩm liên quan
                    // Không có sản phẩm liên quan
                    if (string.IsNullOrEmpty(ProductInfo.ProductsConnection) || ProductInfo.ProductsConnection.Trim(',').Length <= 0)
                        return;

                    ViewBag.Data = ModProduct_InfoService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true && o.Deleted == false)
                                    .WhereIn(p => p.ID, ProductInfo.ProductsConnection.Trim(','))
                                    .OrderByDesc(o => o.ID)
                                    .Take(PageSize)
                                    .ToList_Cache()
                                    ;

                    #endregion
                    break;

                case (int)Global.EnumValue.TypeProductSub.InCollection:

                    #region Lấy các sản phẩm cùng danh mục

                    ViewBag.Data = ModProduct_InfoService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true && o.Deleted == false && o.ID != ProductInfo.ID && o.MenuID == ProductInfo.MenuID)
                                    .OrderByDesc(o => o.ID)
                                    .Take(PageSize)
                                    .ToList_Cache()
                                    ;

                    #endregion
                    break;
            }

            ViewBag.Title = Title;
        }
    }
}
