﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModMenu_DynamicModel;
    var listItem = ViewBag.Data as List<ModMenu_DynamicEntity>;
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
                Thiết lập menu</h2>
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

    var VSWController = 'ModMenu_Dynamic';

    var VSWArrVar = [
                        'filter_lang', 'LangID',
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
                    '1', 'LangID',
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
                    Ngôn ngữ :<%= ShowDDLLang(model.LangID)%>
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
                        <%= GetSortLink("Loại menu", "ModMenuTypeID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Cha", "ParentID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngôn ngữ", "LangID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Trang liên kết", "SysPageID")%>
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên", "Name")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Thứ tự", "Order")%>
                        <a href="javascript:vsw_exec_cmd('saveorder')" class="saveorder" title="Lưu sắp xếp">
                        </a>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Hiển thị", "Activity")%>
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
                    <td align="right">
                        <%= listItem[i].ID%>
                    </td>
                    <td align="left">
                        <%= GetName(listItem[i].getModMenuType()) %>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <%= listItem[i].getParentName(listItem,listItem[i].ParentID) %>
                    </td>
                    <td align="left">
                        <%= GetName(listItem[i].getLang()) %>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <%= GetName(listItem[i].getSysPage()) %>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Name%></a>
                    </td>
                    <td class="order">
                        <%= GetOrder(listItem[i].ID, listItem[i].Order)%>
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
