<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var item = ViewBag.Data as ModFeedbackEntity;
    if (item == null)
        item = new ModFeedbackEntity();
%>
<div class="div-module">
    <div class="module-title">
        <%= ViewPage.CurrentPage.Name %></div>
    <div class="module-content">
        <form method="post">
        <script type="text/javascript">
            $(document).ready(function () {
                $("#btnSubmit_AddFeedback").click(function () {
                    var Comment_Name = $("#Name");
                    var Comment_Email = $("#Email");
                    var Comment_Title = $("#Title");
                    var Comment_Content = $("#Content");
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
                });
            });
        </script>
        <table class="adminlist">
            <tr>
                <td nowrap="nowrap">
                    {RS:Web_FB_Name}:
                </td>
                <td>
                    <input type="text" id="Name" name="Name" value="<%=item.Name %>" class="required"/>
                    <sup><font color="#FF0000">(*)</font></sup>
                </td>
                <td nowrap="nowrap">
                    Email:
                </td>
                <td>
                    <input type="text" id="Email" name="Email" value="<%=item.Email %>" class="required"/>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    Số điện thoại:
                </td>
                <td>
                    <input type="text" id="Phone" name="Phone" value="<%=item.Phone %>" class="required"/>
                </td>
                <td nowrap="nowrap">
                    Địa chỉ:
                </td>
                <td>
                    <input type="text" id="Address" name="Address" value="<%=item.Address %>" class="required" />
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    {RS:Web_FB_Title}:
                </td>
                <td colspan="3">
                    <input type="text" id="Title" name="Title" value="<%=item.Title %>" class="required" size="95" />
                    <sup><font color="#FF0000">(*)</font></sup>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    {RS:Web_FB_Content}:
                </td>
                <td style="vertical-align:top" valign="top" colspan="3">
                    <textarea id="Content" name="Content" class="required" rows="8" cols="47" style="width:94%"><%=item.Content %></textarea><sup><font
                        color="#FF0000">&nbsp;(*)</font></sup>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <input id="btnSubmit_AddFeedback" type="submit" name="_vsw_action[AddPOST]" value="{RS:Web_FB_Send}" />
                    <input type="reset" name="Reset" value="{RS:Web_FB_Reset}" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div class="module-footer">
    </div>
</div>
