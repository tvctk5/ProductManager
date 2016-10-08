<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    // Lấy dữ liệu ở đây
    int CustomId = 0;
    if (VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"))
        CustomId = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue("VSW.CustomerId"));

    // Lấy mã reset  
    string sKey = ConvertTool.ConvertToString(Request["Key"]);

    // Không tìm thấy mã reset
    if (string.IsNullOrEmpty(sKey))
    {
        Response.Redirect("/");
        return;
    }
    // Tìm thông tin mã reset
    var checkKey = ModProduct_CustomersService.Instance.CreateQuery().Where(o => o.KeyReset == sKey).ToSingle();  
    
%>
<script src="/Content/html/scripts/add/Product.js" type="text/javascript"></script>
<script type="text/javascript">
    langCode = '<%=ViewPage.CurrentLang.Code %>';
    var CustomId = '<%=CustomId %>';
    <% if(checkKey==null) {%>
        alert("Không tìm thấy mã đặt lại mật khẩu trong hệ thống.");
        window.location.href="/";
    <%
    Response.Redirect("/");
    } %>

    <%if(checkKey.Reseted==true){ %>
        alert("Mã đặt lại mật khẩu đã hết hạn sử dụng.");
        window.location.href="/";
    <%
    Response.Redirect("/");
    } %>

    $(document).ready(function () {
     
        // Đã đăng nhập
        if (CustomId != "0")
            window.location.href = "/" + langCode + "/tai-khoan/Thiet-lap-tai-khoan.aspx";

        $("#btnReset").click(function () {
            if (CheckValidateForm() == false)
                return false;

            ResetPassword("<%=checkKey.ID %>", $("#Pass").val());
            return false;
        });
    });

    // Kiểm tra xem đã nhập dữ liệu chưa
    function CheckValidateForm() {
        var Pass = $("#Pass").val();
        var RePass = $("#RePass").val();

        if (Pass.trim() == "") {
            alert("Yêu cầu nhập Mật khẩu mới");
            $("#Pass").focus();
            return false;
        }

        if (RePass.trim() == "") {
            alert("Yêu cầu nhập lại Mật khẩu");
            $("#RePass").focus();
            return false;
        }

        if (RePass != Pass) {
            alert("Mật khẩu không trùng nhau");
            $("#RePass").focus();
            return false;
        }

        return true;
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
                            readonly="readonly" value="<%=checkKey.UserName %>" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Họ và tên :</label>
                    </td>
                    <td nowrap="nowrap">
                        <input class="text_input" type="text" name="FullName" id="FullName" maxlength="50"
                            readonly="readonly" value="<%=checkKey.FullName %>" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Email :</label>
                    </td>
                    <td nowrap="nowrap">
                        <input class="text_input" type="text" name="Email" id="Email" maxlength="30" readonly="readonly"
                            value="<%=checkKey.Email %>" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Mật khẩu mới :</label>
                    </td>
                    <td nowrap="nowrap">
                        <input class="text_input" type="password" name="Pass" id="Pass" maxlength="50" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Nhập lại Mật khẩu :</label>
                    </td>
                    <td nowrap="nowrap">
                        <input class="text_input" type="password" name="RePass" id="RePass" maxlength="50" />
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
                        <input style="width: 30%" class="text_input" type="button" name="btnReset" id="btnReset"
                            value="Đặt lại mật khẩu" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
