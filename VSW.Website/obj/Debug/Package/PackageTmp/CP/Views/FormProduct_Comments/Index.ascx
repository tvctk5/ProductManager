<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_CommentsModel;
    var listItem = ViewBag.Data as List<ModProduct_CommentsEntity>;
    var listItemCustomers = ModProduct_CustomersService.Instance.CreateQuery().ToList();
    var listItemUsers = CPUserService.Instance.CreateQuery().ToList();
%>
<form id="vswForm" name="vswForm" method="post">
<script language="javascript" type="text/javascript">
    function ReturnParent(Key) {
        self.parent.tb_remove();
    }
</script>
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
        <div class="toolbar-list" style="margin-right: 0px !important; text-align: right !important;"
            id="toolbar">
            <table>
                <tr>
                    <td nowrap="nowrap">
                        <%=GetListCommand("edit|Sửa,space,publish|Duyệt,unpublish|Bỏ duyệt,space,delete|Xóa")%>
                    </td>
                    <td nowrap="nowrap">
                        <%=GetListCommandThickboxValidation("closepopup|Đóng")%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="pagetitle icon-16-List" style="padding-left: 20px !important;">
            <h2 style="line-height: normal !important;">
                Các bình luận
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

    var VSWController = 'FormProduct_Comments';

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
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="1%">
                        <%= GetSortLink("Trạng thái", "Activity")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Tài khoản
                    </th>
                    <th width="15%" nowrap="nowrap">
                        Họ và Tên
                    </th>
                    <th>
                        Nội dung
                    </th>
                    <th width="15%">
                        Người duyệt/ thay đổi
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày tạo", "CreateDate")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Ngày sửa cuối", "ModifiedDate")%>
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
                <%
                    ModProduct_CustomersEntity objCustomersCurrent = null;
                    CPUserEntity objCPUserEntity = null;
                    for (int i = 0; listItem != null && i < listItem.Count; i++)
                    { %>
                <tr class="row<%= i%2 %>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="center">
                        <a href="javascript:RedirectControllerParameter('FormProduct_Comments','Add.aspx','RecordID/<%= listItem[i].ID %>/ProductInfoId/<%= listItem[i].ProductInfoId %>')">
                            <%= listItem[i].ID%></a>
                    </td>
                    <td align="center">
                        <%= GetPublish_ActivateBasic(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <% if (listItem[i].CustomersId > 0 && listItemCustomers.Count > 0)
                       {
                           objCustomersCurrent = listItemCustomers.Where(o => o.ID == listItem[i].CustomersId).SingleOrDefault(); if (objCustomersCurrent != null)
                           {  %>
                    <td align="center">
                        <%= objCustomersCurrent.UserName%>
                    </td>
                    <td align="center">
                        <%= objCustomersCurrent.FullName%>
                    </td>
                    <%}
                       }
                       else
                       {  %>
                    <td align="center">
                        Chưa xác định...
                    </td>
                    <td align="center">
                        Không xác định...
                    </td>
                    <%} %>
                    <td>
                        <a href="javascript:RedirectControllerParameter('FormProduct_Comments','Add.aspx','RecordID/<%= listItem[i].ID %>/ProductInfoId/<%= listItem[i].ProductInfoId %>')">
                            <%= listItem[i].Content%></a>
                    </td>
                    <% if (listItem[i].UserId > 0 && listItemUsers.Count > 0)
                       {
                           objCPUserEntity = listItemUsers.Where(o => o.ID == listItem[i].UserId).SingleOrDefault(); if (objCPUserEntity != null)
                           {  %>
                    <td align="center">
                        <%= objCPUserEntity.Name%>
                        (<%= objCPUserEntity.LoginName%>)
                    </td>
                    <%}
                           else
                           { %>
                    <td align="center">
                        Không xác định...
                    </td>
                    <%}
                       }
                       else
                       {  %>
                    <td align="center">
                        Không xác định...
                    </td>
                    <%} %>
                    <td align="center">
                        <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].CreateDate) %>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].ModifiedDate) %>
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
