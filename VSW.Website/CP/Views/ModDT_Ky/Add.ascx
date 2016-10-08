<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModDT_KyModel;
    var item = ViewBag.Data as ModDT_KyEntity;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="DaChotKy" name="DaChotKy" value="<%=model.DaChotKy %>" />
<script type="text/javascript" src="/{CPPath}/Content/add/css/jquery-ui.min.1.8.js"></script>
<link href="/{CPPath}/Content/add/datetimepickermaster/jquery.datetimepicker.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/{CPPath}/Content/add/datetimepickermaster/jquery.datetimepicker.js"></script>
<script language="javascript">
    //#region documentready
    $(document).ready(function () {
        var bolDaChotKy = '<%=model.DaChotKy %>';
        $("#toolbar").find("li#toolbar-save-new").remove();
        if (bolDaChotKy == '1') {
            $("#toolbar").find("li:not(#toolbar-cancel)").remove();
            $(".admintable").find("input").attr("readonly", "readonly");
        }
        else {
            // Ngày bắt đầu khuyến mại
            $("#StartDate").datetimepicker({
                lang: 'vi',
                format: 'd/m/Y H:i',
                mask: true,
                allowBlank: true,
                validateOnBlur: false,
                value: '<%=item.StartDate==null?DateTime.Now.ToString("dd/MM/yyyy HH:mm"):(item.StartDate.Year==0001?DateTime.Now.ToString("dd/MM/yyyy HH:mm"):item.StartDate.ToString("dd/MM/yyyy HH:mm")) %>'
                //,onShow: function (ct) {
                //    this.setOptions({
                //        maxDate: $("#FinishDate").val() ? $("#FinishDate").val() : false
                //    })
                //}
            });
            // Ngày kết thúc khuyến mại
            $("#FinishDate").datetimepicker({
                lang: 'vi',
                format: 'd/m/Y H:i',
                mask: true,
                allowBlank: true,
                validateOnBlur: false,
                value: '<%=item.FinishDate==null?string.Empty:(item.FinishDate.Year==0001?DateTime.Now.AddDays(30).ToString("dd/MM/yyyy HH:mm"):item.FinishDate.ToString("dd/MM/yyyy HH:mm")) %>'
                //, onShow: function (ct) {
                //    this.setOptions({
                //        minDate: $('#StartDate').val() ? $('#StartDate').val() : false
                //    })
                //}
            });
        }
        // Chốt kỳ doanh thu
        $("#btnChotKy").click(function () {
            if (confirm("Bạn có chắc chắn muốn chốt kỳ kinh doanh này?") == false)
                return false;

            vsw_exec_cmd('ChotKy');
        });
    });
</script>
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
                Doanh thu - Kỳ :
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
                            Mã kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" <% if(model.RecordID > 0){%> readonly="readonly" <%} %> />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ngày bắt đầu :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="StartDate" id="StartDate" value="<%=item.StartDate %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ngày kết thúc :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="FinishDate" id="FinishDate" value="<%=item.FinishDate %>"
                            maxlength="255" />
                    </td>
                </tr>
                <%if (model.RecordID > 0)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Tổng giá trị đầu kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="TotalFirst" id="TotalFirst" value="<%=item.TotalFirst %>"
                            maxlength="255" price="true" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tổng giá trị cuối kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="TotalLast" id="TotalLast" value="<%=item.TotalLast %>"
                            maxlength="255" price="true" readonly="readonly" />
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>"
                            maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <%if (CPViewPage.UserPermissions.Approve && item.Activity == false)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Trạng thái :</label>
                    </td>
                    <td>
                        <span style="color:Red;font-weight:bold;">Đã kết thúc</span>
                    <% if (false)
                       { %>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Đang thực hiện
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Đã kết thúc
                        <%} %>
                    </td>
                </tr>
                <%} %>
                <%if (model.RecordID > 0 && item.Activity)
                  {%>
                <tr>
                    <td class="key">
                    </td>
                    <td>
                        <div style="text-align: left; margin-top: 5px; margin-bottom: 5px;">
                            <input id="btnChotKy" value="Chốt kỳ doanh thu" class="text_input button-function button-background-image-checked_out"
                                style="width: 180px;  background-color: gold;" type="button" />
                        </div>
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
