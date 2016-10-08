<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_FilterModel;
    var listItem = ViewBag.Data as List<ModProduct_FilterEntity>;
    var lstFilterGroups = model.ListFilterGroups;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<script type="text/javascript">
    $(document).ready(function () {
        var GiaTri = "";
        var GiaTri_tmp = "";
        var Dong_Mo = "";
        var GiaTri_TenThanhVien = "";

        $("tr[filtergroupid]").each(function (e) {
            GiaTri_tmp = $(this).attr("filtergroupid");
            if (typeof (GiaTri_tmp) === "undefined")
                return;

            GiaTri_TenThanhVien = $(this).attr("filtergroupname");
            if (GiaTri != GiaTri_tmp) {
                GiaTri = GiaTri_tmp;
                Dong_Mo = "<tr class='tr-header'><td colspan='8'><img class='img-icon-category' border='0' />";
                Dong_Mo += GiaTri_TenThanhVien + "</td></tr>";
                $(this).before(Dong_Mo);
            }

        });
    });
</script>
<style type="text/css">
.tr-header{font-weight:bold;}
</style>
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
                Danh sách: Thuộc tính lọc</h2>
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

    var VSWController = 'ModProduct_Filter';

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
                    <th width="5%" nowrap="nowrap">
                        ID
                    </th>
                    <th width="40%" nowrap="nowrap">
                        Giá trị
                    </th>
                    <th width="30%">
                        Ghi chú
                    </th>
                    <th style="width: 40px" nowrap="nowrap">
                        Ảnh
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Thứ tự <a href="javascript:vsw_exec_cmd('saveorder')" class="saveorder" title="Lưu sắp xếp">
                        </a>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Sử dụng
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
                <%
                    string sFilterGroupName = string.Empty;
                    for (int i = 0; listItem != null && i < listItem.Count; i++)
                    {
                        sFilterGroupName = ModProduct_FilterModel.GetNameFilterGroup(listItem[i].FilterGroupsId, lstFilterGroups);
                %>
                <tr class="row<%= i%2 %>" filtergroupid="<%= listItem[i].FilterGroupsId%>" filtergroupname="<%=sFilterGroupName%>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="center">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].ID%></a>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Value%></a>
                    </td>
                    <td align="left">
                        <%= listItem[i].Note%>
                    </td>
                    <td align="center" nowrap="nowrap">
                        <%= Utils.GetMedia(listItem[i].File, 40, 40)%>
                    </td>
                    <td class="order">
                        <%= GetOrder(listItem[i].ID, listItem[i].Order)%>
                    </td>
                    <td align="center">
                        <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
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
