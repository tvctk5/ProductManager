<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModNewsEntity>;
    // Luôn chuyển đến khuyến mại mới nhất (Nếu có)
    string sUrl = string.Empty;
    if (listItem != null && listItem.Count > 0)
        sUrl = ViewPage.GetURL(listItem[0].MenuID, listItem[0].Code).Replace("/Khuyen-mai/","/Tin-khuyen-mai/");
%>
<script type="text/javascript">
    var url = '<%=sUrl %>';
    if (url != "")
        window.location.href = url;
</script>
<div class="div-module">
    <div class="module-title">
        <%= ViewPage.CurrentPage.Name %></div>
    <div class="module-content">
        <div class="no-news">
            Chưa có bản tin khuyến mãi nào</div>
    </div>
    <div class="module-footer">
    </div>
</div>
