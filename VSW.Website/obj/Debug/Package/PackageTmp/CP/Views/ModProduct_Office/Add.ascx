<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script src="../../Content/add/jQuery/jquery-1.8.2.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    var pageUrlCheckCode = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Office/PostData.aspx?Type=1")%>';
    var pageUrlGetTinhThanh = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Office/PostData.aspx?Type=2")%>';
    var formID = "vswForm";

    //#region documentready
    $(document).ready(function () {
        $("#Code").blur(function () {

            if (this.value.trim() == "")
                return;

            CheckDuplicate(pageUrlCheckCode, formID, "Code");

        });

        // Lấy danh sách tỉnh thành
        $("#QuocGia").change(function () {

            if (this.value.trim() == "")
                return;

            GetData(pageUrlGetTinhThanh, formID, "", "ProductCityId");

        });

    });

    function CheckValidationForm() {
        return true;
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_OfficeModel;
    var item = ViewBag.Data as ModProduct_OfficeEntity;
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
                Văn phòng bán sản phẩm :
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
                            Quốc gia :</label>
                    </td>
                    <td>
                        <select name="QuocGia" id="QuocGia" class="DropDownList" require="true">
                            <%=model.DanhSachQuocGia %>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tỉnh thành :</label>
                    </td>
                    <td>
                        <select name="ProductCityId" id="ProductCityId" class="DropDownList" require="true">
                            <%=model.DanhSachTinhThanh %>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã văn phòng :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên văn phòng :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" />
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
