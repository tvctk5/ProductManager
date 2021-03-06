﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_PropertiesListModel;
    var listItem = ViewBag.Data as List<ModProduct_PropertiesListEntity>;
    var listPropertiesGroup = ViewBag.GetListPropertiesGroup as List<ModProduct_PropertiesGroupsEntity>;
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
            <%--GetDefaultListCommand_Not_Create_Copy_Delete() --%>
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Thuộc tính sản phẩm: Danh sách</h2>
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

    var VSWController = 'ModProduct_PropertiesList';

    var VSWArrVar = [
                        'limit', 'PageSize'
                   ];


    var VSWArrVar_QS = [
                        'filter_ProductGroup', 'PropertiesGroupsId',
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
                    <select id="filter_ProductGroup" onchange="VSWRedirect()" class="inputbox" size="1">
                        <option value="0">(Tất cả)</option>
                        <%= Utils.ShowDDLPropertiesGroups(model.ModelPropertiesGroupsId)%>
                    </select>
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
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="5%" nowrap="nowrap">
                        <%= GetSortLink("Trạng thái", "Activity")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Mã thuộc tính", "Code")%>
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên thuộc tính", "Name")%>
                    </th>
                    <th width="25%" nowrap="nowrap">
                        Nhóm thuộc tính
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Sắp xếp", "Order")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
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
                <%ModProduct_PropertiesGroupsEntity objPropertiesGroupsEntity = null;
                  for (int i = 0; listItem != null && i < listItem.Count; i++)
                  {%>
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
                        <%= GetPublish_ActivateBasic(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Code%></a>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Name%></a>
                    </td>
                    <td align="left">
                        <%
                      if (listItem[i].PropertiesGroupsId != null && listPropertiesGroup != null && listPropertiesGroup.Count > 0)
                      {
                          objPropertiesGroupsEntity = listPropertiesGroup.Where(o => o.ID == listItem[i].PropertiesGroupsId).SingleOrDefault();
                          if (objPropertiesGroupsEntity != null)
                          { 
                        %>
                        <%=objPropertiesGroupsEntity.Code%>
                        <%
                          }
                      }
                        %>
                    </td>
                    <td align="right">
                        <%= listItem[i].Order %>
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
