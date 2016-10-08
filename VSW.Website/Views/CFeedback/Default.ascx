<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var item = ViewBag.Data as ModFeedbackEntity;
    if (item == null)
        item = new ModFeedbackEntity();

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
<div class="div-popup-feedback div-module-popup" id="<%=sCookieName %>">
    <div class="module-popup-title" opened="<%=iOpened %>" alwayopen="0" cookiename="<%=sCookieName %>">
        <a href="javascript:void();" title="Bấm để Đóng/ Mở form phản hồi" class="module-popup-title-img" opened="<%=iOpened %>"></a>
        <span>
            <%=ViewBag.Title %></span><input type="hidden" name="CookieName" value="<%=sCookieName %>" /></div>
    <div class="module-popup-content<%=(Opened?"":" hide") %>">
        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=sCookieName %> .btnSubmit_Popup").click(function () {
                    var Comment_Name = $("#Name<%=sCookieName %>");
                    var Comment_Email = $("#Email<%=sCookieName %>");
                    var Comment_Title = $("#Title<%=sCookieName %>");
                    var Comment_Content = $("#Content<%=sCookieName %>");
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                    if (Comment_Name == null || Comment_Name.val() == "") {
                        alert("Yêu cầu nhập Họ và tên");
                        Comment_Name.focus();
                        return false;
                    }

                    if (Comment_Title == null || Comment_Title.val() == "") {
                        alert("Yêu cầu nhập Tiêu đề phản hồi");
                        Comment_Title.focus();
                        return false;
                    }

                    if (Comment_Content == null || Comment_Content.val() == "") {
                        alert("Yêu cầu nhập Nội dung phản hồi");
                        Comment_Content.focus();
                        return false;
                    }

                    if (Comment_Content != null || Comment_Content.val() != "") {
                        if (!filter.test(Comment_Email.val())) {
                            alert("Email không đúng định dạng");
                            Comment_Email.focus();
                            return false;
                        }
                    }

                    // post đi
                    linkPost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=submitpopup_feedback";
                    SubmitPopup_Feedback($("#<%=sCookieName %>"));
                });
            });
        </script>
        <table class="adminlist">
            <tr>
                <td nowrap="nowrap">
                    {RS:Web_FB_Name}:
                </td>
                <td>
                    <input type="text" id="Name<%=sCookieName %>" name="Name<%=sCookieName %>" value="<%=item.Name %>" class="required" />
                    <sup><font color="#FF0000">(*)</font></sup>
                </td>
                <td nowrap="nowrap">
                    Email:
                </td>
                <td>
                    <input type="text" id="Email<%=sCookieName %>" name="Email<%=sCookieName %>" value="<%=item.Email %>" class="required" />
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    Số điện thoại:
                </td>
                <td>
                    <input type="text" id="Phone<%=sCookieName %>" name="Phone<%=sCookieName %>" value="<%=item.Phone %>" class="required" />
                </td>
                <td nowrap="nowrap">
                    Địa chỉ:
                </td>
                <td>
                    <input type="text" id="Address<%=sCookieName %>" name="Address<%=sCookieName %>" value="<%=item.Address %>" class="required" />
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    {RS:Web_FB_Title}:
                </td>
                <td colspan="3">
                    <input type="text" id="Title<%=sCookieName %>" name="Title<%=sCookieName %>" value="<%=item.Title %>" class="required"
                        style="width: 90%" />
                    <sup><font color="#FF0000">(*)</font></sup>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    {RS:Web_FB_Content}:
                </td>
                <td style="vertical-align: top" valign="top" colspan="3">
                    <textarea id="Content<%=sCookieName %>" name="Content<%=sCookieName %>" class="required" rows="8" cols="47" style="width: 90%"><%=item.Content %></textarea><sup><font
                        color="#FF0000">&nbsp;(*)</font></sup>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <input class="btnSubmit_Popup" type="button" value="Gửi phản hồi" />
                </td>
            </tr>
        </table>
    </div>
    <div class="module-footer">
    </div>
</div>
