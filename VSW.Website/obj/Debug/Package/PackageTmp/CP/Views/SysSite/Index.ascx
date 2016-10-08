<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<% 
    var model = ViewBag.Model as SysSiteModel;
    var listItem = ViewBag.Data as List<SysSiteEntity>;
%>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetListCommand("new|Thêm,edit|Sửa,space,delete|Xóa,space,config|Xóa cache")%>
        </div>
        <div class="pagetitle icon-48-module">
            <h2>Site</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'SysSite';

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
                    <th class="title">
                        <%= GetSortLink("Tên", "Name")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Mã", "Code")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Ngôn ngữ", "LangID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Mặc định", "Default")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Sắp xếp", "Order")%>
                        <a href="javascript:vsw_exec_cmd('saveorder')" class="saveorder" title="Lưu sắp xếp"></a>
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
                    <td align="center">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)"><%= listItem[i].Name%></a>
                    </td>
                    <td align="center">
                       <%= listItem[i].Code %>
                    </td>
                    <td align="center">
                       <%= GetName(SysLangService.Instance.GetByID(listItem[i].LangID)) %>
                    </td>
                    <td align="center">
                       <%if (!listItem[i].Default){ %><a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[defaultgx][<%= listItem[i].ID %>]')"><%} %>
                        <span class="jgrid">
                            <span class="state <%= listItem[i].Default ? "publish" : "unpublish" %>"></span>
                        </span>
                       <%if (!listItem[i].Default){ %></a><%} %>
                    </td>
                    <td class="order">
                       <%= GetOrder(listItem[i].ID, listItem[i].Order)%>
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