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
<style type="text/css">
    .container3_border_le
    {
        background-image: url('/Content/html/images/Box/container3_border_le.png');
        width: 5px;
        background-repeat: repeat-y;
    }
    .container3_body
    {
        background-image: url('/Content/html/images/Box/container3_body.png');
        background-repeat: repeat-x;
    }
    .container3_border_ri
    {
        width: 5px;
        background-image: url('/Content/html/images/Box/container3_border_ri.png');
        background-repeat: repeat-y;
    }
    .container3_border_bo
    {
        width: 100%;
        height: 5px;
        background-image: url('/Content/html/images/Box/container3_border_bo.png');
        background-repeat: repeat-x;
    }
    .MenuLeft_Title
    {
        font-weight: bold;
        color: White;
    }
    
</style>
<div style="padding-bottom: 10px;width:100%">
    <table bgcolor="#EEEEEE" width="230" border="0" cellpadding="0" cellspacing="0" style="border: solid 0px #BBBDB5;">
        <tr>
            <td class="container3_border_le" valign="top">
                <img src="/Content/html/images/Box/container3_top_left.png" />
            </td>
            <td style="background-image: url('/Content/html/images/Box/container3_body_top.png');
                background-repeat: repeat-x; height: 31px; width: 97%">
                <table cellpadding="0" cellspacing="0" style="height: 30px; width: 1%; border: 0 !important;">
                    <tr>
                        <td style="width: 5px; padding-left: 10px" valign="top">
                            <img src="/Content/html/images/Box/container3_left.png" />
                        </td>
                        <td class="container3_body">
                            <table border="0" cellpadding="0" cellspacing="0" style="height: 31px; text-align: center">
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
            <td width="100%" style="border-right: 0px solid #E4E4E4; border-left: 0px solid #E4E4E4;
                padding-top: 10px;">
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="cssOhidoInfoBox">
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
                                                <img src="/Content/html/images/Menu/MenuLeft/arrow.gif" />
                                                <%=listItem[i].Name%></span></a>
                                        <% SysPageService.BuildingMenu(listAllItem, listItem[i].ID, ref sBuildingMenu, sHostPort); %>
                                        <%=sBuildingMenu%>
                                    </li>
                                    <img src="/Content/html/images/Menu/MenuLeft/vach.gif" width="100%" />
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
