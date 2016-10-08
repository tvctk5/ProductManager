using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : So sánh sản phẩm", Code = "MProduct_Compare", Order = 50)]
    public class MProduct_CompareController : Controller
    {
        public void ActionIndex(MProduct_CompareModel model)
        {
            var ListProductCompareID = new List<int>();

            if (VSW.Lib.Global.Session.Exists("ListProductCompareID"))
                ListProductCompareID = (List<int>)VSW.Lib.Global.Session.GetValue("ListProductCompareID");

            if (ListProductCompareID == null || ListProductCompareID.Count <= 0)
            {
                ViewBag.ListProduct = new List<int>();
                return;
            }

            // Lấy danh sách ID
            string ArrList_Id = VSW.Core.Global.Array.ToString(ListProductCompareID.ToArray());
            var ListProduct = ModProduct_InfoService.Instance.CreateQuery()
                .WhereIn(o => o.ID, ArrList_Id)
                .ToList_Cache();

            // Lấy chủng loại theo Id  
            var MenuId = ListProduct[0].MenuID;

            // Danh sách lĩnh vực tương ứng với thuộc tính sẽ hiển thị
            var AreaID = WebMenuService.Instance.CreateQuery().Where(o => o.ID == MenuId)
                                            .Select(o => o.ProductAreaId)
                                            .ToSingle();
            var iAreaID = AreaID.ProductAreaId;

            // Lấy danh sách các thuộc tính, nhóm thuộc tính thuộc lĩnh vực
            var ArrListArea_PropertyGroup = ModProduct_Area_PropretyGroupService.Instance.CreateQuery()
                                                                        .Where(o => o.ProductAreaId == iAreaID)
                                                                        .Select(o => o.PropertiesGroupId)
                                                                        .ToList();
            // Lấy nhóm thuộc tính Id
            var ArrListPropertyGroup = ArrListArea_PropertyGroup.Select(o => o.PropertiesGroupId).ToArray();
            string sArrListPropertyGroup = VSW.Core.Global.Array.ToString(ArrListPropertyGroup);

            // Lấy danh sách nhóm thuộc tính
            var ListPropertyGroup = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                                                                       .WhereIn(o => o.ID, sArrListPropertyGroup)
                                                                       .OrderByAsc(o => o.Order)
                                                                       .ToList();
            if (ListPropertyGroup == null)
                ListPropertyGroup = new List<ModProduct_PropertiesGroupsEntity>();

            // Lấy danh sách các thuộc tính
            var ListProperty = ModProduct_PropertiesListService.Instance.CreateQuery()
                                                                .WhereIn(o => o.PropertiesGroupsId, sArrListPropertyGroup)
                                                                .Where(o => o.Activity == true)
                                                                .OrderByAsc(o => o.PropertiesGroupsId)
                                                                .OrderByAsc(o => o.Order)
                                                                .ToList();
            if (ListProperty == null)
                ListProperty = new List<ModProduct_PropertiesListEntity>();


            // Lấy danh sách các giá thuộc tính tương ứng với sản phẩm
            var ListPropertyValue_Product = ModProduct_Info_DetailsService.Instance.CreateQuery()
                                                                .WhereIn(o => o.ProductInfoId, ArrList_Id)
                                                                .ToList();
            if (ListPropertyValue_Product == null)
                ListPropertyValue_Product = new List<ModProduct_Info_DetailsEntity>();

            // Danh sách sản phẩm
            ViewBag.ListProduct = ListProduct;
            ViewBag.Data = Data_Compare(ListProduct, ListPropertyGroup, ListProperty, ListPropertyValue_Product);

        }

        /// <summary>
        /// Lấy danh sách thông tin so sánh dưới dạng Tr
        /// </summary>
        /// <param name="ListProduct"></param>
        /// <param name="ListPropertyGroup"></param>
        /// <param name="ListProperty"></param>
        /// <returns></returns>
        private string Data_Compare(List<ModProduct_InfoEntity> ListProduct, List<ModProduct_PropertiesGroupsEntity> ListPropertyGroup, List<ModProduct_PropertiesListEntity> ListProperty, List<ModProduct_Info_DetailsEntity> ListPropertyValue_Product)
        {
            string sData = string.Empty;
            string sUrl = string.Empty;
            string sUrlImage = string.Empty;
            string sPrice = string.Empty;
            string sValue = string.Empty;

            int iTd_Add = 0;
            int iColspan = 5;

            #region Tạo hàng tên sản phẩm
            sData = "<tr class='tr-product-compare-title'>";

            // Ô trống
            sData += "<td nowrap='nowrap'>";
            sData += "</td>";

            // Duyệt hiển thị tên sản phẩm
            foreach (var itemProduct in ListProduct)
            {
                // Lấy đường dẫn link
                sUrl = "/" + ViewPage.CurrentLang.Code + "/" + ViewPage.GetURL(itemProduct.MenuID, itemProduct.Code);

                sData += "<td class='td-product-compare-title'>";
                sData += "<img class='td-product-compare-title-image-delete' alt='Xóa sản phẩm khỏi danh sách so sánh' title='Xóa sản phẩm khỏi danh sách so sánh' productid='" + itemProduct.ID + "' onclick='return Product_Compare_Delete_ReloadForm(this);' />";
                sData += "<a href='" + sUrl + "' alt='" + itemProduct.Name + "' title='" + itemProduct.Name + "'>";
                sData += "<span>" + itemProduct.Name + "</span>";
                sData += "</a></td>";
            }

            // Ko đủ 4 sản phẩm
            if (ListProduct.Count < 4)
            {
                iTd_Add = 4 - ListProduct.Count;
                for (int iadd_td = 0; iadd_td < iTd_Add; iadd_td++)
                    sData += "<td class='td-product-compare-title'><span></span></td>";
            }

            sData += "</tr>";
            #endregion

            #region Tạo hàng ảnh sản phẩm
            sData += "<tr class='tr-product-compare-image'>";

            // Ô trống
            sData += "<td nowrap='nowrap' class='key'>";
            sData += "<span>Ảnh sản phẩm:</span></td>";

            // Duyệt hiển thị tên sản phẩm
            foreach (var itemProduct in ListProduct)
            {
                // Lấy đường dẫn link
                sUrl = "/" + ViewPage.CurrentLang.Code + "/" + ViewPage.GetURL(itemProduct.MenuID, itemProduct.Code);

                // Đường dẫn ảnh
                sUrlImage = Utils.GetResizeFile(itemProduct.File, 2, 130, 110);

                sData += "<td class='td-product-compare-image'>";
                sData += "<a href='" + sUrl + "' alt='" + itemProduct.Name + "' title='" + itemProduct.Name + "'>";
                sData += "<img src='" + sUrlImage + "' />";
                sData += "</a></td>";
            }

            // Nếu Ko đủ 4 sản phẩm 
            for (int iadd_td = 0; iadd_td < iTd_Add; iadd_td++)
                sData += "<td class='td-product-compare-image'></td>";

            sData += "</tr>";
            #endregion

            #region Tạo hàng giá sản phẩm
            sData += "<tr class='tr-product-compare-price'>";

            // Ô trống
            sData += "<td nowrap='nowrap' class='key'>";
            sData += "<span>Giá:</span></td>";

            // Duyệt hiển thị giá sản phẩm
            foreach (var itemProduct in ListProduct)
            {
                sPrice = string.Empty;

                // Có hiển thị giá hay không
                if (itemProduct.ShowPrice)
                {
                    if (itemProduct.PriceSale > 0 && itemProduct.Price > itemProduct.PriceSale)
                    {
                        sPrice = "<span class='span-price-new'>" + ConvertTool.ConvertToMoney(itemProduct.PriceSale) + "</span>";
                        sPrice += "&nbsp;(<span class='span-price-old'>" + ConvertTool.ConvertToMoney(itemProduct.Price) + "</span>)";
                    }
                    else
                        sPrice = "<span class='span-price-new'>" + ConvertTool.ConvertToMoney(itemProduct.Price) + "</span>";
                }
                // Hiển thị thông tin liên hệ
                else
                    sPrice += "<p class='current_price_notshow'></p>";

                sData += "<td class='td-product-compare-price'>";
                sData += sPrice;
                sData += "</td>";

            }

            // Nếu Ko đủ 4 sản phẩm

            for (int iadd_td = 0; iadd_td < iTd_Add; iadd_td++)
                sData += "<td class='td-product-compare-price'></td>";

            sData += "</tr>";
            #endregion

            #region Tạo các thông tin thuộc tính

            foreach (var itemPropertyGroup in ListPropertyGroup)
            {
                sData += "<tr class='tr-product-compare-group'>";
                sData += "<td colspan='" + iColspan + "'><span>" + itemPropertyGroup.Name + "</span></td></tr>";

                #region Duyệt tiếp các dòng tiếp theo tương ứng với thuộc tính
                var ListProductGroup_Property = ListProperty.Where(o => o.PropertiesGroupsId == itemPropertyGroup.ID).OrderBy(o => o.Order).ToList();
                if (ListProductGroup_Property == null || ListProductGroup_Property.Count <= 0)
                    continue;

                foreach (var itemProperty in ListProductGroup_Property)
                {
                    sData += "<tr class='tr-product-compare-group-property'>";

                    // Tên thuộc tính
                    sData += "<td class='key' nowrap='nowrap'><span>" + itemProperty.Name + "</span></td>";

                    foreach (var itemProduct in ListProduct)
                    {
                        var objProduct_Detail = ListPropertyValue_Product.Where(o => o.ProductInfoId == itemProduct.ID && o.PropertiesListId == itemProperty.ID).FirstOrDefault();
                        if (objProduct_Detail == null)
                            sValue = string.Empty;
                        else
                            sValue = objProduct_Detail.Content;

                        // Giá trị thuộc tính
                        sData += "<td><span>" + sValue + "</span></td>";
                    }

                    // Xem có đủ số sản phẩm ko (4), Nếu ko đủ, đổ các td trống
                    for (int iadd_td = 0; iadd_td < iTd_Add; iadd_td++)
                        sData += "<td></td>";

                    sData += "</tr>";
                }
                #endregion

            }

            //ListPropertyValue_Product
            #endregion

            return sData;
        }
    }

    public class MProduct_CompareModel
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
