<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModDT_Ky_DaiLyModel;
    var item = ViewBag.Data as ModDT_Ky_DaiLyEntity;

    var listItem = ViewBag.ListData as List<ModDT_Ky_DaiLyEntity>;

    string strTree = string.Empty;
    if (listItem != null && listItem.Count > 0)
    {
        foreach (var itemData in listItem)
        {
            if (!itemData.ModProductAgentParentId.HasValue || itemData.ID == item.ID)
                strTree += "{ \"id\": \"" + itemData.ModProductAgentId + "\", \"parent\": \"#\", \"text\": \"" + itemData.Name + "\", \"icon\" : \"/CP/Content/jstree/img/folder-parent.png\",\"state\" : {\"opened\": \"true\" }},";
            else
                strTree += "{ \"id\": \"" + itemData.ModProductAgentId + "\", \"parent\": \"" + itemData.ModProductAgentParentId + "\", \"text\": \"" + itemData.Name + "\",\"state\" : {\"opened\": \"true\" }},";
        }
    }
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<input type="hidden" id="DaChotKy" name="DaChotKy" value="<%=model.DaChotKy %>" />
<script type="text/javascript" src="/{CPPath}/Content/add/jQuery/jquery-2.1.1.min.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/jstree/jstree.min.js"></script>
<link rel="stylesheet" href="/{CPPath}/Content/jstree/themes/default/style.min.css" />
<script type="text/javascript">
    $(document).ready(function () {
        var DaChotKy = '<%=model.DaChotKy %>';
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
        $("#toolbar").find("li#toolbar-save-new").remove();

        // Đã chốt kỳ
        if(DaChotKy==1)
        {
            // Xóa hết các nút chức năng đi
            $("#toolbar").find("li:not(#toolbar-cancel)").remove();
        }

    });
</script>
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Doanh thu - Kỳ - Đại lý:
                <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
