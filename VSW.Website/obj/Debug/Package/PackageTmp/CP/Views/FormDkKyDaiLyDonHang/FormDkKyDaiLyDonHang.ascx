<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_InfoModel;
    var listProduct_LienQuan = ViewBag.Data as List<ModProduct_InfoEntity>;
    var ProductCurrent = model.ProductCurrent;
    var GetListManufacture = model.GetListManufacture;
    
    var urlAdd = ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=formDkKyDaiLyDonHangAdd&GetList=TRUE");
    var urlDelete = ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=formDkKyDaiLyDonHangDelete&GetList=TRUE");

     
%>
<script src="/{CPPath}/Content/add/js/ProductionInfo.js" type="text/javascript"></script>
<form id="vswForm" name="vswForm" method="post">
<script language="javascript" type="text/javascript">
    // Link check Size sản phẩm
    var RecordID = '<%= model.RecordID %>';
    var urlProduct_Add = '<%=urlAdd%>';
    var urlProduct_Delete = '<%=urlDelete%>';

    function ReturnParent(Key) {
        self.parent.tb_remove();
    }

    function ReLoadDataInParent(ListString) {
        self.parent.ReloadData_SanPhamLienQuan(ListString);
    }

    //#region SearchProduct_LienQuan
    function SearchProduct_LienQuan() {
        // Có tìm kiếm cả danh mục con hay không
        var checked = $("#chkModProductInfoSearch_SeachChildren").is(":checked");
        if (checked)
            $("#ModProductInfoSearch_SeachChildren").val("1");
        else
            $("#ModProductInfoSearch_SeachChildren").val("0");

        vsw_exec_cmd("AddRelativeProduct");
        return false;
    }
    //#endregion SearchProduct_LienQuan 
</script>
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<input type="hidden" id="ModProductInfoSearch_SeachChildren" name="ModProductInfoSearch_SeachChildren"
    <%=model.ModProductInfoSearch_SeachChildren?"value='1'":"value='0'" %> />
