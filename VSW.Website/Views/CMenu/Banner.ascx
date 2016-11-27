<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<ModMenu_DynamicEntity>; 
    var listAllPage = ViewBag.ListAllSysPage as List<SysPageEntity>;
    var listSlideItem = ViewBag.DataSlide as List<ModAdvEntity>;
    if (listSlideItem == null)
        listSlideItem = new List<ModAdvEntity>();
   %>

<%--<%for (int i = 0; listItem != null && i < listItem.Count; i++ )
{ %>
  <%= Utils.GetCodeAdv(listItem[i])%>
<%} %>--%>

<div class="col-md-2">
<%--	<a href="index.html"><img src="/Content/plugins/adidas-pack/images/logo.png" alt=""/></a>--%>
<a href="/"><img src="/Data/upload/images/Logo/Td_Mart_NewLogo_Thin_134_135.png" alt="" style="max-width:150px;"/></a>

</div>
<div class="col-md-10">
<div class="row text-right">
    <div class="cssmenu">
	    <ul>
        <%for (int i = 0; listItem != null && i < listItem.Count; i++ )
        { 
              // var page = listAllPage.Where(o=>o.ID == listItem[i].SysPageID).SingleOrDefault(); page = page == null? new SysPageEntity() : page;
              var link = string.IsNullOrEmpty(listItem[i].Url) ? ViewPage.GetPageURL(listItem[i].SysPageID, listAllPage) : listItem[i].Url;
         %>
            <li><a href="<%=link %>"><%=listItem[i].Name %></a></li> 
    <%} %>
    <%--		<li class="active"><a href="register.html">Sign up & Save</a></li> 
		    <li><a href="shop.html">Store Locator</a></li> 
		    <li><a href="login.html">My Account</a></li> 
		    <li><a href="checkout.html">CheckOut</a></li> --%>
	    </ul>
    </div>
</div>
    <!-- Slide in Banner-->
<div class="row">
    <div class="banner-slide">
        <div id="myCarousel-Banner" class="carousel slide" data-ride="carousel">
          <!-- Indicators -->
<%--          <ol class="carousel-indicators">
          <% for (int i = 0; i < listSlideItem.Count; i++)
             {%>
                <li data-target="#myCarousel-Banner" data-slide-to="<%=i %>" class='<%= i == 0? "active" : "" %>'></li> 
          <%   }
               %>
          </ol>--%>

          <!-- Wrapper for slides -->
          <div class="carousel-inner" role="listbox">

          <% 
              string sUrl = string.Empty;
              string sName = string.Empty;
              for (int i = 0; i < listSlideItem.Count; i++)
             {
                 var item = listSlideItem[i];
                 sName = item.Name;
                 sUrl = Utils.GetResizeFile(item.File, 2, 1000, 100); //VSW.Lib.Global.Utils.GetLinkFile(item.File);
                  %>
                <div class="item <%= i == 0? "active" : "" %>">
                <div class="img-banner-slide">
                  <a href="<%=item.URL %>"><img src="<%=sUrl%>" alt="<%=sName %>" class="img-responsive" style="max-width:100%"></a>
                 </div>
                  <%--<div class="carousel-caption">
                    <a href="<%=item.URL %>"><%= sName%></a>
                    <p><%= item.Description %></p>
                  </div>--%>
                </div>
          <%   }
               %>
          </div>

          <!-- Left and right controls -->
        <%--  <a class="left carousel-control" href="#myCarousel-Banner" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a class="right carousel-control" href="#myCarousel-Banner" role="button" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>--%>
        </div>
    </div>
    <!-- End Slide in Banner-->
  </div>

</div>
<ul class="icon2 sub-icon2 profile_img hidden">
	<li><a class="active-icon c2" href="#"> </a>
		<ul class="sub-icon2 list">
			<li><h3>Products</h3><a href=""></a></li>
			<li><p>Lorem ipsum dolor sit amet, consectetuer  <a href="">adipiscing elit, sed diam</a></p></li>
		</ul>
	</li>
</ul>
<div class="clear"></div>
<style type="text/css">
    /*.cssmenu{float:right !important;}*/
    .cssmenu1 ul li{display:inline-block !important;}
    .cssmenu1 ul li a{text-transform:none !important;}
    .img-banner-slide{}
</style>