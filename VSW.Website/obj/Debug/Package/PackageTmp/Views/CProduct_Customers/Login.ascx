<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    // Lấy dữ liệu ở đây
    ModProduct_CustomersEntity objCustomer = null;
    int CustomId = 0;
    if (VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"))
    {
        CustomId = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue("VSW.CustomerId"));
        objCustomer = ModProduct_CustomersService.Instance.CreateQuery()
                               .Where(o => o.ID == CustomId)
                               .ToSingle_Cache();

        ViewBag.Title = "Thông tin tài khoản";
    }
%>
<script src="/Content/html/scripts/add/Product.js" type="text/javascript"></script>
<script type="text/javascript">
    langCode = '<%=ViewPage.CurrentLang.Code %>';
    $(document).ready(function () {
        $("#btnLogin").click(function () {
            if (CheckValidateForm() == false)
                return false;

            linkPost = '<%=ResolveUrl("~/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=publiclogin")%>';

            Login($("#UserName").val(), $("#Pass").val());
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

        $("#btnViewDetail").click(function () {
            window.location.href = "/" + langCode + "/tai-khoan/Thiet-lap-tai-khoan.aspx";
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
            <% 
                // Chưa đăng nhập
                if (CustomId <= 0)
                {%>
            <table class="adminlist tbl-account-login">
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Tên đăng nhập :</label>
                    </td>
                </tr>
                <tr>
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
                </tr>
                <tr>
                    <td nowrap="nowrap">
                        <input class="text_input" type="password" name="Pass" id="Pass" maxlength="20" require="true" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                    </td>
                </tr>
                <tr>
                    <td class="td-help">
                        <div style="margin-bottom:10px;">
                            <a href="/<%=ViewPage.CurrentLang.Code %>/tai-khoan/dang-ky.aspx">Đăng ký tài khoản
                                mới</a></div>
                        <div style="margin-bottom:10px;">
                            <a href="/<%=ViewPage.CurrentLang.Code %>/tai-khoan/lay-lai-mat-khau.aspx">Quên mật khẩu?</a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                    </td>
                </tr>
                <tr>
                    <td>
                        <input class="text_input" type="button" name="btnLogin" id="btnLogin" value="Đăng nhập" />
                    </td>
                </tr>
            </table>
            <%}
                else
                {%>
            <table class="adminlist tbl-account-viewinfobasic">
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Tài khoản :</label>
                    </td>
                    <td style="width: 90%" nowrap="nowrap">
                        <span>
                            <%=objCustomer.UserName %></span>
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Email :</label>
                    </td>
                    <td nowrap="nowrap">
                        <span>
                            <%=objCustomer.Email %></span>
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Điện thoại :</label>
                    </td>
                    <td nowrap="nowrap">
                        <span>
                            <%=objCustomer.PhoneNumber %></span>
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Điểm :</label>
                    </td>
                    <td nowrap="nowrap">
                        <span>
                            <%=objCustomer.PointTotal %></span>
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td nowrap="nowrap">
                        <span>
                            <%=objCustomer.CreateDate %></span>
                    </td>
                </tr>
                <tr>
                    <td class="td-help">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input class="text_input" type="button" name="btnViewDetail" id="btnViewDetail" value="Quản lý tài khoản" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input class="text_input" type="button" name="btnLogout" id="btnLogout" value="Đăng xuất" />
                    </td>
                </tr>
            </table>
            <%} %>
        </div>
    </div>
</div>
