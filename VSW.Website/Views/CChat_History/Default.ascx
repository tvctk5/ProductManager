<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var listItem = ViewBag.Data as List<ModChat_HistoryEntity>;
%>

<div class="title"><%= ViewBag.Title %></div>

<div class="list">
<%for(int i = 0; listItem != null && i < listItem.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listItem[i].ID.ToString());%>
<%}%>
</div>

