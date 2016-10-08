<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModProduct_RegisterEmailEntity;
    var listOther = ViewBag.Other as List<ModProduct_RegisterEmailEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-fullname">Full name : <%= item.FullName %></p>
    <p class="item-sex">Giới tính : <%= string.Format("{0:#,##0}", item.Sex) %></p>
    <p class="item-email">Email : <%= item.Email %></p>
    <p class="item-allow">Allow : <%= item.Allow ? "Có" : "Không" %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].ID.ToString());%>
<%}%>
</div>
