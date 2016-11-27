<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var listItem = ViewBag.Data as List<ModMenu_DynamicEntity>; 
    var listAllPage = ViewBag.ListAllSysPage as List<SysPageEntity>;
   %>
<div class="footer">
    <ul class="navi-main menuFooter">
    <%for (int i = 0; listItem != null && i < listItem.Count; i++ )
    { 
          // var page = listAllPage.Where(o=>o.ID == listItem[i].SysPageID).SingleOrDefault(); page = page == null? new SysPageEntity() : page;
        var link = string.IsNullOrEmpty(listItem[i].Url) ? ViewPage.GetPageURL(listItem[i].SysPageID, listAllPage) : listItem[i].Url;
     %>
        <li><a href="<%=link %>"><%=listItem[i].Name %></a></li> <%if (i != listItem.Count - 1){%><li>|</li> <%} %>
<%} %>
        <%--<li><a href="/gioi-thieu/gioi-thieu-chung">Giới thiệu</a></li>
        <li>|</li>
        <li><a href="/san-pham">Sản phẩm</a></li>
        <li>|</li>
        <li><a href="/n/cong-nghe">Công nghệ</a></li>
        <li>|</li>
        <li><a href="/n/nong-truong">Nông trường</a></li>
        <li>|</li>
        <li><a href="/n/tin-tuc">Tin tức</a></li>
        <li>|</li>
        <li><a href="/n/dinh-duong">Dinh dưỡng</a></li>
        <li>|</li>
        <li><a href="/n/tuyen-dung">Tuyển dụng</a></li>
        <li>|</li>
        <li><a href="/lien-he">Liên hệ</a></li>
        <li>|</li>--%>
    </ul>
    <div class="div-footer-p hidden-xs hidden-sm">
        <%--<p>
            Copyright 2014 by Vingroup All rights reserved&nbsp;&nbsp;Times City, Minh Khai,
            Hà Nội</p>
        <p>
            Điện thoại: +84 (0) 963 817 825 / +84 (0) 981 611 911 - Email: info@vineco.net.vn</p>--%>
            {RS:Web_Footer}
    </div>
    <div class="clearfix">
    </div>
