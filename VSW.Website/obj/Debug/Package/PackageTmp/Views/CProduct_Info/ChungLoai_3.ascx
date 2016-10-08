<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var listProductGroups = ModProduct_GroupsService.Instance.CreateQuery()
        .Where(o => o.Activity == true)
        .OrderByAsc(o => o.ID).ToList();// Danh sách các chủng loại đang được sử dụng

    //var listItem = ViewBag.Data as List<ModProduct_InfoEntity>;
    var listItemSale = ModProduct_InfoService.Instance.CreateQuery().Where(o => o.SaleOff == true).ToList();// Danh sách tất cả các sản phẩm khuyến mãi
    var listItemSaleByGroup = new List<ModProduct_InfoEntity>(); // Khởi tạo danh sách SP khuyến mãi trong một nhóm nào đó

    int iCount = 0;// Đếm lần lượt phần tử trong nhóm, lấy đối tượng đầu tiên
    int iCountItem = 0;// Đếm lần lượt số lượng phần tử trong số SP thuộc nhóm.
    int iItemInPage = 8;// Số lượng SP thuộc một trang (Mặc định là 8)
%>
<div class="box-div-parent">
    <table class="box-table-parent" bgcolor="#EEEEEE" width="100%" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td class="container3_border_le" valign="top">
                <img src="/Content/html/images/Box/container3_top_left.png" />
            </td>
            <td class="box-table-parent-td">
                <table cellpadding="0" cellspacing="0" class="box-table-parent-td-table-title-1">
                    <tr>
                        <td style="width: 5px; padding-left: 10px" valign="top">
                            <img src="/Content/html/images/Box/container3_left.png" />
                        </td>
                        <td class="container3_body">
                            <table border="0" cellpadding="0" cellspacing="0" class="box-table-parent-td-table-title-2">
                                <tr>
                                    <td height="31" width="100%" class="cssOhidoContainerTitle2">
                                        <nobr><span class="MenuLeft_Title"><%=ViewBag.Title %></span></nobr>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 5px;" valign="top">
                            <img src="/Content/html/images/Box/container3_right.png" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="container3_border_ri" valign="top">
                <img src="/Content/html/images/Box/container3_top_right.png" />
            </td>
        </tr>
        <tr>
            <td class="container3_border_le">
                <img width="0" height="0" />
            </td>
            <td width="100%" class="box-table-parent-td-content">
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="cssInfoBox">
                            <!--Thêm dữ liệu mới ở đây-->
                           Danh sách các sản phẩm thuộc chủng loại 3
                        </td>
                    </tr>
                </table>
            </td>
            <td class="container3_border_ri">
                <img width="0" height="0" />
            </td>
        </tr>
        <tr>
            <td valign="bottom" style="width: 5px">
                <img src="/Content/html/images/Box/container3_bottom_le.png" />
            </td>
            <td class="container3_border_bo">
                <img width="0" height="0" />
            </td>
            <td valign="bottom" style="width: 5px">
                <img src="/Content/html/images/Box/container3_bottom_ri.png" />
            </td>
        </tr>
    </table>
</div>