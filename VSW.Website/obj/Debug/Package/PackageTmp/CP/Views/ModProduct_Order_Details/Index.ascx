<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModProduct_Order_DetailsModel;
    var listItem = ViewBag.Data as List<ModProduct_Order_DetailsEntity>;
%>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>Product_ order_ details</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModProduct_Order_Details';

    var VSWArrVar = [ 
                        'limit', 'PageSize'
                   ];


    var VSWArrQT = [
                      '<%= model.PageIndex + 1 %>', 'PageIndex', 
                      '<%= model.Sort %>', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
                    '20', 'PageSize'
                  ];
</script>

<%= ShowMessage()%>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">

       <table>
            <tr>
                <td width="100%">
                </td>
                <td nowrap="nowrap">
                </td>
            </tr>
        </table>

        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        #
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Order id", "OrderId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Product info id", "ProductInfoId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Quantity", "Quantity")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Frice input", "FriceInput")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Frice", "Frice")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Price sale buy", "PriceSaleBuy")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("V a t", "VAT")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Price type sale", "PriceTypeSale")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Price text sale view", "PriceTextSaleView")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Total frice", "TotalFrice")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Gifts", "Gifts")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Attach", "Attach")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Note", "Note")%>
                    </th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <td colspan="15">
                        <del class="container">
                            <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord)%>
                        </del>
                    </td>
                </tr>
            </tfoot>
            <tbody>
               <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                 { %>
                <tr class="row<%= i%2 %>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].ID%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].OrderId)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].ProductInfoId)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].Quantity)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].FriceInput)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].Frice)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].PriceSale)%>
                    </td>
                    <td align="center">
                       <a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[vatgx][<%= listItem[i].ID %>,<%= !listItem[i].VAT %>]')">
                          <span class="jgrid">
                             <span class="state <%= listItem[i].VAT ? "publish" : "unpublish" %>"></span>
                          </span>
                       </a>
                    </td>
                    <td align="center">
                       <a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[pricetypesalegx][<%= listItem[i].ID %>,<%= !listItem[i].SaleOffType %>]')">
                          <span class="jgrid">
                             <span class="state <%= listItem[i].SaleOffType ? "publish" : "unpublish" %>"></span>
                          </span>
                       </a>
                    </td>
                    <td align="center">
                       <%= listItem[i].PriceTextSaleView%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].TotalFrice)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Gifts%>
                    </td>
                    <td align="center">
                       <a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[attachgx][<%= listItem[i].ID %>,<%= !listItem[i].Attach %>]')">
                          <span class="jgrid">
                             <span class="state <%= listItem[i].Attach ? "publish" : "unpublish" %>"></span>
                          </span>
                       </a>
                    </td>
                    <td align="center">
                       <%= listItem[i].Note%>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
                            
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>
