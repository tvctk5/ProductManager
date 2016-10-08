<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModDT_KyModel;
    var listItem = ViewBag.Data as List<ModDT_KyEntity>;
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
            <h2>Doanh thu - Kỳ</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModDT_Ky';

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

    $(document).ready(function () {
        $("#toolbar").find("li:not(#toolbar-new,#toolbar-delete,#toolbar-config)").remove();
    });
</script>

<%= ShowMessage()%>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">

       <table>
            <tr>
                <td width="100%">
                     Lọc: <input type="text" id="filter_search" value="<%= model.SearchText %>" class="text_area" onchange="VSWRedirect();return false;" />
                    <button onclick="VSWRedirect();return false;">Tìm kiếm</button>
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
                        <%= GetSortLink("Mã", "Code")%>
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên", "Name")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày bắt đầu", "StartDate")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày kết thúc", "FinishDate")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng đầu kỳ", "TotalFirst")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng cuối kỳ", "TotalLast")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Trạng thái", "Activity")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày tạo", "CreateDate")%>
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
                    <td align="left">
                       <%= listItem[i].Code%>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)"><%= listItem[i].Name%></a>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].StartDate) %>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].FinishDate) %>
                    </td>
                    <td align="right">
                       <%= string.Format("{0:#,##0}", listItem[i].TotalFirst)%>
                    </td>
                    <td align="right">
                       <%= string.Format("{0:#,##0}", listItem[i].TotalLast)%>
                    </td>
                    <td align="left" nowrap="nowrap">
                       <%if (listItem[i].Activity)
                         {%>
                         <span style="color:Blue;font-weight:bold;">Đang thực hiện</span>
                       <%}
                         else
                         {%>
                         <span style="color:Red;">Đã kết thúc</span>
                       <%} %>
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
