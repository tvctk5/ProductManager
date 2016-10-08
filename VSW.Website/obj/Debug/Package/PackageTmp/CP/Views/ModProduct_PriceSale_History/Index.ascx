<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModProduct_PriceSale_HistoryModel;
    var listItem = ViewBag.Data as List<ModProduct_PriceSale_HistoryEntity>;
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
            <h2>Product_ price sale_ history</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModProduct_PriceSale_History';

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
                        <%= GetSortLink("User id", "UserId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Product info id", "ProductInfoId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giá", "Price")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Sale off type", "SaleOffType")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Sale off value", "SaleOffValue")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Price sale", "PriceSale")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Start date", "StartDate")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Finish date", "FinishDate")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Create date", "CreateDate")%>
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
                       <%= string.Format("{0:#,##0}", listItem[i].UserId)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].ProductInfoId)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].Price)%>
                    </td>
                    <td align="center">
                       <a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[saleofftypegx][<%= listItem[i].ID %>,<%= !listItem[i].SaleOffType %>]')">
                          <span class="jgrid">
                             <span class="state <%= listItem[i].SaleOffType ? "publish" : "unpublish" %>"></span>
                          </span>
                       </a>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].SaleOffValue)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].PriceSale)%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].StartDate) %>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].FinishDate) %>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].CreateDate) %>
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
