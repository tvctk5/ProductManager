<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_CustomersModel;
    var item = ViewBag.Data as ModProduct_CustomersEntity;
    var ListCustomerOut = ViewBag.GetListCustomersGroupsOut as List<ModProduct_CustomersGroupsEntity>;
    var ListCustomerIn = ViewBag.GetListCustomersGroupsIn as List<ModProduct_CustomersGroupsEntity>;
%>
<script language="javascript" type="text/javascript">
    function RToL() {
        var lstCustommerGroupOut = document.getElementById("lstCustommerGroupOut");
        var lstCustommerGroupIn = document.getElementById("lstCustommerGroupIn");
        var GroupOutLength = lstCustommerGroupOut.options.length;
        var GroupInLength = lstCustommerGroupIn.options.length;
        if (GroupOutLength <= 0)
            return;

        for (var i = 0; i < GroupOutLength; i++) {
            if (lstCustommerGroupOut.options[i].selected) {
                lstCustommerGroupIn.add(new Option(lstCustommerGroupOut.options[i].text, lstCustommerGroupOut.options[i].value));
                lstCustommerGroupOut.remove(i);

                i--;
                GroupOutLength = GroupOutLength - 1;
            }
        }

        RefreshCustomersGroupsId();
    }

    function LToR() {
        var lstCustommerGroupOut = document.getElementById("lstCustommerGroupOut");
        var lstCustommerGroupIn = document.getElementById("lstCustommerGroupIn");
        var GroupOutLength = lstCustommerGroupOut.options.length;
        var GroupInLength = lstCustommerGroupIn.options.length;
        if (GroupInLength <= 0)
            return;

        for (var i = 0; i < GroupInLength; i++) {
            if (lstCustommerGroupIn.options[i].selected) {
                lstCustommerGroupOut.add(new Option(lstCustommerGroupIn.options[i].text, lstCustommerGroupIn.options[i].value));
                lstCustommerGroupIn.remove(i);

                i--;
                GroupInLength = GroupInLength - 1;
            }
        }

        RefreshCustomersGroupsId();
    }

    function AllRToL() {
        var lstCustommerGroupOut = document.getElementById("lstCustommerGroupOut");
        var lstCustommerGroupIn = document.getElementById("lstCustommerGroupIn");
        var GroupOutLength = lstCustommerGroupOut.options.length;
        var GroupInLength = lstCustommerGroupIn.options.length;
        if (GroupOutLength <= 0)
            return;

        for (var i = 0; i < GroupOutLength; i++) {
            lstCustommerGroupIn.add(new Option(lstCustommerGroupOut.options[i].text, lstCustommerGroupOut.options[i].value));
            lstCustommerGroupOut.remove(i);

            i--;
            GroupOutLength = GroupOutLength - 1;
        }

        RefreshCustomersGroupsId();
    }

    function AllLToR() {
        var lstCustommerGroupOut = document.getElementById("lstCustommerGroupOut");
        var lstCustommerGroupIn = document.getElementById("lstCustommerGroupIn");
        var GroupOutLength = lstCustommerGroupOut.options.length;
        var GroupInLength = lstCustommerGroupIn.options.length;
        if (GroupInLength <= 0)
            return;

        for (var i = 0; i < GroupInLength; i++) {
            lstCustommerGroupOut.add(new Option(lstCustommerGroupIn.options[i].text, lstCustommerGroupIn.options[i].value));
            lstCustommerGroupIn.remove(i);

            i--;
            GroupInLength = GroupInLength - 1;
        }

        RefreshCustomersGroupsId();
    }

    // Cập nhật lại Id của các nhóm
    function RefreshCustomersGroupsId() {
        var lstCustommerGroupOut = document.getElementById("lstCustommerGroupOut");
        var lstCustommerGroupIn = document.getElementById("lstCustommerGroupIn");

        var sCustomGroupOut = "";
        var sCustomGroupIn = "";

        var GroupInLength = lstCustommerGroupIn.options.length;
        var GroupOutLength = lstCustommerGroupOut.options.length;

        // Id của các nhóm không chứa Khách hàng
        if (GroupOutLength > 0) {
            for (var i = 0; i < GroupOutLength; i++) {
                if (sCustomGroupOut.value = "")
                { sCustomGroupOut = lstCustommerGroupOut.options[i].value; }
                else {
                    sCustomGroupOut = sCustomGroupOut + "," + lstCustommerGroupOut.options[i].value;
                }

            }
        }
        // Id của các nhóm chứa Khách hàng
        if (GroupInLength > 0) {
            for (var i = 0; i < GroupInLength; i++) {
                if (sCustomGroupIn.value = "")
                { sCustomGroupIn = lstCustommerGroupIn.options[i].value; }
                else {
                    sCustomGroupIn = sCustomGroupIn + "," + lstCustommerGroupIn.options[i].value;
                }

            }
        }

        document.getElementById('CustomGroupInId').value = sCustomGroupIn;
        document.getElementById('CustomGroupOutId').value = sCustomGroupOut;
    }

    function CheckValidationForm() {

        var bolUserName = false;
        var bolPass = false;
        var bolFullName = false;
        var bolEmail = false;

        var Mess = "";
        var txtUserName = document.getElementById("UserName");
        var txtPass = document.getElementById("Pass");
        var txtID = document.getElementById("ID");
        var txtFullName = document.getElementById("FullName");
        var txtNewPassword = document.getElementById("NewPassword");
        var txtEmail = document.getElementById("Email");
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;


        if (txtUserName != null) {
            if (txtUserName.value.trim() != "") {
                if (txtUserName.value.trim().length < 5) {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Yêu cầu nhập Tên đăng nhập từ 3 ký tự trở lên";
                }
                else {
                    bolUserName = true;
                }
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Mã Nhóm khách hàng";
            }
        }

        if (txtID == null) {
            if (txtNewPassword != null) {
                if (txtNewPassword.value.trim() != "") {
                    if (txtNewPassword.value.trim().length < 6) {
                        if (Mess != "")
                        { Mess = Mess + "\r\n"; }
                        Mess = Mess + " - Yêu cầu nhập Mật khẩu từ 6 ký tự trở lên";
                    }
                    else {
                        bolPass = true;
                    }
                }
                else {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Yêu cầu nhập Mật khẩu";
                }
            }
        }
        else {
            if (txtNewPassword != null) {
                if (txtNewPassword.value.trim() != "") {
                    if (txtNewPassword.value.trim().toUpperCase() != "PASSWORD") {
                        if (txtNewPassword.value.trim().length < 6) {
                            if (Mess != "")
                            { Mess = Mess + "\r\n"; }
                            Mess = Mess + " - Yêu cầu nhập Mật khẩu từ 6 ký tự trở lên";
                        }
                        else {
                            bolPass = true;
                        }
                    }
                    else {
                        bolPass = true;
                    }
                }
                else {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Yêu cầu nhập Mật khẩu";
                    bolPass = false;
                }
            }
        }

        if (txtFullName != null) {
            if (txtFullName.value.trim() != "") {
                if (txtFullName.value.trim().length < 3) {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Yêu cầu nhập Họ và tên từ 3 ký tự trở lên";
                }
                else {
                    bolFullName = true;
                }
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Họ và tên khách hàng";
            }
        }

        if (txtEmail != null) {
            if (txtEmail.value != "") {
                if (!filter.test(txtEmail.value)) {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Địa chỉ Email không hợp lệ"
                    bolEmail = false;
                }
                else {
                    bolEmail = true;
                }
            }
            else {
                bolEmail = true;
            }
        }
        else {
            bolEmail = true;
        }

        // Kiểm tra cuối cùng
        if (bolUserName && bolPass && bolFullName && bolEmail) {
            return true;
        }
        else {
            alert(Mess);
            if (bolUserName == false) {
                txtUserName.focus(); return false;
            }

            if (bolPass == false) {
                txtNewPassword.focus(); return false;
            }

            if (bolFullName == false) {
                txtFullName.focus(); return false;
            }

            if (bolEmail == false) {
                txtEmail.focus(); return false;
            }

        }
        return true;
    }

    $(document).ready(function () {
        $("#Code").blur(function () {

            if (this.value.trim() == "")
                return;

            CheckDuplicate(pageUrl, formID, "Code");

        });
    });
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
            <%= GetDefaultAddCommandValidation()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Khách hàng :
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
                            Tên đăng nhập :</label>
                    </td>
                    <td>
                        <%
                            if (model.RecordID > 0)
                            {
                        %>
                        <input class="text_input" type="text" name="UserName" id="UserName" value="<%=item.UserName %>"
                            maxlength="50" readonly="readonly" require="true" /><%=ShowRequireValidation()%>
                        <input id="ID" name="Pass" type="hidden" value="<%=item.ID %>" />
                        <%
                            }
                            else
                            {  %>
                        <input class="text_input" type="text" name="UserName" id="UserName" value="<%=item.UserName %>"
                            maxlength="50" require="true" /><%=ShowRequireValidation()%>
                        <%}%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mật khẩu :</label>
                    </td>
                    <td>
                        <input class="text_input" type="password" name="NewPassword" id="NewPassword" value="<%=model.NewPassword %>"
                            maxlength="50" require="true" /><%=ShowRequireValidation()%>
                        <input id="Pass" name="Pass" type="hidden" value="<%=item.Pass %>" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Họ và Tên :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="FullName" id="FullName" value="<%=item.FullName %>"
                            maxlength="255" require="true" /><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ngày sinh :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Birthday" id="Birthday" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Giới tính :</label>
                    </td>
                    <td>
                        <input name="Sex" <%= item.Sex ? "checked" : "" %> type="radio" value='1' />
                        Nữ
                        <input name="Sex" <%= !item.Sex ? "checked" : "" %> type="radio" value='0' />
                        Nam
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Địa chỉ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Address" id="Address" value="<%=item.Address %>"
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
                            Số điện thoại :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="PhoneNumber" id="PhoneNumber" value="<%=item.PhoneNumber %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ảnh đại diện :</label>
                    </td>
                    <td>
                        <%= Utils.GetMedia(item.File, 100, 80)%>
                        <br />
                        <input class="text_input" type="text" name="File" id="File" style="width: 80%;" value="<%=item.File %>"
                            maxlength="255" />
                        <input class="text_input" style="width: 17%;" type="button" onclick="ShowFileForm('File');return false;"
                            value="Chọn tệp tin" />
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
                            Số điểm :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="PointTotal" id="PointTotal" value="<%=item.PointTotal %>"
                            maxlength="255" />
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
                        DANH SÁCH NHÓM CỦA KHÁCH HÀNG
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 45%; color: White; font-weight: bold; background-color: #336699;
                        height: 32px;" align="center">
                        DANH SÁCH CÁC NHÓM KHÁC
                    </td>
                </tr>
                <tr>
                    <td style="width: 45%; height: 200px">
                        <select id="lstCustommerGroupIn" style="width: 100%; height: 100%; font-size: medium;
                            color: #0000FF; font-family: 'Times New Roman', Times, serif;" multiple="multiple">
                            <% if (ListCustomerIn != null && ListCustomerIn.Count > 0)
                               {
                                   foreach (ModProduct_CustomersGroupsEntity itemCustomersGroups in ListCustomerIn)
                                   {%>
                            <option value="<%=itemCustomersGroups.ID %>">
                                <%=itemCustomersGroups.Name%>
                            </option>
                            <%}
                               } %>
                        </select>
                    </td>
                    <td style="width: 10%" align="center">
                        <input type="button" id="btnLR" onclick="LToR();" runat="server" style="background-position: center center;
                            width: 100%; height: 30px; background-image: url('/{CPPath}/Content/add/img/Product/control_double_right.png');
                            background-repeat: no-repeat;" tooltip="sagfasgsdg" />
                        <!-- onmouseover="showToolTip(event,'Bấm vào đây để xóa nhóm cho Khách hàng')"
							onmouseout="hideToolTip()"  -->
                        <br />
                        <input type="button" id="btnLRAll" runat="server" style="width: 100%; height: 30px;
                            background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_stop_right.png');
                            background-repeat: no-repeat;" onclick="AllLToR();" />
                        <!--onmouseover="showToolTip(event,'Bấm vào đây để xóa toàn bộ nhóm cho Khách hàng')"
							onmouseout="hideToolTip()" -->
                        <br />
                        <input type="button" id="btnRLAll" runat="server" style="width: 100%; height: 30px;
                            background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_stop_left.png');
                            background-repeat: no-repeat;" onclick="AllRToL();" />
                        <!--  onmouseover="showToolTip(event,'Bấm vào đây để thêm toàn bộ nhóm cho Khách hàng')"
							onmouseout="hideToolTip()" -->
                        <br />
                        <input type="button" id="btnRL" runat="server" style="width: 100%; height: 30px;
                            background-position: center center; background-image: url('/{CPPath}/Content/add/img/Product/control_double_left.png');
                            background-repeat: no-repeat;" onclick="RToL();" />
                        <!-- onmouseover="showToolTip(event,'Bấm vào đây để thêm nhóm cho Khách hàng')"
							onmouseout="hideToolTip()" -->
                    </td>
                    <td style="width: 45%; height: 200px">
                        <select id="lstCustommerGroupOut" style="width: 100%; height: 100%; font-size: medium;
                            color: #0000FF; font-family: 'Times New Roman', Times, serif;" multiple="multiple">
                            <% if (ListCustomerOut != null && ListCustomerOut.Count > 0)
                               {
                                   foreach (ModProduct_CustomersGroupsEntity itemCustomersGroups in ListCustomerOut)
                                   {%>
                            <option value="<%=itemCustomersGroups.ID %>">
                                <%=itemCustomersGroups.Name%>
                            </option>
                            <%}
                               } %>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 45%">
                        <input id="CustomGroupInId" name="CustomGroupInId" value="<%=item.CustomGroupInId %>"
                            type="hidden" />
                    </td>
                    <td style="width: 10%" align="center">
                        &nbsp;
                    </td>
                    <td style="width: 45%">
                        <input id="CustomGroupOutId" name="CustomGroupOutId" value="<%=item.CustomGroupOutId %>"
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
<!-- Script by hscripts.com -->
<table id="bubble_tooltip" border="0" cellpadding="0" cellspacing="0">
    <tr class="bubble_top">
        <td>
        </td>
    </tr>
    <tr class="bubble_middle">
        <td>
            <div id="bubble_tooltip_content">
            </div>
        </td>
    </tr>
    <tr class="bubble_bottom">
        <td colspan="5">
        </td>
    </tr>
