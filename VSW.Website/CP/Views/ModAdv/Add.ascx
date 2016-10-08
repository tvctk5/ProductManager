<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<% 
    var model = ViewBag.Model as ModAdvModel;
    var item = ViewBag.Data as ModAdvEntity;
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
        <div class="pagetitle icon-48-mediamanager">
            <h2>
                Quảng cáo/Liên kết :
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
                            Tên :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" value="<%=item.Name %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mô tả :</label>
                    </td>
                    <td>
                        <textarea class="text_input" style="height: 50px" name="Description"><%=item.Description%></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ảnh/Flash :</label>
                    </td>
                    <td>
                        <%= Utils.GetMedia(item.File, 100, 80)%>
                        <br />
                        <input class="text_input" type="text" name="File" id="File" value="<%=item.File %>"
                            style="width: 80%;" maxlength="255" />
                        <input class="text_input" style="width: 17%;" type="button" onclick="ShowFileForm('File');return false;"
                            value="Chọn File" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Chiều rộng :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Width" value="<%=item.Width %>" maxlength="50" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Chiều cao :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Height" value="<%=item.Height %>" maxlength="50" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Chèn thêm :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="AddInTag" value="<%=item.AddInTag %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            URL :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="URL" value="<%=item.URL %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Target :</label>
                    </td>
                    <td>
                        <select class="text_input" name="Target">
                            <option value=''></option>
                            <option <%if(item.Target=="_blank") {%>selected<%} %> value='_blank'>_blank</option>
                            <option <%if(item.Target=="_parent") {%>selected<%} %> value='_parent'>_parent</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã code :</label>
                    </td>
                    <td>
                        <textarea class="text_input" style="height: 150px" name="AdvCode"><%=item.AdvCode%></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Chuyên mục :</label>
                    </td>
                    <td>
                        <select name="MenuID" id="MenuID" class="text_input">
                            <option value="0">Root</option>
                            <%= Utils.ShowDDLMenuByType("Adv", model.LangID, item.MenuID)%>
                        </select>
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