<%= ShowMessage()%>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="col width-100">
            <table class="admintable">
                <tr>
                    <td class="key">
                        <label>
                            Kỳ doanh thu :</label>
                    </td>
                    <td>
                        <select name="ModDtKyId" id="ModDtKyId" disabled="disabled" class="DropDownList">
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModDT_KyService.Instance, item.ModDtKyId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Đại lý giới thiệu :</label>
                    </td>
                    <td>
                        <select name="ModProductAgentParentId" id="ModProductAgentParentId" disabled="disabled"
                            class="DropDownList">
                            <%if (item.ModProductAgentParentId == null || ((int)item.ModProductAgentParentId) == 0)
                              { %>
                            <option>--- Là đại lý giới thiệu ---</option>
                            <%}
                              else
                              {%>
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModProduct_AgentService.Instance, item.ModProductAgentParentId == null ? 0 : (int)item.ModProductAgentParentId)%>
                            <%} %>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Đại lý :</label>
                    </td>
                    <td>
                        <select name="ModProductAgentId" id="ModProductAgentId" disabled="disabled" class="DropDownList">
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModProduct_AgentService.Instance, item.ModProductAgentId)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="key">
                        <label>
                            Loại khuyến mãi trong kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Type" id="Type" value="<%=item.Type %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="key">
                        <label>
                            Giá trị khuyến mãi :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Value" id="Value" value="<%=item.Value %>"
                            maxlength="255" />
                    </td>
                </tr>
                <%if(model.DaChotKy==1) {%>
                <tr>
                    <td class="key">
                        <label>
                            Tổng tiền nhập hàng trong kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="TongTienLayHang" id="TongTienLayHang" value="<%=string.Format("{0:#,##0}", item.TongTienLayHang) %>"
                            maxlength="255" readonly="readonly" price="TRUE"/>
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td class="key">
                        <label>
                            Tổng thu nhập trước kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="TotalFirst" id="TotalFirst" value="<%=string.Format("{0:#,##0}", item.TotalFirst) %>"
                            maxlength="255" price="TRUE" <%if(model.DaChotKy==1){ %> readonly="readonly" <%} %>/>
                    </td>
                </tr>
                <%if(model.DaChotKy==1) {%>
                <tr>
                    <td class="key">
                        <label>
                            Tổng thu nhập trong kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="TongTienHoaHong" id="TongTienHoaHong" value="<%=string.Format("{0:#,##0}", item.TongTienHoaHong) %>"
                            maxlength="255" readonly="readonly" price="TRUE"/>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tổng thu nhập sau kỳ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="TotalLast" id="TotalLast" value="<%=string.Format("{0:#,##0}", item.TotalLast) %>"
                            maxlength="255" readonly="readonly" price="TRUE"/>
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td class="key">
                        <label>
                            Mã loại đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="ModLoaiDaiLyCode" id="ModLoaiDaiLyCode"
                            value="<%=item.ModLoaiDaiLyCode %>" maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên loại đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="ModLoaiDaiLyName" id="ModLoaiDaiLyName"
                            value="<%=item.ModLoaiDaiLyName %>" maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="key">
                        <label>
                            Hình thức khuyến mại :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="ModLoaiDaiLyType" id="ModLoaiDaiLyType"
                            value="<%=item.ModLoaiDaiLyType %>" maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="key">
                        <label>
                            Giá trị khuyến mại :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="ModLoaiDaiLyValue" id="ModLoaiDaiLyValue"
                            value="<%=item.ModLoaiDaiLyValue %>" maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <%if (CPViewPage.UserPermissions.Approve && false)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Duyệt :</label>
                    </td>
                    <td>
                        <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                        Không
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>"
                            maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 20%; vertical-align: text-top;">
                                    <div style="font-weight: bold; margin-top: 5px; margin-bottom: 5px;">
                                        Các đại lý con hiển thị theo cây</div>
                                    <div id="using_json">
                                    </div>
                                </td>
                                <td style="width: 80%">
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
                                                <th width="1%" nowrap="nowrap" style="display: none;">
                                                    <%= GetSortLink("Kỳ", "ModDtKyId")%>
                                                </th>
                                                <th width="1%" nowrap="nowrap">
                                                    <%= GetSortLink("Đại lý giới thiệu", "ModProductAgentParentId")%>
                                                </th>
                                                <th width="1%" nowrap="nowrap" style="display: none;">
                                                    <%= GetSortLink("Mã đại lý", "Code")%>
                                                </th>
                                                <th class="title">
                                                    <%= GetSortLink("Tên đại lý", "Name")%>
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
                                                <th width="1%" nowrap="nowrap">
                                                    Đơn hàng
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                                              { %>
                                            <tr class="row<%= i%2 %>">
                                                <td align="center">
                                                    <%= i + 1%>
                                                </td>
                                                <td align="center" modproductagentid="<%= listItem[i].ModProductAgentId %>">
                                                    <%= GetCheckbox(listItem[i].ID, i)%>
                                                </td>
                                                <td align="right">
                                                    <%= listItem[i].ID%>
                                                </td>
                                                <td align="left" nowrap="nowrap" style="display: none;">
                                                    <!--%= listItem[i].getModDT_KyEntity().Name%-->
                                                </td>
                                                <td align="left" nowrap="nowrap">
                                                    <%= listItem[i].getModProduct_AgentParent().Name%>
                                                </td>
                                                <td align="center" style="display: none;">
                                                    <%= listItem[i].Code%>
                                                </td>
                                                <td>
                                                    <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                                                        <%= listItem[i].Name%></a>
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
                                                <td align="center">
                                                <a href="javascript:RedirectController('ModDT_Ky_DaiLy_DonHang','AddDonHang', <%= listItem[i].ID %>,'ModDTKyDaiLyId')">
                                                            <span class="jgrid"><span class="state icon-16-help-shop"></span></span></a>
                                                </td>
                                            </tr>
                                            <%} %>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
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
</form>
