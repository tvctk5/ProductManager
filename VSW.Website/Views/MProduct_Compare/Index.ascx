<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var ListProduct = ViewBag.ListProduct as List<ModProduct_InfoEntity>;
    string sData = ViewBag.Data as string;
%>
<div class="DefaultModuleContent">
    <div class="defaultContentTitle TitleContent title">
        <div class="title">
            So sánh sản phẩm</div>
    </div>
    <div class="defaultContentDetail defaultContent">
        <%if (ListProduct == null || ListProduct.Count <= 0)
          {%>
        <div class="div-product-compare-empty">
            Chưa có sản phẩm nào.</div>
    </div>
</div>
<%
              return;
          } 
%>
<div>
    <table class="adminlist tbl-product-compare-form">
        <%=sData%>
    </table>
</div>
</div> </div> 