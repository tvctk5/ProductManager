<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var objData = ViewBag.Data as WebResourceEntity;
    string sContent = objData.Value;
%>
<div class="box100">
    <div id="vertical-menu" class="DefaultModule cate-menu">
        <div class="defaultTitle cate-menu-title title-module">
            <span>
                <%=ViewBag.Title %></span></div>
        <div>
            <div class="defaultContent cate-menu-content">
                <%=sContent%>
            </div>
            <div class="defaultFooter cate-menu-footer">
            </div>
        </div>
    </div>
</div>
