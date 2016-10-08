<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_FilterGroupsModel;
    var item = ViewBag.Data as ModProduct_FilterGroupsEntity;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Nhóm thuộc tính lọc :
                <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
        </div>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
<div class="clr">
</div>
<%= ShowMessage()%>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="col width-100">
            <table class="admintable">
                <tr>
                    <td class="key">
                        <label>
                            Mã :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Thứ tự :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Order" id="Order" value="<%=item.Order %>"
                            maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Kiểu :</label>
                    </td>
                    <td>
                        <select id="Type" name="Type" class="DropDownList">
                            <%= Utils.ShowDDLByConfigkey("Mod.TypeFilterGroup",item.Type)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Hiển thị checkbox/ radio :</label>
                    </td>
                    <td>
                        <input name="ShowControl" <%= item.ShowControl ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="ShowControl" <%= !item.ShowControl ? "checked" : "" %> type="radio"
                            value='0' />
                        Không
                    </td>
                </tr>
                <%if (CPViewPage.UserPermissions.Approve)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Duyệt :</label>
                    </td>
                    <td>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Không
                    </td>
                </tr>
                <%} %>
            </table>
        </div>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
</form>
