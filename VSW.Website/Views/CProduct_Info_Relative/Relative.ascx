<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var Data = ViewBag.Data as List<ModProduct_InfoEntity>;
    if (Data == null || Data.Count <= 0)
        return;

    string sData = string.Empty;
    string Url = string.Empty;
    string sFilePath = string.Empty;

    sData = "<ul class='ul-product-relative'>";
    foreach (var item in Data)
    {
        Url = ViewPage.GetURL(item.MenuID, item.Code);
        sFilePath = Utils.GetResizeFile(item.File, 2, 60, 50);

        sData += "<li class='li-product-relative'>";
        sData += "<a href='" + Url + "'>";

        sData += "<div class='div-product-relative-image-label'>";
        // Ảnh sản phẩm
        sData += "<div class='div-product-relative-image'>";
        sData += "<img src='" + sFilePath + "'/>";
        sData += "</div>";

        // Tên SP
        sData += "<div class='div-product-relative-label'>";
        sData += item.Name;
        sData += "</div>";
        sData += "</div>";

        // Giá
        sData += "<div class='div-product-relative-price'>";
        // Có hiển thị giá hay không
        if (item.ShowPrice)
        {
            if (VSW.Lib.Global.ConvertTool.CheckSaleOff(item))
            {
                //Giá cũ
                sData += "<div class='div-product-relative-price-old'>";
                sData += ConvertTool.ConvertToMoney(item.Price);
                sData += "</div>";

                // Giá mới
                sData += "<div class='div-product-relative-price-new'>";
                sData += ConvertTool.ConvertToMoney(item.PriceSale);
                sData += "</div>";
            }
            else
            {
                // Giá mới
                sData += "<div class='div-product-relative-price-new'>";
                sData += ConvertTool.ConvertToMoney(item.Price);
                sData += "</div>";
            }
        }
        // Hiển thị liên hệ
        else
            sData += "<p class='current_price_notshow'></p>";

        sData += "</div>";

        sData += "</a></li>";
    }

    sData += "</ul>";
%>
<div class="box100">
    <div class="DefaultModuleContent">
        <div class="defaultContentTitle TitleContent title">
            <div class="title">
                <%= ViewBag.Title %></div>
        </div>
        <div class="defaultContentDetail defaultContent">
            <%=sData %>
        </div>
        <div class="defaultFooter cate-menu-footer">
        </div>
    </div>
</div>
