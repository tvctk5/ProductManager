<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModDT_Ky_DaiLyModel;
    var listItem = ViewBag.Data as List<ModDT_Ky_DaiLyEntity>;

    string strTree = string.Empty;
    if (listItem != null && listItem.Count > 0)
    {
        foreach (var item in listItem)
        {
            if (!item.ModProductAgentParentId.HasValue)
                strTree += "{ \"id\": \"" + item.ModProductAgentId + "\", \"parent\": \"#\", \"text\": \"" + item.Name + "\", \"icon\" : \"/CP/Content/jstree/img/folder-parent.png\",\"state\" : {\"opened\": \"true\" }},";
            else
                strTree += "{ \"id\": \"" + item.ModProductAgentId + "\", \"parent\": \"" + item.ModProductAgentParentId + "\", \"text\": \"" + item.Name + "\",\"state\" : {\"opened\": \"true\" }},";
        }
    }
%>
<form id="vswForm" name="vswForm" method="post">
<script type="text/javascript" src="/{CPPath}/Content/add/jQuery/jquery-2.1.1.min.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/jstree/jstree.min.js"></script>
<link rel="stylesheet" href="/{CPPath}/Content/jstree/themes/default/style.min.css" />
<script type="text/javascript">
    $(document).ready(function () {
        $('#using_json').jstree({ 'core': {
            'data': [ <%=strTree %>
        ]
        }
        });

        $('#using_json').on('activate_node.jstree', function(e, data) {
        var modProductAgentId = data.node.id;
        var checkbox = $("td[ModProductAgentId='"+ modProductAgentId +"'] input[type='checkbox']:first");
        if(checkbox!=null){
            var count = document.vswForm.boxchecked.value;
            if(count>0){
                var checkboxOnChecked = $("td[ModProductAgentId] input[type='checkbox']:checked");
                if(checkboxOnChecked.size()>0)
                {
                    checkboxOnChecked.each(function(){
                        $(this).prop('checked', false);
                        isChecked(false);
                    });
                }
            }
            checkbox.prop('checked', true);
            isChecked(true);
            }
        });

        // Xóa hết các nút button xử lý đi
        $("#toolbar").find("li.button:not(#toolbar-config), li.divider").remove();
    });
</script>
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
                Doanh thu - Kỳ - Đại lý</h2>
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

    var VSWController = 'ModDT_Ky_DaiLy';

    var VSWArrVar = [
                        'filter_ky', 'KyId',
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
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20%; vertical-align: text-top;">
                    <div style="font-weight: bold; margin-top: 5px; margin-bottom: 5px;">
                        Các đại lý hiển thị theo cây</div>
                    <div id="using_json">
                    </div>
                </td>
                <td style="width: 80%">
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
                                Kỳ doanh thu :
                                <select id="filter_ky" onchange="VSWRedirect();return false;" class="inputbox" size="1">
                                    <option value="0">--- Tất cả ---</option>
                                    <%= Utils.ShowDDLList(ModDT_KyService.Instance, "", "[ID] DESC", model.KyId)%>
                                </select>
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
                                <th width="1%" nowrap="nowrap" style="display:none;">
                                    <%= GetSortLink("Kỳ", "ModDtKyId")%>
                                </th>
                                <th width="1%" nowrap="nowrap" style="display:none;">
                                    <%= GetSortLink("Mã đại lý", "Code")%>
                                </th>
                                <th class="title">
                                    <%= GetSortLink("Tên đại lý", "Name")%>
                                </th>
                                <th width="1%" nowrap="nowrap">
                                    <%= GetSortLink("Đại lý giới thiệu", "ModProductAgentParentId")%>
                                </th>
                                <th width="1%" nowrap="nowrap">
                                    <%= GetSortLink("Giá trị đầu kỳ", "TotalFirst")%>
                                </th>
                                <th width="1%" nowrap="nowrap">
                                    <%= GetSortLink("Giá trị cuối kỳ", "TotalLast")%>
                                </th>
                                <th width="1%" nowrap="nowrap">
                                    <%= GetSortLink("Loại đại lý", "ModLoaiDaiLyName")%>
                                </th>
                                <th width="1%" nowrap="nowrap" style="display:none;">
                                    <%= GetSortLink("Duyệt", "Activity")%>
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
                                <td align="center" ModProductAgentId="<%= listItem[i].ModProductAgentId %>">
                                    <%= GetCheckbox(listItem[i].ID, i)%>
                                </td>
                                <td align="right">
                                    <%= listItem[i].ID%>
                                </td>
                                <td align="left" nowrap="nowrap" style="display:none;">
                                    <!--%= listItem[i].getModDT_KyEntity().Name%-->
                                </td>
                                <td align="center" style="display:none;">
                                    <%= listItem[i].Code%>
                                </td>
                                <td>
                                    <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                                        <%= listItem[i].Name%></a>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <%= listItem[i].getModProduct_AgentParent().Name%>
                                </td>
                                <td align="center">
                                    <%= string.Format("{0:#,##0}", listItem[i].TotalFirst)%>
                                </td>
                                <td align="center">
                                    <%= string.Format("{0:#,##0}", listItem[i].TotalLast)%>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <%= listItem[i].ModLoaiDaiLyName%>
                                </td>
                                <td align="center" style="display:none;">
                                    <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
                                </td>
                                <td align="center">
                                    <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].CreateDate) %>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </td>
            </tr>
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
