<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_OrderModel;
    var listItem = ViewBag.Data as List<ModProduct_OrderEntity>;
    List<VSW.Lib.Global.ListItem.Item> listOrderStatus = VSW.Lib.Global.ListItem.List.GetListByConfigkey("Mod.OrderStatus");
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Danh sách các đơn hàng</h2>
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
<script type="text/javascript">

    var VSWController = 'ModProduct_Order';

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
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
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
                    <th width="1%" nowrap="nowrap" class="hide">
                        <%= GetSortLink("Transport id", "TransportId")%>
                    </th>
                    <th width="1%" nowrap="nowrap" class="hide">
                        <%= GetSortLink("Payment id", "PaymentId")%>
                    </th>
                    <th width="1%" nowrap="nowrap" class="hide">
                        <%= GetSortLink("Customers code", "CustomersCode")%>
                    </th>
                    <th width="1%" nowrap="nowrap" class="hide">
                        <%= GetSortLink("User id", "UserId")%>
                    </th>
                    <th width="1%" nowrap="nowrap" class="hide">
                        <%= GetSortLink("User modified id", "UserModifiedId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Mã", "Code")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Số loại sản phẩm", "QuantityProduct")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng số sản phẩm", "QuantityTotal")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giảm trừ", "Discount")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng giá trị trước giảm", "TotalFriceFirst")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng giá trị sau giảm", "TotalFrice")%>
                    </th>
                    <th width="1%" nowrap="nowrap" class="hide">
                        <%= GetSortLink("Note", "Note")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Status", "Status")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Người đặt", "NguoiDat_FullName")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày đặt hàng", "CreateDate")%>
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
                    <td align="right">
                        <%= i + 1%>.
                    </td>
                    <td align="center" nowrap="nowrap">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].ID%></a>
                    </td>
                    <td align="center" class="hide">
                        <%= string.Format("{0:#,##0}", listItem[i].TransportId)%>
                    </td>
                    <td align="center" class="hide">
                        <%= string.Format("{0:#,##0}", listItem[i].PaymentId)%>
                    </td>
                    <td align="center" class="hide">
                        <%= string.Format("{0:#,##0}", listItem[i].CustomersCode)%>
                    </td>
                    <td align="center" class="hide">
                        <%= string.Format("{0:#,##0}", listItem[i].UserId)%>
                    </td>
                    <td align="center" class="hide">
                        <%= string.Format("{0:#,##0}", listItem[i].UserModifiedId)%>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Code%></a>
                    </td>
                    <td align="center" nowrap="nowrap">
                        <%= string.Format("{0:#,##0}", listItem[i].QuantityProduct)%>
                    </td>
                    <td align="center" nowrap="nowrap">
                        <%= string.Format("{0:#,##0}", listItem[i].QuantityTotal)%>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <%= string.Format("{0:#,##0}", listItem[i].Discount)%>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <%= string.Format("{0:#,##0}", listItem[i].TotalFriceFirst)%>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <%= string.Format("{0:#,##0}", listItem[i].TotalFrice)%>
                    </td>
                    <td align="center" class="hide">
                        <%= listItem[i].Note%>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <%
                      var objStatus = listOrderStatus.Where(o => o.Value == listItem[i].Status.ToString()).FirstOrDefault();
                        %>
                        <%= objStatus.Name%>
                    </td>
                    <td align="left" style="width:100%">
                        <%= listItem[i].NguoiDat_FullName%>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].CreateDate) %>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
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
