<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModDT_Ky_DaiLy_DonHang_SanPhamModel;
    var listItem1 = ViewBag.Data as List<ModDT_Ky_DaiLy_DonHang_SanPhamEntity>;
    var listItem = ViewBag.DataLazy as List<VSW.Lib.LinqToSql.Mod_DT_Ky_DaiLy_DonHang_SanPham>;
%>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>Doanh thu - Kỳ - Đại lý - Đơn hàng - Sản phẩm</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">
    $(document).ready(function () { 
        $("#toolbar").remove();
    });
    var VSWController = 'ModDT_Ky_DaiLy_DonHang_SanPham';

    var VSWArrVar = [
                        'ModDtKyId', 'ModDtKyId',
                        'ModDTKyDaiLyId', 'ModDTKyDaiLyId',
                        'ModDTKyDaiLyDonHangId','ModDTKyDaiLyDonHangId',
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

        <table>
            <tr>
                <td width="100%">
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
                    Đơn hàng :
                    <select id="ModDTKyDaiLyDonHangId" name="ModDTKyDaiLyDonHangId" onchange="VSWRedirect();return false;" class="inputbox"
                        size="1">
                        <option value="0">--- Tất cả ---</option>
                        <%
                            string strWhere = "";
                            if (model.ModDtKyId > 0)
                                strWhere += "ModDtKyId=" + model.ModDtKyId;

                            if (model.ModDTKyDaiLyId > 0)
                                if (string.IsNullOrEmpty(strWhere))
                                    strWhere += " ModDTKyDaiLyId=" + model.ModDTKyDaiLyId;
                                else
                                    strWhere += " AND ModDTKyDaiLyId=" + model.ModDTKyDaiLyId;
                                
                             %>
                        <%=VSW.Lib.Global.Utils.ShowDDLList(ModDT_Ky_DaiLy_DonHangService.Instance, strWhere,
                                                               "Name", model.ModDTKyDaiLyDonHangId)%>
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
                    <th width="25%">
                        <%= GetSortLink("Đại lý", "ModDTKyDaiLyDonHangId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Đơn hàng", "ModDTKyDaiLyDonHangId")%>
                    </th>
                    <th width="100%">
                        <%= GetSortLink("Sản phẩm", "ModProductId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Đơn giá", "DonGia")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Số lượng", "SoLuong")%>
                    </th>
                    <th width="1%" nowrap="nowrap" style="display:none;">
                        <%= GetSortLink("Chiet khau", "ChietKhau")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Tổng tiền", "Tổng tiền")%>
                    </th>
                    <th width="1%" nowrap="nowrap" style="display:none;">
                        <%= GetSortLink("Tong sau giam", "TongSauGiam")%>
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
                    <td align="right">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="right">
                       <%= listItem[i].ID%>
                    </td>
                    <td align="left">
                       <%= listItem[i].Mod_DT_Ky_DaiLy_DonHang.Mod_DT_Ky_DaiLy.Name%>
                    </td>
                    <td align="left">
                       <%= listItem[i].Mod_DT_Ky_DaiLy_DonHang.Code%>
                    </td>
                    <td align="left">
                       <%= listItem[i].Mod_Product_Info.Name%>
                    </td>
                    <td align="right">
                       <%= string.Format("{0:#,##0}", listItem[i].DonGia)%>
                    </td>
                    <td align="right">
                       <%= string.Format("{0:#,##0}", listItem[i].SoLuong)%>
                    </td>
                    <td align="right" style="display:none;">
                       <%= string.Format("{0:#,##0}", listItem[i].ChietKhau)%>
                    </td>
                    <td align="right">
                       <%= string.Format("{0:#,##0}", listItem[i].TongTien)%>
                    </td>
                    <td align="right" style="display:none;">
                       <%= string.Format("{0:#,##0}", listItem[i].TongSauGiam)%>
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
