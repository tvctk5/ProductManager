<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModAdvEntity>;

   if (listItem == null)
       return;

   string sWidth = (ViewBag.Width == null || ViewBag.Width == 0) ? "" : ((int)ViewBag.Width).ToString();
   string sHeight = (ViewBag.Height == null || ViewBag.Height == 0) ? "" : ((int)ViewBag.Height).ToString();
   
   if (string.IsNullOrEmpty(sWidth))
       sWidth = "980";

   if (string.IsNullOrEmpty(sHeight))
       sHeight = "250";

   sHeight += "px";
       
%>
<link href="/Content/slideshow/Slide_Type04/css/layout.css" rel="stylesheet" type="text/css" />
<link href="/Content/slideshow/Slide_Type04/css/style4.css" rel="stylesheet" type="text/css" />
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>
<script type="text/javascript" src="/Content/slideshow/Slide_Type04/js/jquery.easing.js"></script>
<script type="text/javascript" src="/Content/slideshow/Slide_Type04/js/script.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        // buttons for next and previous item						 
        var buttons = { previous: $('#jslidernews1 .button-previous'),
            next: $('#jslidernews1 .button-next')
        };
        $obj = $('#jslidernews1').lofJSidernews({ interval: 4000,
            easing: 'easeInOutQuad',
            duration: 1200,
            auto: true,
            maxItemDisplay: 3,
            startItem: 1,
            navPosition: 'horizontal', // horizontal
            navigatorHeight: null,
            navigatorWidth: null,
            mainWidth: <%=sWidth %>,//980
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
    #jslidernews1, .main-slider-content
    {
        height: <%=sHeight %>; /*250px;*/
    }
    ul.lof-main-wapper li
    {
        position: relative;
    }
    
    .navigator-content .navigator-wrapper .navigator-wrap-inner h3
    {
        padding-top: 5px;
    }
</style>
<div class="div-slideshow">
    <div id="container">
        <div id="jslidernews1" class="lof-slidecontent">
            <div class="preload">
                <div>
                </div>
            </div>
            <div class="button-previous">
                previous</div>
            <div class="button-next">
                next</div>
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
                        <img class="image-slide" title="" src="<%=sUrl %>" width="100%" height='<%=sHeight %>' title="<%=item.Description %>" />
                        <div class="slider-description">
                            <a target="<%=item.Target%>" title="<%=item.Name%>" href="<%=item.URL %>">
                                <h4>
                                    <%=item.Name%></h4>
                            </a>
                            <p>
                                <%=item.Description %>
                                ... <a class="readmore" href="<%=item.URL %>" target="<%=item.Target%>">Xem chi tiết</a>
                            </p>
                        </div>
                    </li>
                    <%}%>
                </ul>
            </div>
            <!-- END MAIN CONTENT -->
            <!-- NAVIGATOR -->
            <div class="navigator-content">
                <div class="button-control">
                    <span></span>
                </div>
                <div class="navigator-wrapper">
                    <ul class="navigator-wrap-inner">
                        <%for (int i = 1; i <= listItem.Count; i++)
                          {%>
                        <li><span>
                            <%=i%></span></li>
                        <% } %>
                    </ul>
                </div>
            </div>
            <!----------------- END OF NAVIGATOR --------------------->
        </div>
    </div>
</div>
