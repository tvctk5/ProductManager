<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_PropertiesGroupsModel;
    var item = ViewBag.Data as ModProduct_PropertiesGroupsEntity;

    var ListPropertiesOut = ViewBag.GetListPropertiesOut as List<ModProduct_PropertiesListEntity>;
    var ListPropertiesIn = ViewBag.GetListPropertiesIn as List<ModProduct_PropertiesListEntity>;
%>
<script language="javascript" type="text/javascript">
    var pageUrl = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_PropertiesGroups/PostData.aspx?Type=1")%>';
    var formID = "vswForm";

    function CheckValidationForm() {

        var bolCode = false;
        var bolName = false;
        var Mess = "";
        var txtCode = document.getElementById("Code");
        var txtName = document.getElementById("Name");

        if (txtCode != null) {
            if (txtCode.value.trim() != "") {
                if (txtCode.value.trim().length < 3) {
                    Mess = " - Yêu cầu nhập Mã nhóm thuộc tính từ 3 ký tự trở lên";
                    bolCode = false;
                }
                else {
                    bolCode = true;
                }
            }
            else {
                Mess = " - Yêu cầu nhập Mã nhóm thuộc tính";
            }
        }

        if (txtName != null) {
            if (txtName.value.trim() != "") {
                bolName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên nhóm thuộc tính";
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

    $(document).ready(function () {
        $("#Code").blur(function () {

            if (this.value.trim() == "")
                return;

            CheckDuplicate(pageUrl, formID, "Code");

        });
    });
</script>
<script type="text/javascript" language="javascript">

    // Sự kiện trên ListBox

    function RToL() {
        var lstPropertiesOut = document.getElementById("lstPropertiesOut");
        var lstPropertiesIn = document.getElementById("lstPropertiesIn");
        var GroupOutLength = lstPropertiesOut.options.length;
        var GroupInLength = lstPropertiesIn.options.length;
        if (GroupOutLength <= 0)
            return;

        for (var i = 0; i < GroupOutLength; i++) {
            if (lstPropertiesOut.options[i].selected) {
                var objOption = new Option(lstPropertiesOut.options[i].text, lstPropertiesOut.options[i].value)

                var AttributeColor = lstPropertiesOut.options[i].style.color;
                if (AttributeColor != null) {
                    objOption.style.color = AttributeColor;
                }

                lstPropertiesIn.add(objOption);
                lstPropertiesOut.remove(i);

                i--;
                GroupOutLength = GroupOutLength - 1;
            }
        }

        RefreshCustomersGroupsId();
    }

    function LToR() {
        var lstPropertiesOut = document.getElementById("lstPropertiesOut");
        var lstPropertiesIn = document.getElementById("lstPropertiesIn");
        var GroupOutLength = lstPropertiesOut.options.length;
        var GroupInLength = lstPropertiesIn.options.length;
        if (GroupInLength <= 0)
            return;

        for (var i = 0; i < GroupInLength; i++) {
            if (lstPropertiesIn.options[i].selected) {
                var objOption = new Option(lstPropertiesIn.options[i].text, lstPropertiesIn.options[i].value)

                var AttributeColor = lstPropertiesIn.options[i].style.color;
                if (AttributeColor != null) {
                    objOption.style.color = AttributeColor;
                }

                lstPropertiesOut.add(objOption);
                lstPropertiesIn.remove(i);

                i--;
                GroupInLength = GroupInLength - 1;
            }
        }

        RefreshCustomersGroupsId();
    }

    function AllRToL() {
        var lstPropertiesOut = document.getElementById("lstPropertiesOut");
        var lstPropertiesIn = document.getElementById("lstPropertiesIn");
        var GroupOutLength = lstPropertiesOut.options.length;
        var GroupInLength = lstPropertiesIn.options.length;
        if (GroupOutLength <= 0)
            return;

        for (var i = 0; i < GroupOutLength; i++) {
            var objOption = new Option(lstPropertiesOut.options[i].text, lstPropertiesOut.options[i].value)

            var AttributeColor = lstPropertiesOut.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstPropertiesIn.add(objOption);
            lstPropertiesOut.remove(i);

            i--;
            GroupOutLength = GroupOutLength - 1;
        }

        RefreshCustomersGroupsId();
    }

    function AllLToR() {
        var lstPropertiesOut = document.getElementById("lstPropertiesOut");
        var lstPropertiesIn = document.getElementById("lstPropertiesIn");
        var GroupOutLength = lstPropertiesOut.options.length;
        var GroupInLength = lstPropertiesIn.options.length;
        if (GroupInLength <= 0)
            return;

        for (var i = 0; i < GroupInLength; i++) {

            var objOption = new Option(lstPropertiesIn.options[i].text, lstPropertiesIn.options[i].value)

            var AttributeColor = lstPropertiesIn.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstPropertiesOut.add(objOption);
            lstPropertiesIn.remove(i);

            i--;
            GroupInLength = GroupInLength - 1;
        }

        RefreshCustomersGroupsId();
    }

    // Cập nhật lại Id của các nhóm
    function RefreshCustomersGroupsId() {
        var lstPropertiesOut = document.getElementById("lstPropertiesOut");
        var lstPropertiesIn = document.getElementById("lstPropertiesIn");

        var sPropertiesOutId = "";
        var sPropertiesInId = "";

        var GroupInLength = lstPropertiesIn.options.length;
        var GroupOutLength = lstPropertiesOut.options.length;

        // Id của các nhóm không chứa Khách hàng
        if (GroupOutLength > 0) {
            for (var i = 0; i < GroupOutLength; i++) {
                if (sPropertiesOutId.value = "")
                { sPropertiesOutId = lstPropertiesOut.options[i].value; }
                else {
                    sPropertiesOutId = sPropertiesOutId + "," + lstPropertiesOut.options[i].value;
                }

            }
        }
        // Id của các nhóm chứa Khách hàng
        if (GroupInLength > 0) {
            for (var i = 0; i < GroupInLength; i++) {
                if (sPropertiesInId.value = "")
                { sPropertiesInId = lstPropertiesIn.options[i].value; }
                else {
                    sPropertiesInId = sPropertiesInId + "," + lstPropertiesIn.options[i].value;
                }

            }
        }

        document.getElementById('PropertiesInId').value = sPropertiesInId;
        document.getElementById('PropertiesOutId').value = sPropertiesOutId;
    }
</script>
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
                Nhóm thuộc tính :
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
                            Mã nhóm :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" require="true" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên nhóm :</label>
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
                <tr>
                    <td class="key">
                        <label>
                            Vị trí sắp xếp :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Order" id="Order" value="<%=item.Order %>"
                            maxlength="10" />
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
            <table class="admintable">
                <tr>
                    <th align="center" colspan="3">
                    </th>
                </tr>
                <tr>
                    <td style="width: 45%; color: White; font-weight: bold; background-color: #336699;
                        height: 32px;" align="center">
                        DANH SÁCH THUỘC TÍNH CỦA NHÓM
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 45%; color: White; font-weight: bold; background-color: #336699;
                        height: 32px;" align="center">
                        DANH SÁCH THUỘC TÍNH KHÁC
                    </td>
                </tr>
                <tr>
                    <td style="width: 45%; height: 200px">
                        <select id="lstPropertiesIn" style="width: 100%; height: 100%; font-size: medium;
                            color: #0000FF; font-family: 'Times New Roman', Times, serif;" multiple="multiple">
                            <% if (ListPropertiesIn != null && ListPropertiesIn.Count > 0)
                               {
                                   string sNotUse = "FALSE";
                                   foreach (ModProduct_PropertiesListEntity itemPropertiesListEntity in ListPropertiesIn)
                                   {
                                       sNotUse = "FALSE";
                                       if (!itemPropertiesListEntity.Activity)
                                           sNotUse = "TRUE";
                            %>
                            <option <%= sNotUse=="TRUE"? "style='color: Red !important;'":"" %> value="<%=itemPropertiesListEntity.ID %>">
                                <%=itemPropertiesListEntity.Name%>
                                <%="( " + itemPropertiesListEntity.Code+ " ) "%>
                                <% if (!itemPropertiesListEntity.Activity)
                                   { %>
                                <%= sNotUse=="TRUE"?"- [Đang ngừng sử dụng]":"" %>
                                <%} %>
                            </option>
                            <%}
                               } %>
                        </select>
                    </td>
                    <td style="width: 10%" align="center">
                        <input type="button" id="btnLR" onclick="LToR();" runat="server" style="background-position: center center;
                            width: 100%; height: 30px; background-image: url('/{CPPath}/Content/add/img/Product/control_double_right.png');
                            background-repeat: no-repeat;" tooltip="Bỏ thuộc tính đã chọn ra khỏi nhóm" />
                        <br />
                        <input type="button" id="btnLRAll" runat="server" style="width: 100%; height: 30px;
                            background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_stop_right.png');
                            background-repeat: no-repeat;" onclick="AllLToR();" tooltip="Bỏ tất cả thuộc tính ra khỏi nhóm" />
                        <br />
                        <input type="button" id="btnRLAll" runat="server" style="width: 100%; height: 30px;
                            background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_stop_left.png');
                            background-repeat: no-repeat;" onclick="AllRToL();" tooltip="Gán tất cả các thuộc tính cho nhóm" />
                        <br />
                        <input type="button" id="btnRL" runat="server" style="width: 100%; height: 30px;
                            background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_left.png');
                            background-repeat: no-repeat;" onclick="RToL();" tooltip="Gán thuộc tính đã chọn cho nhóm" />
                    </td>
                    <td style="width: 45%; height: 200px">
                        <select id="lstPropertiesOut" style="width: 100%; height: 100%; font-size: medium;
                            color: #0000FF; font-family: 'Times New Roman', Times, serif;" multiple="multiple">
                            <% if (ListPropertiesOut != null && ListPropertiesOut.Count > 0)
                               {
                                   string sNotUse = "FALSE";
                                   foreach (ModProduct_PropertiesListEntity itemPropertiesListEntity in ListPropertiesOut)
                                   {
                                       sNotUse = "FALSE";
                                       if (!itemPropertiesListEntity.Activity)
                                           sNotUse = "TRUE";
                            %>
                            <option <%= sNotUse=="TRUE"? "style='color: Red !important;'":"" %> value="<%=itemPropertiesListEntity.ID %>">
                                <%=itemPropertiesListEntity.Name%>
                                <%="( " + itemPropertiesListEntity.Code+ " ) "%>
                                <% if (!itemPropertiesListEntity.Activity)
                                   { %>
                                <%= sNotUse=="TRUE"?"- [Đang ngừng sử dụng]":"" %>
                                <%} %>
                            </option>
                            <%}
                               } %>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 45%">
                        <input id="PropertiesInId" name="PropertiesInId" value="<%=model.PropertiesInId%>"
                            type="hidden" />
                    </td>
                    <td style="width: 10%" align="center">
                        &nbsp;
                    </td>
                    <td style="width: 45%">
                        <input id="PropertiesOutId" name="PropertiesOutId" value="<%=model.PropertiesOutId%>"
                            type="hidden" />
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
