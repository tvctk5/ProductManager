<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModProduct_CommentsEntity;
    var listOther = ViewBag.Other as List<ModProduct_CommentsEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-productinfoid">Product info id : <%= string.Format("{0:#,##0}", item.ProductInfoId) %></p>
    <p class="item-userid">User id : <%= string.Format("{0:#,##0}", item.UserId) %></p>
    <p class="item-customersid">Customers id : <%= string.Format("{0:#,##0}", item.CustomersId) %></p>
    <p class="item-name">Tên : <%= item.Name %></p>
    <p class="item-phonenumber">Phone number : <%= item.PhoneNumber %></p>
    <p class="item-email">Email : <%= item.Email %></p>
    <p class="item-content">Nội dung : <%= item.Content %></p>
    <p class="item-approved">Approved : <%= item.Approved ? "Có" : "Không" %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
    <p class="item-modifieddate">Modified date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.ModifiedDate) %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].ID.ToString());%>
    <p class="list-other-name"><a href="<%=Url %>"><%= listOther[i].Name %></a></p>
<%}%>
</div>