<input type="hidden" id="PageSize" name="PageSize" value="<%=model.PageSize %>" />
<input type="hidden" id="PageIndex" name="PageIndex" value="<%=model.PageIndex %>" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" style="margin-right: 0px !important; text-align: right !important;"
            id="toolbar">
            <%=GetListCommandThickboxValidation("closepopup|Đóng")%>
        </div>
        <div class="pagetitle icon-16-List" style="padding-left: 20px !important;">
            <h2 style="line-height: normal !important;">
                Tìm kiếm các sản phẩm liên quan
            </h2>
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

    var VSWController = 'FormDkKyDaiLyDonHang';

    var VSWArrVar = [
                        'limit', 'PageSize'
                   ];


    var VSWArrQT = [
                      '<%= model.PageIndex + 1 %>', 'PageIndex',
                      '<%= model.Sort %>', 'Sort',
                      '<%=model.RecordID %>', 'RecordID'
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
        <input type="hidden" id="RecordID" name="RecordID" value="<%=model.RecordID %>" />
        <table width="100%" id="SearchLienQuan">
            <tr>
                <td class="key" style="width: 0% !important;" nowrap="nowrap">
                    <label>
                        Chủng loại :</label>
                </td>
                <td style="width: 100% !important;">
                    <select name="ModProductInfoSearch_ProductGroupsId" id="ModProductInfoSearch_ProductGroupsId"
                        class="DropDownList" style="width: 70% !important;">
                        <option selected="selected" value="">-------------- Tất cả chủng loại --------------</option>
                        <%= Utils.ShowDDLMenuByType("Product", model.LangID, model.ModProductInfoSearch_ProductGroupsId)%>
                    </select>
                    <input type="checkbox" name="chkModProductInfoSearch_SeachChildren" id="chkModProductInfoSearch_SeachChildren"
                        <%= model.ModProductInfoSearch_SeachChildren ? "checked='checked' value='1'" : "value='0'" %> />&nbsp;Tìm
                    cả những chủng loại con
                </td>
            </tr>
            <tr>
                <td class="key" nowrap="nowrap">
                    <label>
                        Nhà sản xuất :</label>
                </td>
                <td>
                    <select name="ModSearchManufacturerId_LienQuan" id="ModSearchManufacturerId_LienQuan"
                        class="DropDownList">
                        <option selected="selected" value="0">-------------- Tất cả nhà sản xuất --------------</option>
                        <%= Utils.ShowDDLManufacturer(model.ModSearchManufacturerId_LienQuan.ToString())%>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="key" nowrap="nowrap">
                    <label>
                        Mã sản phẩm :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ModProductInfoSearch_Code_LienQuan" id="ModProductInfoSearch_Code_LienQuan"
                        value="<%=model.ModProductInfoSearch_Code_LienQuan %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key" nowrap="nowrap">
                    <label>
                        Tên sản phẩm :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ModProductInfoSearch_Name_LienQuan" id="ModProductInfoSearch_Name_LienQuan"
                        value="<%=model.ModProductInfoSearch_Name_LienQuan %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input id="btnSearchProduct_LienQuan" onclick="SearchProduct_LienQuan();" type="button"
                        value="Tìm kiếm" class="text_input" style="width: 150px; background-color: #003366;
                        color: #FFFFFF; font-weight: bold;" />
                </td>
            </tr>
            <% 
                if (listProduct_LienQuan != null && listProduct_LienQuan.Count > 0)
                {
            %>
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
                        <th width="1%">
                            Số lượng
                        </th>
                        <th nowrap="nowrap">
                           Đơn Giá
                        </th>
                        <th width="10%" nowrap="nowrap" style="display:none">
                            Nhà sản xuất
                        </th>
                        <th width="1%" nowrap="nowrap" style="display:none">
                            Ngày tạo
                        </th>
                        <th width="1%" nowrap="nowrap">
                            Chọn
                        </th>
                        <th width="1%">
                            Hủy chọn
                        </th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <td colspan="16">
                            <del class="container">
                                <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord, "AddRelativeProduct", "OnChangePageSize_LienQuan(this,'AddRelativeProduct');", "OnChangePageIndex_LienQuan",true)%>
                            </del>
                        </td>
                    </tr>
                </tfoot>
                <tbody style="width: 100% !important;">
                    <%for (int i = 0; listProduct_LienQuan != null && i < listProduct_LienQuan.Count; i++)
                      { %>
                    <tr class="row<%= i%2 %>">
                        <td align="center">
                            <%= i + 1%>
                        </td>
                        <td align="center">
                            <%= listProduct_LienQuan[i].ID%>
                        </td>
                        <td class='text-right' align='center' nowrap='nowrap'>
                            <%  if (listProduct_LienQuan[i].Activity)
                                {%>
                            <span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>
                            <%}
                                else
                                {  %>
                            <span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>
                            <%}%>
                        </td>
                        <td align="center">
                            <%= Utils.GetMedia(listProduct_LienQuan[i].File, 60, 60)%>
                        </td>
                        <td align="left">
                            <%= listProduct_LienQuan[i].Code%>
                        </td>
                        <td>
                            <%= listProduct_LienQuan[i].Name%>
                        </td>
                        <td align="right" nowrap="nowrap">
                             <%--string.Format("{0:#,##0}", listProduct_LienQuan[i].CountNumber) --%>
                            <input type="text" id='txt<%=listProduct_LienQuan[i].ID %>_SoLuong' name='txt<%=listProduct_LienQuan[i].ID %>_SoLuong' value="1" style="text-align:right;width:30px" class="text_input" />
                        </td>
                        <td align="right" nowrap="nowrap">
                            <input type="text" id='txt<%=listProduct_LienQuan[i].ID %>_DonGia' name='txt<%=listProduct_LienQuan[i].ID %>_DonGia' value='<%= string.Format("{0:#,##0}", listProduct_LienQuan[i].Price)%>'  style="text-align:right;width:80px" class="text_input"/>
                        </td>
                        <td align="left" style="display:none">
                            <%if (listProduct_LienQuan[i].ManufacturerId == null || listProduct_LienQuan[i].ManufacturerId == 0)
                              {%>
                            Không xác định
                            <%}
                              else
                              { %>
                            <% if (GetListManufacture != null && GetListManufacture.Count > 0)
                               {
                                   ModProduct_ManufacturerEntity objManufacturerEntity = GetListManufacture.Where(o => o.ID == listProduct_LienQuan[i].ManufacturerId).SingleOrDefault();
                                   if (objManufacturerEntity == null)
                                   { 
                            %>
                            Không xác định
                            <%}
                                   else
                                   { %>
                            <%= objManufacturerEntity.Name%>
                            <%}
                               }
                              }%>
                        </td>
                        <td align="center" style="display:none">
                            <%= string.Format("{0:dd/MM/yyyy HH:mm}", listProduct_LienQuan[i].CreateDate)%>
                        </td>
                        <td align="center">
                            <a class="jgrid Add <%=FormDkKyDaiLyDonHangController.CheckExisted(ProductCurrent.ProductsConnection,listProduct_LienQuan[i].ID)?"a-hide":"" %>"
                                title="Thêm sản phẩm liên quan" href="javascript:void(0);" onclick="KyDaiLyDonHangSanPhamAdd(urlProduct_Add,this,'<%=listProduct_LienQuan[i].ID %>',true);return false;">
                                <span class="jgrid"><span class="state add"></span></span></a>
                        </td>
                        <td align="center">
                            <a class="jgrid Delete <%=FormDkKyDaiLyDonHangController.CheckExisted(ProductCurrent.ProductsConnection,listProduct_LienQuan[i].ID)?"":"a-hide" %>"
                                title="Xóa sản phẩm liên quan" href="javascript:void(0);" onclick="KyDaiLyDonHangSanPhamDelete(urlProduct_Delete,this,'<%=listProduct_LienQuan[i].ID %>',true);return false;">
                                <span class="jgrid"><span class="state delete"></span></span></a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
            <%}

                else
                { 
            %>
            <tr>
                <td>
                </td>
                <td style="color: Red; font-weight: bold;">
                    <br />
                    Không tìm thấy sản phẩm nào
                </td>
            </tr>
            <%} %>
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