</table>
<script type="text/javascript">

    //This is the text to be displayed on the tooltip.

    if (document.images) {
        pic1 = new Image();
        pic1.src = '/{CPPath}/Content/add/img/htooltip/bubble_top.gif';
        pic2 = new Image();
        pic2.src = '/{CPPath}/Content/add/img/htooltip/bubble_middle.gif';
        pic3 = new Image();
        pic3.src = '/{CPPath}/Content/add/img/htooltip/bubble_bottom.gif';
    }

    function showToolTip(e, text) {

        if (document.all) e = event;
        var obj = document.getElementById('bubble_tooltip');
        var obj2 = document.getElementById('bubble_tooltip_content');
        obj2.innerHTML = text;
        obj.style.display = 'block';
        var st = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
        if (navigator.userAgent.toLowerCase().indexOf('safari') >= 0) st = 0;
        var leftPos = e.clientX - 2;
        if (leftPos < 0) leftPos = 0;
        obj.style.left = leftPos + 'px';
        obj.style.top = e.clientY - obj.offsetHeight + 2 + st + 'px';
    }
    function hideToolTip() {
        document.getElementById('bubble_tooltip').style.display = 'none';
    }
</script>
<!-- Script by hscripts.com -->
<style type="text/css">
    #bubble_tooltip
    {
        width: 210px;
        position: absolute;
        display: none;
    }
    #bubble_tooltip .bubble_top
    {
        position: relative;
        background-image: url(/{CPPath}/Content/add/img/htooltip/bubble_top.gif);
        background-repeat: no-repeat;
        height: 18px;
    }
    #bubble_tooltip .bubble_middle
    {
        position: relative;
        background-image: url(/{CPPath}/Content/add/img/htooltip/bubble_middle.gif);
        background-repeat: repeat-y;
        background-position: bottom left;
    }
    #bubble_tooltip .bubble_middle div
    {
        padding-left: 12px;
        padding-right: 20px;
        position: relative;
        font-size: 11px;
        font-family: arial, verdana, san-serif;
        text-decoration: none;
        color: Black;
        text-align: justify;
    }
    #bubble_tooltip .bubble_bottom
    {
        background-image: url(/{CPPath}/Content/add/img/htooltip/bubble_bottom.gif);
        background-repeat: no-repeat;
        height: 65px;
        position: relative;
        top: 0px;
    }
</style>
