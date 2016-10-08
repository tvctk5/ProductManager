<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var sSiteMap = ViewBag.SiteMap as string;
%>
<div class="div-sitemap">
<%=sSiteMap%>
</div>
 

