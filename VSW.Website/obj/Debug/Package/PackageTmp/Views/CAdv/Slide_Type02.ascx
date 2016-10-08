<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModAdvEntity>;

   if (listItem == null)
       return;
%>
<link href="/Content/slideshow/Slide_Type02/css/layout.css" rel="stylesheet" type="text/css" />
<link href="/Content/slideshow/Slide_Type02/css/style1.css" rel="stylesheet" type="text/css" />
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>
<script type="text/javascript" src="/Content/slideshow/Slide_Type02/js/jquery.easing.js"></script>
<script type="text/javascript" src="/Content/slideshow/Slide_Type02/js/script.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        // buttons for next and previous item						 
        var buttons = { previous: $('#jslidernews1 .button-previous'),
            next: $('#jslidernews1 .button-next')
        };
        $('#jslidernews1').lofJSidernews({ interval: 4000,
            direction: 'opacitys',
            easing: 'easeInOutExpo',
            duration: 1200,
            auto: true,
            maxItemDisplay: 4,
            navPosition: 'horizontal', // horizontal
            navigatorHeight: 32,
            navigatorWidth: 80,
            mainWidth: 980,
            buttons: buttons
        });
    });
</script>
<style type="text/css">
    .div-slideshow
    {
        display: block;
        padding-left: 5px;
        padding-right: 5px;
    }
    #jslidernews1,.main-slider-content {height:240px;}
</style>
<div class="div-slideshow">
    <div id="container">
        <!------------------------------------- THE CONTENT ------------------------------------------------->
        <div id="jslidernews1" class="lof-slidecontent">
            <div class="preload">
                <div>
                </div>
            </div>
            <!-- MAIN CONTENT -->
            <div class="main-slider-content">
                <ul class="sliders-wrap-inner">
                    <% 
                        string sUrl = string.Empty;
                        string sName = string.Empty;
                        foreach (var item in listItem)
                        {
                            sName = item.Name;
                            sUrl = VSW.Lib.Global.Utils.GetLinkFile(item.File);
                    %>
                    <li>
                        <img class="image-slide" title="" src="<%=sUrl %>" width="100%" height="200px" title="<%=item.Description %>" />
                        <div class="slider-description">
                            <a target="<%=item.Target%>" title="<%=item.Name%>" href="<%=item.URL %>">
                                <h4>
                                    <%=item.Name%></h4>
                            </a>
                            <p>
                                <%=item.Description %> ...
                                <a class="readmore" href="<%=item.URL %>" target="<%=item.Target%>"> Xem chi tiết</a>
                            </p>
                        </div>
                    </li>
                    <%}%>
                </ul>
            </div>
            <!-- END MAIN CONTENT -->
            <!-- NAVIGATOR -->
            <div class="navigator-content">
                <div class="button-next">
                    Next</div>
                <div class="navigator-wrapper">
                    <ul class="navigator-wrap-inner">
                        <%  
                            foreach (var item in listItem)
                            {
                                sName = item.Name;
                                sUrl = VSW.Lib.Global.Utils.GetResizeFile(item.File, 2, 75, 30);
                        %>
                        <li>
                            <img src="<%=sUrl %>" alt="<%=sName %>" /></li>
                        <%} %>
                    </ul>
                </div>
                <div class="button-previous">
                    Previous</div>
            </div>
            <!----------------- END OF NAVIGATOR --------------------->
            <!-- BUTTON PLAY-STOP -->
            <div class="button-control">
                <span></span>
            </div>
            <!-- END OF BUTTON PLAY-STOP -->
        </div>
        <!------------------------------------- END OF THE CONTENT ------------------------------------------------->
    </div>
</div>
