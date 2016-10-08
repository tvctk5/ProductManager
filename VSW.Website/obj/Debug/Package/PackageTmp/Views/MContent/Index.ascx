<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<div class="div-news">
    <div class="news-title">
        <%= ViewPage.CurrentPage.Name %></div>
    <div class="news-content">
        <p>
            <%= Utils.GetHTMLForSeo(ViewPage.CurrentPage.Content) %>
        </p>
    </div>
    <div class="news-footer">
    </div>
</div>
