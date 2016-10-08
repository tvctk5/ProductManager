<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_FilterModel;
    var item = ViewBag.Data as ModProduct_FilterEntity;
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
                Thuộc tính lọc :
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
                            Nhóm thuộc tính lọc :</label>
                    </td>
                    <td>
                        <select name="FilterGroupsId" id="FilterGroupsId" class="DropDownList">
                            <%= Utils.ShowDDLProductFilterGroups(item.FilterGroupsId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Giá trị :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Value" id="Value" value="<%=item.Value %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ghi chú :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Note" id="Note" value="<%=item.Note %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ảnh :</label>
                    </td>
                    <td>
                        <%= Utils.GetMedia(item.File, 100, 80)%>
                        <br />
                        <input class="text_input" type="text" name="File" id="File" style="width: 80%;" value="<%=item.File %>"
                            maxlength="255" />
                        <input class="text_input" style="width: 17%;" type="button" onclick="ShowFileForm('File');return false;"
                            value="Chọn File" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Sử dụng :</label>
                    </td>
                    <td>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Không
                    </td>
                </tr>
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
