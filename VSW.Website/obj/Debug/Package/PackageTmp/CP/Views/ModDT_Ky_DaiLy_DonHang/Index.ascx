<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModDT_Ky_DaiLy_DonHangModel;
    var listItem = ViewBag.Data as List<ModDT_Ky_DaiLy_DonHangEntity>;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<script type="text/javascript">
    $(document).ready(function () {

    });

    function doEdit(ID, DaiLyId) {
        $("#ModDTKyDaiLyId").val(DaiLyId.toString());
        VSWRedirect('Add', ID);
        return false;
    }
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
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Doanh thu - Kỳ - Đại lý - Đơn hàng</h2>
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

    var VSWController = 'ModDT_Ky_DaiLy_DonHang';

    var VSWArrVar = [
                        'ModDtKyId', 'ModDtKyId',
                        'ModDTKyDaiLyId', 'ModDTKyDaiLyId',
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
                    <select id="ModDtKyId" name="ModDtKyId" onchange="VSWRedirect();return false;" class="inputbox" size="1">
                        <option value="0">--- Tất cả ---</option>
                        <%= Utils.ShowDDLList(ModDT_KyService.Instance, "", "[ID] DESC", model.ModDtKyId)%>
                    </select>
                </td>
                <td nowrap="nowrap">
                    Đại lý :
                    <select id="ModDTKyDaiLyId" name="ModDTKyDaiLyId" onchange="VSWRedirect();return false;" class="inputbox"
                        size="1">
                        <option value="0">--- Tất cả ---</option>
                        <%=VSW.Lib.Global.Utils.ShowDDLDynamic(ModDT_Ky_DaiLyService.Instance, model.ModDtKyId <= 0 ? "" : ("ModDtKyId=" + model.ModDtKyId),
                                                                "Name", model.ModDTKyDaiLyId, "ModProductAgentId", "ModProductAgentParentId", "Name", "ID")%>
                    </select>
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
                        <%= GetSortLink("Mã", "Code")%>
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên", "Name")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày tạo", "NgayTao")%>
                    </th>
                    <th width="60px">
                        <%= GetSortLink("Tổng số sản phẩm", "TongSanPham")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng tiền", "TongTien")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Chiết khấu", "ChietKhau")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng sau giảm", "TongSauGiam")%>
                    </th>
                    <th width="1%" nowrap="nowrap" style="display:none;">
                        <%= GetSortLink("Duyệt", "Activity")%>
                    </th>
                    <th width="1%" nowrap="nowrap" style="display:none;">
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
                    <td align="left">
                        <%= listItem[i].Code%>
                    </td>
                    <td>
                        <a onclick="return doEdit(<%=listItem[i].ID %>, <%=listItem[i].ModDTKyDaiLyId %>);" href="#">
                            <%= listItem[i].Name%></a>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].NgayTao) %>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:#,##0}", listItem[i].TongSanPham)%>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:#,##0}", listItem[i].TongTien)%>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:#,##0}", listItem[i].ChietKhau)%>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:#,##0}", listItem[i].TongSauGiam)%>
                    </td>
                    <td align="center" style="display:none;">
                        <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td align="center" style="display:none;">
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
