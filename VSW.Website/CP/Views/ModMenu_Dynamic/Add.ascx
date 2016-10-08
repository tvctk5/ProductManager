<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModMenu_DynamicModel;
    var item = ViewBag.Data as ModMenu_DynamicEntity;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" name="LangID" value="<%=model.LangID %>" />
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
                Thiết lập menu :
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
                            Loại menu :</label>
                    </td>
                    <td>
                        <select name="ModMenuTypeID" id="ModMenuTypeID" class="text_input DropDownList" onchange="vsw_exec_cmd('GetParentIdByMenuType')">
                            <option value="0">--- Chọn ---</option>
                            <%= Utils.ShowDDLMenuType(item.ModMenuTypeID)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Menu cha :</label>
                    </td>
                    <td>
                        <select name="ParentID" id="ParentID" class="text_input DropDownList">
                            <option value="0">--- Chọn ---</option>
                            <%= Utils.ShowDDLMenuDynamicByMenuType(item.ModMenuTypeID,item.ParentID, model.RecordID)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Trang liên kết :</label>
                    </td>
                    <td>
                        <select name="SysPageID" id="SysPageID" class="text_input DropDownList">
                            <option value="0">--- Chọn ---</option>
                            <%= Utils.ShowDDLTrangLienKet( item.SysPageID,model.LangID)%>
                        </select>
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
                            Đường dẫn :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Url" id="Url" value="<%=item.Url %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Icon :</label>
                    </td>
                    <td>
                        <%if (!string.IsNullOrEmpty(item.IconUrl))
                          { %>
                        <%= Utils.GetMedia(item.IconUrl, 30, 30, string.Empty, true, "id='img_view'")%><%}
                          else
                          { %>
                        <img id="img_view" width="30" height="30" />
                        <%} %>
                        <br />
                        <input class="text_input" type="text" name="IconUrl" id="IconUrl" style="width: 65%"
                            value="<%=item.IconUrl %>" />
                        <input class="text_input" style="width: 100px;" type="button" onclick="ShowFileForm('IconUrl');return false;"
                            value="Chọn ảnh icon" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Hiển thị dạng popup :</label>
                    </td>
                    <td>
                        <input name="ShowPopup" <%= item.ShowPopup ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="ShowPopup" <%= !item.ShowPopup ? "checked" : "" %> type="radio" value='0' />
                        Không
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Hiển thị :</label>
                    </td>
                    <td>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Không
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input readonly="readonly" class="text_input" type="text" name="CreateDate" id="CreateDate"
                            value="<%=item.CreateDate %>" maxlength="255" />
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
