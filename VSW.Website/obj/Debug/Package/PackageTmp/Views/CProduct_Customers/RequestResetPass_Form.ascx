<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    // Lấy dữ liệu ở đây
    int CustomId = 0;
    if (VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"))
        CustomId = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue("VSW.CustomerId"));
%>
<script src="/Content/html/scripts/add/Product.js" type="text/javascript"></script>
<script type="text/javascript">
    langCode = '<%=ViewPage.CurrentLang.Code %>';
    var CustomId = '<%=CustomId %>';

    $(document).ready(function () {

        // Đã đăng nhập
        if (CustomId != "0")
            window.location.href = "/" + langCode + "/tai-khoan/Thiet-lap-tai-khoan.aspx";

        $("#btnRequest").click(function () {
            if (CheckValidateForm() == false)
                return false;

            RequestResetPassword($("#UserName").val(), $("#Email").val());
            return false;
        }); 
    });

    // Kiểm tra xem đã nhập dữ liệu chưa
    function CheckValidateForm() {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var UserName = $("#UserName").val();
        var Email = $("#Email").val();

        if (UserName.trim() == "") {
            alert("Yêu cầu nhập tài khoản khách hàng");
            $("#UserName").focus();
            return false;
        }

        if (Email.trim() == "") {
            alert("Yêu cầu nhập Email");
            $("#Email").focus();
            return false;
        }
        else {
            if (!filter.test(Email)) {
                alert("Email sai định dạng");
                $("#Email").focus();
                return false;
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
            <table class="adminlist tbl-account-login-form">
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Tên đăng nhập :</label>
                    </td>
                    <td style="width: 90%" nowrap="nowrap">
                        <input class="text_input" type="text" name="UserName" id="UserName" maxlength="50"
                            require="true" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Email :</label>
                    </td>
                    <td nowrap="nowrap">
                        <input class="text_input" type="text" name="Email" id="Email" maxlength="30" require="true" />
                    </td>
                </tr>
                <tr>
                    <td class="key" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="td-help">
                        <div style="margin-bottom: 10px;">
                            - <a href="/<%=ViewPage.CurrentLang.Code %>/tai-khoan/dang-ky.aspx">Đăng ký tài khoản
                                mới</a></div>
                        <div style="margin-bottom: 10px;">
                            - <a href="/<%=ViewPage.CurrentLang.Code %>/tai-khoan/dang-nhap.aspx">Đăng nhập</a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input style="width: 35%" class="text_input" type="button" name="btnRequest" id="btnRequest"
                            value="Gửi yêu cầu đặt lại mật khẩu" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
