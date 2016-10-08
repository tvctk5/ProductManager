<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_InfoModel;
    var listProduct_BanKem = ViewBag.Data as List<ModProduct_InfoEntity>;
    var ProductCurrent = model.ProductCurrent;
    var GetListManufacture = model.GetListManufacture;
     
%>
<script src="/{CPPath}/Content/add/js/ProductionInfo.js" type="text/javascript"></script>
<form id="vswForm" name="vswForm" method="post">
<script language="javascript" type="text/javascript">
    // Link check Size sản phẩm
    var urlProduct_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=attachAdd&GetList=TRUE")%>';
    var urlProduct_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=attachDelete&GetList=TRUE")%>';

    function ReturnParent(Key) {
        self.parent.tb_remove();
    }

    function ReLoadDataInParent(ListString) {
        self.parent.ReloadData_SanPhamBanKem(ListString);
    }

    //#region SearchProduct_BanKem
    function SearchProduct_BanKem() {
        // Có tìm kiếm cả danh mục con hay không
        var checked = $("#chkModProductInfoSearch_SeachChildren").is(":checked");
        if (checked)
            $("#ModProductInfoSearch_SeachChildren").val("1");
        else
            $("#ModProductInfoSearch_SeachChildren").val("0");

        vsw_exec_cmd("AddAttachProduct");
        return false;
    }
    //#endregion SearchProduct_BanKem
     
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
                Tìm kiếm các sản phẩm bán kèm
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

    var VSWController = 'FormProduct_Attach';

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
        <table width="100%" id="SearchBanKem">
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
                    <select name="ModSearchManufacturerId_BanKem" id="ModSearchManufacturerId_BanKem"
                        class="DropDownList">
                        <option selected="selected" value="0">-------------- Tất cả nhà sản xuất --------------</option>
                        <%= Utils.ShowDDLManufacturer(model.ModSearchManufacturerId_BanKem.ToString())%>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="key" nowrap="nowrap">
                    <label>
                        Mã sản phẩm :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ModProductInfoSearch_Code_BanKem" id="ModProductInfoSearch_Code_BanKem"
                        value="<%=model.ModProductInfoSearch_Code_BanKem %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td class="key" nowrap="nowrap">
                    <label>
                        Tên sản phẩm :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="ModProductInfoSearch_Name_BanKem" id="ModProductInfoSearch_Name_BanKem"
                        value="<%=model.ModProductInfoSearch_Name_BanKem %>" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input id="btnSearchProduct_BanKem" onclick="SearchProduct_BanKem();" type="button"
                        value="Tìm kiếm" class="text_input" style="width: 150px; background-color: #003366;
                        color: #FFFFFF; font-weight: bold;" />
                </td>
            </tr>
            <% 
                if (listProduct_BanKem != null && listProduct_BanKem.Count > 0)
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
                        <th width="1%" nowrap="nowrap">
                            Số lượng
                        </th>
                        <th nowrap="nowrap">
                            Giá
                        </th>
                        <th width="10%" nowrap="nowrap">
                            Nhà sản xuất
                        </th>
                        <th width="1%" nowrap="nowrap">
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
                                <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord, "AddAttachProduct", "OnChangePageSize_BanKem(this,'AddAttachProduct');", "OnChangePageIndex_BanKem", true)%>
                            </del>
                        </td>
                    </tr>
                </tfoot>
                <tbody style="width: 100% !important;">
                    <%for (int i = 0; listProduct_BanKem != null && i < listProduct_BanKem.Count; i++)
                      { %>
                    <tr class="row<%= i%2 %>">
                        <td align="center">
                            <%= i + 1%>
                        </td>
                        <td align="center">
                            <%= listProduct_BanKem[i].ID%>
                        </td>
                        <td class='text-right' align='center' nowrap='nowrap'>
                            <%  if (listProduct_BanKem[i].Activity)
                                {%>
                            <span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>
                            <%}
                                else
                                {  %>
                            <span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>
                            <%}%>
                        </td>
                        <td align="center">
                            <%= Utils.GetMedia(listProduct_BanKem[i].File, 60, 60)%>
                        </td>
                        <td align="left">
                            <%= listProduct_BanKem[i].Code%>
                        </td>
                        <td>
                            <%= listProduct_BanKem[i].Name%>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <%= string.Format("{0:#,##0}", listProduct_BanKem[i].CountNumber)%>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <%= string.Format("{0:#,##0}", listProduct_BanKem[i].Price)%>
                        </td>
                        <td align="left">
                            <%if (listProduct_BanKem[i].ManufacturerId == null || listProduct_BanKem[i].ManufacturerId == 0)
                              {%>
                            Không xác định
                            <%}
                              else
                              { %>
                            <% if (GetListManufacture != null && GetListManufacture.Count > 0)
                               {
                                   ModProduct_ManufacturerEntity objManufacturerEntity = GetListManufacture.Where(o => o.ID == listProduct_BanKem[i].ManufacturerId).SingleOrDefault();
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
                        <td align="center">
                            <%= string.Format("{0:dd/MM/yyyy HH:mm}", listProduct_BanKem[i].CreateDate)%>
                        </td>
                        <td align="center">
                            <a class="jgrid Add <%=FormProduct_AttachController.CheckExisted(ProductCurrent.ProductsAttach,listProduct_BanKem[i].ID)?"a-hide":"" %>"
                                title="Thêm sản phẩm bán kèm" href="javascript:void(0);" onclick="AttachAdd(urlProduct_Add,this,'<%=listProduct_BanKem[i].ID %>',true);return false;">
                                <span class="jgrid"><span class="state add"></span></span></a>
                        </td>
                        <td align="center">
                            <a class="jgrid Delete <%=FormProduct_RelativeController.CheckExisted(ProductCurrent.ProductsAttach,listProduct_BanKem[i].ID)?"":"a-hide" %>"
                                title="Xóa sản phẩm bán kèm" href="javascript:void(0);" onclick="AttachDelete(urlProduct_Delete,this,'<%=listProduct_BanKem[i].ID %>',true);return false;">
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
