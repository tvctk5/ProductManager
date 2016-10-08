<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
   %>
<div class="div-compare-product">
    <div class="div-compare-product-title div-compare-product-title-hideicon" hide="TRUE">
        <span class="hide">
            <%=ViewBag.Title%></span>
    </div>
    <div class="div-compare-product-content hide">
    </div>
    <div class="div-compare-product-function hide">
        <a href="/vn/san-pham/So-sanh-san-pham.aspx">
            <input class="text_input" type="button" name="name" value="So sánh" /></a>
    </div>
</div>
<div class="div-compare-product-image">
</div>
