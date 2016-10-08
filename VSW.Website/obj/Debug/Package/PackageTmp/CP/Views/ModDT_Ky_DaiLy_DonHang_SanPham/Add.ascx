<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModDT_Ky_DaiLy_DonHang_SanPhamModel;
    var item = ViewBag.Data as ModDT_Ky_DaiLy_DonHang_SanPhamEntity;
%>

<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>D t_ ky_ dai ly_ don hang_ san pham : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>
<div class="clr"></div>

<%= ShowMessage()%>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="col width-100">
          <table class="admintable">
           <tr>
                <td class="key">
                    <label>Mod d t ky dai ly don hang id :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ModDTKyDaiLyDonHangId" id="ModDTKyDaiLyDonHangId" value="<%=item.ModDTKyDaiLyDonHangId %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Mod product id :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ModProductId" id="ModProductId" value="<%=item.ModProductId %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>So luong :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="SoLuong" id="SoLuong" value="<%=item.SoLuong %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Don gia :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="DonGia" id="DonGia" value="<%=item.DonGia %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Chiet khau :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ChietKhau" id="ChietKhau" value="<%=item.ChietKhau %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Tong tien :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="TongTien" id="TongTien" value="<%=item.TongTien %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Tong sau giam :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="TongSauGiam" id="TongSauGiam" value="<%=item.TongSauGiam %>" maxlength="255" />
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>