<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModAdvEntity>;

   if (listItem == null)
       return;
%>
<script type="text/javascript" src="/Content/slideshow/Slide_Type01/js/jquery.bxslider.min.js"></script>
<link href="/Content/slideshow/Slide_Type01/css/jquery.bxslider.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $('.bxslider').bxSlider({ auto: true, pager: false, infiniteLoop: false, hideControlOnEnd: true, autoDelay: 0, pause: 3000 });
    });
</script>
<style type="text/css">
    .div-slideshow
    {
        display: block;
        padding-left: 5px;
        padding-right: 5px;
    }
</style>
<div class="div-slideshow">
    <ul class="bxslider">
        <% 
            string sUrl = string.Empty;
            string sName = string.Empty;
            foreach (var item in listItem)
            {
                sName = item.Name;
                sUrl = VSW.Lib.Global.Utils.GetLinkFile(item.File);
        %>
        <li>
            <img class="image-slide" src="<%=sUrl %>" width="100%" height="200px"/></li>
        <%}%>
    </ul>
</div>
