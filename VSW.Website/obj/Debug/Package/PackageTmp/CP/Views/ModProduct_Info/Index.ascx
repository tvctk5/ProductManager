<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_InfoModel;
    var listItem = ViewBag.Data as List<ModProduct_InfoEntity>;
    var GetListManufacture = model.GetListManufacture;
%>
<form id="vswForm" name="vswForm" method="post">
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
                Sản phẩm: Danh sách</h2>
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

    var VSWController = 'ModProduct_Info';

    var VSWArrVar = [
                        'filter_menu', 'MenuID',
                        'filter_lang', 'LangID',
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
                    '1', 'LangID',
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
                    Chuyên mục :
                    <select id="filter_menu" onchange="VSWRedirect()" class="inputbox" size="1">
                        <option value="0">(Tất cả)</option>
                        <%= Utils.ShowDDLMenuByType("Product", model.LangID, model.MenuID)%>
                    </select>
                    Ngôn ngữ :<%= ShowDDLLang(model.LangID)%>
                </td>
            </tr>
        </table>
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        STT
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="20px">
                        <%= GetSortLink("Trạng thái", "Activity")%>
                    </th>
                    <th style="width: 40px" nowrap="nowrap">
                        <%= GetSortLink("Ảnh", "File")%>
                    </th>
                    <th width="5%" nowrap="nowrap">
                        <%= GetSortLink("Mã sản phẩm", "Code")%>
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên sản phẩm", "Name")%>
                    </th>
                    <th width="20px">
                        Thuộc tính
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Số lượng", "CountNumber")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giá", "Price")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giảm giá", "Sale")%>
                    </th>
                    <th width="20px">
                        <%= GetSortLink("Số lượt xem", "Preview")%>
                    </th>
                    <th width="20px">
                        <%= GetSortLink("Sản phẩm mới", "New")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Nhà sản xuất", "ManufacturerId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày tạo", "CreateDate")%>
                    </th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <td colspan="16">
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
                    <td align="center">
                        <%= GetPublish_ActivateBasic(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td align="center">
                        <%= Utils.GetMedia(listItem[i].File, 60, 60)%>
                    </td>
                    <td align="left">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                        <%= listItem[i].Code%></a>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Name%></a>
                    </td>
                    <td align="center">
                        <a href="javascript:RedirectController('ModProduct_Info_Details','Add', <%= listItem[i].ID %>)">
                            <span class="jgrid"><span class="state default"></span></span></a>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <%= string.Format("{0:#,##0}", listItem[i].CountNumber)%>
                    </td>
                    <td align="right">
                        <%= string.Format("{0:#,##0}", listItem[i].Price)%>
                    </td>
                    <td align="center">
                        <a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[salegx][<%= listItem[i].ID %>,<%= !listItem[i].SaleOff %>]')">
                            <span class="jgrid"><span class="state <%= listItem[i].SaleOff ? "publish" : "unpublish" %>">
                            </span></span></a>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:#,##0}", listItem[i].Preview)%>
                    </td>
                    <td align="center">
                        <a class="jgrid" href="javascript:void(0);" onclick="vsw_exec_cmd('[newgx][<%= listItem[i].ID %>,<%= !listItem[i].Status %>]')">
                            <span class="jgrid"><span class="state <%= listItem[i].Status ? "publish" : "unpublish" %>">
                            </span></span></a>
                    </td>
                    <td align="left">
                        <%if (listItem[i].ManufacturerId == null || listItem[i].ManufacturerId == 0)
                          {%>
                        Không xác định
                        <%}
                          else
                          { %>
                        <% if (GetListManufacture != null && GetListManufacture.Count > 0)
                           {
                               ModProduct_ManufacturerEntity objManufacturerEntity = GetListManufacture.Where(o => o.ID == listItem[i].ManufacturerId).SingleOrDefault();
                               if (objManufacturerEntity == null)
                               { 
                        %>
                        Không xác định
                        <%}
                               else
                               { %>
                        <a href="javascript:RedirectController('ModProduct_Manufacturer','Add', <%= listItem[i].ManufacturerId %>)">
                            <%= objManufacturerEntity.Name%>
                        </a>
                        <%}
                           }
                          }%>
                    </td>
                    <%--                    <td align="center">
                       <%= GetName(listItem[i].getMenu()) %>
                    </td>--%>
                    <td align="center">
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
