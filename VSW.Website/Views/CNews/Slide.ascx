<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%--<div class="wmuSlider example1" style="height: 560px;">
    <div class="wmuSliderWrapper">
    <article style="position: relative; width: 100%; opacity: 1;"> 
        <div class="banner-wrap">
	        <div class="slider-left">
		        <img src="/Content/plugins/adidas-pack/images/banner1.jpg" alt=""/> 
	        </div>
		    <div class="slider-right">
		        <h1>Classic</h1>
		        <h2>White</h2>
		        <p>Lorem ipsum dolor sit amet</p>
		        <div class="btn"><a href="shop.html">Shop Now</a></div>
		    </div>
		    <div class="clear"></div>
	    </div>
    </article>
    <article style="position: absolute; width: 100%; opacity: 0;"> 
	    <div class="banner-wrap">
	    <div class="slider-left">
		    <img src="/Content/plugins/adidas-pack/images/banner2.jpg" alt=""/> 
	    </div>
		    <div class="slider-right">
		    <h1>Classic</h1>
		    <h2>White</h2>
		    <p>Lorem ipsum dolor sit amet</p>
		    <div class="btn"><a href="shop.html">Shop Now</a></div>
		    </div>
		    <div class="clear"></div>
	    </div>
    </article>
    <article style="position: absolute; width: 100%; opacity: 0;">
    <div class="banner-wrap">
	    <div class="slider-left">
		    <img src="/Content/plugins/adidas-pack/images/banner1.jpg" alt=""/> 
	    </div>
		    <div class="slider-right">
		    <h1>Classic</h1>
		    <h2>White</h2>
		    <p>Lorem ipsum dolor sit amet</p>
		    <div class="btn"><a href="shop.html">Shop Now</a></div>
		    </div>
		    <div class="clear"></div>
	    </div>
    </article>
    <article style="position: absolute; width: 100%; opacity: 0;">
    <div class="banner-wrap">
	    <div class="slider-left">
		    <img src="/Content/plugins/adidas-pack/images/banner2.jpg" alt=""/> 
	    </div>
		    <div class="slider-right">
		    <h1>Classic</h1>
		    <h2>White</h2>
		    <p>Lorem ipsum dolor sit amet</p>
		    <div class="btn"><a href="shop.html">Shop Now</a></div>
		    </div>
		    <div class="clear"></div>
	    </div>
    </article>
    <article style="position: absolute; width: 100%; opacity: 0;"> 
	    <div class="banner-wrap">
	    <div class="slider-left">
		    <img src="/Content/plugins/adidas-pack/images/banner1.jpg" alt=""/> 
	    </div>
		    <div class="slider-right">
		    <h1>Classic</h1>
		    <h2>White</h2>
		    <p>Lorem ipsum dolor sit amet</p>
		    <div class="btn"><a href="shop.html">Shop Now</a></div>
		    </div>
		    <div class="clear"></div>
	    </div>
    </article>

    </div>
    <a class="wmuSliderPrev">Previous</a><a class="wmuSliderNext">Next</a>
    <ul class="wmuSliderPagination">
        <li><a href="#" class="">0</a></li>
        <li><a href="#" class="">1</a></li>
        <li><a href="#" class="wmuActive">2</a></li>
        <li><a href="#">3</a></li>
        <li><a href="#">4</a></li>
    </ul>
    <a class="wmuSliderPrev">Previous</a><a class="wmuSliderNext">Next</a><ul class="wmuSliderPagination">
        <li><a href="#" class="wmuActive">0</a></li><li><a href="#" class="">1</a></li><li><a
            href="#" class="">2</a></li><li><a href="#" class="">3</a></li><li><a href="#" class="">
                4</a></li></ul>
</div>
<script src="/Content/plugins/adidas-pack/js/jquery.wmuSlider.js"></script>
<script type="text/javascript" src="/Content/plugins/adidas-pack/js/modernizr.custom.min.js"></script>--%>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
        
    var listItem = ViewBag.Data as List<ModNewsEntity>;
    if (listItem == null || listItem.Any() == false)
        return;

    listItem.OrderBy(o => o.Order).ThenByDescending(o => o.Published);//.ToList();
%>
<div id="myCarousel" class="carousel slide" data-ride="carousel">
  <!-- Indicators -->
  <ol class="carousel-indicators">
  <% for (int i = 0; i < listItem.Count; i++)
     {%>
        <li data-target="#myCarousel" data-slide-to="<%=i %>" class='<%= i == 0? "active" : "" %>'></li> 
  <%   }
       %>
    <%--<li data-target="#myCarousel" data-slide-to="0" class="active"></li>
    <li data-target="#myCarousel" data-slide-to="1"></li>
    <li data-target="#myCarousel" data-slide-to="2"></li>
    <li data-target="#myCarousel" data-slide-to="3"></li>--%>
  </ol>

  <!-- Wrapper for slides -->
  <div class="carousel-inner" role="listbox">

  <% for (int i = 0; i < listItem.Count; i++)
     {
         var item = listItem[i];%>
        <div class="item <%= i == 0? "active" : "" %>">
          <img src="<%=Utils.GetResizeFile(item.File, 2, 900, 450)%>" alt="Chania" class="img-responsive" width="100%">
          <div class="carousel-caption">
            <h3><a href="<%=ViewPage.GetURL(item.MenuID, item.Code) %>"><%= item.Name %></a></h3>
            <p><%= item.Summary %></p>
          </div>
        </div>
  <%   }
       %>

<%--    <div class="item active">
      <img src="/Content/plugins/adidas-pack/images/banner1.jpg" alt="Chania">
      <div class="carousel-caption">
        <h3>Chania</h3>
        <p>The atmosphere in Chania has a touch of Florence and Venice.</p>
      </div>
    </div>

    <div class="item">
      <img src="/Content/plugins/adidas-pack/images/banner2.jpg" alt="Chania">
      <div class="carousel-caption">
        <h3>Chania</h3>
        <p>The atmosphere in Chania has a touch of Florence and Venice.</p>
      </div>
    </div>

    <div class="item">
      <img src="/Content/plugins/adidas-pack/images/banner1.jpg" alt="Flower">
      <div class="carousel-caption">
        <h3>Flowers</h3>
        <p>Beatiful flowers in Kolymbari, Crete.</p>
      </div>
    </div>

    <div class="item">
      <img src="/Content/plugins/adidas-pack/images/banner2.jpg" alt="Flower">
      <div class="carousel-caption">
        <h3>Flowers</h3>
        <p>Beatiful flowers in Kolymbari, Crete.</p>
      </div>
    </div>--%>
  </div>

  <!-- Left and right controls -->
  <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
<script>
    //$('.example1').wmuSlider();         
</script>
<style type="text/css">
.carousel-control.right{background-image: linear-gradient(to right, rgba(0, 0, 0, .0001) 0%, rgba(0, 0, 0, 0.05) 100%) !important;}
.carousel-control.left{background-image: linear-gradient(to left, rgba(0, 0, 0, .0001) 0%, rgba(0, 0, 0, 0.05) 100%) !important;}
.carousel-indicators li{border-color:#bdbbbb !important;background-color: white !important;}
.carousel-indicators li.active{background-color:#8e8c8c !important;}
.carousel-caption{background-color:rgba(51, 171, 20, 0.58);right: 0% !important; left: 0% !important; padding-left:25px; padding-right:25px;}
.carousel-caption a{color: White !important;}
</style>