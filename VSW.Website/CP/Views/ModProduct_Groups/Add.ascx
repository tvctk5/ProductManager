<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    var pageUrl = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Groups/PostData.aspx?Type=1")%>';
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
                    Mess = " - Yêu cầu nhập Mã nhóm sản phẩm từ 3 ký tự trở lên";
                    bolCode = false;
                }
                else {
                    bolCode = true;
                }
            }
            else {
                Mess = " - Yêu cầu nhập Mã nhóm sản phẩm";
            }
        }

        if (txtName != null) {
            if (txtName.value.trim() != "") {
                bolName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên nhóm sản phẩm";
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
<%
    var model = ViewBag.Model as ModProduct_GroupsModel;
    var item = ViewBag.Data as ModProduct_GroupsEntity;
    var ListPropertiesGroupsIn = ViewBag.GetListPropertiesGroupsIn as List<ModProduct_PropertiesGroupsEntity>;
    var ListPropertiesGroupsOut = ViewBag.GetListPropertiesGroupsOut as List<ModProduct_PropertiesGroupsEntity>;
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
                Nhóm sản phẩm :
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
                            Mã nhóm cha :</label>
                    </td>
                    <td>
                        <select name="ParentId" id="ParentId" class="text_input">
<%--                            <option value="0">------- Chọn nhóm cha -------</option>--%>
                            <%= Utils.ShowDDLProductGroups(item.ParentId.ToString(),model.RecordID.ToString())%>
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
                            maxlength="255" require="true"/><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên nhóm :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" require="true"/><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ghi chú :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Note" id="Note" value="<%=item.Note %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ảnh :</label>
                    </td>
                    <td>
                        <%= Utils.GetMedia(item.File, 100, 80)%>
                        <br />
                        <input class="text_input" type="text" name="File" id="File" style="width: 80%;" value="<%=item.File %>"
                            maxlength="255" />
                        <input class="text_input" style="width: 17%;" type="button" onclick="ShowFileForm('File');return false;"
                            value="Chọn File" />
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
                    <td colspan="2">
                        <table class="admintable">
                            <tr>
                                <th align="center" colspan="3">
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 45%; color: White; font-weight: bold; background-color: #336699;
                                    height: 32px;" align="center">
                                    DANH SÁCH NHÓM THUỘC TÍNH CỦA NHÓM
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 45%; color: White; font-weight: bold; background-color: #336699;
                                    height: 32px;" align="center">
                                    DANH SÁCH NHÓM THUỘC TÍNH KHÁC
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 45%; height: 100px">
                                    <select id="lstPropertiesGroupsIn" style="width: 100%; height: 100%; font-size: medium;
                                        color: #0000FF; font-family: 'Times New Roman', Times, serif;" multiple="multiple">
                                        <% if (ListPropertiesGroupsIn != null && ListPropertiesGroupsIn.Count > 0)
                                           {
                                               string sNotUse = "FALSE";
                                               foreach (ModProduct_PropertiesGroupsEntity itemPropertiesGroupsEntity in ListPropertiesGroupsIn)
                                               {
                                                   sNotUse = "FALSE";
                                                   if (!itemPropertiesGroupsEntity.Activity)
                                                       sNotUse = "TRUE";
                                        %>
                                        <option <%= sNotUse=="TRUE"? "style='color: Red !important;'":"" %> value="<%=itemPropertiesGroupsEntity.ID %>">
                                            <%=itemPropertiesGroupsEntity.Name%>
                                            <%="( " + itemPropertiesGroupsEntity.Code + " ) "%>
                                            <% if (!itemPropertiesGroupsEntity.Activity)
                                               { %>
                                            <%= sNotUse=="TRUE"?"- [Đang ngừng sử dụng]":"" %>
                                            <%} %>
                                        </option>
                                        <%}
                                           } %>
                                    </select>
                                </td>
                                <td style="width: 10%" align="center">
                                    <input type="button" id="btnLR_PropertiesGroups" onclick="LToR_PropertiesGroups();"
                                        runat="server" style="background-position: center center; width: 100%; height: 30px;
                                        background-image: url('/{CPPath}/Content/add/img/Product/control_double_right.png');
                                        background-repeat: no-repeat;" />
                                    <br />
                                    <input type="button" id="btnLRAll_PropertiesGroups" runat="server" style="width: 100%;
                                        height: 30px; background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_stop_right.png');
                                        background-repeat: no-repeat;" onclick="AllLToR_PropertiesGroups();" />
                                    <br />
                                    <input type="button" id="btnRLAll_PropertiesGroups" runat="server" style="width: 100%;
                                        height: 30px; background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_stop_left.png');
                                        background-repeat: no-repeat;" onclick="AllRToL_PropertiesGroups();" />
                                    <br />
                                    <input type="button" id="btnRL_PropertiesGroups" runat="server" style="width: 100%;
                                        height: 30px; background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_left.png');
                                        background-repeat: no-repeat;" onclick="RToL_PropertiesGroups();" />
                                </td>
                                <td style="width: 45%; height: 100px">
                                    <select id="lstPropertiesGroupsOut" style="width: 100%; height: 100%; font-size: medium;
                                        color: #0000FF; font-family: 'Times New Roman', Times, serif;" multiple="multiple">
                                        <% if (ListPropertiesGroupsOut != null && ListPropertiesGroupsOut.Count > 0)
                                           {
                                               string sNotUse = "FALSE";
                                               foreach (ModProduct_PropertiesGroupsEntity itemPropertiesGroupsEntity in ListPropertiesGroupsOut)
                                               {
                                                   sNotUse = "FALSE";
                                                   if (!itemPropertiesGroupsEntity.Activity)
                                                       sNotUse = "TRUE";
                                        %>
                                        <option <%= sNotUse=="TRUE"? "style='color: Red !important;'":"" %> value="<%=itemPropertiesGroupsEntity.ID %>">
                                            <%=itemPropertiesGroupsEntity.Name%>
                                            <%="( " + itemPropertiesGroupsEntity.Code + " ) "%>
                                            <% if (!itemPropertiesGroupsEntity.Activity)
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
                                    <input id="PropertiesGroupsInId" name="PropertiesGroupsInId" value="<%=model.PropertiesGroupsInId%>"
                                        type="hidden" />
                                </td>
                                <td style="width: 10%" align="center">
                                    &nbsp;
                                </td>
                                <td style="width: 45%">
                                    <input id="PropertiesGroupsOutId" name="PropertiesGroupsOutId" value="<%=model.PropertiesGroupsOutId%>"
                                        type="hidden" />
                                </td>
                            </tr>
                        </table>
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
<script type="text/javascript" language="javascript">

    // Sự kiện trên ListBox

    function RToL_PropertiesGroups() {
        var lstPropertiesGroupsOut = document.getElementById("lstPropertiesGroupsOut");
        var lstPropertiesGroupsIn = document.getElementById("lstPropertiesGroupsIn");
        var GroupOutLength = lstPropertiesGroupsOut.options.length;
        var GroupInLength = lstPropertiesGroupsIn.options.length;
        if (GroupOutLength <= 0)
            return;

        for (var i = 0; i < GroupOutLength; i++) {
            if (lstPropertiesGroupsOut.options[i].selected) {
                var objOption = new Option(lstPropertiesGroupsOut.options[i].text, lstPropertiesGroupsOut.options[i].value)

                var AttributeColor = lstPropertiesGroupsOut.options[i].style.color;
                if (AttributeColor != null) {
                    objOption.style.color = AttributeColor;
                }

                lstPropertiesGroupsIn.add(objOption);
                lstPropertiesGroupsOut.remove(i);

                i--;
                GroupOutLength = GroupOutLength - 1;
            }
        }

        RefreshPropertiesGroupsId();
    }

    function LToR_PropertiesGroups() {
        var lstPropertiesGroupsOut = document.getElementById("lstPropertiesGroupsOut");
        var lstPropertiesGroupsIn = document.getElementById("lstPropertiesGroupsIn");
        var GroupOutLength = lstPropertiesGroupsOut.options.length;
        var GroupInLength = lstPropertiesGroupsIn.options.length;
        if (GroupInLength <= 0)
            return;

        for (var i = 0; i < GroupInLength; i++) {
            if (lstPropertiesGroupsIn.options[i].selected) {
                var objOption = new Option(lstPropertiesGroupsIn.options[i].text, lstPropertiesGroupsIn.options[i].value)

                var AttributeColor = lstPropertiesGroupsIn.options[i].style.color;
                if (AttributeColor != null) {
                    objOption.style.color = AttributeColor;
                }

                lstPropertiesGroupsOut.add(objOption);
                lstPropertiesGroupsIn.remove(i);

                i--;
                GroupInLength = GroupInLength - 1;
            }
        }

        RefreshPropertiesGroupsId();
    }

    function AllRToL_PropertiesGroups() {
        var lstPropertiesGroupsOut = document.getElementById("lstPropertiesGroupsOut");
        var lstPropertiesGroupsIn = document.getElementById("lstPropertiesGroupsIn");
        var GroupOutLength = lstPropertiesGroupsOut.options.length;
        var GroupInLength = lstPropertiesGroupsIn.options.length;
        if (GroupOutLength <= 0)
            return;

        for (var i = 0; i < GroupOutLength; i++) {
            var objOption = new Option(lstPropertiesGroupsOut.options[i].text, lstPropertiesGroupsOut.options[i].value)

            var AttributeColor = lstPropertiesGroupsOut.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstPropertiesGroupsIn.add(objOption);
            lstPropertiesGroupsOut.remove(i);

            i--;
            GroupOutLength = GroupOutLength - 1;
        }

        RefreshPropertiesGroupsId();
    }

    function AllLToR_PropertiesGroups() {
        var lstPropertiesGroupsOut = document.getElementById("lstPropertiesGroupsOut");
        var lstPropertiesGroupsIn = document.getElementById("lstPropertiesGroupsIn");
        var GroupOutLength = lstPropertiesGroupsOut.options.length;
        var GroupInLength = lstPropertiesGroupsIn.options.length;
        if (GroupInLength <= 0)
            return;

        for (var i = 0; i < GroupInLength; i++) {

            var objOption = new Option(lstPropertiesGroupsIn.options[i].text, lstPropertiesGroupsIn.options[i].value)

            var AttributeColor = lstPropertiesGroupsIn.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstPropertiesGroupsOut.add(objOption);
            lstPropertiesGroupsIn.remove(i);

            i--;
            GroupInLength = GroupInLength - 1;
        }

        RefreshPropertiesGroupsId();
    }

    // Cập nhật lại Id của các nhóm
    function RefreshPropertiesGroupsId() {
        var lstPropertiesGroupsOut = document.getElementById("lstPropertiesGroupsOut");
        var lstPropertiesGroupsIn = document.getElementById("lstPropertiesGroupsIn");

        var sPropertiesGroupsOutId = "";
        var sPropertiesGroupsInId = "";

        var GroupInLength = lstPropertiesGroupsIn.options.length;
        var GroupOutLength = lstPropertiesGroupsOut.options.length;

        // Id của các sản phẩm không chứa sản phẩm
        if (GroupOutLength > 0) {
            for (var i = 0; i < GroupOutLength; i++) {
                if (sPropertiesGroupsOutId.value = "")
                { sPropertiesGroupsOutId = lstPropertiesGroupsOut.options[i].value; }
                else {
                    sPropertiesGroupsOutId = sPropertiesGroupsOutId + "," + lstPropertiesGroupsOut.options[i].value;
                }

            }
        }
        // Id của các sản phẩm chứa sản phẩm
        if (GroupInLength > 0) {
            for (var i = 0; i < GroupInLength; i++) {
                if (sPropertiesGroupsInId.value = "")
                { sPropertiesGroupsInId = lstPropertiesGroupsIn.options[i].value; }
                else {
                    sPropertiesGroupsInId = sPropertiesGroupsInId + "," + lstPropertiesGroupsIn.options[i].value;
                }

            }
        }

        document.getElementById('PropertiesGroupsInId').value = sPropertiesGroupsInId;
        document.getElementById('PropertiesGroupsOutId').value = sPropertiesGroupsOutId;
    }
</script>
