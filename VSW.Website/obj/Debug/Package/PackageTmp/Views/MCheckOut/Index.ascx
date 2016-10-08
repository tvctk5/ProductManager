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
            sTrList += "<input readonly='readonly' name='ArrCount' id='ArrCount' type='text' value='" + itemCart.Quantity + "' size='1' maxlength='3'/>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-price'>";
            sTrList += "<span>" + string.Format("{0:###,##0}", itemCart.Price) + "</span>";
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

            sTrList += "<td class='cart-td-delete hide'>";
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

    // Lấy dữ liệu đăng nhập
    int CustomId = 0;
    ModProduct_CustomersEntity objCustomer = null;
    if (VSW.Lib.Global.Cookies.Exists("VSW.CustomerId"))
    {
        CustomId = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue("VSW.CustomerId"));
        // Lấy thông tin khách hàng
        objCustomer = ModProduct_CustomersService.Instance.GetByID(CustomId);
    }

    if (objCustomer == null)
        objCustomer = new ModProduct_CustomersEntity();
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<div class="box100">
    <script src="/CP/Content/add/js/cp_v1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var langCode = '<%=ViewPage.CurrentLang.Code %>';

        $(document).ready(function () {

            $(".redrect-login").click(function () {
                window.location.href = "/" + langCode + "/tai-khoan/dang-nhap.aspx";
            });

            $(".cart-btn-sentcartdata").click(function () {
                if (CheckValidateForm())
                    vsw_exec_cmd('[CheckOut]');

                //window.location.href = '/';
                return false;
            });

            // Copy thông tin
            $("#chkCopy").change(function () {
                var NguoiDat_FullName = $("#NguoiDat_FullName");
                var NguoiDat_Address = $("#NguoiDat_Address");
                var NguoiDat_Email = $("#NguoiDat_Email");
                var NguoiDat_PhoneNumber = $("#NguoiDat_PhoneNumber");
                var NguoiDat_Sex = $("input[name='NguoiDat_Sex']:checked");

                var NguoiNhan_FullName = $("#NguoiNhan_FullName");
                var NguoiNhan_Address = $("#NguoiNhan_Address");
                var NguoiNhan_Email = $("#NguoiNhan_Email");
                var NguoiNhan_PhoneNumber = $("#NguoiNhan_PhoneNumber");
                var NguoiNhan_Sex = $("input[name='NguoiNhan_Sex'][value='" + NguoiDat_Sex.val() + "']");

                var checked = $(this).is(":checked");

                // Sao chép
                if (checked == true) {
                    NguoiNhan_FullName.val(NguoiDat_FullName.val());
                    NguoiNhan_Address.val(NguoiDat_Address.val());
                    NguoiNhan_Email.val(NguoiDat_Email.val());
                    NguoiNhan_PhoneNumber.val(NguoiDat_PhoneNumber.val());
                    NguoiNhan_Sex.attr("checked", "checked");

                    //                    // Ko cho chỉnh sửa
                    //                    NguoiNhan_FullName.attr("readonly", "readonly");
                    //                    NguoiNhan_Address.attr("readonly", "readonly");
                    //                    NguoiNhan_Email.attr("readonly", "readonly");
                    //                    NguoiNhan_PhoneNumber.attr("readonly", "readonly");
                    //                    $("input[name='NguoiNhan_Sex']").attr('disabled', 'disabled');
                }
                // Hiển thị control cho chỉnh sửa
                else {
                    //                    // Ko cho chỉnh sửa
                    //                    NguoiNhan_FullName.removeAttr("readonly");
                    //                    NguoiNhan_Address.removeAttr("readonly");
                    //                    NguoiNhan_Email.removeAttr("readonly");
                    //                    NguoiNhan_PhoneNumber.removeAttr("readonly");
                    //                    $("input[name='NguoiNhan_Sex']").removeAttr("disabled");
                }
            });
        });

    </script>
    <div class="DefaultModuleContent">
        <div class="defaultContentTitle TitleContent title">
            <div class="title">
                <%= ViewBag.Title %></div>
        </div>
        <div class="defaultContentDetail defaultContent">
            <div>
                <table border="0" cellpadding="0" cellspacing="1" class="adminlist">
                    <tr>
                        <td>
                            Hình thức thanh toán:
                            <select style="width: 50%" name="TransportId" id="TransportId">
                                <%= Utils.ShowDDLList(ModProduct_PaymentService.Instance)%>
                            </select>
                        </td>
                        <td>
                            Hình thức vận chuyển:
                            <select style="width: 50%" name="PaymentId" id="PaymentId">
                                <%= Utils.ShowDDLList(ModProduct_TransportService.Instance)%>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <table border="0" cellpadding="1" class="adminlist">
                                <thead>
                                    <tr>
                                        <th colspan="2">
                                            Thông tin người đặt hàng
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td colspan="2" style="font-style: italic">
                                            <%if (CustomId <= 0)
                                              {%>
                                            Đã có tài khoản, bấm <a href="javascript:void(0)" class="redrect-login">vào đây</a>
                                            để đăng nhập.
                                            <%}
                                              else
                                              {%>
                                            Đã lấy thông tin khách hàng đăng nhập
                                            <%--<div style="height:15px;"></div>--%>
                                            <%} %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Họ tên:
                                        </td>
                                        <td style="width: 100%">
                                            <input class="text_input" type="text" name="NguoiDat_FullName" id="NguoiDat_FullName" value="<%=objCustomer.FullName %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Giới tính:
                                        </td>
                                        <td>
                                            <input name="NguoiDat_Sex" type="radio" value='1' <%=objCustomer.Sex?"checked='checked'" : ""%> />
                                            Nữ
                                            <input name="NguoiDat_Sex" type="radio" value='0' <%=objCustomer.Sex==false?"checked='checked'" : ""%> />
                                            Nam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Số điện thoại:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiDat_PhoneNumber" id="NguoiDat_PhoneNumber" value="<%=objCustomer.PhoneNumber %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Email:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiDat_Email" id="NguoiDat_Email" value="<%=objCustomer.Email%>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Địa chỉ:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiDat_Address" id="NguoiDat_Address" value="<%=objCustomer.Address%>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Ghi chú:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiDat_Note" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td style="width: 100%">
                            <table border="0" cellpadding="1" class="adminlist">
                                <thead>
                                    <tr>
                                        <th colspan="2">
                                            Thông tin người nhận
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td colspan="2" style="font-style: italic">
                                            <input type="checkbox" name="chkCopy" id="chkCopy" /><span>Thông tin người nhận giống
                                                với thông tin người gửi</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Họ tên:
                                        </td>
                                        <td style="width: 100%">
                                            <input class="text_input" type="text" name="NguoiNhan_FullName" id="NguoiNhan_FullName" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Giới tính:
                                        </td>
                                        <td>
                                            <input name="NguoiNhan_Sex" type="radio" value='1' checked="checked" />
                                            Nữ
                                            <input name="NguoiNhan_Sex" type="radio" value='0' />
                                            Nam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Số điện thoại:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiNhan_PhoneNumber" id="NguoiNhan_PhoneNumber" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Email:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiNhan_Email" id="NguoiNhan_Email" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Địa chỉ:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiNhan_Address" id="NguoiNhan_Address" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="line-height: 10px; height: 28px;">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table border="0" cellpadding="1" class="adminlist">
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
                            <th class="cart-th-delete hide">
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
                                    <span>Tiếp tục mua hàng</span> </a><a href="/vn/gio-hang.aspx">
                                        <img class="cart-edit" alt="Chỉnh sửa giỏ hàng" />
                                        <span>Chỉnh sửa giỏ hàng</span> </a><a href="javascript:void(0)" class="cart-btn-sentcartdata">
                                            <img class="cart-sentcartdata" alt="Gửi đơn đặt" />
                                            <span>Gửi đơn hàng</span> </a>
                                <%} %>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="defaultFooter cate-menu-footer">
        </div>
    </div>
