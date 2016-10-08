<%@ Control Language="C#" AutoEventWireup="true" %>
<ul id="menu">
    <li class="node"><a href="/CP/">{RS:MenuTop_Home}</a>
        <li class="node"><a>{RS:MenuTop_Category}</a>
            <ul>
                <% var listModule = VSW.Lib.Web.Application.CPModules.Where(o => o.ShowInMenu == true && o.MenuGroupId == 1).OrderBy(o => o.Order).ToList(); %>
                <%for (int i = 0; listModule != null && i < listModule.Count; i++)
                  { %>
                <li><a class="<%=listModule[i].CssClass%>" href="/{CPPath}/<%=listModule[i].Code%>/Index.aspx">
                    <%=listModule[i].Name%></a></li>
                <%} %>
            </ul>
        </li>
        <li class="node"><a>{RS:MenuTop_News}</a>
            <ul>
                <% listModule = VSW.Lib.Web.Application.CPModules.Where(o => o.ShowInMenu == true && o.MenuGroupId == 2).OrderBy(o => o.Order).ToList(); %>
                <%for (int i = 0; listModule != null && i < listModule.Count; i++)
                  { %>
                <li><a class="<%=listModule[i].CssClass%>" href="/{CPPath}/<%=listModule[i].Code%>/Index.aspx">
                    <%=listModule[i].Name%></a></li>
                <%} %>
            </ul>
        </li>
        <li class="node"><a>{RS:MenuTop_Product}</a>
            <ul>
                <% listModule = VSW.Lib.Web.Application.CPModules.Where(o => o.ShowInMenu == true && (o.MenuGroupId == null || o.MenuGroupId == 0)).OrderBy(o => o.Order).ToList(); %>
                <%for (int i = 0; listModule != null && i < listModule.Count; i++)
                  { %>
                <li><a class="<%=listModule[i].CssClass%>" href="/{CPPath}/<%=listModule[i].Code%>/Index.aspx">
                    <%=listModule[i].Name%></a></li>
                <%} %>
            </ul>
        </li>
        <li class="node"><a>{RS:MenuTop_Utilities}</a>
            <ul>
                <% listModule = VSW.Lib.Web.Application.CPModules.Where(o => o.ShowInMenu == true && o.MenuGroupId == 4).OrderBy(o => o.Order).ToList(); %>
                <%for (int i = 0; listModule != null && i < listModule.Count; i++)
                  { %>
                <li><a class="<%=listModule[i].CssClass%>" href="/{CPPath}/<%=listModule[i].Code%>/Index.aspx">
                    <%=listModule[i].Name%></a></li>
                <%} %>
            </ul>
        </li>
        <li class="node"><a>Quản lý Doanh thu</a>
            <ul>
                <% listModule = VSW.Lib.Web.Application.CPModules.Where(o => o.ShowInMenu == true && o.MenuGroupId == 5).OrderBy(o => o.Order).ToList(); %>
                <%for (int i = 0; listModule != null && i < listModule.Count; i++)
                  { %>
                <li><a class="<%=listModule[i].CssClass%>" href="/{CPPath}/<%=listModule[i].Code%>/Index.aspx">
                    <%=listModule[i].Name%></a></li>
                <%} %>
            </ul>
        </li>
        <li class="node"><a>{RS:MenuTop_Design}</a>
            <ul>
                <li><a class="icon-16-menu" href="/{CPPath}/SysPage/Index.aspx">{RS:MenuTop_Page}</a></li>
                <li><a class="icon-16-themes" href="/{CPPath}/SysTemplate/Index.aspx">{RS:MenuTop_Template}</a></li>
                <li><a class="icon-16-module" href="/{CPPath}/SysSite/Index.aspx">Site</a></li>
                <%--<li><a class="icon-16-plugin" href="/{CPPath}/SysModule/Index.aspx">Quản lý module</a></li>--%>
            </ul>
        </li>
        <li class="node"><a>{RS:MenuTop_Admin}</a>
            <ul>
                <li><a class="icon-16-category" href="/{CPPath}/SysMenu/Index.aspx">Chuyên mục</a></li>
                <li><a class="icon-16-groups" href="/{CPPath}/SysRole/Index.aspx">Nhóm người sử dụng</a></li>
                <li><a class="icon-16-user" href="/{CPPath}/SysUser/Index.aspx">Người sử dụng</a></li>
                <li><a class="icon-16-language" href="/{CPPath}/SysResource/Index.aspx">Tài nguyên</a></li>
                <li><a class="icon-16-info" href="/{CPPath}/SysUserLog/Index.aspx">Nhật ký đăng nhập</a></li>
            </ul>
        </li>
        <li class="node"><a>{RS:MenuTop_Private}</a>
            <ul>
                <li><a class="icon-16-component" href="/{CPPath}/UserChangePass.aspx">Thay đổi mật khẩu</a></li>
                <li><a class="icon-16-component" href="/{CPPath}/UserChangeInfo.aspx">Thay đổi thông
                    tin</a></li>
            </ul>
        </li>
</ul>
