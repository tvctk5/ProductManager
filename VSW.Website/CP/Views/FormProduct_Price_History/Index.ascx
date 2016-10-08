<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_Price_HistoryModel;
    var listItem = ViewBag.Data as List<ModProduct_Price_HistoryEntity>;
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
                Lịch sử thay đổi giá</h2>
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

    var VSWController = 'FormProduct_Price_History';

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
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Loại thay đổi", "Type")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giá trước (VNĐ)", "BeforePrice")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Giá sau (VNĐ)", "AfterPrice")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Người thay đổi", "UserId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày thay đổi", "CreateDate")%>
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
                    <td align="center">
                        <%if (listItem[i].Type)
                          { %>
                        <span style="color: Blue; font-weight: bold">Tăng giá </span>
                        <% }
                          else
                          {  %>
                        <span style="color: Red; font-weight: bold">Giảm giá </span>
                        <%} %>
                    </td>
                    <td align="right">
                        <%= string.Format("{0:#,##0}", listItem[i].BeforePrice)%>
                    </td>
                    <td align="right">
                        <%= string.Format("{0:#,##0}", listItem[i].AfterPrice)%>
                    </td>
                    <td align="left">
                        <%
                      if (listItemUser != null && listItemUser.Count > 0)
                      {
                          var UserCurrent = listItemUser.Where(o => o.ID == listItem[i].UserId).SingleOrDefault();
                          if (UserCurrent != null)
                          {%>
                        <%=(UserCurrent.Name + " (" + UserCurrent.LoginName + ")")%>
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
