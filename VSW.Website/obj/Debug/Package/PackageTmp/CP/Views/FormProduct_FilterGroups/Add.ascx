<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {

        var bolCode = false;
        var bolName = false;
        var Mess = "";
        var txtCode = document.getElementById("Code");
        var txtName = document.getElementById("Name");

        if (txtCode != null) {
            if (txtCode.value.trim() != "") {
                if (txtCode.value.trim().length < 3) {
                    Mess = " - Yêu cầu nhập Mã nhóm thuộc tính lọc từ 3 ký tự trở lên";
                    bolCode = false;
                }
                else {
                    bolCode = true;
                }
            }
            else {
                Mess = " - Yêu cầu nhập Mã nhóm thuộc tính lọc";
            }
        }

        if (txtName != null) {
            if (txtName.value.trim() != "") {
                bolName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên nhóm thuộc tính lọc";
            }
        }

        if (bolName && bolCode) {
            return true;
        }
        else {
            alert(Mess);
            if (bolCode == false) {
                txtCode.focus(); return false;
            }

            if (bolName == false) {
                txtName.focus(); return false;
            }
        }
    }

    function ReturnParent(Key) {
        if (Key == null) {
            self.parent.tb_remove();
        }
        else {
            parent.ReturnParent(Key);
        }
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_FilterGroupsModel;
    var item = ViewBag.Data as ModProduct_FilterGroupsEntity;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<div class="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="pagetitle icon-16-Edit" style="padding-left: 20px !important;">
            <h2 style="line-height: normal !important;">
                Nhóm thuộc tính lọc :
                <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%>
            </h2>
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
<div class="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=  GetListCommandThickboxValidation("apply|Lưu,space,closepopup|Đóng")%>
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
<br />
<%= ShowMessage()%>
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
                            Chủng loại :</label>
                    </td>
                    <td>
                        <select name="ProductGroupsId" id="ProductGroupsId" class="text_input" style="width: 92% !important;">
                            <%= Utils.ShowDDLProductGroups(item.ProductGroupsId.ToString())%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã nhóm :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" style="width: 90% !important;" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên nhóm :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" style="width: 90% !important;" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <%if (CPViewPage.UserPermissions.Approve)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Trạng thái :</label>
                    </td>
                    <td>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Sử dụng
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Không sử dụng
                    </td>
                </tr>
                <%} %>
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
            <%=  GetListCommandThickboxValidation("apply|Lưu,space,closepopup|Đóng")%>
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
