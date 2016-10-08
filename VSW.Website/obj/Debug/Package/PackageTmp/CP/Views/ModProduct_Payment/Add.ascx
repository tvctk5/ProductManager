<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<script language="javascript" type="text/javascript">
function CheckValidationForm() {
 return true; 
}
</script>

<%
    var model = ViewBag.Model as ModProduct_PaymentModel;
    var item = ViewBag.Data as ModProduct_PaymentEntity;
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
            <h2>Hình thức thanh toán : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Tên hình thức :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Ghi chú :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Note" id="Note" value="<%=item.Note %>" maxlength="255" />
                </td>
            </tr>
              <%
                    if (model.RecordID > 0)
                    {
              %>
              <tr>
                  <td class="key" style="width: 0% !important;" nowrap="nowrap">
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
                  <td class="key" style="width: 0% !important;" nowrap="nowrap">
                      <label>
                          Ngày tạo :</label>
                  </td>
                  <td>
                      <input class="text_input" type="text" name="CreateDate" name="CreateDate" id="CreateDate"
                          value="<%=item.CreateDate %>" maxlength="255" />
                  </td>
              </tr>
              <%}%>
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