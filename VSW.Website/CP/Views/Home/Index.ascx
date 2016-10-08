<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%= ShowMessage()%>
<script type="text/javascript">
    window.addEvent('domready', function () { new Accordion($$('div#panel-sliders.pane-sliders > .panel > h3.pane-toggler'), $$('div#panel-sliders.pane-sliders > .panel > div.pane-slider'), { onActive: function (toggler, i) { toggler.addClass('pane-toggler-down'); toggler.removeClass('pane-toggler'); i.addClass('pane-down'); i.removeClass('pane-hide'); Cookie.write('jpanesliders_panel-sliders', $$('div#panel-sliders.pane-sliders > .panel > h3').indexOf(toggler)); }, onBackground: function (toggler, i) { toggler.addClass('pane-toggler'); toggler.removeClass('pane-toggler-down'); i.addClass('pane-hide'); i.removeClass('pane-down'); if ($$('div#panel-sliders.pane-sliders > .panel > h3').length == $$('div#panel-sliders.pane-sliders > .panel > h3.pane-toggler').length) Cookie.write('jpanesliders_panel-sliders', -1); }, duration: 300, opacity: false, alwaysHide: true }); });
</script>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="adminform">
            <div class="cpanel-left">
                <div id="cpanel">
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/ModNews/Add.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-article-add.png"
                                    alt="" />
                                <span>Thêm bài viết</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/ModNews/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-article.png"
                                    alt="" />
                                <span>Bài viết</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/SysMenu/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-category.png"
                                    alt="" />
                                <span>Chuyên mục</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/ModAdv/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-media.png"
                                    alt="" />
                                <span>Quảng cáo/Liên kết</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/ModFeedback/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-massmail.png"
                                    alt="" />
                                <span>Liên hệ</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/ModFile/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-section.png"
                                    alt="" />
                                <span>File tải lên</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/SysPage/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-menumgr.png"
                                    alt="" />
                                <span>Trang</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/SysTemplate/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-themes.png"
                                    alt="" />
                                <span>Mẫu giao diện</span></a>
                        </div>
                    </div>
                    <%-- <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/SysModule/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-plugin.png" alt="" />
                                <span>Chức năng</span></a>
                        </div>
                    </div>--%>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/SysResource/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-language.png"
                                    alt="" />
                                <span>Tài nguyên</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/SysUser/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-user.png" alt="" />
                                <span>Người sử dụng</span></a>
                        </div>
                    </div>
                    <div class="icon-wrapper">
                        <div class="icon">
                            <a href="/{CPPath}/SysUserLog/Index.aspx">
                                <img src="/{CPPath}/Content/templates/bluestork/images/header/icon-48-user-profile.png"
                                    alt="" />
                                <span>Nhật ký đăng nhập</span></a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="cpanel-right">
                <style type="text/css">
                    .div-more-info
                    {
                        padding: 4px;
                        text-align: right;
                    }
                    .div-more-info a
                    {
                        font-weight: bold;
                        color: Red !important;
                    }
                </style>
                <div id="panel-sliders" class="pane-sliders">
                    <% var listUserLog = CPUserLogService.Instance.CreateQuery()
                            .Take(5)
                            .OrderByDesc(o => o.ID)
                            .ToList(); %>
                    <div class="panel">
                        <h3 class="pane-toggler title" id="cpanel-panel-logged">
                            <a href="javascript:void(0);"><span>5 đăng nhập gần nhất</span></a>
                        </h3>
                        <div class="pane-slider content">
                            <table class="adminlist">
                                <thead>
                                    <tr>
                                        <th>
                                            #
                                        </th>
                                        <th>
                                            <strong>Ghi chú</strong>
                                        </th>
                                        <th>
                                            <strong>IP</strong>
                                        </th>
                                        <th>
                                            <strong>Ngày</strong>
                                        </th>
                                        <th>
                                            <strong>ID</strong>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%for (int i = 0; listUserLog != null && i < listUserLog.Count; i++)
                                      { %>
                                    <tr>
                                        <td>
                                            <%= listUserLog[i].getUser() != null ? listUserLog[i].getUser().LoginName : string.Empty%>
                                        </td>
                                        <td class="center">
                                            <%= listUserLog[i].Note%>
                                        </td>
                                        <td class="center">
                                            <%= listUserLog[i].IP%>
                                        </td>
                                        <td class="center">
                                            <%= string.Format("{0:dd-MM-yyyy HH:mm}", listUserLog[i].Created)%>
                                        </td>
                                        <td class="center">
                                            <%= listUserLog[i].ID%>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <% var listNews = ModNewsService.Instance.CreateQuery()
                            .Take(5)
                            .OrderByDesc(o => o.ID)
                            .ToList(); %>
                    <div class="panel">
                        <h3 class="pane-toggler title" id="cpanel-panel-latest">
                            <a href="javascript:void(0);"><span>5 bài viết mới</span></a>
                        </h3>
                        <div class="pane-slider content">
                            <div class="div-more-info">
                                <a href="/{CPPath}/ModNews/Index.aspx"><span>Xem thêm...</span></a></div>
                            <table class="adminlist">
                                <thead>
                                    <tr>
                                        <th>
                                            Tiêu đề
                                        </th>
                                        <th>
                                            <strong>Duyệt</strong>
                                        </th>
                                        <th>
                                            <strong>Xuất bản</strong>
                                        </th>
                                        <th>
                                            <strong>Chuyên mục</strong>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%for (int i = 0; listNews != null && i < listNews.Count; i++)
                                      { %>
                                    <tr>
                                        <td>
                                            <a href="/{CPPath}/ModNews/Add.aspx/RecordID/<%= listNews[i].ID%>/LangID/<%= listNews[i].getMenu().LangID%>">
                                                <%= listNews[i].Name%></a>
                                        </td>
                                        <td class="center">
                                            <span class="jgrid"><span class="state <%= listNews[i].Activity ? "publish" : "unpublish" %>">
                                            </span></span>
                                        </td>
                                        <td class="center">
                                            <%= string.Format("{0:dd-MM-yyyy HH:mm}", listNews[i].Published)%>
                                        </td>
                                        <td class="center">
                                            <%= GetName(listNews[i].getMenu())%>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <% var listCart = ModProduct_OrderService.Instance.CreateQuery()
                            .Take(5)
                            .OrderByDesc(o => o.CreateDate)
                            .ToList();
                       List<VSW.Lib.Global.ListItem.Item> listOrderStatus = VSW.Lib.Global.ListItem.List.GetListByConfigkey("Mod.OrderStatus");
                    %>
                    <div class="panel">
                        <h3 class="pane-toggler title" id="H1">
                            <a href="javascript:void(0);"><span>5 đơn hàng mới</span></a>
                        </h3>
                        <div class="pane-slider content">
                            <div class="div-more-info">
                                <a href="/{CPPath}/ModProduct_Order/Index.aspx"><span>Xem thêm...</span></a></div>
                            <table class="adminlist">
                                <thead>
                                    <tr>
                                        <th>
                                            <strong>Ngày đặt</strong>
                                        </th>
                                        <th>
                                            Mã
                                        </th>
                                        <th>
                                            <strong>Người đặt hàng</strong>
                                        </th>
                                        <th>
                                            <strong>SL</strong>
                                        </th>
                                        <th>
                                            <strong>Tổng SL</strong>
                                        </th>
                                        <th>
                                            <strong>Tổng giá trị</strong>
                                        </th>
                                        <th>
                                            <strong>Trạng thái</strong>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%for (int i = 0; listCart != null && i < listCart.Count; i++)
                                      { 
                                    %>
                                    <tr class="row<%= i%2 %>">
                                        <td align="center">
                                            <%= string.Format("{0:dd/MM/yyyy HH:mm}", listCart[i].CreateDate)%>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            <a href="/{CPPath}/ModProduct_Order/Add.aspx/RecordID/<%= listCart[i].ID %>">
                                                <%= listCart[i].Code%></a>
                                        </td>
                                        <td align="left" style="width: 100%">
                                            <%= listCart[i].NguoiDat_FullName%>
                                        </td>
                                        <td align="center" nowrap="nowrap">
                                            <%= string.Format("{0:#,##0}", listCart[i].QuantityProduct)%>
                                        </td>
                                        <td align="center" nowrap="nowrap">
                                            <%= string.Format("{0:#,##0}", listCart[i].QuantityTotal)%>
                                        </td>
                                        <td align="right" nowrap="nowrap">
                                            <%= string.Format("{0:#,##0}", listCart[i].TotalFrice)%>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            <%
                                          var objStatus = listOrderStatus.Where(o => o.Value == listCart[i].Status.ToString()).FirstOrDefault();
                                            %>
                                            <%= objStatus.Name%>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <% var listComment = ModProduct_CommentsService.Instance.CreateQuery()
                            .Take(5)
                            .OrderByDesc(o => o.CreateDate)
                            .ToList(); 
                    %>
                    <div class="panel">
                        <h3 class="pane-toggler title" id="H2">
                            <a href="javascript:void(0);"><span>5 bình luận mới</span></a>
                        </h3>
                        <div class="pane-slider content">
                            <div class="div-more-info">
                                <a href="/{CPPath}/ModProduct_Comments/Index.aspx"><span>Xem thêm...</span></a></div>
                            <table class="adminlist">
                                <thead>
                                    <tr>
                                        <th>
                                            <strong>Ngày bình luận</strong>
                                        </th>
                                        <th>
                                            Người bình luận
                                        </th>
                                        <th>
                                            Nội dung
                                        </th>
                                        <th>
                                            Trạng thái
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%for (int i = 0; listComment != null && i < listComment.Count; i++)
                                      { 
                                    %>
                                    <tr class="row<%= i%2 %>">
                                        <td align="center">
                                            <%= string.Format("{0:dd/MM/yyyy HH:mm}", listComment[i].CreateDate)%>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            <%= listComment[i].Name%>
                                        </td>
                                        <td align="left" style="width: 100%">
                                            <a href="/{CPPath}/ModProduct_Comments/Add.aspx/RecordID/<%= listComment[i].ID %>">
                                                <%= listComment[i].Content%></a>
                                        </td>
                                        <td align="center" nowrap="nowrap">
                                            <%=listComment[i].Approved ? "Đã duyệt" : "Chờ duyệt"%>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <% var listCustomer = ModProduct_CustomersService.Instance.CreateQuery()
                            .Take(5)
                            .OrderByDesc(o => o.CreateDate)
                            .ToList(); 
                    %>
                    <div class="panel">
                        <h3 class="pane-toggler title" id="H3">
                            <a href="javascript:void(0);"><span>5 tài khoản mới đăng ký</span></a>
                        </h3>
                        <div class="pane-slider content">
                            <div class="div-more-info">
                                <a href="/{CPPath}/ModProduct_Customers/Index.aspx"><span>Xem thêm...</span></a></div>
                            <table class="adminlist">
                                <thead>
                                    <tr>
                                        <th>
                                            Tài khoản
                                        </th>
                                        <th>
                                            Họ tên
                                        </th>
                                        <th>
                                            Giới tính
                                        </th>
                                        <th>
                                            Ngày đăng ký
                                        </th>
                                        <th>
                                            Trạng thái
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%for (int i = 0; listCustomer != null && i < listCustomer.Count; i++)
                                      { 
                                    %>
                                    <tr class="row<%= i%2 %>">
                                        <td nowrap="nowrap">
                                            <a href="/{CPPath}/ModProduct_Customers/Add.aspx/RecordID/<%= listCustomer[i].ID %>">
                                                <%= listCustomer[i].UserName%></a>
                                        </td>
                                        <td nowrap="nowrap">
                                            <a href="/{CPPath}/ModProduct_Customers/Add.aspx/RecordID/<%= listCustomer[i].ID %>">
                                                <%= listCustomer[i].FullName%></a>
                                        </td>
                                        <td nowrap="nowrap">
                                            <%=listCustomer[i].Sex?"Nữ" : "Nam" %>
                                        </td>
                                        <td align="center">
                                            <%= string.Format("{0:dd/MM/yyyy HH:mm}", listCustomer[i].CreateDate)%>
                                        </td>
                                        <td align="center" nowrap="nowrap">
                                            <%=listCustomer[i].Activity ? "Đã duyệt" : "Chờ duyệt"%>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
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
