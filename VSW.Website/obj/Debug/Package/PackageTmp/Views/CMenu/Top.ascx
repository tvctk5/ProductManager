<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<SysPageEntity>;
    if (listItem != null)
        listItem = listItem.Where(o => o.ViewInMenu == true).ToList();
    else
        listItem = new List<SysPageEntity>();
%>
<div class="box_menu">
    <ul class="menu">
        <%for (int i = 0; listItem != null && i < listItem.Count; i++)
          {%>
        <li <%if(ViewPage.IsPageActived(listItem[i], 0)){ %> class="current" <%} %>><a href="<%=ViewPage.GetPageURL(listItem[i]) %>"
            title="<%=listItem[i].LinkTitle%>"><span <%if(ViewPage.IsPageActived(listItem[i], 0)){ %>
                style="color: #FF0000" <%} %>>
                <%=listItem[i].Name%></span></a></li>
        <%if (i != listItem.Count - 1)
          {%><li>
              <img src="/Content/html/images/space.jpg" /></li><%} %>
        <%} %>
    </ul>
</div>
<div class="boxsearch">
    <div class="s1" style="color: #FDFDFD">
        1</div>
    <div class="s2">
        <div class="box-topmenu">
            <span class="pr"><a href="/vn/default.aspx">
                <img src="/Content/html/images/vi.png" class="pr" /></a><a href="/en/default.aspx"><img
                    src="/Content/html/images/en.png" class="pr pl" /></a></span></div>
    </div>
</div>
