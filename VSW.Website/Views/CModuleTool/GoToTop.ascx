<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
     %>
<a href="javascript:void(0)" title="Lên đầu trang" class="go_top"><img /></a>