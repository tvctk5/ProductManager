<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_SurveyGroup_DetailModel;
    var listItem = ViewBag.Data as List<ModProduct_SurveyGroup_DetailEntity>;
    var lstGroup = ModProduct_SurveyGroupService.Instance.CreateQuery().ToList();
    if (lstGroup == null)
        lstGroup = new List<ModProduct_SurveyGroupEntity>();
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

        $("tr[Surveygroupid]").each(function (e) {
            GiaTri_tmp = $(this).attr("Surveygroupid");
            if (typeof (GiaTri_tmp) === "undefined")
                return;

            GiaTri_TenThanhVien = $(this).attr("Surveygroupname");
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
    .tr-header{font-weight: bold;}
    .SurveyGroup-infoDateTime{font-size:10pt;color:#b8ae9c;padding-left:10px;font-weight:normal;}
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
                Chi tiết khảo sát</h2>
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

    var VSWController = 'ModProduct_SurveyGroup_Detail';

    var VSWArrVar = [
                        'limit', 'PageSize'
                   ];


    var VSWArrVar_QS = [
                        'Survey_search', 'SearchText'
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
                    <input type="text" id="Survey_search" value="<%= model.SearchText %>" class="text_area"
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
                        #
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th width="1%" nowrap="nowrap">
                        ID
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Mã
                    </th>
                    <th class="title">
                        Tên
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Vote
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Thứ tự <a href="javascript:vsw_exec_cmd('saveorder')" class="saveorder" title="Lưu sắp xếp">
                        </a>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Duyệt
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
                    string sSurveyGroupName = string.Empty;
                    for (int i = 0; listItem != null && i < listItem.Count; i++)
                    {
                        sSurveyGroupName = ModProduct_SurveyGroup_DetailModel.GetGroupName(listItem[i].SurveyGroupId, lstGroup);
                %>
                <tr class="row<%= i%2 %>" surveygroupid="<%= listItem[i].SurveyGroupId%>" surveygroupname="<%=sSurveyGroupName%>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <%= listItem[i].ID%>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <%= listItem[i].Code%>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Name%></a>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <%= string.Format("{0:#,##0}", listItem[i].Vote)%>
                    </td>
                    <td class="order">
                        <%= GetOrder(listItem[i].ID, listItem[i].Order)%>
                    </td>
                    <td align="center" nowrap="nowrap">
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
