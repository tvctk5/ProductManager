<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModChat_HistoryEntity;
    var listOther = ViewBag.Other as List<ModChat_HistoryEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-from_name">From_ name : <%= item.From_Name %></p>
    <p class="item-from_username">From_ user name : <%= item.From_UserName %></p>
    <p class="item-from_id">From_ id : <%= string.Format("{0:#,##0}", item.From_Id) %></p>
    <p class="item-to_name">To_ name : <%= item.To_Name %></p>
    <p class="item-to_username">To_ user name : <%= item.To_UserName %></p>
    <p class="item-to_id">To_ id : <%= string.Format("{0:#,##0}", item.To_Id) %></p>
    <p class="item-message">Message : <%= item.Message %></p>
    <p class="item-ip">I p : <%= item.IP %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].ID.ToString());%>
<%}%>
</div>
