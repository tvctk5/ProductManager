<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_CurrencyModel;
    var listItem = ViewBag.Data as List<ModProduct_CurrencyEntity>;
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
                Tỷ giá : Danh sách</h2>
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

    var VSWController = 'ModProduct_Currency';

    var VSWArrVar = [
                        'limit', 'PageSize'
                   ];


    var VSWArrVar_QS = [
                        'filter_search', 'SearchText'
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
                    Lọc:
                    <input type="text" id="filter_search" value="<%= model.SearchText %>" class="text_area"
                        onchange="VSWRedirect();return false;" />
                    <button onclick="VSWRedirect();return false;">
                        Tìm kiếm</button>
                </td>
                <td nowrap="nowrap">
                </td>
            </tr>
        </table>
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        STT
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th width="5%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Trạng thái", "Activity")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Mã tỷ giá", "Code")%>
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên tỷ giá", "Name")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("VNĐ", "VND")%>
                    </th>
                    <th width="30%" nowrap="nowrap">
                        <%= GetSortLink("Mô tả", "Description")%>
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
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="right">
                        <%= listItem[i].ID%>
                    </td>
                    <td align="center">
                        <%= GetPublish_ActivateBasic(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td align="left">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Code%></a>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Name%></a>
                    </td>
                    <td align="right">
                        <%= ConvertToMoney(listItem[i].VND,1)%>
                    </td>
                    <td align="left">
                        <%= listItem[i].Description%>
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
