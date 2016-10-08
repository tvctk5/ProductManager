<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    // Lấy dữ liệu ở đây
    int CustomId = 0;
    ModProduct_CustomersEntity objCustom = new ModProduct_CustomersEntity();
    if (VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"))
    {
        CustomId = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue("VSW.CustomerId"));

        objCustom = ModProduct_CustomersService.Instance.GetByID(CustomId);
        if (objCustom == null)
            objCustom = new ModProduct_CustomersEntity();
    } 
        
%>
<script src="/Content/html/scripts/add/Product.js" type="text/javascript"></script>
<form id="frmAccountSetting" method="post">
<div class="DefaultModuleContent">
    <div class="defaultContentTitle TitleContent title">
        <div class="title">
            <%= ViewBag.Title %></div>
    </div>
    <div class="defaultContentDetail defaultContent">
        <div class="info-detail">
            <div class="info-detail-tab">
                <div class="menu_bar">
                    <ul>
                        <li control="#div_tab_content_01" class="menu_bar-li menu_bar-seleted"><span>Thông tin
                            tài khoản</span></li>
                        <li control="#div_tab_content_02" class="menu_bar-li"><span>Danh sách các đơn hàng</span></li>
                    </ul>
                </div>
                <div class="detail_content">
                    <div id="div_tab_content_01" class="div_tab_content_detail">
                        <table class="adminlist tbl-account-setting">
                            <tr>
                                <td style="width: 5%">
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Tài khoản :</label>
                                </td>
                                <td style="width: 90%">
                                    <input class="text_input" type="text" name="UserName" id="UserName" maxlength="50"
                                        require="true" readonly="readonly" value="<%=objCustom.UserName %>" />
                                </td>
                                <td style="width: 3%">
                                    &nbsp;<span>(<span class="require">*</span>)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                </td>
                                <td>
                                    <span>
                                        <input type="checkbox" id="chkChangPass" name="chkChangPass" />
                                        Đổi mật khẩu</span>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr class="tr-changepass hide">
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Mật khẩu cũ :</label>
                                </td>
                                <td>
                                    <input class="text_input" type="password" name="OldPass" id="OldPass" maxlength="20"
                                        require="true" />
                                </td>
                                <td>
                                    &nbsp;<span>(<span class="require">*</span>)</span>
                                </td>
                            </tr>
                            <tr class="tr-changepass hide">
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Mật khẩu mới :</label>
                                </td>
                                <td>
                                    <input class="text_input" type="password" name="Pass" id="Pass" maxlength="20" require="true" />
                                </td>
                                <td>
                                    &nbsp;<span>(<span class="require">*</span>)</span>
                                </td>
                            </tr>
                            <tr class="tr-changepass hide">
                                <td>
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
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Họ và Tên :</label>
                                </td>
                                <td>
                                    <input class="text_input" type="text" name="FullName" id="FullName" maxlength="255"
                                        require="true" value="<%=objCustom.FullName %>" />
                                </td>
                                <td>
                                    &nbsp;<span>(<span class="require">*</span>)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Ngày sinh :</label>
                                </td>
                                <td>
                                    <input class="text_input" type="text" name="Birthday" id="Birthday" maxlength="10"
                                        value="<%=objCustom.Birthday==null?"": ConvertTool.ConvertToDateTime(objCustom.Birthday).ToString("dd/MM/yyyy") %>" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Giới tính :</label>
                                </td>
                                <td>
                                    <input name="Sex" type="radio" value='1' <%=objCustom.Sex?"checked":"" %> />
                                    Nữ
                                    <input name="Sex" type="radio" value='0' <%=objCustom.Sex?"":"checked" %> />
                                    Nam
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Địa chỉ :</label>
                                </td>
                                <td>
                                    <input class="text_input" type="text" name="Address" id="Address" maxlength="255"
                                        value="<%=objCustom.Address %>" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Email :</label>
                                </td>
                                <td>
                                    <input class="text_input" type="text" name="Email" id="Email" maxlength="255" value="<%=objCustom.Email %>" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Số điện thoại :</label>
                                </td>
                                <td>
                                    <input class="text_input" type="text" name="PhoneNumber" id="PhoneNumber" maxlength="255"
                                        value="<%=objCustom.PhoneNumber %>" />
                                </td>
                                <td>
                                    &nbsp;<span>(<span class="require">*</span>)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                    <label>
                                        Ảnh đại diện :</label>
                                </td>
                                <td>
                                    <div>
                                        <%= Utils.GetMedia(objCustom.File, 100, 80)%></div>
                                    <br />
                                    <input class="text_input" type="file" name="File" id="File" style="width: 90%;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="key">
                                </td>
                                <td>
                                    <input class="text_input" type="button" name="btnUpdateAccount" id="btnUpdateAccount"
                                        value="Cập nhật tài khoản" style="width: 26%" />

                                        <input class="text_input" type="button" name="btnRefreshAccountSetting" id="btnRefreshAccountSetting"
                                        value="Hủy cập nhật" style="width: 20%" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="div_tab_content_02" class="div_tab_content_detail hide">
                        qwfqfqwf
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        langCode = '<%=ViewPage.CurrentLang.Code %>';
        var CustomId = '<%=CustomId %>';

        if (CustomId == "0")
            window.location.href = '/' + langCode + "/tai-khoan/dang-nhap.aspx";

        $(document).ready(function () {

            $("li.menu_bar-li").click(function () {
                $("li.menu_bar-li").removeClass("menu_bar-seleted");
                $(this).addClass("menu_bar-seleted");

                $("div.div_tab_content_detail").addClass("hide");
                var div_control_id = $(this).attr("control");
                $(div_control_id).removeClass("hide");
            });

            $("#chkChangPass").change(function () {
                var checked = $(this).is(":checked");
                if (checked == true)
                    $("tr.tr-changepass").removeClass("hide");
                else
                    $("tr.tr-changepass").addClass("hide");
            });

            $("#btnUpdateAccount").click(function () {
                if (CheckValidateForm() == false)
                    return false;

                linkPost = '<%=ResolveUrl("~/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=publicUpdateAccount")%>';

                UpdateAccount_Form("frmAccountSetting", CustomId);
                return false;
            });

            $("#btnRefreshAccountSetting").click(function () {
                location.reload(false);
            });

        });

        function CheckValidateForm() {
            var bolUserName = false;
            var bolOldPass = true;
            var bolPass = true;
            var bolRePass = true;
            var bolPassCompare = true;
            var bolFullName = false;
            //var bolAddress = false;
            var bolEmail = false;
            var bolPhoneNumber = false;

            var Mess = "";
            var UserName = $("#UserName");
            var OldPass = $("#OldPass");
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

            var checked = $("#chkChangPass").is(":checked");
            // thay đổi mật khẩu
            if (checked == true) {
                if (OldPass != null) {
                    if (OldPass.val().trim() == "") {
                        bolOldPass = false;
                        if (Mess != "")
                            Mess = Mess + "\r\n";
                        Mess = Mess + " - Yêu cầu nhập Mật khẩu cũ";
                    }
                }

                if (Pass != null) {
                    if (Pass.val().trim() == "") {
                        bolPass = false;
                        if (Mess != "")
                            Mess = Mess + "\r\n";
                        Mess = Mess + " - Yêu cầu nhập Mật khẩu mới";
                    }
                }

                if (RePass != null) {
                    if (RePass.val().trim() == "") {
                        bolRePass = false;

                        if (Mess != "")
                            Mess = Mess + "\r\n";
                        Mess = Mess + " - Yêu cầu nhập lại Mật khẩu mới";
                    }
                }

                if (Pass != null && RePass != null) {
                    if (Pass.val() != RePass.val()) {
                        bolPassCompare = false;

                        if (Mess != "")
                            Mess = Mess + "\r\n";

                        bolRePass = false;
                        Mess = Mess + " - Mật khẩu không trùng nhau";
                    }
                }

                // End: If changrb password
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

            if (bolUserName && bolOldPass && bolPass && bolRePass && bolPassCompare && bolFullName && bolEmail && bolPhoneNumber)
                return true;
            else {
                alert(Mess);
                if (bolUserName == false) {
                    UserName.focus(); return false;
                }

                if (bolOldPass == false) {
                    OldPass.focus(); return false;
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
</div>
</form>