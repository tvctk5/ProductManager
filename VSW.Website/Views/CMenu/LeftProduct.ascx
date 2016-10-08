<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    //var listItem = ViewBag.Data as List<SysPageEntity>;
    var listAllItem = SysPageService.get_all();
    var listItem = listAllItem.Where(o => o.ParentID == 1).ToList(); 
%>
<script src="/Content/html/menu/jquery-1.7.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
		<!--
    $(document).ready(function () {
        $("#nav_left li:has(ul)").addClass("down");
        $("#nav_left li li:has(ul)").addClass("right");
        $("#nav_left li").hover(function () {
            $(this).find("ul:first").css({ display: "none" }).slideDown(100);
        }, function () {
            $(this).find("ul:first").fadeOut(100);
        });
    });
		//-->
</script>
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
                            <div id="wrapper_left">
                                <ul id="nav_left">
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
                                                <img src="/Content/html/images/Menu/MenuLeft/arrow.gif" border="0" />
                                                <%=listItem[i].Name%></span></a>
                                        <% SysPageService.BuildingMenu(listAllItem, listItem[i].ID, ref sBuildingMenu, sHostPort); %>
                                        <%=sBuildingMenu%>
                                        <div>
                                            <img src="/Content/html/images/Menu/MenuLeft/vach.gif" width="100%" />
                                        </div>
                                    </li>
                                    <!--<li class="left-menu-li-image">
                                      <div style="padding-top:2px;padding-bottom:2px;"> 
                                  
                                    </div>
                                     </li> -->
                                    <%} %>
                                </ul>
                            </div>
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
