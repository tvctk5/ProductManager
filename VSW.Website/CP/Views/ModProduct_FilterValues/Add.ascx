<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {
        return true;
    }
</script>
<script type="text/javascript">
    function ReloadGrid() {
        vsw_exec_cmd("Add");
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_FilterValuesModel;
    var item = ViewBag.Data as ModProduct_FilterValuesEntity;
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
                Product_ filter values :
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
                            Chủng loại :</label>
                    </td>
                    <td>
                         
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Chủng loại :</label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Product filter id :</label>
                    </td>
                    <td>
                        <a id="lnkAdd" onclick="<%=model.Url %>">LinkButton</a>
                        <input class="text_input" type="text" name="ProductFilterId" id="ProductFilterId"
                            value="<%=item.ProductFilterId %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Giá trị :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Value" id="Value" value="<%=item.Value %>"
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
