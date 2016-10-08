<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<script runat="server">
   
    List<CPRoleEntity> listRole = null;
    SysUserModel model = null;
    CPUserEntity item = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        model = ViewBag.Model as SysUserModel;
        item = ViewBag.Data as CPUserEntity;

        listRole = CPRoleService.Instance.CreateQuery().ToList();
    }

    bool HasRole(int roleID)
    {
        if (item.ID == 0)
            return false;

        return item.GetRole().Find(o => o.ID == roleID) != null;
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
        <div class="pagetitle icon-48-user">
            <h2>Người sử dụng : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Tên người sử dụng :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="LoginName" value="<%=item.LoginName %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Mật khẩu :</label>
                </td>
                <td>
                    <input class="text_input" type="password" name="NewPassword" value="" maxlength="50" />
                </td>
            </tr>
             <tr>
                <td class="key">
                    <label>Họ và tên :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Name" value="<%=item.Name %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Địa chỉ :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Address" value="<%=item.Address %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Điện thoại :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Phone" value="<%=item.Phone %>" maxlength="50" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Email :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Email" value="<%=item.Email %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Thuộc nhóm :</label>
                </td>
                <td>
                    <%for (int i = 0; listRole != null && i < listRole.Count; i++)
                      {%>
                      <input name="ArrRole" <%if(HasRole(listRole[i].ID)) {%> checked="checked" <%} %> value="<%= listRole[i].ID%>" type="checkbox" />
                      <%= listRole[i].Name%> <br />
                    <%} %>
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Duyệt :</label>
                </td>
                <td>
                    <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>