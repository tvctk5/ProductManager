<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModProduct_Order_DetailsModel;
    var item = ViewBag.Data as ModProduct_Order_DetailsEntity;
%>

<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>Product_ order_ details : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Order id :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="OrderId" id="OrderId" value="<%=item.OrderId %>" maxlength="255" />
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
                    <label>Quantity :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Quantity" id="Quantity" value="<%=item.Quantity %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Frice input :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="FriceInput" id="FriceInput" value="<%=item.FriceInput %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Frice :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Frice" id="Frice" value="<%=item.Frice %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Price sale buy :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="PriceSaleBuy" id="PriceSaleBuy" value="<%=item.PriceSale %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>V a t :</label>
                </td>
                <td>
                    <input name="VAT" <%= item.VAT ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="VAT" <%= !item.VAT ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Price type sale :</label>
                </td>
                <td>
                    <input name="SaleOffType" <%= item.SaleOffType ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="SaleOffType" <%= !item.SaleOffType ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Price text sale view :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="PriceTextSaleView" id="PriceTextSaleView" value="<%=item.PriceTextSaleView %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Total frice :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="TotalFrice" id="TotalFrice" value="<%=item.TotalFrice %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Gifts :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Gifts" id="Gifts" value="<%=item.Gifts %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Attach :</label>
                </td>
                <td>
                    <input name="Attach" <%= item.Attach ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="Attach" <%= !item.Attach ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Note :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Note" id="Note" value="<%=item.Note %>" maxlength="255" />
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>