</div>
<script type="text/javascript">

    function CheckValidateForm() {
        var bolNguoiDat_FullName = false;
        var bolNguoiDat_Address = false;
        var bolNguoiDat_Email = false;
        var bolNguoiDat_PhoneNumber = false;
        var bolNguoiNhan_FullName = false;
        var bolNguoiNhan_Address = false;
        var bolNguoiNhan_Email = false;
        var bolNguoiNhan_PhoneNumber = false;

        var Mess = "";
        var NguoiDat_FullName = $("#NguoiDat_FullName");
        var NguoiDat_Address = $("#NguoiDat_Address");
        var NguoiDat_Email = $("#NguoiDat_Email");
        var NguoiDat_PhoneNumber = $("#NguoiDat_PhoneNumber");

        var NguoiNhan_FullName = $("#NguoiNhan_FullName");
        var NguoiNhan_Address = $("#NguoiNhan_Address");
        var NguoiNhan_Email = $("#NguoiNhan_Email");
        var NguoiNhan_PhoneNumber = $("#NguoiNhan_PhoneNumber");

        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        // ----------------Thông tin người đặt hàng

        if (NguoiDat_FullName != null) {
            if (NguoiDat_FullName.val().trim() != "") {
                bolNguoiDat_FullName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên người đặt hàng";
            }
        }

        if (NguoiDat_Address != null) {
            if (NguoiDat_Address.val().trim() != "") {
                bolNguoiDat_Address = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Địa chỉ người đặt";
            }
        }

        if (NguoiDat_Email != null) {
            if (NguoiDat_Email.val() != "") {
                if (!filter.test(NguoiDat_Email.val())) {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Địa chỉ Email không hợp lệ"
                    bolNguoiDat_Email = false;
                }
                else
                    bolNguoiDat_Email = true;
            }
            else
                bolNguoiDat_Email = true;
        }
        else
            bolNguoiDat_Email = true;

        if (NguoiDat_PhoneNumber != null) {
            if (NguoiDat_PhoneNumber.val().trim() != "") {
                bolNguoiDat_PhoneNumber = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Số điện thoại người đặt";
            }
        }

        // ----------------Thông tin người nhận hàng

        if (NguoiNhan_FullName != null) {
            if (NguoiNhan_FullName.val().trim() != "") {
                bolNguoiNhan_FullName = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Tên người nhận hàng";
            }
        }

        if (NguoiNhan_Address != null) {
            if (NguoiNhan_Address.val().trim() != "") {
                bolNguoiNhan_Address = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Địa chỉ người nhận";
            }
        }

        if (NguoiNhan_Email != null) {
            if (NguoiNhan_Email.val() != "") {
                if (!filter.test(NguoiNhan_Email.val())) {
                    if (Mess != "")
                    { Mess = Mess + "\r\n"; }
                    Mess = Mess + " - Địa chỉ Email không hợp lệ"
                    bolNguoiNhan_Email = false;
                }
                else
                    bolNguoiNhan_Email = true;
            }
            else
                bolNguoiNhan_Email = true;
        }
        else
            bolNguoiNhan_Email = true;

        if (NguoiNhan_PhoneNumber != null) {
            if (NguoiNhan_PhoneNumber.val().trim() != "") {
                bolNguoiNhan_PhoneNumber = true;
            }
            else {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Yêu cầu nhập Số điện thoại người nhận";
            }
        }

        if (bolNguoiDat_FullName && bolNguoiDat_Address && bolNguoiDat_Email && bolNguoiDat_PhoneNumber &&
            bolNguoiNhan_FullName && bolNguoiNhan_Address && bolNguoiNhan_Email && bolNguoiNhan_PhoneNumber
        )
            return true;
        else {
            alert(Mess);
            // ------------ Người đặt
            if (bolNguoiDat_FullName == false) {
                NguoiDat_FullName.focus(); return false;
            }

            if (bolNguoiDat_Address == false) {
                NguoiDat_Address.focus(); return false;
            }

            if (bolNguoiDat_Email == false) {
                NguoiDat_Email.focus(); return false;
            }

            if (bolNguoiDat_PhoneNumber == false) {
                NguoiDat_PhoneNumber.focus(); return false;
            }

            // ------------ Người nhận
            if (bolNguoiNhan_FullName == false) {
                NguoiNhan_FullName.focus(); return false;
            }

            if (bolNguoiNhan_Address == false) {
                NguoiNhan_Address.focus(); return false;
            }

            if (bolNguoiNhan_Email == false) {
                NguoiNhan_Email.focus(); return false;
            }

            if (bolNguoiNhan_PhoneNumber == false) {
                NguoiNhan_PhoneNumber.focus(); return false;
            }
        }
    }

</script>
</form>
