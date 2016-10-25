<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    
 %><link href="../../Content/plugins/bootstrap/css/bootstrap.css" rel="stylesheet"
    type="text/css" />
<div id='<%=ViewBag.ModuleId %>' class="div-category-homepage">
    <a data-toggle="tooltip" data-placement="bottom" title="Tooltip ở đây">
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a data-toggle="tooltip" data-placement="bottom" title="Tooltip ở đây">
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a data-toggle="tooltip" data-placement="bottom" title="Tooltip ở đây">
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a data-toggle="tooltip" data-placement="bottom" title="Tooltip ở đây">
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a>
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a>
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a>
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a>
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a>
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
    <a>
        <img alt="Alternate Text" src="http://admin.vineco.net.vn/uploads/files/image016.png" class="img-responsive" />
    </a>
</div>
<style type="text/css">
    .div-category-homepage{text-align:center;}
    .div-category-homepage a
    {
        display:inline-block;
        margin-right: 20px;
        margin-bottom: 20px;
        transform: perspective(2000px);
        border: 1px solid #eee;
        border-radius: 100%;
        overflow: hidden;
        z-index: 999;
        width: 150px;
        height: 150px;
    }
</style>
<script type="text/javascript">
    $(".div-category-homepage a[data-toggle='tooltip']").tooltip();
