<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    // Lấy dữ liệu ở đây
    //var listItem = ViewBag.Data as List<ModProduct_CustomersEntity>; 
%>
<form id="FormCreateAccount" name="FormCreateAccount">
<%--<input type="hidden" id="_vsw_action" name="_vsw_action" />method="post" enctype="multipart/form-data"--%>
<%--<script src="/CP/Content/add/js/cp_v1.js" type="text/javascript"></script>--%>
<script type="text/javascript">
    langCode = '<%=ViewPage.CurrentLang.Code %>';
    $(document).ready(function () {
        $("#btnCreateAccount").click(function () {
            if (CheckValidateForm() == false)
                return false;

            linkPost = '<%=ResolveUrl("~/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=publicCreateAccount")%>';

            CreateAccount("FormCreateAccount");
            return false;
        });
    });

    function CheckValidateForm() {
        var bolUserName = false;
        var bolPass = false;
        var bolRePass = false;
        var bolPassCompare = false;
        var bolFullName = false;
        //var bolAddress = false;
        var bolEmail = false;
        var bolPhoneNumber = false;

        var Mess = "";
        var UserName = $("#UserName");
        var Pass = $("#Pass");
        var RePass = $("#RePass");
        var FullName = $("#FullName");
        //var Address = $("#Address");
        var Email = $("#Email");
        var PhoneNumber = $("#PhoneNumber");

        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        // ----------------Thông tin người đặt hàng

        if (UserName != null) {
            if (UserName.val().trim() != "") {
                bolUserName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tài khoản";
            }
        }

        if (Pass != null) {
            if (Pass.val().trim() != "") {
                bolPass = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Mật khẩu";
            }
        }

        if (RePass != null) {
            if (RePass.val().trim() != "") {
                bolRePass = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập lại Mật khẩu";
            }
        }

        if (Pass != null && RePass != null) {
            if (Pass.val() == RePass.val()) {
                bolPassCompare = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }

                bolRePass = false;
                Mess = Mess + " - Mật khẩu không trùng nhau";
            }
        }

        if (FullName != null) {
            if (FullName.val().trim() != "") {
                bolFullName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Họ tên";
            }
        }
         
        if (Email != null) {
            if (Email.val() != "") {
                if (!filter.test(Email.val())) {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Địa chỉ Email không hợp lệ"
                    bolEmail = false;
                }
                else
                    bolEmail = true;
            }
            else
                bolEmail = true;
        }
        else
            bolEmail = true;

        if (PhoneNumber != null) {
            if (PhoneNumber.val().trim() != "") {
                bolPhoneNumber = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Số điện thoại";
            }
        }

        if (bolUserName && bolPass && bolRePass && bolPassCompare && bolFullName && bolEmail && bolPhoneNumber)
            return true;
        else {
            alert(Mess);
            if (bolUserName == false) {
                UserName.focus(); return false;
            }

            if (bolPass == false) {
                Pass.focus(); return false;
            }

            if (bolRePass == false) {
                RePass.focus(); return false;
            }

            if (bolFullName == false) {
                FullName.focus(); return false;
            }

            if (bolEmail == false) {
                Email.focus(); return false;
            }

            if (bolPhoneNumber == false) {
                PhoneNumber.focus(); return false;
            }
        }
    }
</script>
<div class="DefaultModuleContent">
    <div class="defaultContentTitle TitleContent title">
        <div class="title">
            <%= ViewBag.Title %></div>
    </div>
    <div class="defaultContentDetail defaultContent">
        <div>
            <table class="adminlist tbl-account-register">
                <tr>
                    <td class="key" style="width: 5%">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Tài khoản :</label>
                    </td>
                    <td style="width: 90%">
                        <input class="text_input" type="text" name="UserName" id="UserName" maxlength="50"
                            require="true" />
                    </td>
                    <td style="width: 3%">
                        &nbsp;<span>(<span class="require">*</span>)</span>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Mật khẩu :</label>
                    </td>
                    <td>
                        <input class="text_input" type="password" name="Pass" id="Pass" maxlength="20" require="true" />
                        <%--<input id="Pass" name="Pass" type="hidden" />--%>
                    </td>
                    <td>
                        &nbsp;<span>(<span class="require">*</span>)</span>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Nhập lại mật khẩu :</label>
                    </td>
                    <td>
                        <input class="text_input" type="password" name="RePass" id="RePass" maxlength="20"
                            require="true" />
                    </td>
                    <td>
                        &nbsp;<span>(<span class="require">*</span>)</span>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Họ và Tên :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="FullName" id="FullName" maxlength="255"
                            require="true" />
                    </td>
                    <td>
                        &nbsp;<span>(<span class="require">*</span>)</span>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Ngày sinh :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Birthday" id="Birthday" maxlength="10" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Giới tính :</label>
                    </td>
                    <td>
                        <input name="Sex" type="radio" value='1' checked="checked" />
                        Nữ
                        <input name="Sex" type="radio" value='0' />
                        Nam
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Địa chỉ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Address" id="Address" maxlength="255" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Email :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Email" id="Email" maxlength="255" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Số điện thoại :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="PhoneNumber" id="PhoneNumber" maxlength="255" />
                    </td>
                    <td>
                        &nbsp;<span>(<span class="require">*</span>)</span>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                        <label>
                            Ảnh đại diện :</label>
                    </td>
                    <td>
                        <input class="text_input" type="file" name="File" id="File" style="width: 90%;" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        &nbsp;
                    </td>
                    <td class="key">
                    </td>
                    <td>
                        <input class="text_input" type="button" name="btnCreateAccount" id="btnCreateAccount"
                            value="Tạo tài khoản" style="width: 26%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
</form>
