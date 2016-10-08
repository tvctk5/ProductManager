<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var listItem = ViewBag.Data as List<ModTestEntity>;
%>
<div class="title">
    <%= ViewBag.Title %></div>
<div class="list">
    <%for (int i = 0; listItem != null && i < listItem.Count; i++)
      {
          string Url = ViewPage.GetURLCurrentPage(listItem[i].ID.ToString());%>
    <%="Column 1:" + listItem[i].Column1%>
    <%="-- Column 2:" + listItem[i].Column2%>
    <br />
    <%}%>
</div>
