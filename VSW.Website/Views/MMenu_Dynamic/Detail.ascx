<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModMenu_DynamicEntity;
    var listOther = ViewBag.Other as List<ModMenu_DynamicEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-modmenutypeid">Mod menu type : <%= item.getModMenuType().Name %></p>
    <p class="item-parentid">Cha : <%= item.getParent().Name %></p>
    <p class="item-langid">Ngôn ngữ : <%= item.getLang().Name %></p>
    <p class="item-syspageid">Sys page : <%= item.getSysPage().Name %></p>
    <p class="item-name">Tên : <%= item.Name %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].ID.ToString());%>
    <p class="list-other-name"><a href="<%=Url %>"><%= listOther[i].Name %></a></p>
<%}%>
</div>
