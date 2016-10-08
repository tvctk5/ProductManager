<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModListMailNewsLetterModel;
    var item = ViewBag.Data as ModListMailNewsLetterEntity;
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
            <h2>List mail news letter : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Tên :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Email :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Email" id="Email" value="<%=item.Email %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Giới tình :</label>
                </td>
                <td>
                    <input name="Sex" <%= item.Sex ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="Sex" <%= !item.Sex ? "checked" : "" %> type="radio" value='0' /> Không
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
           <tr>
                <td class="key">
                    <label>Code remove list :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="CodeRemoveList" id="CodeRemoveList" value="<%=item.CodeRemoveList %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Create date :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>" maxlength="255" />
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
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>