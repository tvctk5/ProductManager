<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<% 
    var model = ViewBag.Model as ModFeedbackModel;
    var listItem = ViewBag.Data as List<ModFeedbackEntity>;
 %>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetListCommand("delete|Xóa")%>
        </div>
        <div class="pagetitle icon-48-massmail">
            <h2>Liên hệ</h2> 
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModFeedback';

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
             
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        #
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                     <th nowrap="nowrap">
                        <%= GetSortLink("Tiêu đề", "Title")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Họ và tên", "Name")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Email", "Email")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("IP", "IP")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày gửi", "Created")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
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
                    <td class="title">
                       <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)"> 
                          <%= listItem[i].Title%>
                       </a>
                    </td>
                    <td class="center">
                       <%= listItem[i].Name%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Email%>
                    </td>
                     <td align="center">
                       <%= listItem[i].IP%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].Created) %>
                    </td>
                    <td align="center">
                       <%= listItem[i].ID%>
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