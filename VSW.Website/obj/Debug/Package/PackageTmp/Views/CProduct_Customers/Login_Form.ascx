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

        $("#btnLogin").click(function () {
            if (CheckValidateForm() == false)
                return false;

            linkPost = '<%=ResolveUrl("~/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=publiclogin")%>';

            Login_Form($("#UserName").val(), $("#Pass").val());
            return false;
        });

        $("#btnLogout").click(function () {
            if (!confirm("Đăng xuất tài khoản?"))
                return false;


            linkPost = '<%=ResolveUrl("~/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=publiclogout")%>';
            // Đăng xuất
            Logout();
            return false;
        });
    });

    // Kiểm tra xem đã nhập dữ liệu chưa
    function CheckValidateForm() {
        var UserName = $("#UserName").val();
        var Pass = $("#Pass").val();

        if (UserName.trim() == "") {
            alert("Yêu cầu nhập tài khoản khách hàng");
            $("#UserName").focus();
            return false;
        }

        if (Pass.trim() == "") {
            alert("Yêu cầu nhập mật khẩu");
            $("#Pass").focus();
            return false;
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
                            Mật khẩu :</label>
                    </td>
                    <td nowrap="nowrap">
                        <input class="text_input" type="password" name="Pass" id="Pass" maxlength="20" require="true" />
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
                            - <a href="/<%=ViewPage.CurrentLang.Code %>/tai-khoan/lay-lai-mat-khau.aspx">Quên mật
                                khẩu?</a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input style="width: 20%" class="text_input" type="button" name="btnLogin" id="btnLogin"
                            value="Đăng nhập" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