</script>
<%--<div class="content-bottom">
    <div class="box1">
        <div class="col_1_of_3 span_1_of_3">
            <a href="single.html">
                <div class="view view-fifth">
                    <div class="top_box">
                        <h3 class="m_1">
                            Lorem ipsum dolor sit amet</h3>
                        <p class="m_2">
                            Lorem ipsum</p>
                        <div class="grid_img">
                            <div class="css3">
                                <img src="/Content/plugins/adidas-pack/images/pic.jpg" alt="" /></div>
                            <div class="mask">
                                <div class="info">
                                    Quick View</div>
                            </div>
                        </div>
                        <div class="price">
                            £480</div>
                    </div>
                </div>
                <span class="rating">
                    <input type="radio" class="rating-input" id="rating-input-1-5" name="rating-input-1">
                    <label for="rating-input-1-5" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-4" name="rating-input-1">
                    <label for="rating-input-1-4" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-3" name="rating-input-1">
                    <label for="rating-input-1-3" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-2" name="rating-input-1">
                    <label for="rating-input-1-2" class="rating-star">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-1" name="rating-input-1">
                    <label for="rating-input-1-1" class="rating-star">
                    </label>
                    &nbsp; (45) </span>
                <ul class="list">
                    <li>
                        <img src="/Content/plugins/adidas-pack/images/plus.png" alt="" />
                        <ul class="icon1 sub-icon1 profile_img">
                            <li><a class="active-icon c1" href="#">Add To Bag </a>
                                <ul class="sub-icon1 list">
                                    <li>
                                        <h3>
                                            sed diam nonummy</h3>
                                        <a href=""></a></li>
                                    <li>
                                        <p>
                                            Lorem ipsum dolor sit amet, consectetuer <a href="">adipiscing elit, sed diam</a></p>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </a>
        </div>
        <div class="col_1_of_3 span_1_of_3">
            <a href="single.html">
                <div class="view view-fifth">
                    <div class="top_box">
                        <h3 class="m_1">
                            Lorem ipsum dolor sit amet</h3>
                        <p class="m_2">
                            Lorem ipsum</p>
                        <div class="grid_img">
                            <div class="css3">
                                <img src="/Content/plugins/adidas-pack/images/pic1.jpg" alt="" /></div>
                            <div class="mask">
                                <div class="info">
                                    Quick View</div>
                            </div>
                        </div>
                        <div class="price">
                            £480</div>
                    </div>
                </div>
                <span class="rating">
                    <input type="radio" class="rating-input" id="rating-input-1-5" name="rating-input-1">
                    <label for="rating-input-1-5" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-4" name="rating-input-1">
                    <label for="rating-input-1-4" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-3" name="rating-input-1">
                    <label for="rating-input-1-3" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-2" name="rating-input-1">
                    <label for="rating-input-1-2" class="rating-star">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-1" name="rating-input-1">
                    <label for="rating-input-1-1" class="rating-star">
                    </label>
                    &nbsp; (45) </span>
                <ul class="list">
                    <li>
                        <img src="/Content/plugins/adidas-pack/images/plus.png" alt="" />
                        <ul class="icon1 sub-icon1 profile_img">
                            <li><a class="active-icon c1" href="#">Add To Bag </a>
                                <ul class="sub-icon1 list">
                                    <li>
                                        <h3>
                                            sed diam nonummy</h3>
                                        <a href=""></a></li>
                                    <li>
                                        <p>
                                            Lorem ipsum dolor sit amet, consectetuer <a href="">adipiscing elit, sed diam</a></p>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </a>
        </div>
        <div class="col_1_of_3 span_1_of_3">
            <a href="single.html">
                <div class="view view-fifth">
                    <div class="top_box">
                        <h3 class="m_1">
                            Lorem ipsum dolor sit amet</h3>
                        <p class="m_2">
                            Lorem ipsum</p>
                        <div class="grid_img">
                            <div class="css3">
                                <img src="/Content/plugins/adidas-pack/images/pic2.jpg" alt="" /></div>
                            <div class="mask">
                                <div class="info">
                                    Quick View</div>
                            </div>
                        </div>
                        <div class="price">
                            £480</div>
                    </div>
                </div>
                <span class="rating">
                    <input type="radio" class="rating-input" id="rating-input-1-5" name="rating-input-1">
                    <label for="rating-input-1-5" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-4" name="rating-input-1">
                    <label for="rating-input-1-4" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-3" name="rating-input-1">
                    <label for="rating-input-1-3" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-2" name="rating-input-1">
                    <label for="rating-input-1-2" class="rating-star">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-1" name="rating-input-1">
                    <label for="rating-input-1-1" class="rating-star">
                    </label>
                    &nbsp; (45) </span>
                <ul class="list">
                    <li>
                        <img src="/Content/plugins/adidas-pack/images/plus.png" alt="" />
                        <ul class="icon1 sub-icon1 profile_img">
                            <li><a class="active-icon c1" href="#">Add To Bag </a>
                                <ul class="sub-icon1 list">
                                    <li>
                                        <h3>
                                            sed diam nonummy</h3>
                                        <a href=""></a></li>
                                    <li>
                                        <p>
                                            Lorem ipsum dolor sit amet, consectetuer <a href="">adipiscing elit, sed diam</a></p>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </a>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="box1">
        <div class="col_1_of_3 span_1_of_3">
            <a href="single.html">
                <div class="view view-fifth">
                    <div class="top_box">
                        <h3 class="m_1">
                            Lorem ipsum dolor sit amet</h3>
                        <p class="m_2">
                            Lorem ipsum</p>
                        <div class="grid_img">
                            <div class="css3">
                                <img src="/Content/plugins/adidas-pack/images/pic3.jpg" alt="" /></div>
                            <div class="mask">
                                <div class="info">
                                    Quick View</div>
                            </div>
                        </div>
                        <div class="price">
                            £480</div>
                    </div>
                </div>
                <span class="rating">
                    <input type="radio" class="rating-input" id="rating-input-1-5" name="rating-input-1">
                    <label for="rating-input-1-5" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-4" name="rating-input-1">
                    <label for="rating-input-1-4" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-3" name="rating-input-1">
                    <label for="rating-input-1-3" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-2" name="rating-input-1">
                    <label for="rating-input-1-2" class="rating-star">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-1" name="rating-input-1">
                    <label for="rating-input-1-1" class="rating-star">
                    </label>
                    &nbsp; (45) </span>
                <ul class="list">
                    <li>
                        <img src="/Content/plugins/adidas-pack/images/plus.png" alt="" />
                        <ul class="icon1 sub-icon1 profile_img">
                            <li><a class="active-icon c1" href="#">Add To Bag </a>
                                <ul class="sub-icon1 list">
                                    <li>
                                        <h3>
                                            sed diam nonummy</h3>
                                        <a href=""></a></li>
                                    <li>
                                        <p>
                                            Lorem ipsum dolor sit amet, consectetuer <a href="">adipiscing elit, sed diam</a></p>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </a>
        </div>
        <div class="col_1_of_3 span_1_of_3">
            <a href="single.html">
                <div class="view view-fifth">
                    <div class="top_box">
                        <h3 class="m_1">
                            Lorem ipsum dolor sit amet</h3>
                        <p class="m_2">
                            Lorem ipsum</p>
                        <div class="grid_img">
                            <div class="css3">
                                <img src="/Content/plugins/adidas-pack/images/pic4.jpg" alt="" /></div>
                            <div class="mask">
                                <div class="info">
                                    Quick View</div>
                            </div>
                        </div>
                        <div class="price">
                            £480</div>
                    </div>
                </div>
                <span class="rating">
                    <input type="radio" class="rating-input" id="rating-input-1-5" name="rating-input-1">
                    <label for="rating-input-1-5" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-4" name="rating-input-1">
                    <label for="rating-input-1-4" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-3" name="rating-input-1">
                    <label for="rating-input-1-3" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-2" name="rating-input-1">
                    <label for="rating-input-1-2" class="rating-star">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-1" name="rating-input-1">
                    <label for="rating-input-1-1" class="rating-star">
                    </label>
                    &nbsp; (45) </span>
                <ul class="list">
                    <li>
                        <img src="/Content/plugins/adidas-pack/images/plus.png" alt="" />
                        <ul class="icon1 sub-icon1 profile_img">
                            <li><a class="active-icon c1" href="#">Add To Bag </a>
                                <ul class="sub-icon1 list">
                                    <li>
                                        <h3>
                                            sed diam nonummy</h3>
                                        <a href=""></a></li>
                                    <li>
                                        <p>
                                            Lorem ipsum dolor sit amet, consectetuer <a href="">adipiscing elit, sed diam</a></p>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </a>
        </div>
        <div class="col_1_of_3 span_1_of_3">
            <a href="single.html">
                <div class="view view-fifth">
                    <div class="top_box">
                        <h3 class="m_1">
                            Lorem ipsum dolor sit amet</h3>
                        <p class="m_2">
                            Lorem ipsum</p>
                        <div class="grid_img">
                            <div class="css3">
                                <img src="/Content/plugins/adidas-pack/images/pic5.jpg" alt="" /></div>
                            <div class="mask">
                                <div class="info">
                                    Quick View</div>
                            </div>
                        </div>
                        <div class="price">
                            £480</div>
                    </div>
                </div>
                <span class="rating">
                    <input type="radio" class="rating-input" id="rating-input-1-5" name="rating-input-1">
                    <label for="rating-input-1-5" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-4" name="rating-input-1">
                    <label for="rating-input-1-4" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-3" name="rating-input-1">
                    <label for="rating-input-1-3" class="rating-star1">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-2" name="rating-input-1">
                    <label for="rating-input-1-2" class="rating-star">
                    </label>
                    <input type="radio" class="rating-input" id="rating-input-1-1" name="rating-input-1">
                    <label for="rating-input-1-1" class="rating-star">
                    </label>
                    &nbsp; (45) </span>
                <ul class="list">
                    <li>
                        <img src="/Content/plugins/adidas-pack/images/plus.png" alt="" />
                        <ul class="icon1 sub-icon1 profile_img">
                            <li><a class="active-icon c1" href="#">Add To Bag </a>
                                <ul class="sub-icon1 list">
                                    <li>
                                        <h3>
                                            sed diam nonummy</h3>
                                        <a href=""></a></li>
                                    <li>
                                        <p>
                                            Lorem ipsum dolor sit amet, consectetuer <a href="">adipiscing elit, sed diam</a></p>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </a>
        </div>
        <div class="clear">
        </div>
    </div>
</div>--%>