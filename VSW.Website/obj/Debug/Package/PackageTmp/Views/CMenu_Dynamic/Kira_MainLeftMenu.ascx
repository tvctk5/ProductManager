<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModMenu_DynamicEntity>;
%>
<div class="slide-container clear">
    <div class="container">
        <div class="slide-sidebar">
            <h2 class="sidebar-title">
                <i class="fa fa-align-justify"></i>DANH MỤC SẢN PHẨM</h2>
            <ul>
                <li><a href="#"><i class="fa fa-laptop"></i>Electronics</a>
                    <ul>
                        <li><a href="#"><i class="fa fa-car"></i>Televisions</a></li>
                        <li><a href="#"><i class="fa fa-headphones"></i>Knitwear</a></li>
                        <li><a href="#"><i class="fa fa-car"></i>Audio &amp; Video</a></li>
                        <li><a href="#"><i class="fa fa-camera-retro"></i>Sweat Tops</a></li>
                        <li><a href="#"><i class="fa fa-gamepad"></i>Belts</a></li>
                        <li><a href="#"><i class="fa fa-car"></i>Watches</a></li>
                    </ul>
                </li>
                <li><a href="#"><i class="fa fa-car"></i>Televisions</a>
                    <ul>
                        <li><a href="#"><i class="fa fa-car"></i>Televisions</a></li>
                        <li><a href="#"><i class="fa fa-headphones"></i>Knitwear</a></li>
                        <li><a href="#"><i class="fa fa-car"></i>Audio &amp; Video</a></li>
                        <li><a href="#"><i class="fa fa-camera-retro"></i>Sweat Tops</a></li>
                        <li><a href="#"><i class="fa fa-gamepad"></i>Belts</a></li>
                        <li><a href="#"><i class="fa fa-car"></i>Watches</a></li>
                    </ul>
                </li>
                <li><a href="#"><i class="fa fa-headphones"></i>Knitwear</a>
                    <ul>
                        <li><a href="#"><i class="fa fa-car"></i>Televisions</a></li>
                        <li><a href="#"><i class="fa fa-headphones"></i>Knitwear</a></li>
                        <li><a href="#"><i class="fa fa-car"></i>Audio &amp; Video</a></li>
                        <li><a href="#"><i class="fa fa-camera-retro"></i>Sweat Tops</a></li>
                        <li><a href="#"><i class="fa fa-gamepad"></i>Belts</a></li>
                        <li><a href="#"><i class="fa fa-car"></i>Watches</a></li>
                    </ul>
                </li>
                <li><a href="#"><i class="fa fa-car"></i>Audio &amp; Video</a></li>
                <li><a href="#"><i class="fa fa-camera-retro"></i>Sweat Tops</a></li>
                <li><a href="#"><i class="fa fa-gamepad"></i>Belts</a></li>
                <li><a href="#"><i class="fa fa-car"></i>Watches</a></li>
                <li><a href="#"><i class="fa fa-plus-square-o"></i>More</a></li>
            </ul>
        </div>
    </div>
</div>
