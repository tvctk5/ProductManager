<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {
        return true;
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_RegisterEmailModel;
    var item = ViewBag.Data as ModProduct_RegisterEmailEntity;
%>
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
            <!--GetDefaultAddCommand()-->
            <%= GetDefaultAddCommandValidation()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Đăng ký nhận Email :
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
                            Full name :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="FullName" id="FullName" value="<%=item.FullName %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Giới tính :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Sex" id="Sex" value="<%=item.Sex %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Email :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Email" id="Email" value="<%=item.Email %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Allow :</label>
                    </td>
                    <td>
                        <input name="Allow" <%= item.Allow ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="Allow" <%= !item.Allow ? "checked" : "" %> type="radio" value='0' />
                        Không
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Create date :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>"
                            maxlength="255" />
                    </td>
                </tr>
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