</div>
<%--<div class="footer">
        <div class="footer-top">
            <div class="wrap">
                <div class="col_1_of_footer-top span_1_of_footer-top">
                    <ul class="f_list">
                        <li>
                            <img src="/Content/plugins/adidas-pack/images/f_icon.png" alt="" /><span class="delivery">Free delivery
                                on all orders over £100*</span></li>
                    </ul>
                </div>
                <div class="col_1_of_footer-top span_1_of_footer-top">
                    <ul class="f_list">
                        <li>
                            <img src="/Content/plugins/adidas-pack/images/f_icon1.png" alt="" /><span class="delivery">Customer Service
                                :<span class="orange"> (800) 000-2587 (freephone)</span></span></li>
                    </ul>
                </div>
                <div class="col_1_of_footer-top span_1_of_footer-top">
                    <ul class="f_list">
                        <li>
                            <img src="/Content/plugins/adidas-pack/images/f_icon2.png" alt="" /><span class="delivery">Fast delivery
                                & free returns</span></li>
                    </ul>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="footer-middle">
            <div class="wrap">
                <div class="section group">
                    <div class="col_1_of_middle span_1_of_middle">
                        <dl id="sample" class="dropdown">
                            <dt><a href="#"><span>Please Select a Country</span></a></dt>
                            <dd>
                                <ul>
                                    <li><a href="#">Australia<img class="flag" src="/Content/plugins/adidas-pack/images/as.png" alt="" /><span
                                        class="value">AS</span></a></li>
                                    <li><a href="#">Sri Lanka<img class="flag" src="/Content/plugins/adidas-pack/images/srl.png" alt="" /><span
                                        class="value">SL</span></a></li>
                                    <li><a href="#">Newziland<img class="flag" src="/Content/plugins/adidas-pack/images/nz.png" alt="" /><span
                                        class="value">NZ</span></a></li>
                                    <li><a href="#">Pakistan<img class="flag" src="/Content/plugins/adidas-pack/images/pk.png" alt="" /><span
                                        class="value">Pk</span></a></li>
                                    <li><a href="#">United Kingdom<img class="flag" src="/Content/plugins/adidas-pack/images/uk.png" alt="" /><span
                                        class="value">UK</span></a></li>
                                    <li><a href="#">United States<img class="flag" src="/Content/plugins/adidas-pack/images/us.png" alt="" /><span
                                        class="value">US</span></a></li>
                                </ul>
                            </dd>
                        </dl>
                    </div>
                    <div class="col_1_of_middle span_1_of_middle">
                        <ul class="f_list1">
                            <li><span class="m_8">Sign up for email and Get 15% off</span>
                                <div class="search">
                                    <input type="text" name="s" class="textbox" value="Search" onfocus="this.value = '';"
                                        onblur="if (this.value == '') {this.value = 'Search';}">
                                    <input type="submit" value="Subscribe" id="submit" name="submit">
                                    <div id="response">
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
        <div class="footer-bottom">
            <div class="wrap">
                <div class="section group">
                    <div class="col_1_of_5 span_1_of_5">
                        <h3 class="m_9">
                            Shop</h3>
                        <ul class="sub_list">
                            <h4 class="m_10">
                                Men</h4>
                            <li><a href="shop.html">Men's Shoes</a></li>
                            <li><a href="shop.html">Men's Clothing</a></li>
                            <li><a href="shop.html">Men's Accessories</a></li>
                        </ul>
                        <ul class="sub_list">
                            <h4 class="m_10">
                                Women</h4>
                            <li><a href="shop.html">Women's Shoes</a></li>
                            <li><a href="shop.html">Women's Clothing</a></li>
                            <li><a href="shop.html">Women's Accessories</a></li>
                        </ul>
                        <ul class="sub_list">
                            <h4 class="m_10">
                                Kids</h4>
                            <li><a href="shop.html">Kids Shoes</a></li>
                            <li><a href="shop.html">Kids Clothing</a></li>
                            <li><a href="shop.html">Kids Accessories</a></li>
                        </ul>
                        <ul class="sub_list">
                            <h4 class="m_10">
                                style</h4>
                            <li><a href="shop.html">Porsche Design Sport</a></li>
                            <li><a href="shop.html">Porsche Design Shoes</a></li>
                            <li><a href="shop.html">Porsche Design Clothing</a></li>
                        </ul>
                        <ul class="sub_list">
                            <h4 class="m_10">
                                Adidas Neo Label</h4>
                            <li><a href="shop.html">Adidas NEO Shoes</a></li>
                            <li><a href="shop.html">Adidas NEO Clothing</a></li>
                        </ul>
                        <ul class="sub_list1">
                            <h4 class="m_10">
                                Customise</h4>
                            <li><a href="shop.html">mi adidas</a></li>
                            <li><a href="shop.html">mi team</a></li>
                            <li><a href="shop.html">new arrivals</a></li>
                        </ul>
                    </div>
                    <div class="col_1_of_5 span_1_of_5">
                        <h3 class="m_9">
                            Sports</h3>
                        <ul class="list1">
                            <li><a href="shop.html">Basketball</a></li>
                            <li><a href="shop.html">Football</a></li>
                            <li><a href="shop.html">Football Boots</a></li>
                            <li><a href="shop.html">Predator</a></li>
                            <li><a href="shop.html">F50</a></li>
                            <li><a href="shop.html">Football Clothing</a></li>
                            <li><a href="shop.html">Golf</a></li>
                            <li><a href="shop.html">Golf Shoes</a></li>
                            <li><a href="shop.html">Golf Clothing</a></li>
                            <li><a href="shop.html">Outdoor</a></li>
                            <li><a href="shop.html">Outdoor Shoes</a></li>
                            <li><a href="shop.html">Outdoor Clothing</a></li>
                            <li><a href="shop.html">Rugby</a></li>
                            <li><a href="shop.html">Running</a></li>
                            <li><a href="shop.html">Running Shoes</a></li>
                            <li><a href="shop.html">Boost</a></li>
                            <li><a href="shop.html">Supernova</a></li>
                            <li><a href="shop.html">Running Clothing</a></li>
                            <li><a href="shop.html">Swimming</a></li>
                            <li><a href="shop.html">Tennis</a></li>
                            <li><a href="shop.html">Tennis Shoes</a></li>
                            <li><a href="shop.html">Tennis Clothing</a></li>
                            <li><a href="shop.html">Training</a></li>
                            <li><a href="shop.html">Training Shoes</a></li>
                            <li><a href="shop.html">Training Clothing</a></li>
                            <li><a href="shop.html">Training Accessories</a></li>
                            <li><a href="shop.html">miCoach</a></li>
                            <li><a href="shop.html">All Sports</a></li>
                        </ul>
                    </div>
                    <div class="col_1_of_5 span_1_of_5">
                        <h3 class="m_9">
                            Originals</h3>
                        <ul class="list1">
                            <li><a href="shop.html">Originals Shoes</a></li>
                            <li><a href="shop.html">Gazelle</a></li>
                            <li><a href="shop.html">Samba</a></li>
                            <li><a href="shop.html">LA Trainer</a></li>
                            <li><a href="shop.html">Superstar</a></li>
                            <li><a href="shop.html">SL</a></li>
                            <li><a href="shop.html">ZX</a></li>
                            <li><a href="shop.html">Campus</a></li>
                            <li><a href="shop.html">Spezial</a></li>
                            <li><a href="shop.html">Dragon</a></li>
                            <li><a href="shop.html">Originals Clothing</a></li>
                            <li><a href="shop.html">Firebird</a></li>
                            <li><a href="shop.html">Originals Accessories</a></li>
                            <li><a href="shop.html">Men's Originals</a></li>
                            <li><a href="shop.html">Women's Originals</a></li>
                            <li><a href="shop.html">Kid's Originals</a></li>
                            <li><a href="shop.html">All Originals</a></li>
                        </ul>
                    </div>
                    <div class="col_1_of_5 span_1_of_5">
                        <h3 class="m_9">
                            Product Types</h3>
                        <ul class="list1">
                            <li><a href="shop.html">Shirts</a></li>
                            <li><a href="shop.html">Pants & Tights</a></li>
                            <li><a href="shop.html">Shirts</a></li>
                            <li><a href="shop.html">Jerseys</a></li>
                            <li><a href="shop.html">Hoodies & Track Tops</a></li>
                            <li><a href="shop.html">Bags</a></li>
                            <li><a href="shop.html">Jackets</a></li>
                            <li><a href="shop.html">Hi Tops</a></li>
                            <li><a href="shop.html">SweatShirts</a></li>
                            <li><a href="shop.html">Socks</a></li>
                            <li><a href="shop.html">Swimwear</a></li>
                            <li><a href="shop.html">Tracksuits</a></li>
                            <li><a href="shop.html">Hats</a></li>
                            <li><a href="shop.html">Football Boots</a></li>
                            <li><a href="shop.html">Other Accessories</a></li>
                            <li><a href="shop.html">Sandals & Flip Flops</a></li>
                            <li><a href="shop.html">Skirts & Dresseses</a></li>
                            <li><a href="shop.html">Balls</a></li>
                            <li><a href="shop.html">Watches</a></li>
                            <li><a href="shop.html">Fitness Equipment</a></li>
                            <li><a href="shop.html">Eyewear</a></li>
                            <li><a href="shop.html">Gloves</a></li>
                            <li><a href="shop.html">Sports Bras</a></li>
                            <li><a href="shop.html">Scarves</a></li>
                            <li><a href="shop.html">Shinguards</a></li>
                            <li><a href="shop.html">Underwear</a></li>
                        </ul>
                    </div>
                    <div class="col_1_of_5 span_1_of_5">
                        <h3 class="m_9">
                            Support</h3>
                        <ul class="list1">
                            <li><a href="shop.html">Store finder</a></li>
                            <li><a href="shop.html">Customer Service</a></li>
                            <li><a href="shop.html">FAQ</a></li>
                            <li><a href="shop.html">Online Shop Contact Us</a></li>
                            <li><a href="shop.html">about adidas Products</a></li>
                            <li><a href="shop.html">Size Charts </a></li>
                            <li><a href="shop.html">Ordering </a></li>
                            <li><a href="shop.html">Payment </a></li>
                            <li><a href="shop.html">Shipping </a></li>
                            <li><a href="shop.html">Returning</a></li>
                            <li><a href="shop.html">Using out Site</a></li>
                            <li><a href="shop.html">Delivery Terms</a></li>
                            <li><a href="shop.html">Site Map</a></li>
                            <li><a href="shop.html">Gift Card</a></li>
                        </ul>
                        <ul class="sub_list2">
                            <h4 class="m_10">
                                Company Info</h4>
                            <li><a href="shop.html">About Us</a></li>
                            <li><a href="shop.html">Careers</a></li>
                            <li><a href="shop.html">Press</a></li>
                        </ul>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
        <div class="copy">
            <div class="wrap">
                <p>
                    © All rights reserved | Template by&nbsp;<a href="http://w3layouts.com/"> W3Layouts</a></p>
            </div>
        </div>
    </div>--%>