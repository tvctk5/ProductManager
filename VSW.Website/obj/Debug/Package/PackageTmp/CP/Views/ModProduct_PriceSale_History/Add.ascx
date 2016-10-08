<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<script language="javascript" type="text/javascript">
function CheckValidationForm() {
 return true; 
}
</script>

<%
    var model = ViewBag.Model as ModProduct_PriceSale_HistoryModel;
    var item = ViewBag.Data as ModProduct_PriceSale_HistoryEntity;
%>

<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="RecordID" name="RecordID" value="<%=model.RecordID %>" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <!--GetDefaultAddCommand()-->
            <%= GetDefaultAddCommandValidation()%>
     </div>
        <div class="pagetitle icon-48-generic">
            <h2>Product_ price sale_ history : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>
<div class="clr"></div>

<%= ShowMessage()%>
<div id="divMessError" style="display:none;"></div>
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
                    <label>Giá :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Price" id="Price" value="<%=item.Price %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Sale off type :</label>
                </td>
                <td>
                    <input name="SaleOffType" <%= item.SaleOffType ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="SaleOffType" <%= !item.SaleOffType ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Sale off value :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="SaleOffValue" id="SaleOffValue" value="<%=item.SaleOffValue %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Price sale :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="PriceSale" id="PriceSale" value="<%=item.PriceSale %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Start date :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="StartDate" id="StartDate" value="<%=item.StartDate %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Finish date :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="FinishDate" id="FinishDate" value="<%=item.FinishDate %>" maxlength="255" />
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