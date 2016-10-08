<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModAdvEntity>; %>

<%for (int i = 0; listItem != null && i < listItem.Count; i++ )
{ %>
  <%= Utils.GetCodeAdv(listItem[i])%>
<%} %>