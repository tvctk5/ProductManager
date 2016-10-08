<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<script runat="server">
   
    List<CPAccessEntity> listUserAccess = null;
    SysRoleModel model = null;
    CPRoleEntity item = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        model = ViewBag.Model as SysRoleModel;
        item = ViewBag.Data as CPRoleEntity;

        if (model.RecordID > 0)
        {
            listUserAccess = CPAccessService.Instance.CreateQuery()
                                 .Where(o => o.Type == "CP.MODULE" && o.RoleID == model.RecordID)
                                 .ToList();
        }
    }

    int GetAccess(string ref_code)
    {
        if (listUserAccess == null)
            return 0;

        CPAccessEntity obj = listUserAccess.Find(o => o.RefCode == ref_code);

        return obj == null ? 0 : obj.Value;
    }
</script>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-groups">
            <h2>Nhóm người sử dụng : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Tên nhóm người sử dụng :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Name" value="<%=item.Name %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Quyền :</label>
                </td>
                <td>
                    <table class="adminlist" style="width:99%;" cellspacing="1">
                        <thead>
                            <tr>
                                <th width="5">
                                    #
                                </th>
                                <th width="50">
                                    Duyệt
                                    <br />
                                    <input onclick="javascript:vsw_checkAll(document.forms[0], 'ArrApprove', this.checked);" type="checkbox" />
                                </th>
                                <th width="50">
                                    Xóa
                                    <br />
                                    <input onclick="javascript:vsw_checkAll(document.forms[0], 'ArrDelete', this.checked);" type="checkbox" />
                                </th>
                                <th width="50">
                                    Sửa
                                    <br />
                                    <input onclick="javascript:vsw_checkAll(document.forms[0], 'ArrEdit', this.checked);" type="checkbox" />
                                </th>
                                <th width="50">
                                    Thêm
                                    <br />
                                    <input onclick="javascript:vsw_checkAll(document.forms[0], 'ArrAdd', this.checked);" type="checkbox" />
                                </th>
                                <th width="50">
                                    Truy cập<br />
                                    <input onclick="javascript:vsw_checkAll(document.forms[0], 'ArrView', this.checked);" type="checkbox" />
                                </th>
                                <th class="title">
                                    Tên chức năng
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                           <tr class="row0">
                                <td>
                                    1
                                </td>
                                <td align="center">
                                    <input name="ArrApprove" value="SysAdministrator" disabled="disabled" type="checkbox" />
                                </td>
                                <td align="center">
                                    <input name="ArrApprove" value="SysAdministrator" disabled="disabled" type="checkbox" />
                                </td>
                                <td align="center">
                                   <input name="ArrApprove" value="SysAdministrator" disabled="disabled" type="checkbox" />
                                </td>
                                <td align="center">
                                    <input name="ArrApprove" value="SysAdministrator" disabled="disabled" type="checkbox" />
                                </td>
                                <td align="center">
                                    <input name="ArrView" value="SysAdministrator" <%if((GetAccess("SysAdministrator")&1) == 1){ %>checked="checked"<%} %> type="checkbox" />
                                </td>
                                <td>
                                    QUẢN TRỊ HỆ THỐNG
                                </td>
                            </tr>
                           <%for (int i = 0; i < VSW.Lib.Web.Application.CPModules.OrderBy(o => o.Order).ToList().Count; i++)
                             { %>
                            <tr class="row<%= (i+1)%2 %>">
                                <td>
                                    <%= i + 2%>
                                </td>
                                <td align="center">
                                    <input name="ArrApprove" value="<%= VSW.Lib.Web.Application.CPModules[i].Code%>" <%if((GetAccess(VSW.Lib.Web.Application.CPModules[i].Code)&16) == 16){ %>checked="checked"<%} %> <%if((VSW.Lib.Web.Application.CPModules[i].Access&16) != 16){ %>disabled="disabled"<%} %> type="checkbox" />
                                </td>
                                <td align="center">
                                    <input name="ArrDelete" value="<%= VSW.Lib.Web.Application.CPModules[i].Code%>" <%if((GetAccess(VSW.Lib.Web.Application.CPModules[i].Code)&8) == 8){ %>checked="checked"<%} %> <%if((VSW.Lib.Web.Application.CPModules[i].Access&8) != 8){ %>disabled="disabled"<%} %> type="checkbox" />
                                </td>
                                <td align="center">
                                    <input name="ArrEdit" value="<%= VSW.Lib.Web.Application.CPModules[i].Code%>" <%if((GetAccess(VSW.Lib.Web.Application.CPModules[i].Code)&4) == 4){ %>checked="checked"<%} %> <%if((VSW.Lib.Web.Application.CPModules[i].Access&4) != 4){ %>disabled="disabled"<%} %> type="checkbox" />
                                </td>
                                <td align="center">
                                    <input name="ArrAdd" value="<%= VSW.Lib.Web.Application.CPModules[i].Code%>" <%if((GetAccess(VSW.Lib.Web.Application.CPModules[i].Code)&2) == 2){ %>checked="checked"<%} %> <%if((VSW.Lib.Web.Application.CPModules[i].Access&2) != 2){ %>disabled="disabled"<%} %> type="checkbox" />
                                </td>
                                <td align="center">
                                    <input name="ArrView" value="<%= VSW.Lib.Web.Application.CPModules[i].Code%>" <%if((GetAccess(VSW.Lib.Web.Application.CPModules[i].Code)&1) == 1){ %>checked="checked"<%} %> <%if((VSW.Lib.Web.Application.CPModules[i].Access&1) != 1){ %>disabled="disabled"<%} %> type="checkbox" />
                                </td>
                                <td>
                                    <%= VSW.Lib.Web.Application.CPModules[i].Description%>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>