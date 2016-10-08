<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModWardModel;
    var item = ViewBag.Data as ModWardEntity;
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
            <h2>Xã phường : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Quận huyện :</label>
                </td>
                <td>
                    <select class="text_input" name="ModDistrictId" id="ModDistrictId" >
                        <%=VSW.Lib.Global.Utils.ShowDDLList(ModDistrictService.Instance, item.ModDistrictId)%>
                    </select>
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Mã :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>" maxlength="255" />
                </td>
            </tr>
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
                    <label>Loại :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Type" id="Type" value="<%=item.Type %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Tên đầy đủ :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="FullName" id="FullName" value="<%=item.FullName %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Tên quận huyện :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="DistrictFullName" id="DistrictFullName" value="<%=item.DistrictFullName %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Location :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Location" id="Location" value="<%=item.Location %>" maxlength="255" />
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
                    <label>Ngày tạo :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>" maxlength="255" readonly="readonly"/>
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>