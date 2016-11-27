<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var model = ViewBag.Model as MProduct_InfoModel;
    var listItem = ViewBag.Data as List<ModProduct_InfoEntity>;
    if (listItem == null)
        listItem = new List<ModProduct_InfoEntity>();

    string sData = string.Empty;
    string Url = string.Empty;
    string sFilePath = string.Empty;
    bool bolPriceTypeSale_Percent = Convert.ToBoolean((int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_PriceTypeSale.Percent);
    bool bolPriceTypeSale_Money = Convert.ToBoolean((int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_PriceTypeSale.Mony);

    sData += "<ul class='list_product'>";
    foreach (var item in listItem)
    {
        Url = ViewPage.GetURL(item.MenuID, item.Code);
        sFilePath = Utils.GetResizeFile(item.File, 2, 175, 175);

        sData += "<li>";
        sData += "<div>";
        sData += "<a href='" + Url + "' class='image'>";
        sData += "<img width='100%' src='" + sFilePath + "' alt='" + item.Name + "' />";

        // Icon giảm giá
        if (item.ShowIcon == (int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_ShowIcon.SALE && VSW.Lib.Global.ConvertTool.CheckSaleOff(item))
        {
            if (item.SaleOffType == bolPriceTypeSale_Percent)
                sData += "<span class='sale-percent'>-" + item.PriceTextSaleView + "</span>";

            if (item.SaleOffType == bolPriceTypeSale_Money)
                sData += "<span class='sale-mony'>-" + item.PriceTextSaleView + "</span>";
        }
        // New - Host
        else
        {
            if (item.ShowIcon == (int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_ShowIcon.NEW)
                sData += "<span class='product-new'>&nbsp;</span>";
            else
                if (item.ShowIcon == (int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_ShowIcon.HOT)
                    sData += "<span class='product-hot'>&nbsp;</span>";
        }

        sData += "</a>";

        sData += "<h2 class='intro'><a href='" + Url + "'>" + item.Name + "</a></h2>";

        // Có hiển thị giá hay không
        if (item.ShowPrice)
        {
            // Có hiển thị giá khuyến mại - giá cũ hay không
            if (VSW.Lib.Global.ConvertTool.CheckSaleOff(item))
            {
                sData += "<p class='current_price'>" + VSW.Lib.Global.ConvertTool.ConvertToMoney(item.PriceSale) + "</p>";
                sData += "<p class='old_price'>" + VSW.Lib.Global.ConvertTool.ConvertToMoney(item.Price) + "</p>";
            }
            else
            {
                sData += "<p class='current_price'>" + VSW.Lib.Global.ConvertTool.ConvertToMoney(item.Price) + "</p>";
                sData += "<p class='old_price'></p>";
            }
        }
        // Không hiển thị giá bán
        else
        {
            sData += "<p class='current_price_notshow'></p>";
            sData += "<p class='old_price'></p>";
        }

        sData += "</div>"; // Xem chi tiết
        sData += "<div class='show-detail'><div class='show-detail-inline'>";
        sData += "<div class='show-detail-left'><span class='action-sub-checkbox'><input type='checkbox' productid='" + item.ID + "' class='input-compare-product' /><span class='action-sub action-sub-checkbox-label'>&nbsp;So sánh</span></span></div>";
        sData += "<div class='show-detail-right'><a href='" + Url + "'>Chi tiết</a></div>";
        sData += "</div></div>";
        sData += "</li>";
    }
    sData += "</ul>";

   
%>
<%--<div class="box100">
    <div class="DefaultModuleContent">
        <div class="defaultContentTitle TitleContent title">
            <div class="title">
                <%= ViewPage.CurrentPage.Name%></div>
        </div>
        <div class="defaultContentDetail defaultContent">
            <%=sData %>
        </div>
        <div class="defaultFooter cate-menu-footer">
        </div>
    </div>
</div>--%>

<div>
    <div class="page-title title-background"><h4><%= ViewPage.CurrentPage.Name%></h4></div>
    <div class="page-list">
        <% foreach (var item in listItem)
           {
               string ProductUrl = ViewPage.GetURL(item.MenuID, item.Code);
               %>
               <div class="product-item">
                <div class="product-image">
                    <a href='<%=ProductUrl %>'><img class="img-responsive" src="<%=Utils.GetResizeFile(item.File, 2, 175, 175) %>" /></a>
                </div>
                <div class="product-name">
                    <h5 class='intro'><a href='<%=ProductUrl %>'><%=item.Name %></a></h5>
               </div>
              </div>
        <% } %>
    </div>
</div>

<div class="navigation">
    <%= GetPagination(model.Page, model.PageSize, model.TotalRecord)%>
</div>
<style type="text/css">
.page-title{padding:10px; background-color:Blue;color:White;margin:5px 0px 5px 0px;}
.product-name{text-align:center;}
.product-item {float:left;margin-right: 10px;}
</style>