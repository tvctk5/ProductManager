<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModDT_CapDaiLy_TyLeModel;
    var listItem = ViewBag.Data as List<ModDT_CapDaiLy_TyLeEntity>;

    string strTree = string.Empty;
    if (listItem != null && listItem.Count > 0)
    {
        foreach (var item in listItem)
        {
            if (item.ParentID==null || item.ParentID == 0)
                strTree += "{ \"id\": \"" + item.ID + "\", \"parent\": \"#\", \"text\": \"" + item.Name + "\", \"icon\" : \"/CP/Content/jstree/img/folder-parent.png\",\"state\" : {\"opened\": \"true\" }},";
            else
                strTree += "{ \"id\": \"" + item.ID + "\", \"parent\": \"" + item.ParentID + "\", \"text\": \"" + item.Name + "\",\"state\" : {\"opened\": \"true\" }},";
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
            'data': [<%=strTree %>]
        }
        });

        $('#using_json').on('activate_node.jstree', function(e, data) {
                var id = data.node.id;
                var checkbox = $("td[IDCheck='"+ id +"'] input[type='checkbox']:first");
                if(checkbox!=null){
                    var count = document.vswForm.boxchecked.value;
                    if(count>0){
                        var checkboxOnChecked = $("td[IDCheck] input[type='checkbox']:checked");
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
                Doanh thu - Cấp đại lý - Hoa hồng</h2>
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

    var VSWController = 'ModDT_CapDaiLy_TyLe';

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
                                    <%= GetSortLink("Cha", "ParentID")%>
                                </th>
                                <th width="1%" nowrap="nowrap">
                                    <%= GetSortLink("Mã cấp", "Code")%>
                                </th>
                                <th class="title">
                                    <%= GetSortLink("Tên cấp", "Name")%>
                                </th>
                                <th width="1%" nowrap="nowrap" style="display: none;">
                                    <%= GetSortLink("Loại", "Type")%>
                                </th>
                                <th width="1%" nowrap="nowrap">
                                    <%= GetSortLink("Giá trị hoa hồng", "Value")%>
                                </th>
                                <th width="1%" nowrap="nowrap" style="display: none;">
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
                                <td align="center" IDCheck="<%= listItem[i].ID %>">
                                    <%= GetCheckbox(listItem[i].ID, i)%>
                                </td>
                                <td align="right">
                                    <%= listItem[i].ID%>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <%= GetName(listItem[i].getParent()) %>
                                </td>
                                <td align="left">
                                    <%= listItem[i].Code%>
                                </td>
                                <td>
                                    <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                                        <%= listItem[i].Name%></a>
                                </td>
                                <td align="center" style="display: none;">
                                    <%= string.Format("{0:#,##0}", listItem[i].Type)%>
                                </td>
                                <td align="right">
                                    <%= string.Format("{0:#,##0}", listItem[i].Value)%> %
                                </td>
                                <td align="center" style="display: none;">
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
