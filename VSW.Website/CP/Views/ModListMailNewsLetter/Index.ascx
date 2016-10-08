<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModListMailNewsLetterModel;
    var listItem = ViewBag.Data as List<ModListMailNewsLetterEntity>;
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
            <h2>Danh sách email nhận tin</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModListMailNewsLetter';

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
                    <th class="title">
                        <%= GetSortLink("Tên", "Name")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Email", "Email")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giới tình", "Sex")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("I p", "IP")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Code remove list", "CodeRemoveList")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Create date", "CreateDate")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Duyệt", "Activity")%>
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
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)"><%= listItem[i].Name%></a>
                    </td>
                    <td align="center">
                       <%= listItem[i].Email%>
                    </td>
                    <td align="center">
                       <a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[sexgx][<%= listItem[i].ID %>,<%= !listItem[i].Sex %>]')">
                          <span class="jgrid">
                             <span class="state <%= listItem[i].Sex ? "publish" : "unpublish" %>"></span>
                          </span>
                       </a>
                    </td>
                    <td align="center">
                       <%= listItem[i].IP%>
                    </td>
                    <td align="center">
                       <%= listItem[i].CodeRemoveList%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].CreateDate) %>
                    </td>
                    <td align="center">
                       <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
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
