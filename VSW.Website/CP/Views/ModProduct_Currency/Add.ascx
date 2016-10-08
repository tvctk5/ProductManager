<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_CurrencyModel;
    var item = ViewBag.Data as ModProduct_CurrencyEntity;
%>
<script language="javascript" type="text/javascript">
    var pageUrl = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Currency/PostData.aspx?Type=1")%>';
    var formID = "vswForm";

    function CheckValidationForm() {
        var bolCode = false;
        var bolName = false;
        var bolVND = false;
        var Mess = "";
        var txtCode = document.getElementById("Code");
        var txtName = document.getElementById("Name");
        var txtVND = document.getElementById("VND");

        if (txtCode != null) {
            if (txtCode.value.trim() != "") {
                if (txtCode.value.trim().length < 3) {
                    Mess = " - Yêu cầu nhập Mã Tỷ giá từ 3 ký tự trở lên";
                    bolCode = false;
                }
                else {
                    bolCode = true;
                }
            }
            else {
                Mess = " - Yêu cầu nhập Mã Tỷ giá";
            }
        }

        if (txtName != null) {
            if (txtName.value.trim() != "") {
                bolName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên Tỷ giá";
            }
        }

        if (txtVND != null) {
            if (txtVND.value.trim() != "") {
                var VNDparseFloat = parseFloat(txtVND.value.trim().replace(",","."));
                if (VNDparseFloat <= 0) {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Tỷ giá sao với VNĐ phải lớn hơn 0";
                }
                else { bolVND = true; }

                txtVND.value = txtVND.value.trim().replace(".", ",")
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tỷ giá so với VNĐ";
            }
        }

        if (bolName && bolCode && bolVND) {
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

            if (bolVND == false) {
                txtVND.focus(); return false;
            }
        }
    }

    $(document).ready(function () {
        $("#Code").blur(function () {

            if (this.value.trim() == "")
                return;

            CheckDuplicate(pageUrl, formID, "Code");

        });
    });
</script>
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
            <%= GetDefaultAddCommandValidation()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Tỷ giá :
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
<div id="divMessError" style="display:none;"></div>
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
                            Mã tỷ giá :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="50" require="true"/><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên tỷ giá :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="200" require="true"/><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tỷ giá so với VNĐ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="VND" id="VND" value="<%=item.VND %>"
                            maxlength="255" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mô tả :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Description" id="Description" value="<%=item.Description %>"
                            maxlength="200" />
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
    <div class="clr">
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
