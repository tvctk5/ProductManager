<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModDT_Ky_DaiLy_DonHang_SanPhamEntity;
    var listOther = ViewBag.Other as List<ModDT_Ky_DaiLy_DonHang_SanPhamEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-moddtkydailydonhangid">Mod d t ky dai ly don hang id : <%= string.Format("{0:#,##0}", item.ModDTKyDaiLyDonHangId) %></p>
    <p class="item-modproductid">Mod product id : <%= string.Format("{0:#,##0}", item.ModProductId) %></p>
    <p class="item-soluong">So luong : <%= string.Format("{0:#,##0}", item.SoLuong) %></p>
    <p class="item-dongia">Don gia : <%= string.Format("{0:#,##0}", item.DonGia) %></p>
    <p class="item-chietkhau">Chiet khau : <%= string.Format("{0:#,##0}", item.ChietKhau) %></p>
    <p class="item-tongtien">Tong tien : <%= string.Format("{0:#,##0}", item.TongTien) %></p>
    <p class="item-tongsaugiam">Tong sau giam : <%= string.Format("{0:#,##0}", item.TongSauGiam) %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].ID.ToString());%>
<%}%>
</div>
