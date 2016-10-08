<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModDT_Ky_DaiLy_DonHangEntity;
    var listOther = ViewBag.Other as List<ModDT_Ky_DaiLy_DonHangEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-moddtkydailyid">Mod d t ky dai ly id : <%= string.Format("{0:#,##0}", item.ModDTKyDaiLyId) %></p>
    <p class="item-code">Mã : <%= item.Code %></p>
    <p class="item-name">Tên : <%= item.Name %></p>
    <p class="item-ngaytao">Ngay tao : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.NgayTao) %></p>
    <p class="item-chietkhau">Chiet khau : <%= string.Format("{0:#,##0}", item.ChietKhau) %></p>
    <p class="item-tongsanpham">Tong san pham : <%= string.Format("{0:#,##0}", item.TongSanPham) %></p>
    <p class="item-tongtien">Tong tien : <%= string.Format("{0:#,##0}", item.TongTien) %></p>
    <p class="item-tongsaugiam">Tong sau giam : <%= string.Format("{0:#,##0}", item.TongSauGiam) %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].Code);%>
    <p class="list-other-name"><a href="<%=Url %>"><%= listOther[i].Name %></a></p>
<%}%>
</div>
