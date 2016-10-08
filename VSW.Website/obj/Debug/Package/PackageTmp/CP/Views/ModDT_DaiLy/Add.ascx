<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModDT_DaiLyModel;
    var item = ViewBag.Data as ModDT_DaiLyEntity;
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
            <h2>Doanh thu - Đại lý : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Đại lý giới thiệu :</label>
                </td>
                <td>
                    <select name="ModProductAgentParentId" id="ModProductAgentParentId" class="DropDownList">
                        <option value="0">--- Là đại lý giới thiệu ---</option>
                        <%=VSW.Lib.Global.Utils.ShowDDLDynamic(ModProduct_AgentService.Instance, "ID<>" + item.ModProductAgentId + " AND ID IN (SELECT ModProductAgentId FROM Mod_DT_DaiLy)", "Name", item.ModProductAgentParentId)%>
                    </select>
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Đại lý được giới thiệu :</label>
                </td>
                <td>
                    <select name="ModProductAgentId" id="ModProductAgentId" class="DropDownList">
                        <option value="0">--- Chọn ---</option>
                        <%=VSW.Lib.Global.Utils.ShowDDLDTDaiLy(ModProduct_AgentService.Instance, "" , "Name", item.ModProductAgentId)%>
                    </select>
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Loại đại lý :</label>
                </td>
                <td>
                    <select name="ModDTLoaiDaiLyId" id="ModDTLoaiDaiLyId" class="DropDownList">
                        <option value="0">--- Chọn ---</option>
                        <%=VSW.Lib.Global.Utils.ShowDDLList(ModLoai_DaiLyService.Instance, item.ModDTLoaiDaiLyId)%>
                    </select>
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