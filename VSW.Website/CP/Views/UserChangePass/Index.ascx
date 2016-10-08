<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetListCommand("apply|Lưu,save|Lưu  &amp; đóng,space,cancel|Đóng")%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>Thay đổi mật khẩu</h2> 
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
                    <label>Mật khẩu cũ :</label>
                </td>
                <td>
                    <input class="text_input" type="password" name="CurrentPassword" maxlength="50" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Mật khẩu mới :</label>
                </td>
                <td>
                    <input class="text_input" type="password" name="NewPassword" maxlength="50" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Xác nhận lại mật khẩu:</label>
                </td>
                <td>
                    <input class="text_input" type="password" name="ConfirmPassword" maxlength="50" />
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>
</form>
