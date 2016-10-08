<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModProduct_CustomersEntity;
    var listOther = ViewBag.Other as List<ModProduct_CustomersEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-code">Mã : <%= item.Code %></p>
    <p class="item-username">User name : <%= item.UserName %></p>
    <p class="item-pass">Pass : <%= item.Pass %></p>
    <p class="item-fullname">Full name : <%= item.FullName %></p>
    <p class="item-sex">Giới tình : <%= item.Sex ? "Có" : "Không" %></p>
    <p class="item-address">Địa chỉ : <%= item.Address %></p>
    <p class="item-email">Email : <%= item.Email %></p>
    <p class="item-phonenumber">Phone number : <%= item.PhoneNumber %></p>
    <p class="item-file">Ảnh : <%= item.File %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
    <p class="item-pointtotal">Point total : <%= string.Format("{0:#,##0}", item.PointTotal) %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].Code);%>
    <p class="list-other-img"><a href="<%=Url %>">
                    <%if (!string.IsNullOrEmpty(listOther[i].File))
                      { %><img src="<%= Utils.GetResizeFile(listOther[i].File, 2, 100, 100)%>" alt="<%= listOther[i].Name %>" /><%} %></a></p>
<%}%>
</div>
