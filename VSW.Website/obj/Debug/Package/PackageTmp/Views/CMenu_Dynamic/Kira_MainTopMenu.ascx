<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModMenu_DynamicEntity>;
%>
<div class="header-bottom">
    <div class="container">
        <!--menu re-->
        <div id="cssmenu">
            <div id="menu-button">
                Menu</div>
            <ul>
                <li><a href="#">Home</a></li>
                <li class="active has-sub"><span class="submenu-button"></span><a href="#">Products</a>
                    <ul>
                        <li class="has-sub"><span class="submenu-button"></span><a href="#">Product 1</a>
                            <ul>
                                <li><a href="#">Sub Product</a></li>
                                <li><a href="#">Sub Product</a></li>
                            </ul>
                        </li>
                        <li class="has-sub"><span class="submenu-button"></span><a href="#">Product 2</a>
                            <ul>
                                <li><a href="#">Sub Product</a></li>
                                <li><a href="#">Sub Product</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li><a href="#">About</a></li>
                <li><a href="#">Contact</a></li>
            </ul>
        </div>
        <!--menu re-->
        <div class="menu">
            <ul>
                <li class="active"><a href="#">HOME</a></li>
                <li><a href="#">ELECTRONICS</a></li>
                <li><a href="#">MOBILE</a></li>
                <li><a href="#">FASHION</a></li>
                <li><a href="#">ABOUT</a></li>
                <li><a href="#">CONTACT US</a></li>
                <li><a href="#">BLOG</a></li>
            </ul>
            <ul>
            </ul>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
