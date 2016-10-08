<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModAdvEntity>;

   if (listItem == null)
       return;
%>
<link href="/Content/slideshow/Slide_Type03/css/layout.css" rel="stylesheet" type="text/css" />
<link href="/Content/slideshow/Slide_Type03/css/style2.css" rel="stylesheet" type="text/css" />
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>
<script type="text/javascript" src="/Content/slideshow/Slide_Type03/js/jquery.easing.js"></script>
<script type="text/javascript" src="/Content/slideshow/Slide_Type03/js/script.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var buttons = { previous: $('#jslidernews2 .button-previous'),
            next: $('#jslidernews2 .button-next')
        };
        $('#jslidernews2').lofJSidernews({ interval: 5000,
            easing: 'easeInOutQuad',
            duration: 1200,
            auto: true,
            mainWidth: 684,
            mainHeight: 300,
            navigatorHeight: 100,
            navigatorWidth: 310,
            maxItemDisplay: 3,
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
    #jslidernews2, .main-slider-content
    {
        height: 300px;
    }
    ul.lof-main-wapper li
    {
        position: relative;
    }
    
    .navigator-content .navigator-wrapper .navigator-wrap-inner h3{ padding-top: 5px;}
</style>
<div class="div-slideshow">
    <div id="container">
        <!------------------------------------- THE CONTENT ------------------------------------------------->
        <div id="jslidernews2" class="lof-slidecontent">
            <div class="preload">
                <div>
                </div>
            </div>
            <div class="button-previous">
                previous</div>
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
                        <img class="image-slide" title="" src="<%=sUrl %>" width="100%" height="300px" title="<%=item.Description %>" />
                    </li>
                    <%} %>
                </ul>
            </div>
            <!-- END MAIN CONTENT -->
            <!-- NAVIGATOR -->
            <div class="navigator-content">
                <div class="navigator-wrapper">
                    <ul class="navigator-wrap-inner">
                        <%  
                            foreach (var item in listItem)
                            {
                                sName = item.Name;
                                sUrl = VSW.Lib.Global.Utils.GetResizeFile(item.File, 2, 75, 30);
                        %>
                        <li>
                            <div>
                                <img src="<%=sUrl %>" alt="<%=sName %>" title="<%=sName %>" />
                                <h3>
                                    <%=sName %></h3>
                                <span>
                                    <%=item.Description%></span>
                            </div>
                        </li>
                        <%} %>
                    </ul>
                </div>
            </div>
            <!----------------- END OF NAVIGATOR --------------------->
            <div class="button-next">
                next</div>
            <!-- BUTTON PLAY-STOP -->
            <div class="button-control">
                <span></span>
            </div>
            <!-- END OF BUTTON PLAY-STOP -->
        </div>
        <!------------------------------------- END OF THE CONTENT ------------------------------------------------->
    </div>
</div>
