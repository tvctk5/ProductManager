<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    var pageUrl = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Manufacturer/PostData.aspx?Type=1")%>';
    var formID = "vswForm";

    function CheckValidationForm() {

        var bolCode = false;
        var bolName = false;
        var bolEmail = false;
        var Mess = "";
        var txtCode = document.getElementById("Code");
        var txtName = document.getElementById("Name");
        var txtEmail = document.getElementById("Email");
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (txtCode != null) {
            if (txtCode.value.trim() != "") {
                if (txtCode.value.trim().length < 3) {
                    Mess = " - Yêu cầu nhập Mã nhà sản xuất từ 3 ký tự trở lên";
                    bolCode = false;
                }
                else {
                    bolCode = true;
                }
            }
            else {
                Mess = " - Yêu cầu nhập Mã nhà sản xuất";
            }
        }

        if (txtName != null) {
            if (txtName.value.trim() != "") {
                bolName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên nhà sản xuất";
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

        if (bolName && bolCode && bolEmail) {
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

            if (bolEmail == false) {
                txtEmail.focus(); return false;
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

    function PostData1() {

        var pageUrl = '<%=ResolveUrl("~/Tools/PostForm.aspx?Type=1")%>'
        var dataPostBack = $("#vswForm").serialize();
        //.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        //dataPostBack
        $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Erros) {
                    alert("Dữ liệu trả về (FALSE): " + data.ThongTin);
                }
                else
                    alert("Dữ liệu trả về (TRUE): " + data.ThongTin);
            })
            .done(function () {
                alert("second success - done");

                alert("last");
            })
            .fail(function () {
                alert("error - fail");
            })
            .always(function () {
                alert("finished - always");
            })
            ;
    }

    function PostData2() {

        var pageUrl = '<%=ResolveUrl("~/Tools/PostForm.aspx?Type=0")%>'
        var dataPostBack = $("#vswForm").serialize();
        //.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
        //dataPostBack
        $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Erros) {
                    alert("Dữ liệu trả về (TRUE): " + data.ThongTin);
                }
                else
                    alert("Dữ liệu trả về (FALSE): " + data.ThongTin);
            });
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_ManufacturerModel;
    var item = ViewBag.Data as ModProduct_ManufacturerEntity;
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
                Nhà sản xuất :
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
        <div>
            <input type="button" value="click me post Data 1" onclick="PostData1();return false;" />
            <input type="button" value="click me post Data 2" onclick="PostData2();return false;" />
        </div>
        <div class="col width-100">
            <table class="admintable">
                <tr>
                    <td class="key">
                        <label>
                            Mã nhà sản xuất :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" require="true"/><%=ShowRequireValidation()%>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên nhà sản xuất :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" require="true"/><%=ShowRequireValidation()%>
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
