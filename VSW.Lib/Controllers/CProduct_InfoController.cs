using System;
using System.Linq;
using VSW.Lib.MVC;
using VSW.Lib.Models;
using System.Collections.Generic;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Product_ info", Code = "CProduct_Info", IsControl = true, Order = 50)]
    public class CProduct_InfoController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        [VSW.Core.MVC.PropertyInfo("Chuyên mục", "Type|Product")]
        public int MenuID;

        // Hãng sản xuất
        [VSW.Core.MVC.PropertyInfo("Hãng sản xuất", "List|Mod_Product_Manufacturer#Name&ID#Activity=1#Name")]
        public int ManufacturerId;

        // Loại sản phẩm: Mới, nổi bật, khuyến mại, ...
        [VSW.Core.MVC.PropertyInfo("Loại sản phẩm", "List|Mod_Product_Types#Name&ID#Activity=1#Name")]
        public int ProductTypesId;

        [VSW.Core.MVC.PropertyInfo("Trang liên kết")]//, "List|Sys_Page#Name&ID#Activity=1 AND LangID=1#Name")]
        public int PageID;

        [VSW.Core.MVC.PropertyInfo("Bấm vào tiêu đề", "ConfigKey|Mod.LinkOnTitle")]
        public int LinkOnTitle;

        [VSW.Core.MVC.PropertyInfo("Xem thêm", "ConfigKey|Mod.ShowLinkViewAll")]
        public int ShowLinkViewAll;

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

            string lstProducInfo = string.Empty;
            if (ProductTypesId > 0)
            {
                var lstProduct_Info_Types = ModProduct_Info_TypesService.Instance.CreateQuery()
                                                                            .Where(p => p.ProductTypesId == ProductTypesId).ToList();

                if (lstProduct_Info_Types != null && lstProduct_Info_Types.Count > 0)
                {
                    var ArrList_Id = lstProduct_Info_Types.Select(p => p.ProductInfoId).ToArray();
                    lstProducInfo = VSW.Core.Global.Array.ToString(ArrList_Id);
                }
            }

            ViewBag.Data = ModProduct_InfoService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.Deleted == false)
                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                            .Where(ManufacturerId > 0, o => o.ManufacturerId == ManufacturerId)
                            .WhereIn(ProductTypesId > 0 && string.IsNullOrEmpty(lstProducInfo) == false, o => o.ID, lstProducInfo)
                            .OrderByDesc(o => o.ID)
                            .Take(PageSize)
                            .ToList_Cache()
                            ;

            ViewBag.Title = Title;
            ViewBag.LinkOnTitle = LinkOnTitle;
            ViewBag.ShowLinkViewAll = ShowLinkViewAll;
            ViewBag.ReferentPageId = PageID;

            //string ProductId = Global.Cookies.GetValue("Product.Detail.ProductId");
        }
    }
}
