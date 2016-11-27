<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var item = ViewBag.Data as ModProduct_InfoEntity;
    var listOther = ViewBag.Other as List<ModProduct_InfoEntity>;
    var lstSlideShow = ViewBag.SlideShow as List<ModProduct_SlideShowEntity>;
    var ListProperties = ViewBag.ListProperties as string;
    var ListAgent = ViewBag.ListAgent as string;
    var ListComment = ViewBag.ListComment as string;

    //var GiaBan = item.PriceSale > 0 ? item.PriceSale : item.Price;

    #region Lấy danh sách ảnh của sản phẩm
    string sImageList = string.Empty;
    if (lstSlideShow != null && lstSlideShow.Count > 0)
    {
        sImageList = "<ul>";
        foreach (var itemImage in lstSlideShow)
        {
            sImageList += "<li>";
            sImageList += "<a href='javascript:void(0)'>";
            sImageList += "<img src='" + Utils.GetResizeFile(itemImage.UrlFull, 2, 50, 50) + "' urlimg='" + VSW.Lib.Global.Utils.GetLinkFile(itemImage.UrlFull) + "' />";
            sImageList += "</a>";
            sImageList += "</li>";
        }

        sImageList += "</ul>";
    }
    #endregion

    #region Lấy ảnh mặc định
    string sImageDefaultView = string.Empty;
    if (!string.IsNullOrEmpty(item.File))
        sImageDefaultView = "<img class='imgDefaultView' src='" + VSW.Lib.Global.Utils.GetLinkFile(item.File) + "' width='100%' height='100%' title='" + item.Name + "' alt='" + item.Name + "'>";
    #endregion


    #region Hiển thị thông tin cơ bản của sản phẩm
    string sInfo_Basic = string.Empty;
    sInfo_Basic += "<p class='info-basic-top'>";
    sInfo_Basic += "<table border='0' cellpadding='0' cellspacing='0'>";
    sInfo_Basic += "<td>";
    sInfo_Basic += "</td>";
    sInfo_Basic += "</table>";
    sInfo_Basic += "</p>";

    // Giá
    sInfo_Basic = "<div class='detail-price'>";
    // Có hiển thị giá hay không
    if (item.ShowPrice)
    {
        if (VSW.Lib.Global.ConvertTool.CheckSaleOff(item))
        {
            sInfo_Basic += "<span class='price'>" + ConvertTool.ConvertToMoney(item.PriceSale) + "</span>";
            sInfo_Basic += "  (<span class='price-old'>" + ConvertTool.ConvertToMoney(item.Price) + "</span>)";
        }
        else
            sInfo_Basic += "<span class='price'>" + ConvertTool.ConvertToMoney(item.Price) + "</span>";
    }
    else
        // Hiển thị liên hệ giá
        sInfo_Basic += "<p class='current_price_notshow price'></p>";

    if (item.ShowVAT)
    {
        if (item.VAT)
            sInfo_Basic += "<div class='div-price-vat'><span class='price-vat'>(Giá đã bao gồm 10% VAT)</span></div>";
        else
            sInfo_Basic += "<div class='div-price-vat'><span class='price-vat'>(Giá chưa bao gồm 10% VAT)</span></div>";
    }
    sInfo_Basic += "</div>";

    // Bảo hành
    if (!string.IsNullOrEmpty(item.Warranty))
    {
        sInfo_Basic += "<div class='div-group-info-detail'><div class='group-info-detail'>Bảo hành: </div>";
        sInfo_Basic += "<div class='list-info-detail'>" + item.Warranty + "</div>";
        sInfo_Basic += "</div>";
    }

    // Quà tặng
    if (!string.IsNullOrEmpty(item.Gifts))
    {
        sInfo_Basic += "<div class='div-group-info-detail'><div class='group-info-detail'>Quà tặng: </div>";
        sInfo_Basic += "<div class='list-info-detail'>" + item.Gifts + "</div>";
        sInfo_Basic += "</div>";
    }

    // Khuyến mại
    if (!string.IsNullOrEmpty(item.Promotion))
    {
        sInfo_Basic += "<div class='div-group-info-detail'><div class='group-info-detail'>Khuyến mại: </div>";
        sInfo_Basic += "<div class='list-info-detail'>" + item.Promotion + "</div>";
        sInfo_Basic += "</div>";
    }

    // Chính sách
    if (!string.IsNullOrEmpty(item.Policy))
    {
        sInfo_Basic += "<div class='div-group-info-detail'><div class='group-info-detail'>Chính sách: </div>";
        sInfo_Basic += "<div class='list-info-detail'>" + item.Policy + "</div>";
        sInfo_Basic += "</div>";
    }

    // Thông tin nổi bật khác
    if (!string.IsNullOrEmpty(item.InfoHighlight))
    {
        sInfo_Basic += "<div class='div-group-info-detail'><div class='group-info-detail'>Thông tin nổi bật: </div>";
        sInfo_Basic += "<div class='list-info-detail'>" + item.InfoHighlight + "</div>";
        sInfo_Basic += "</div>";
    }

    #endregion

    #region Lấy link chia sẻ face book
    string LinkLikeShare_FaceBook = string.Empty;
    string LinkLikeShare_FaceBook_Replated = string.Empty;
    string Host = string.Empty;
    string LinkPage = string.Empty;

    // Link trang hiện tại
    LinkPage = ViewPage.ReturnPath;

    var lstResource = WebResourceService.Instance.CreateQuery().Where(o => o.Code == "Web_Facebook" || o.Code == "Web_Host").ToList();

    var objResource = lstResource.Where(o => o.Code == "Web_Host").SingleOrDefault();
    if (objResource != null)
        Host = Server.HtmlEncode(objResource.Value);

    objResource = lstResource.Where(o => o.Code == "Web_Facebook").SingleOrDefault();
    if (objResource != null)
        LinkLikeShare_FaceBook = objResource.Value;

    LinkLikeShare_FaceBook_Replated = LinkLikeShare_FaceBook.Replace("LINK", Host + LinkPage);
        
    #endregion
    
