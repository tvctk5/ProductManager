<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<script language="javascript" type="text/javascript">
function CheckValidationForm() {
 return true; 
}
</script>

<%
    var model = ViewBag.Model as ModProduct_Price_HistoryModel;
    var item = ViewBag.Data as ModProduct_Price_HistoryEntity;
%>

<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <!--GetDefaultAddCommand()-->
            <%= GetDefaultAddCommandValidation()%>
     </div>
        <div class="pagetitle icon-48-generic">
            <h2>Product_ price_ history : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>
<div class="clr"></div>

<%= ShowMessage()%>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="col width-100">
          <table class="admintable">
           <tr>
                <td class="key">
                    <label>User id :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="UserId" id="UserId" value="<%=item.UserId %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Product info id :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ProductInfoId" id="ProductInfoId" value="<%=item.ProductInfoId %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Loại :</label>
                </td>
                <td>
                    <input name="Type" <%= item.Type ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="Type" <%= !item.Type ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Before price :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="BeforePrice" id="BeforePrice" value="<%=item.BeforePrice %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>After price :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="AfterPrice" id="AfterPrice" value="<%=item.AfterPrice %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Create date :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>" maxlength="255" />
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
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