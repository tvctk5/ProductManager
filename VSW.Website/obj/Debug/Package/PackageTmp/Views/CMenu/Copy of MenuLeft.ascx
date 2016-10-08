<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var lstData = ViewPage.Data as List<ModMenu_DynamicEntity>;
    var lstSysPageEntity = ViewPage.ListAllSysPage as List<SysPageEntity>;
    if (lstData == null)
        lstData = new List<ModMenu_DynamicEntity>();

    List<ModMenu_DynamicEntity> lstParent = lstData.Where(o => o.ParentID == 0).ToList();
    List<ModMenu_DynamicEntity> lstChilden = null;
%>
<div class="box100">
    <div id="vertical-menu" class="DefaultModule cate-menu">
        <div class="defaultTitle cate-menu-title title-module">
            <span>
                <%=ViewBag.Title %></span></div>
        <div>
            <div class="defaultContent cate-menu-content">
                <ul>
                    <% foreach (var itemParent in lstParent)
                       {%>
                    <li><a href="<%=ViewPage.GetPageURL(itemParent.SysPageID,lstSysPageEntity) %>">
                        <%=itemParent.Name %></a>
                        <%
                           lstChilden = lstData.Where(p => p.ParentID == itemParent.ID).ToList();
                           if (lstChilden == null || lstChilden.Count <= 0)
                               continue;
                        %>
                        <ul>
                            <%
                           foreach (var itemChilden in lstChilden)
                           {%>
                            <li><a href="<%=ViewPage.GetPageURL(listItem[i])%>">
                                <%=itemChilden.Name %></a></li>
                            <%}
                            %>
                        </ul>
                        <ul>
                        </ul>
                    </li>
                    <% } %>
                </ul>
            </div>
            <div class="defaultFooter cate-menu-footer">
            </div>
        </div>
    </div>
</div>
