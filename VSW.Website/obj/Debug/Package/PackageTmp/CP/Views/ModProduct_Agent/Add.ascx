<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%--<script src="../../Content/add/jQuery/jquery-1.8.2.js" type="text/javascript"></script>
<script src="../../Content/add/jQuery/Common.js" type="text/javascript"></script>--%>
<script language="javascript" type="text/javascript">

    var pageUrl = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Agent/PostData.aspx?Type=1")%>';
    var pageUrlGetTinhThanh = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Agent/PostData.aspx?Type=2")%>';
    var formID = "vswForm";

    function CheckValidationForm() {
        var bolCode = false;
        var bolName = false;
        var Mess = "";
        var txtCode = document.getElementById("Code");
        var txtName = document.getElementById("Name");

        if (txtCode != null) {
            if (txtCode.value.trim() != "") {
                if (txtCode.value.trim().length < 3) {
                    Mess = " - Yêu cầu nhập Mã đại lý từ 3 ký tự trở lên";
                    bolCode = false;
                }
                else {
                    bolCode = true;
                }
            }
            else {
                Mess = " - Yêu cầu nhập Mã đại lý";
            }
        }

        if (txtName != null) {
            if (txtName.value.trim() != "") {
                bolName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên đại lý";
            }
        }

        if (bolName && bolCode) {
            return true;
        }
        else {
            alert(Mess);
            if (bolCode == false) {
                txtCode.focus(); return false;
            }

            if (bolName == false) {
                txtName.focus(); return false;
            }
        }
    }

    $(document).ready(function () {
        $("#Code").blur(function () {

            if (this.value.trim() == "")
                return;

            CheckDuplicate(pageUrl, formID, "Code");

        });

        // Lấy danh sách tỉnh thành
        $("#QuocGia").change(function () {

            if (this.value.trim() == "")
                return;

            GetData(pageUrlGetTinhThanh, formID, "", "ProductCityId");

        });
    });
 
</script>
<%
    var model = ViewBag.Model as ModProduct_AgentModel;
    var item = ViewBag.Data as ModProduct_AgentEntity;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="RecordID" name="RecordID" value="<%=model.RecordID %>" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <!--GetDefaultAddCommand()-->
            <%= GetDefaultAddCommandValidation()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Đại lý :
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
<div id="divMessError" style="display: none;">
</div>
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
                            Đại lý cấp cha :</label>
                    </td>
                    <td>
                        <select name="ParentID" id="ParentID" class="DropDownList">
                            <option value="0">--- Chọn ---</option>
                            <%=VSW.Lib.Global.Utils.ShowDDLDynamic(ModProduct_AgentService.Instance, "ID<>" + item.ID, "Name",item.ParentID)%>

                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Quốc gia :</label>
                    </td>
                    <td>
                        <select name="ModCountryId" id="ModCountryId" class="DropDownList" onchange="vsw_exec_cmd('RefreshPage')">
                            <option value="0">--- Chọn ---</option>
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModCountryService.Instance, item.ModCountryId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tỉnh thành :</label>
                    </td>
                    <td>
                        <select name="ModProvinceId" id="ModProvinceId" class="DropDownList" onchange="vsw_exec_cmd('RefreshPage')">
                            <option value="0">--- Chọn ---</option>
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModProvinceService.Instance, "ModCountryId=" + item.ModCountryId, item.ModProvinceId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Quận huyện :</label>
                    </td>
                    <td>
                        <select name="ModDistrictId" id="ModDistrictId" class="DropDownList" onchange="vsw_exec_cmd('RefreshPage')">
                            <option value="0">--- Chọn ---</option>
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModDistrictService.Instance, "ModProvinceId=" + item.ModProvinceId, item.ModDistrictId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Xã phường :</label>
                    </td>
                    <td>
                        <select name="ModWardId" id="ModWardId" class="DropDownList">
                            <option value="0">--- Chọn ---</option>
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModWardService.Instance, "ModDistrictId=" + item.ModDistrictId, item.ModWardId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" require="true" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" require="true" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Địa chỉ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Address" id="Address" value="<%=item.Address %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Số điện thoại :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="PhoneNumber" id="PhoneNumber" value="<%=item.PhoneNumber %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Loại đại lý :</label>
                    </td>
                    <td>
                        <select name="ModLoaiDaiLyId" id="ModLoaiDaiLyId" class="DropDownList">
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModLoai_DaiLyService.Instance, item.ModLoaiDaiLyId)%>
                        </select>
                    </td>
                </tr>
                <%
                    if (model.RecordID > 0)
                    {
                %>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" readonly="readonly" type="text" name="CreateDate" id="CreateDate"
                            value="<%=item.CreateDate %>" maxlength="255" />
                    </td>
                </tr>
                <%
                    }
                    else
                    {  %>
                <tr style="display: none;">
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" name="CreateDate" id="CreateDate"
                            value="<%=item.CreateDate %>" maxlength="255" />
                    </td>
                </tr>
                <%}%>
                <%if (CPViewPage.UserPermissions.Approve)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Sử dụng :</label>
                    </td>
                    <td>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Sử dụng
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Không sử dụng
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
<div class="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list">
            <%= GetDefaultAddCommandValidation()%>
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
