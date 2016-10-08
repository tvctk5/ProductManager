<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_PriceSale_HistoryModel;
    var listItem = ViewBag.Data as List<ModProduct_PriceSale_HistoryEntity>;
    var listItemUser = CPUserService.Instance.CreateQuery().ToList();
%>
<script language="javascript" type="text/javascript">
    function ReturnParent(key) {
        self.parent.tb_remove();
    }
</script>
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
            <%=  GetListCommandThickboxValidation("closepopup|Đóng")%>
        </div>
        <div class="pagetitle icon-16-List" style="padding-left: 20px !important;">
            <h2 style="line-height: normal !important;">
                Lịch sử khuyến mãi</h2>
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

    var VSWController = 'ModProduct_PriceSale_History';

    var VSWArrVar = [
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
                </td>
                <td nowrap="nowrap">
                </td>
            </tr>
        </table>
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        STT
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="15%" nowrap="nowrap">
                        <%= GetSortLink("Người thực hiện", "UserId")%>
                    </th>
                    <th width="15%" nowrap="nowrap">
                        <%= GetSortLink("Giá gốc (VNĐ)", "Price")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giá trị", "SaleOffValue")%>
                    </th>
                    <th width="50px">
                        <%= GetSortLink("Kiểu giảm", "SaleOffType")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Giá đã khuyến mại<br/> (VNĐ)", "PriceSale")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày bắt đầu", "StartDate")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày kết thúc", "FinishDate")%>
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
                    <td align="center">
                        <%= listItem[i].ID%>
                    </td>
                    <td align="left">
                        <%
                      if (listItemUser != null && listItemUser.Count > 0)
                      {
                          var UserCurrent = listItemUser.Where(o => o.ID == listItem[i].UserId).SingleOrDefault();
                          if (UserCurrent != null)
                          {%>
                        <%= (UserCurrent.Name + " (" + UserCurrent.LoginName + ")" )%>
                        <%}
                         else
                         {%>
                        Không xác định
                        <%}
                     }
                     else
                     {%>
                        Không xác định
                        <%} %>
                    </td>
                    <td align="right">
                       <span style="font-weight: bold; color: Red"><%= string.Format("{0:#,##0}", listItem[i].Price)%></span> 
                    </td>
                    <td align="center">
                        <%= string.Format("{0:#,##0}", listItem[i].SaleOffValue)%>
                    </td>
                    <td align="center">
                        <%= listItem[i].SaleOffType?"%":"VNĐ" %>
                    </td>
                    <td align="right">
                        <span style="font-weight:bold;color: Blue"> <%= string.Format("{0:#,##0}", listItem[i].PriceSale)%></span>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].StartDate) %>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].FinishDate) %>
                    </td>
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
