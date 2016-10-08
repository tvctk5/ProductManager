<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModChat_HistoryModel;
    var listItem = ViewBag.Data as List<ModChat_HistoryEntity>;
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
            <h2>Chat_ history</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModChat_History';

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
                        <%= GetSortLink("From_ name", "From_Name")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("From_ user name", "From_UserName")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("From_ id", "From_Id")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("To_ name", "To_Name")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("To_ user name", "To_UserName")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("To_ id", "To_Id")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Message", "Message")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("I p", "IP")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Duyệt", "Activity")%>
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
                       <%= listItem[i].From_Name%>
                    </td>
                    <td align="center">
                       <%= listItem[i].From_UserName%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].From_Id)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].To_Name%>
                    </td>
                    <td align="center">
                       <%= listItem[i].To_UserName%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].To_Id)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Message%>
                    </td>
                    <td align="center">
                       <%= listItem[i].IP%>
                    </td>
                    <td align="center">
                       <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
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
