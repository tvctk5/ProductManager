<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<%
    var listItem = ViewBag.Data as List<ModDT_Ky_DaiLy_DonHang_SanPhamEntity>;
    var model = ViewBag.Model as MDT_Ky_DaiLy_DonHang_SanPhamModel;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="list">
<%for(int i = 0; listItem != null && i < listItem.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listItem[i].ID.ToString());%>
<%}%>
</div>

<div class="navigation">
  <%= GetPagination(model.Page, model.PageSize, model.TotalRecord)%>
</div>
