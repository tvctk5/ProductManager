<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    //var listItem = ViewBag.Data as List<SysPageEntity>;
    var listAllItem = SysPageService.get_all();
    listAllItem = listAllItem.Where(o => o.ViewInMenu == true).ToList();
    var listItem = listAllItem.Where(o => o.ParentID == 1).ToList();

    // Nếu khách hàng đã đăng nhập --> Lấy thông tin ID    
    int CustomId = 0;
    if (VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"))
        CustomId = ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"));
%>
<%--<script src="/Content/html/menu/jquery-1.7.2.min.js" type="text/javascript"></script>--%>
<script type="text/javascript">
		<!--
    $(document).ready(function () {
        $("#nav li:has(ul)").addClass("down");
        $("#nav li li:has(ul)").addClass("right");
        $("#nav li").hover(function () {
            $(this).find("ul:first").css({ display: "none" }).slideDown(100);
        }, function () {
            $(this).find("ul:first").fadeOut(100);
        });

        // Nếu đã đăng nhập rồi thì ẩn link đăng nhập và đăng ký đi
        var CustomnerId = '<%=CustomId %>';
        if (CustomnerId != "0")
            $(".box_menu #nav li a[href*='Dang-nhap.aspx'],.box_menu #nav li a[href*='Dang-ky.aspx']").parent().hide();
    });
		//-->
</script>
<div id="wrapper">
    <ul id="nav">
        <%
            string sHostPort = string.Empty;
            if (Request.Url.Port == 80)// Nếu mặc định là cổng 80
                sHostPort = "http://" + Request.Url.Host + "/";
            else
                sHostPort = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/";

            string sBuildingMenu = string.Empty;
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                sBuildingMenu = string.Empty;
        %>
        <li <%if(ViewPage.IsPageActived(listItem[i], 0)){ %> class="current current_background"
            <%} %>><a href="<%=ViewPage.GetPageURL(listItem[i]) %>" title="<%=listItem[i].LinkTitle%>">
                <span <%if(ViewPage.IsPageActived(listItem[i], 0)){ %> style="color: #FFF000;" <%} %>>
                    <%=listItem[i].Name%></span></a>
            <% SysPageService.BuildingMenu(listAllItem, listItem[i].ID, ref sBuildingMenu, sHostPort); %>
            <%=sBuildingMenu%>
        </li>
        <%if (i != listItem.Count - 1)
          {%><li class="top-menu-li-image">
              <img width="100%" height="28px" src="/Content/html/images/mainnav-sep.gif" /></li><%} %>
        <%} %>
    </ul>
    <span class="box-search">
        <input type="text" name="TextSearch" id="TextSearch" maxlength="100" title="Nhập nội dung tìm kiếm"
            placeholder="Nhập thông tin tìm kiếm" />
        <a href="javascript:void();" title="Tìm kiếm" class="box-search-icon"></a></span>
</div>
