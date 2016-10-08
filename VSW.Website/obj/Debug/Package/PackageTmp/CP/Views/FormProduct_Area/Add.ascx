<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_National_AreaModel;
    var item = ViewBag.Data as ModProduct_National_AreaEntity;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<script src="/{CPPath}/Content/add/js/ProductionInfo.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
     
    $(document).ready(function () {
        $("a[class='toolbar'][onclick*=\"vsw_exec_cmd('cancel')\"]").removeAttr("onclick").click(function () {
            self.parent.ReloadData_Area();
            self.parent.tb_remove();
            return false;
        });

        $("a[class='toolbar'][onclick*=\"vsw_exec_cmd('save')\"]").removeAttr("onclick").addClass("hide");
          
    });
</script>
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
                <input type="hidden" id="RecordID" name="RecordID" value="<%=model.RecordID %>" />
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Khu vực :
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
                            Quốc gia :</label>
                    </td>
                    <td>
                        <input class="text_input" readonly="readonly" type="text" name="ProductNationalName"
                            id="ProductNationalName" value="<%=model.ProductNationalName %>" maxlength="255" />
                        <input class="text_input hide" type="text" name="ProductNationalId" id="ProductNationalId"
                            value="<%=model.ProductNationalId %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã khu vực :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên khu vực :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" />
                    </td>
                </tr>
                <%if (CPViewPage.UserPermissions.Approve)
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
