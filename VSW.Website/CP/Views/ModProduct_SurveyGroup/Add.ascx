<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_SurveyGroupModel;
    var item = ViewBag.Data as ModProduct_SurveyGroupEntity;
    
%>
<form id="vswForm" name="vswForm" method="post">
<link href="/{CPPath}/Content/add/datetimepickermaster/jquery.datetimepicker.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/{CPPath}/Content/add/datetimepickermaster/jquery.datetimepicker.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        // Ngày bắt đầu
        $("#StartDate").datetimepicker({
            lang: 'vi',
            format: 'd/m/Y H:i',
            mask: true,
            allowBlank: true,
            validateOnBlur: false,
            value: '<%=ConvertTool.CheckDateIsNull(item.StartDate)?string.Empty:item.StartDate.ToString("dd/MM/yyyy HH:mm") %>'
            //, onShow: function (ct) {
            //    this.setOptions({
            //        minDate: $('#FinishDate').val() ? $('#FinishDate').val() : false
            //    })
            //}
        });

        // Ngày kết thúc
        $("#FinishDate").datetimepicker({
            lang: 'vi',
            format: 'd/m/Y H:i',
            mask: true,
            allowBlank: true,
            validateOnBlur: false,
            value: '<%=ConvertTool.CheckDateIsNull(item.FinishDate)?string.Empty:item.FinishDate.ToString("dd/MM/yyyy HH:mm") %>'
            //, onShow: function (ct) {
            //    this.setOptions({
            //        maxDate: $('#StartDate').val() ? $('#StartDate').val() : false
            //    })
            //}
        });
    });
</script>
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Quản lý Khảo sát :
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
                            Tên :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ngày bắt đầu :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="StartDate" id="StartDate" maxlength="20" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ngày kết thúc :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="FinishDate" id="FinishDate" maxlength="20" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Kiểu :</label>
                    </td>
                    <td>
                        <select id="Type" name="Type" class="DropDownList">
                            <%= Utils.ShowDDLByConfigkey("Mod.TypeSurveyGroup", item.Type)%>
                        </select>
                    </td>
                </tr>
                <%if (model.RecordID > 0)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>"
                            maxlength="255" />
                    </td>
                </tr>
                <%}
                  if (CPViewPage.UserPermissions.Approve)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Duyệt :</label>
                    </td>
                    <td>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Không
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
</form>
