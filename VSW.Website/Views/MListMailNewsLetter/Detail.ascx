<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModListMailNewsLetterEntity;
    var listOther = ViewBag.Other as List<ModListMailNewsLetterEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-name">Tên : <%= item.Name %></p>
    <p class="item-email">Email : <%= item.Email %></p>
    <p class="item-sex">Giới tình : <%= item.Sex ? "Có" : "Không" %></p>
    <p class="item-ip">I p : <%= item.IP %></p>
    <p class="item-coderemovelist">Code remove list : <%= item.CodeRemoveList %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].ID.ToString());%>
    <p class="list-other-name"><a href="<%=Url %>"><%= listOther[i].Name %></a></p>
<%}%>
</div>
