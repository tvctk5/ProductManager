<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModChat_HistoryModel;
    var item = ViewBag.Data as ModChat_HistoryEntity;
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
            <h2>Chat_ history : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>From_ name :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="From_Name" id="From_Name" value="<%=item.From_Name %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>From_ user name :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="From_UserName" id="From_UserName" value="<%=item.From_UserName %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>From_ id :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="From_Id" id="From_Id" value="<%=item.From_Id %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>To_ name :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="To_Name" id="To_Name" value="<%=item.To_Name %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>To_ user name :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="To_UserName" id="To_UserName" value="<%=item.To_UserName %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>To_ id :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="To_Id" id="To_Id" value="<%=item.To_Id %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Message :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Message" id="Message" value="<%=item.Message %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>I p :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="IP" id="IP" value="<%=item.IP %>" maxlength="255" />
                </td>
            </tr>
            <%if(CPViewPage.UserPermissions.Approve) {%>
            <tr>
                <td class="key">
                    <label>Duyệt :</label>
                </td>
                <td>
                    <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
            <%} %>
           <tr>
                <td class="key">
                    <label>Create date :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>" maxlength="255" />
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>