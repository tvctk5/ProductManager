<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModAdvEntity>; %>

<%if(listItem != null){ %>
<div class="main_slider"><div class="home_slider">
    <div id="jsn-is424" class="jsn-imageshow" style="width: 100%; height: 265px;"></div>
    <script type="text/javascript" src="/Content/slideshow/default/modules/mod_jsn_imageshow_pro/jsn_imageshow_pro/swfobject.js"></script>
    <script type="text/javascript" src="/Content/slideshow/default/modules/mod_jsn_imageshow_pro/jsn_imageshow_pro/swfobject_addon.js"></script>
    <script type="text/javascript">
// <![CDATA[

        var soImageShow = new SWFObject("/Content/slideshow/default/modules/mod_jsn_imageshow_pro/jsn_imageshow_pro/loader.swf", "imageshow", "100%", "265", "8", "e5e5e5");
        soImageShow.addParam("wmode", "opaque");
        soImageShow.addVariable("dataXml", "/Content/slideshow/default/SlideshowXML.aspx?m=<%=listItem[0].MenuID %>");
        soImageShow.addVariable("imageshowUrl", "/Content/slideshow/default/modules/mod_jsn_imageshow_pro/jsn_imageshow_pro/imageshow.swf");
        soImageShow.addVariable("slideTiming", 6);
        soImageShow.addVariable("repeatCount", "0");
        soImageShow.addVariable("processOrder", "forward");
        soImageShow.addVariable("shadowImageUrl", "");
        soImageShow.addVariable("captionText", "{RS:Web_Company}");
        soImageShow.addVariable("captionFont", "Arial");
        soImageShow.addVariable("captionSize", "12");
        soImageShow.addVariable("captionColor", "0xFFFFFF");
        soImageShow.addVariable("captionAlignment", "left");
        soImageShow.addVariable("captionPosition", "bottom");
        soImageShow.addVariable("captionPadding", "6,10");
        soImageShow.addVariable("captionBgOpacity", "50");
        soImageShow.addVariable("captionBgColor", "0x000000");
        soImageShow.addVariable("showProgress", "1");
        soImageShow.addVariable("overlayEffectName", "");
        soImageShow.addVariable("overlayImageUrl", "");
        soImageShow.addVariable("overlayImageOpacity", "75");
        soImageShow.addVariable("overlayImageX", "100");
        soImageShow.addVariable("overlayImageY", "100");
        soImageShow.addVariable("motionTiming", "3");
        soImageShow.addVariable("motionEase", "Sine");
        soImageShow.addVariable("moveRange", "3");
        soImageShow.addVariable("scaleRange", "15");
        soImageShow.addVariable("rotationRange", "0");
        soImageShow.addVariable("transitionType", "random");
        soImageShow.addVariable("transitionTiming", "2");
        soImageShow.addVariable("transitionEase", "Sine");
        soImageShow.addVariable("enableLink", "1");
        soImageShow.addVariable("linkUrl", "/");
        soImageShow.addVariable("linkOpen", "_self");
        registerSWFObject(soImageShow, "jsn-is424");
	
// ]]>
    </script>
</div></div><%} %>