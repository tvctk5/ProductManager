<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<% 
    var model = ViewBag.Model as SysUserModel;
    var listItem = ViewBag.Data as List<CPUserEntity>;
 %>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetListCommand("new|Thêm,edit|Sửa,space,publish|Duyệt,unpublish|Bỏ duyệt,space,delete|Xóa")%>
        </div>
        <div class="pagetitle icon-48-user">
            <h2>Người sử dụng</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'SysUser';

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
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
                        
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        #
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên đăng nhập", "LoginName")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Họ và tên", "Name")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Địa chỉ", "Address")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Điện thoại", "Phone")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Email", "Email")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <a href="#">Thuộc nhóm</a>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Duyệt", "Activity")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
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
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)"><%= listItem[i].LoginName%></a>
                    </td>
                    <td align="center">
                       <%= listItem[i].Name %>
                    </td>
                    <td align="center">
                       <%= listItem[i].Address %>
                    </td>
                    <td align="center">
                       <%= listItem[i].Phone %>
                    </td>
                    <td align="center">
                       <%= listItem[i].Email%>
                    </td>
                    <td align="center">
                       <%for (int j = 0; listItem[i].GetRole() != null && j < listItem[i].GetRole().Count; j++)
                         { %>
                           <%=listItem[i].GetRole()[j].Name%>
                           <%if(j>0){ %><br /><%} %>
                       <%} %>
                    </td>
                    <td align="center">
                       <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].ID%>
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