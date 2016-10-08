<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    //var listItem = ViewBag.Data as List<SysPageEntity>;
    var listAllItem = SysPageService.get_all();
    var listItem = listAllItem.Where(o => o.ParentID == 1).ToList(); 
%>
<script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.min.js"></script>
<script type="text/javascript">
	
    $(document).ready(function () {
        $("#nav li:has(ul)").addClass("down");
        $("#nav li li:has(ul)").addClass("right");
        $("#nav li").hover(function () {
            $(this).find("ul:first").css({ display: "none" }).slideDown(100);
        }, function () {
            $(this).find("ul:first").fadeOut(100);
        });
    });
		//	<!---->
</script>
<style type="text/css">
    #MenuLeft{}
</style>
<div class="top-right">
    <span>
        <%=ViewBag.Title%></span></div>
<div id="MenuLeft" class="bg-right" style="border: 1px solid #999999">
    <div class="ddsmoothmenu-v">
        <ul>
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
                    <span <%if(ViewPage.IsPageActived(listItem[i], 0)){ %> style="color: Blue;" <%} %>>
                        <%=listItem[i].Name%></span></a>
                <% SysPageService.BuildingMenu(listAllItem, listItem[i].ID, ref sBuildingMenu, sHostPort); %>
                <%=sBuildingMenu%>
            </li>
            <%} %>
        </ul>
    </div>
</div>
<div class="bottom-righ">
</div>
