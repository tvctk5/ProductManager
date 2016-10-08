<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {
     
        var bolName = false;
        var Mess = "";
        var txtName = document.getElementById("Name");

        if (txtName != null) {
            if (txtName.value.trim() != "") {
                bolName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên thuộc tính";
            }
        }

        if (bolName) {
            return true;
        }
        else {
            alert(Mess);

            if (bolName == false) {
                txtName.focus(); return false;
            }
        }
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_PropertiesListModel;
    var item = ViewBag.Data as ModProduct_PropertiesListEntity;
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
            <%= GetDefaultAddCommandValidation_Not_SaveAndCreate()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Thuộc tính sản phẩm :
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
<div id="divMessError" style="display: none;">
</div>
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
                            Nhóm thuộc tính :</label>
                    </td>
                    <td>
                        <select name="ModelPropertiesGroupsId" id="ModelPropertiesGroupsId" class="DropDownList">
                            <option value="0">--------------- Chọn nhóm thuộc tính ---------------</option>
                            <%= Utils.ShowDDLPropertiesGroups(item.PropertiesGroupsId == null? 0 : (int)item.PropertiesGroupsId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã thuộc tính :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255"/>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên thuộc tính :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" require="true" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ghi chú :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Note" id="Note" value="<%=item.Note %>"
                            maxlength="255" require="true" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Kiểu thuộc tính :</label>
                    </td>
                    <td>
                        <select name="Type" id="Type" class="DropDownList">
                            <option value="0" <%=item.Type==0?"selected":"" %>>Một dòng</option>
                            <option value="1" <%=item.Type==1?"selected":"" %>>Nhiều dòng</option>
                        </select>
                        <%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Dữ liệu cũ :</label>
                    </td>
                    <td>
                        <input name="ViewOldData" <%= item.ViewOldData ? "checked" : "" %> type="radio" value='1' />
                        Hiển thị
                        <input name="ViewOldData" <%= !item.ViewOldData ? "checked" : "" %> type="radio"
                            value='0' />
                        Không hiển thị
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Đơn vị tính :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Unit" id="Unit" value="<%=item.Unit %>"
                            maxlength="255"/>
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
                <tr>
                    <td class="key">
                        <label>
                            Sắp xếp :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Order" id="Order" value="<%=item.Order %>"
                            maxlength="255"/>
                    </td>
                </tr>
                <%
                    if (model.RecordID > 0)
                    {
                %>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" readonly="readonly" type="text" name="CreateDate" id="CreateDate"
                            value="<%=item.CreateDate %>" maxlength="255" />
                    </td>
                </tr>
                <%
                    }
                    else
                    {  %>
                <tr style="display: none;">
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" name="CreateDate" id="CreateDate"
                            value="<%=item.CreateDate %>" maxlength="255" />
                    </td>
                </tr>
                <%}%>
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
            <%= GetDefaultAddCommandValidation_Not_SaveAndCreate()%>
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
