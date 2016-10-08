<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var item = ViewBag.Data as ModListMailNewsLetterEntity;
    if (item == null)
        item = new ModListMailNewsLetterEntity();

    // Biến kiểm tra xem form đang đóng hay mở
    bool Opened = true;
    int iOpened = 1;
    string sCookieName = ViewBag.CookieName;
    if (VSW.Lib.Global.Cookies.Exists(sCookieName))
    {
        iOpened = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue(sCookieName));
        Opened = iOpened == 1 ? true : false;
    }
%>
<div class="div-popup-mail-news-letter div-module-popup" id="<%=sCookieName %>">
    <div class="module-popup-title" opened="<%=iOpened %>" alwayopen="0" cookiename="<%=sCookieName %>">
        <a href="javascript:void();" title="Bấm để Đóng/ Mở đăng ký nhận email" class="module-popup-title-img" opened="<%=iOpened %>"></a>
        <span>
            <%=ViewBag.Title %></span><input type="hidden" name="CookieName" value="<%=sCookieName %>" /></div>
    <div class="module-popup-content<%=(Opened?"":" hide") %>">
        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=sCookieName %> .btnSubmit_Popup").click(function () {
                    var Comment_Name = $("#Name<%=sCookieName %>");
                    var Comment_Email = $("#Email<%=sCookieName %>");
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                    if (Comment_Name == null || Comment_Name.val() == "") {
                        alert("Yêu cầu nhập Họ và tên");
                        Comment_Name.focus();
                        return false;
                    }

                    if (Comment_Email == null || Comment_Email.val() == "") {
                        alert("Yêu cầu nhập Email");
                        Comment_Email.focus();
                        return false;
                    } else {
                        if (!filter.test(Comment_Email.val())) {
                            alert("Email không đúng định dạng");
                            Comment_Email.focus();
                            return false;
                        }
                    }


                    // post đi
                    linkPost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=submitpopup_mailletter";
                    SubmitPopup_Feedback($("#<%=sCookieName %>"));
                });
            });
        </script>
        <table class="adminlist">
            <tr>
                <td nowrap="nowrap">
                    Họ và tên:
                </td>
                <td>
                    <input type="text" id="Name<%=sCookieName %>" name="Name<%=sCookieName %>" value="<%=item.Name %>"
                        class="required" />
                    <sup><font color="#FF0000">(*)</font></sup>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    Email:
                </td>
                <td>
                        <input type="text" id="Email<%=sCookieName %>" name="Email<%=sCookieName %>" value="<%=item.Email %>"
                            class="required" />
                    <span><sup><font color="#FF0000">(*)</font></sup>&nbsp; Giới tính:&nbsp;
                        <select id="Sex<%=sCookieName %>" name="Sex<%=sCookieName %>">
                            <option value="0">Nam</option>
                            <option value="1">Nữ</option>
                        </select></span>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input class="btnSubmit_Popup" type="button" value="Đăng ký" />
                </td>
            </tr>
        </table>
    </div>
    <div class="module-footer">
    </div>
</div>