#region Lấy thông tin đăng nhập của khách hàng (Nếu có)
    // Lấy dữ liệu đăng nhập
    int CustomId = 0;
    ModProduct_CustomersEntity objCustomer = null;
    string sReadOnly = string.Empty;
    if (VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"))
    {
        CustomId = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue("VSW.CustomerId"));
        // Lấy thông tin khách hàng
        objCustomer = ModProduct_CustomersService.Instance.GetByID(CustomId);

        sReadOnly = " readonly='readonly' ";
    }

    if (objCustomer == null)
        objCustomer = new ModProduct_CustomersEntity();
#endregion
%>

<div>
    <div class="page-title title-background"><h4><%= ViewPage.CurrentPage.PageTitle%></h4></div>
    <div class="page-description">
        <p><%= item.NewsPost%></p>
    </div>
</div>
<style type="text/css">
.page-title{padding:10px; background-color:Blue;color:White;margin:5px 0px 5px 0px;}
</style>
<%--<div class="box100">
    <div class="DefaultModuleContent">
        <div class="defaultContentTitle TitleContent title">
            <div class="title">
                <%= ViewPage.CurrentPage.PageTitle%>
                <input type="hidden" value='<%=item.ID %>' id="ProductId" />
            </div>
        </div>
        <div class="defaultContentDetail defaultContent product-view-detail">
            <div class="info-image">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="td-image">
                            <div class="div-image-view">
                                <%=sImageDefaultView %>
                            </div>
                            <div class="div-image-list">
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        $("img[urlimg]").hover(function () {
                                            var urlimg = $(this).attr("urlimg");
                                            $("img.imgDefaultView").attr("src", urlimg);
                                            $("img[urlimg]").removeClass("li-hover-select-view");
                                            $(this).addClass("li-hover-select-view");
                                        });
                                    });
                                </script>
                                <%=sImageList%>
                            </div>
                        </td>
                        <td class="td-info-basic">
                            <div class="info-basic">
                                <%if (item.ShowPreview || item.ShowBuyCount || item.ShowCommentCount || ConvertTool.CheckDateIsNull(item.PostDate) == false)
                                  {%>
                                <div class="div-info-view-buy">
                                    <%if (VSW.Lib.Global.ConvertTool.CheckDateIsNull(item.PostDate) == false)
                                      {%>
                                    <p>
                                        <span>Ngày đăng:
                                            <%=item.PostDate.ToString("dd/MM/yyyy HH:mm")%></span></p>
                                    <%} %>
                                    <%if (item.ShowPreview)
                                      {%>
                                    <span>Lượt xem:
                                        <%=item.Preview %></span> &nbsp;|&nbsp;
                                    <%} %>
                                    <%if (item.ShowBuyCount)
                                      {%>
                                    <span>Lượt mua:
                                        <%=item.BuyCount %></span> &nbsp;|&nbsp;
                                    <%} %>
                                    <%if (item.ShowCommentCount)
                                      {%>
                                    <span>Bình luận:
                                        <%=item.CommentCount%></span> &nbsp;|&nbsp;
                                    <%} %>
                                    <%if (item.ShowLike)
                                      {%>
                                    <span>Thích:
                                        <%=item.LikeCount%></span> &nbsp;
                                    <%} %>
                                </div>
                                <%} %>
                                <%=sInfo_Basic%>
                                <div class="buy">
                                    <script type="text/javascript">
                                        linkPost = '<%=ResolveUrl("~/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=addtocart")%>';
                                        langCode = '<%=ViewPage.CurrentLang.Code %>';
                                        $(document).ready(function () {
                                            $(".btnMuaNgay").click(function () {
                                                Product_AddToCart('<%=item.ID %>', $("#CountBuy").val(), true);
                                                return false;
                                            });

                                            $(".btnThemVaoGioHang").click(function () {
                                                Product_AddToCart('<%=item.ID %>', $("#CountBuy").val(), false);
                                                return false;
                                            });
                                        });
                                    </script>
                                    <div class="div-count">
                                        <div class="div-count-label">
                                            Số lượng mua:</div>
                                        <div class="div-count-text">
                                            <input type="text" name="CountBuy" id="CountBuy" value="1" /></div>
                                    </div>
                                    <div class="div-buy-icon">
                                        <div class="div-buy-icon-buy">
                                            <a href="javascript:void(0)">
                                                <button type="button" class="btnMuaNgay">
                                                    <span class="lblMuaNgay">Mua ngay</span>
                                                </button>
                                            </a>
                                        </div>
                                        <div class="div-buy-icon-addtocard">
                                            <a href="javascript:void(0)">
                                                <button type="button" class="btnThemVaoGioHang">
                                                    <span class="lblThemVaoGioHang">Thêm vào giỏ hàng</span></button>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="div-compare">
                                        <span class='action-sub-checkbox'>
                                            <input type='checkbox' productid='<%=item.ID %>' class='input-compare-product' />
                                            <span class='action-sub action-sub-checkbox-label'>&nbsp;Chọn sản phẩm để so sánh</span>
                                        </span>
                                    </div>
                                    <div class="div-payment-online">
                                        <div class='div-group-info-detail'>
                                            <div class='group-info-detail'>
                                                Thanh toán trực tuyến:
                                            </div>
                                            <div class='list-info-detail'>
                                                <a href="#">
                                                    <img class="img-nganluong" alt="Thanh toán qua ngân lượng" /></a> <a>[Hướng dẫn]</a>
                                            </div>
                                            <div class='list-info-detail'>
                                                <a href="#">
                                                    <img class="img-baokim" alt="Thanh toán qua bảo kim" /></a> <a>[Hướng dẫn]</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div-info-like-share">
                                        <%=LinkLikeShare_FaceBook_Replated %>
                                    </div>
                                    <!--Danh sách các đại lý (nếu có)-->
                                    <%=ListAgent %>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="info-detail">
                <div class="info-detail-tab">
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("li.menu_bar-li").click(function () {
                                $("li.menu_bar-li").removeClass("menu_bar-seleted");
                                $(this).addClass("menu_bar-seleted");

                                $("div.div_tab_content_detail").addClass("hide");
                                var div_control_id = $(this).attr("control");
                                $(div_control_id).removeClass("hide");
                            });
                        });
                    </script>
                    <div class="menu_bar">
                        <ul>
                            <li control="#div_tab_content_01" class="menu_bar-li menu_bar-seleted"><span>Mô tả sản
                                phẩm</span></li>
                            <li control="#div_tab_content_02" class="menu_bar-li"><span>Thông tin kỹ thuật</span></li>
                            <li control="#div_tab_content_03" class="menu_bar-li"><span>Bình luận</span></li>
                        </ul>
                    </div>
                    <div class="detail_content">
                        <div id="div_tab_content_01" class="div_tab_content_detail">
                            <span>
                                <%=item.NewsPost%></span></div>
                        <div id="div_tab_content_02" class="div_tab_content_detail hide">
                            <%=ListProperties%>
                        </div>
                        <div id="div_tab_content_03" class="div_tab_content_detail hide">
                            <div class="div-form-sent-comment">
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        $("#btnSentComment").click(function () {
                                            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                                            var Comment_Name = $("#Comment_Name");
                                            var Comment_Email = $("#Comment_Email");
                                            var Comment_Content = $("#Comment_Content");

                                            if (Comment_Name==null || Comment_Name.val() == "") {
                                                alert("Yêu cầu nhập Họ và tên");
                                                Comment_Name.focus();
                                                return false;
                                            }

                                            if (Comment_Email == null || Comment_Email.val() == "") {
                                                alert("Yêu cầu nhập email");
                                                Comment_Email.focus();
                                                return false;
                                            }
                                            else {
                                                if (!filter.test(Comment_Email.val())) {
                                                    alert("Email không đúng định dạng");
                                                    Comment_Email.focus();
                                                    return false;
                                                }
                                            }

                                            if (Comment_Content == null || Comment_Content.val() == "") {
                                                alert("Yêu cầu nhập nội dung bình luận");
                                                Comment_Content.focus();
                                                return false;
                                            }

                                            // Gửi nội dung đi, list lại danh sách
                                            SentComment($(".div-form-sent-comment"), $(".div-comment-list"), $("#ProductId").val());
                                        });
                                    });
                                </script>
                                <table class="adminlist">
                                    <tr>
                                        <td class="tbl-form-sent-comment-label">
                                            Họ và tên:
                                        </td>
                                        <td class="tbl-form-sent-comment-text">
                                            <input type="text" name="Comment_Name" id="Comment_Name" maxlength="50" value="<%=objCustomer.FullName %>" <%=sReadOnly %>/>
                                            (<span class="require">*</span>)
                                        </td>
                                        <td class="tbl-form-sent-comment-label">
                                            Email:
                                        </td>
                                        <td class="tbl-form-sent-comment-text">
                                            <input type="text" name="Comment_Email" id="Comment_Email" maxlength="30" value="<%=objCustomer.Email %>" <%=sReadOnly %>/>
                                            (<span class="require">*</span>)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tbl-form-sent-comment-label">
                                            Địa chỉ:
                                        </td>
                                        <td class="tbl-form-sent-comment-text">
                                            <input type="text" name="Comment_Address" id="Comment_Address" maxlength="200" value="<%=objCustomer.Address %>" <%=sReadOnly %>/>
                                        </td>
                                        <td class="tbl-form-sent-comment-label">
                                            Số điện thoại:
                                        </td>
                                        <td class="tbl-form-sent-comment-text">
                                            <input type="text" name="Comment_PhoneNumber" id="Comment_PhoneNumber" maxlength="20" value="<%=objCustomer.PhoneNumber %>" <%=sReadOnly %>/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tbl-form-sent-comment-label">
                                            Bình luận:
                                        </td>
                                        <td class="tbl-form-sent-comment-text" colspan="3">
                                            <textarea name="Comment_Content" id="Comment_Content" style="width: 90%" rows="3"></textarea>
                                            (<span class="require">*</span>)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tbl-form-sent-comment-label">
                                        </td>
                                        <td class="tbl-form-sent-comment-text" colspan="3">
                                            <input type="button" id="btnSentComment" value="Gửi bình luận" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="div-comment-list">
                                <%=ListComment %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="defaultFooter cate-menu-footer">
        </div>
    </div>
</div>--%>
