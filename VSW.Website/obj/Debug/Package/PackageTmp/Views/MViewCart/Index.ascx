<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var _Cart = ViewBag.Cart as Cart;
    var model = ViewBag.Model as MViewCartModel;

    string sTrList = string.Empty;
    int iSTT = 0;
    int iRow = 0;
    string sImageLink = string.Empty;
    string sLinkProduct = string.Empty;
    long longTotalPrice = 0;
    double PriceUsed = 0;
    int dCount = 0;
    int dVAT = 0;

    //VSW.Lib.Global.Cart _Cart1 = new VSW.Lib.Global.Cart();
    //_Cart1.RemoveAll();
    //_Cart1.Save();
    if (_Cart.Items.Count <= 0)
    {
        sTrList = "<tr><td colspan='8'>Chưa có sản phẩm nào trong giỏ hàng</td></tr>";
    }
    else
        foreach (CartItem itemCart in _Cart.Items)
        {
            sImageLink = Utils.GetResizeFile(itemCart.File, 2, 50, 50);
            sLinkProduct = ViewPage.GetURL(itemCart.MenuID, itemCart.Code);
            dVAT = 0;

            sTrList += "<tr class='row" + iRow + "'>";
            sTrList += "<td class='cart-td-stt'>";
            sTrList += "<span>" + (++iSTT) + ".</span>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-img'>";
            sTrList += "<a href='" + sLinkProduct + "'><img src='" + sImageLink + "'></img></a>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-ten'>";
            sTrList += "<a href='" + sLinkProduct + "'><span>" + itemCart.Name + "</span></a>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-soluong'>";
            sTrList += "<input name='ArrCount' id='ArrCount' type='text' value='" + itemCart.Quantity + "' size='1' maxlength='3'/>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-price'>";
            // Xem có hiển thị giá hay không
            if (itemCart.ShowPrice)
                sTrList += "<span>" + string.Format("{0:###,##0}", itemCart.Price) + "</span>";
            else
                sTrList += "<span class='current_price_notshow'></span>";

            sTrList += "</td>";
            sTrList += "<td class='cart-td-pricesale'>";

            if (itemCart.PriceSale > 0)
            {
                sTrList += "<span>" + string.Format("{0:###,##0}", itemCart.PriceSale) + "</span>";
                PriceUsed = itemCart.PriceSale;
            }
            else
            {
                sTrList += "<span>" + string.Format("{0:###,##0}", itemCart.Price) + "</span>";
                PriceUsed = itemCart.Price;
            }

            sTrList += "</td>";

            sTrList += "<td class='cart-td-vat'>";
            sTrList += "<span>" + (itemCart.ShowVAT == false ? "0%" : (itemCart.VAT == false ? "10%" : "0%")) + "</span>";
            sTrList += "</td>";

            sTrList += "<td class='cart-td-delete'>";
            sTrList += "<a href='javascript:void(0)' productid='" + itemCart.ProductID + "'><img class='cart-td-delete-img'/><a>";
            sTrList += "</td>";
            sTrList += "</tr>";

            // Cộng dồn số lượng
            dCount = (int)PriceUsed * itemCart.Quantity;

            if (itemCart.ShowVAT && itemCart.VAT == false)
                // 10% VAT
                dVAT = (dCount * 10) / 100;

            // VAT (Nếu có)
            longTotalPrice += (dCount + dVAT);

            iRow++;
            if (iRow > 1)
                iRow = 0;
        }
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<div class="box100">
    <script src="/CP/Content/add/js/cp_v1.js" type="text/javascript"></script>
    <script type="text/javascript">
        linkPost = '<%=ResolveUrl("~/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=removefromcart")%>';
        $(document).ready(function () {
            $(".cart-td-delete a").click(function () {

                if (!confirm("Xóa sản phẩm khỏi giỏ hàng?"))
                    return false;

                Product_RemoveProductFromCart($(this).attr('productid'), this);
                return false;
            });

            $(".cart-function-update").click(function () {
                vsw_exec_cmd('[Update]');

                return false;
            });
        });
    </script>
    <div class="DefaultModuleContent">
        <div class="defaultContentTitle TitleContent title">
            <div class="title">
                <%= ViewBag.Title %></div>
        </div>
        <div class="defaultContentDetail defaultContent">
            <table border="0" cellpadding="0" class="adminlist" cellspacing="1">
                <thead>
                    <tr>
                        <th class="cart-th-stt">
                            STT
                        </th>
                        <th class="cart-th-img">
                            Ảnh
                        </th>
                        <th class="cart-th-ten">
                            Tên sản phẩm
                        </th>
                        <th class="cart-th-soluong">
                            Số lượng
                        </th>
                        <th class="cart-th-price">
                            Giá gốc
                            <br />
                            (VNĐ)
                        </th>
                        <th class="cart-th-pricesale">
                            Giá khuyến mại<br />
                            (VNĐ)
                        </th>
                        <th class="cart-th-vat">
                            VAT
                        </th>
                        <th class="cart-th-delete">
                            Xóa
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%=sTrList%>
                    <tr>
                        <td colspan="5" style="text-align: right">
                            Tổng giá trị:
                        </td>
                        <td colspan="3" style="text-align: right">
                            <span class="cart-totalprice">
                                <%=string.Format("{0:###,##0}", longTotalPrice)%></span> (VNĐ)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" class="cart-td-function">
                            <%if (_Cart.Items.Count <= 0)
                              {%>
                            <a href="/">
                                <img class="cart-homeback" alt="Quay về trang chủ" /><span>Quay về trang chủ</span>
                            </a>
                            <%}
                              else
                              {%>
                            <a href="/">
                                <img class="cart-continue-shopping" alt="Tiếp tục mua hàng" />
                                <span>Tiếp tục mua hàng</span> </a><a href="javascript:void(0)" class="cart-function-update">
                                    <img class="cart-updatecart" alt="Cập nhật" /><span>Cập nhật</span> </a>
                            <a href="/<%=ViewPage.CurrentLang.Code %>/gio-hang/gui-don-hang.aspx">
                                <img class="cart-sentcartdata" alt="Gửi đơn đặt" />
                                <span>Gửi đơn đặt</span> </a>
                            <%} %>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="defaultFooter cate-menu-footer">
        </div>
    </div>
</div>
</form>
