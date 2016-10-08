<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModProduct_Info_DetailsModel;
    var listItem = ViewBag.Data as List<ModProduct_Info_DetailsEntity>;
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
            <h2>Product_ info_ details</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModProduct_Info_Details';

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
                        <%= GetSortLink("Product info id", "ProductInfoId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t01", "TT01")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t02", "TT02")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t03", "TT03")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t04", "TT04")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t05", "TT05")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t06", "TT06")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t07", "TT07")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t08", "TT08")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t09", "TT09")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t10", "TT10")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t11", "TT11")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t12", "TT12")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t13", "TT13")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t14", "TT14")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t15", "TT15")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t16", "TT16")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t17", "TT17")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t18", "TT18")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t19", "TT19")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t20", "TT20")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t21", "TT21")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t22", "TT22")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t23", "TT23")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t24", "TT24")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t25", "TT25")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t26", "TT26")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t27", "TT27")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t28", "TT28")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t29", "TT29")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t30", "TT30")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t31", "TT31")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t32", "TT32")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t33", "TT33")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t34", "TT34")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t35", "TT35")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t36", "TT36")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t37", "TT37")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t38", "TT38")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t39", "TT39")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t40", "TT40")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t41", "TT41")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t42", "TT42")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t43", "TT43")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t44", "TT44")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t45", "TT45")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t46", "TT46")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t47", "TT47")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t48", "TT48")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t49", "TT49")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("T t50", "TT50")%>
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
                       <%= string.Format("{0:#,##0}", listItem[i].ProductInfoId)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT01%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT02%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT03%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT04%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT05%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT06%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT07%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT08%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT09%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT10%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT11%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT12%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT13%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT14%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT15%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT16%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT17%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT18%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT19%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT20%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT21%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT22%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT23%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT24%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT25%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT26%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT27%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT28%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT29%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT30%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT31%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT32%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT33%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT34%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT35%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT36%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT37%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT38%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT39%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT40%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT41%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT42%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT43%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT44%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT45%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT46%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT47%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT48%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT49%>
                    </td>
                    <td align="center">
                       <%= listItem[i].TT50%>
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
