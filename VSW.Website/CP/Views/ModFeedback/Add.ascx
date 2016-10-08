<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<% 
    var model = ViewBag.Model as ModFeedbackModel;
    var item = ViewBag.Data as ModFeedbackEntity;
 %>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetListCommand("cancel|Đóng")%>
        </div>
        <div class="pagetitle icon-48-massmail">
            <h2>Liên hệ : Xem</h2> 
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
                    <label>Họ và tên :</label>
                </td>
                <td>
                    <%=item.Name %>
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Địa chỉ :</label>
                </td>
                <td>
                    <%=item.Address %>
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Điện thoại :</label>
                </td>
                <td>
                    <%=item.Phone %>
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Di động :</label>
                </td>
                <td>
                    <%=item.Mobile %>
                </td>
            </tr>
             <tr>
                <td class="key">
                    <label>Email :</label>
                </td>
                <td>
                    <%=item.Email %>
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Tiêu đề :</label>
                </td>
                <td>
                    <%=item.Title %>
                </td>
            </tr>
             <tr>
                <td class="key">
                    <label>Nội dung :</label>
                </td>
                <td>
                    <%if (!string.IsNullOrEmpty(item.Content)){ %>
                       <%=item.Content.Replace("\n","<br />") %>
                    <%} %>
                </td>
            </tr>
             <tr>
                <td class="key">
                    <label>IP :</label>
                </td>
                <td>
                   <%=item.IP %>
                </td>
            </tr>
             <tr>
                <td class="key">
                    <label>Ngày gửi :</label>
                </td>
                <td>
                   <%=string.Format("{0:dd/MM/yyyy HH:mm}", item.Created) %>
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>