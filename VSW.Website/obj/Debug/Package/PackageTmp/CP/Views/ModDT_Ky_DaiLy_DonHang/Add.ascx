<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModDT_Ky_DaiLy_DonHangModel;
    var item = ViewBag.Data as ModDT_Ky_DaiLy_DonHangEntity;
    var KyDaiLy = ViewBag.KyDaiLy as ModDT_Ky_DaiLyEntity;
    var Ky = ViewBag.Ky as ModDT_KyEntity;

    if (KyDaiLy == null)
        KyDaiLy = new ModDT_Ky_DaiLyEntity();

    if (Ky == null)
        Ky = new ModDT_KyEntity();

    var listItem_LienQuan_Current = model.lstModProduct_InfoEntity;
    if(listItem_LienQuan_Current==null)
        listItem_LienQuan_Current = new List<ModProduct_InfoEntity>(); //model.listItem_LienQuan_Current;
        
    var GetListManufacture = new List<ModProduct_ManufacturerEntity>();
%>
<script src="/{CPPath}/Content/add/js/ProductionInfo.js" type="text/javascript"></script>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="DaChotKy" name="DaChotKy" value="<%=model.DaChotKy %>" />
<script type="text/javascript">

    var VSWController = 'ModDT_Ky_DaiLy_DonHang';

    var VSWArrVar = [
                        'RecordID', 'RecordID',
                        'ModDtKyId', 'ModDtKyId',
                        'ModDTKyDaiLyId', 'ModDTKyDaiLyId'
                   ];

    // Link Sản phẩm liên quan
    var urlSoLuong_DonGia_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=formDkKyDaiLyDonHangDelete&GetList=TRUE")%>';
    function ReloadData_SanPhamLienQuan(ListString) {
        $("tbody[class='tbl-data-donhang-sanpham']").html(ListString);

        registerJs();
    }

    function ReLoadDataInParent(ListString) {
        $("tbody[class='tbl-data-donhang-sanpham']").html(ListString);

        registerJs();
    }

    // Link cập nhật số lượng, đơn giá
    var urlSoLuong_DonGia_Save = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=donHang_SanPham_Save&GetList=TRUE")%>';


    $(document).ready(function () {
        var DaChotKy = '<%=model.DaChotKy %>';

        registerJs();

        // Đã chốt kỳ
        if (DaChotKy == 1) {
            // Xóa hết các nút chức năng đi
            $("#toolbar").find("li:not(#toolbar-cancel)").remove();
        }
        else {
            // Xóa hết các nút chức năng đi
            $("#toolbar").find("li:not(#toolbar-cancel, #toolbar-apply)").remove();
        }
    });

    function registerJs() {
        $("input[type='text'][value_old]").change(function () {
            var parentTr = $(this).parents("tr:first");
            var txtSoLuong = parseInt($(parentTr).find("input[type='text'][id*='txtSoLuong']").val().replace(/\./g, "").replace(/\,/g, ""));
            var txtDonGia = parseInt($(parentTr).find("input[type='text'][id*='txtDonGia']").val().replace(/\./g, "").replace(/\,/g, ""));

            $(parentTr).find("label[id*='lblTongTien']").html(((txtSoLuong * txtDonGia).formatCurrency()).toString().replace(/\$/g, ""));
        });
    }

    function KyDaiLyDonHangSanPhamEdit(button) {
        $(button).addClass("hide");
        var parentTr = $(button).parents("tr:first");
        parentTr.find("input[type='text']").removeAttr("disabled");
        parentTr.find("a[save]").removeClass("hide");
        parentTr.find("a[cancel]").removeClass("hide");
    }

    function KyDaiLyDonHangSanPhamCancel(button) {
        $(button).addClass("hide");
        var parentTr = $(button).parents("tr:first");
        parentTr.find("input[type='text']").attr("disabled", "disabled");
        parentTr.find("a[edit]").removeClass("hide");
        parentTr.find("a[save]").addClass("hide");

        // Reset lại value cũ
        var inputControls = parentTr.find("[Value_Old]");
        if (inputControls != null) {
            for (var i = 0; i < inputControls.length; i++) {
                $(inputControls[i]).val($(inputControls[i]).attr("Value_Old"));
                $(inputControls[i]).html($(inputControls[i]).attr("Value_Old"));
            }
        }
    }

    function SoLuong_DonGiaSaveResult() {
        //$(".tbl-data-donhang-sanpham").html(data);data
        VSWRedirect('AddDonHang'); return false;
    }

    // Cập nhật lại thông tin đơn hàng
    function refreshDataDonHang(TongSanPham, TongTien, TongTienSauGiam) {
        $("#TongSanPham").val(TongSanPham);
        $("#TongTien").val(TongTien);
        $("#TongSauGiam").val(TongTienSauGiam);
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
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Quản lý nhập hàng :
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
                        <select id="ModDtKyId" name="ModDtKyId" onchange="VSWRedirect('Add');return false;" class="DropDownList" disabled="disabled">
                        <%= Utils.ShowDDLList(ModDT_KyService.Instance, "", "[ID] DESC", model.ModDtKyId)%>
                    </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Đại lý :</label>
                    </td>
                    <td>
                        <select id="ModDTKyDaiLyId" name="ModDTKyDaiLyId" onchange="VSWRedirect('Add');return false;" class="DropDownList" <%if(model.RecordID>0){ %> disabled="disabled" <%} %>>
                        <option value="0">--- Chọn đại lý ---</option>
                        <%=VSW.Lib.Global.Utils.ShowDDLDynamic(ModDT_Ky_DaiLyService.Instance, model.ModDtKyId <= 0 ? "" : ("ModDtKyId=" + model.ModDtKyId),
                                                                "Name", model.ModDTKyDaiLyId, "ModProductAgentId", "ModProductAgentParentId", "Name", "ID")%>
                    </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="KyDaiLyCode" id="KyDaiLyCode" value="<%=KyDaiLy.Code %>"
                            maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên đại lý :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="KyDaiLyName" id="KyDaiLyName" value="<%=KyDaiLy.Name %>"
                            maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Chọn đơn hàng :</label>
                    </td>
                    <td>
                        <select name="RecordID" id="RecordID" class="DropDownList" onchange="VSWRedirect('AddDonHang');return false;">
                             <%if (model.DaChotKy == 1)
                               { %> 
                                <option value="0">--- Chọn đơn hàng ---</option>
                             <%}
                               else
                               {%>
                               <option value="0">--- Tạo đơn hàng mới ---</option>
                             <%} %>
                            <%=VSW.Lib.Global.Utils.ShowDDLList(ModDT_Ky_DaiLy_DonHangService.Instance, "ModDTKyDaiLyId=" + model.ModDTKyDaiLyId, "CreateDate", model.RecordID)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã đơn hàng :</label>
                    </td>
                    <td>
                        <%if (model.RecordID > 0)
                          {%>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" readonly="readonly" />
                        <%}
                          else
                          {%>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255"  <%if(model.DaChotKy==1){ %> readonly="readonly" <%} %>/><%} %>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên đơn hàng :</label>
                    </td>
                    <td>
                        <%if (model.DaChotKy == 1)
                          {%>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" readonly="readonly" />
                        <%}
                          else
                          {%>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255"/>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="NgayTao" id="NgayTao" value="<%=item.NgayTao %>"
                            maxlength="255"  <%if(model.DaChotKy==1){ %> readonly="readonly" <%} %>/>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Chiết khấu :</label>
                    </td>
                    <td>
                        <input class="text_input" price="TRUE" type="text" name="ChietKhau" id="ChietKhau" value="<%=string.Format("{0:#,##0}", item.ChietKhau) %>"
                            maxlength="255" <%if(model.DaChotKy==1){ %> readonly="readonly" <%} %>/>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tổng sản phẩm :</label>
                    </td>
                    <td>
                        <input class="text_input" price="TRUE" type="text" name="TongSanPham" id="TongSanPham" value="<%=item.TongSanPham %>"
                            maxlength="255" readonly="readonly"/>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tổng tiền :</label>
                    </td>
                    <td>
                        <input class="text_input" price="TRUE" type="text" name="TongTien" id="TongTien" value="<%=string.Format("{0:#,##0}", item.TongTien) %>"
                            maxlength="255" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tổng tiền sau giảm :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="TongSauGiam" id="TongSauGiam" value="<%=string.Format("{0:#,##0}", item.TongSauGiam) %>"
                            maxlength="255" readonly="readonly" />
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
                <%if (false)
                  {%>
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
                <%} %>

                <%if(model.RecordID>0) {%>
                <tr>
                    <td colspan="2">
                        <div id="dvtabProductRelative">
                            <div class="col width-100">
                                <table class="admintable">
                                    <tr>
                                        <td colspan="2" style="width: 100%; color: White; font-weight: bold; background-color: #336699;
                                            height: 32px; padding-left: 5px;" align="left">
                                            DANH SÁCH CÁC SẢN PHẨM
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="text-align: right; margin-top: 5px; margin-bottom: 5px;">
                                               <input id="btnThemSanPhamBanKem" onclick="tb_show('', '/CP/FormDkKyDaiLyDonHang/FormDkKyDaiLyDonHang.aspx/RecordID/<%=model.RecordID %>?TB_iframe=true;height=500;width=850;', ''); return false;"
                                                    value="Thêm sản phẩm" class="text_input button-function button-background-image-add"
                                                    type="button" 
                                                    <%if(model.DaChotKy==1){ %> style="display: none;" <%} else{%>style="width: 150px" <%} %>/>
                                            </div>
                                            <table class="adminlist" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <th width="1%">
                                                            STT
                                                        </th>
                                                        <th width="1%" nowrap="nowrap">
                                                            ID
                                                        </th>
                                                        <th width="20px">
                                                            Trạng thái
                                                        </th>
                                                        <th style="width: 40px" nowrap="nowrap">
                                                            Ảnh
                                                        </th>
                                                        <th width="10%" nowrap="nowrap">
                                                            Mã sản phẩm
                                                        </th>
                                                        <th class="title">
                                                            Tên sản phẩm
                                                        </th>
                                                        <th width="80px" nowrap="nowrap">
                                                            Số lượng
                                                        </th>
                                                        <th width="130px" nowrap="nowrap">
                                                            Đơn giá
                                                        </th>
                                                        <th width="130px" nowrap="nowrap">
                                                            Tổng tiền
                                                        </th>
                                                        <th width="20%" nowrap="nowrap" style="display:none;">
                                                            Nhà sản xuất
                                                        </th>
                                                        <th width="1%" nowrap="nowrap" style="display:none;">
                                                            Ngày tạo
                                                        </th>
                                                        <th width="1%" nowrap="nowrap" <%if(model.DaChotKy==1){ %> style="display:none;" <%} %>>
                                                            Sửa
                                                        </th>
                                                        <th width="1%" nowrap="nowrap" <%if(model.DaChotKy==1){ %> style="display:none;" <%} %>>
                                                            Xóa
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="tbl-data-donhang-sanpham">
                                                    <%=ViewBag.DanhSachSanPhanTrongDon %>
                                                </tbody>
                                            </table>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                    </td>
                </tr>
                <%} %>
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
