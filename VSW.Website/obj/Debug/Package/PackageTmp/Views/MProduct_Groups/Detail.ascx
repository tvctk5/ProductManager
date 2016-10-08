<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModProduct_GroupsEntity;
    var listOther = ViewBag.Other as List<ModProduct_GroupsEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-parentid">Parent id : <%= string.Format("{0:#,##0}", item.ParentId) %></p>
    <p class="item-code">Mã : <%= item.Code %></p>
    <p class="item-codeparent">Code parent : <%= item.CodeParent %></p>
    <p class="item-name">Tên : <%= item.Name %></p>
    <p class="item-note">Note : <%= item.Note %></p>
    <p class="item-file">Ảnh : <%= item.File %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].Code);%>
    <p class="list-other-name"><a href="<%=Url %>"><%= listOther[i].Name %></a></p>
    <p class="list-other-img"><a href="<%=Url %>">
                    <%if (!string.IsNullOrEmpty(listOther[i].File))
                      { %><img src="<%= Utils.GetResizeFile(listOther[i].File, 2, 100, 100)%>" alt="<%= listOther[i].Name %>" /><%} %></a></p>
<%}%>
</div>
