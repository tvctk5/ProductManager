<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_OrderModel;
    var item = ViewBag.Data as ModProduct_OrderEntity;
    var lstOrder_Details = ViewBag.ListOrderDetails as List<ModProduct_Order_DetailsEntity>;

    string sTrList = string.Empty;
    int iSTT = 0;
    int iRow = 0;
    string sImageLink = string.Empty;
    string sLinkProduct = string.Empty;
    long longTotalPrice = 0;
    double PriceUsed = 0;
    int dCount = 0;
    int dVAT = 0;
    var ViewPage = new VSW.Lib.MVC.ViewPage();
    if (lstOrder_Details.Count <= 0)
    {
        sTrList = "<tr><td colspan='8'>Chưa có sản phẩm nào trong giỏ hàng</td></tr>";
    }
    else
        foreach (ModProduct_Order_DetailsEntity itemCart in lstOrder_Details)
        {
            sImageLink = Utils.GetResizeFile(itemCart.File, 2, 50, 50);
            sLinkProduct = "/" + CPViewPage.CurrentLang.Code + "/" + ViewPage.GetURL(itemCart.MenuID, itemCart.Code) + ".aspx";

            dVAT = 0;

            sTrList += "<tr class='row" + iRow + "'>";
            sTrList += "<td class='cart-td-stt'>";
            sTrList += "<span>" + (++iSTT) + ".</span>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-img'>";
            sTrList += "<a href='" + sLinkProduct + "' target='_blank'><img src='" + sImageLink + "'></img></a>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-ten'>";
            sTrList += "<a href='" + sLinkProduct + "' target='_blank'><span>" + itemCart.Name + "</span></a>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-soluong'>";
            sTrList += "<input readonly='readonly' name='ArrCount' id='ArrCount' type='text' value='" + itemCart.Quantity + "' size='1' maxlength='3'/>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-price'>";
            sTrList += "<span>" + string.Format("{0:###,##0}", itemCart.Frice) + "</span>";
            sTrList += "</td>";
            sTrList += "<td class='cart-td-pricesale'>";

            if (itemCart.PriceSale > 0)
            {
                sTrList += "<span>" + string.Format("{0:###,##0}", itemCart.PriceSale) + "</span>";
                PriceUsed = itemCart.PriceSale;
            }
            else
            {
                sTrList += "<span>" + string.Format("{0:###,##0}", itemCart.Frice) + "</span>";
                PriceUsed = itemCart.Frice;
            }

            sTrList += "</td>";

            sTrList += "<td class='cart-td-vat'>";
            sTrList += "<span>" + (itemCart.ShowVAT == false ? "0%" : (itemCart.VAT == false ? "10%" : "0%")) + "</span>";
            sTrList += "</td>";

            sTrList += "<td class='cart-td-delete'>";
            sTrList += "<a class='jgrid color' title='Xóa' href='javascript:void(0);' onclick=\"if(confirm('Xóa sản phẩm khỏi đơn hàng?')){vsw_exec_cmd('[deleteproductincart][" + itemCart.ID + "," + itemCart.OrderId + "]'); return false;} else return false;\"><span class='jgrid'>";
            sTrList += "<span class='state delete'></span></span></a>";
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
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Đơn hàng :
                <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
        </div>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
<div class="clr">
</div>
<%= ShowMessage()%>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="col width-100">
            <div>
                <table border="0" cellpadding="0" cellspacing="1" class="adminlist">
                    <tr>
                        <td>
                            Hình thức thanh toán:
                            <select style="width: 50%" name="TransportId" id="TransportId">
                                <%= Utils.ShowDDLList(ModProduct_PaymentService.Instance,item.TransportId)%>
                            </select>
                        </td>
                        <td>
                            Hình thức vận chuyển:
                            <select style="width: 50%" name="PaymentId" id="Select2">
                                <%= Utils.ShowDDLList(ModProduct_TransportService.Instance,item.TransportId)%>
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
                                        <td nowrap="nowrap">
                                            Họ tên:
                                        </td>
                                        <td style="width: 100%">
                                            <input class="text_input" type="text" name="NguoiDat_FullName" id="NguoiDat_FullName"
                                                value="<%=item.NguoiDat_FullName %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Giới tính:
                                        </td>
                                        <td>
                                            <input name="NguoiDat_Sex" <%= item.NguoiDat_Sex ? "checked" : "" %> type="radio"
                                                value='1' />
                                            Nữ
                                            <input name="NguoiDat_Sex" <%= !item.NguoiDat_Sex ? "checked" : "" %> type="radio"
                                                value='0' />
                                            Nam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Số điện thoại:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiDat_PhoneNumber" id="NguoiDat_PhoneNumber"
                                                value="<%=item.NguoiDat_PhoneNumber %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Email:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiDat_Email" id="NguoiDat_Email" value="<%=item.NguoiDat_Email %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Địa chỉ:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiDat_Address" id="NguoiDat_Address"
                                                value="<%=item.NguoiDat_Address %>" />
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
                                        <td nowrap="nowrap">
                                            Họ tên:
                                        </td>
                                        <td style="width: 100%">
                                            <input class="text_input" type="text" name="NguoiNhan_FullName" id="NguoiNhan_FullName"
                                                value="<%=item.NguoiNhan_FullName %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Giới tính:
                                        </td>
                                        <td>
                                            <input name="NguoiNhan_Sex" <%= item.NguoiNhan_Sex ? "checked" : "" %> type="radio"
                                                value='1' />
                                            Nữ
                                            <input name="NguoiNhan_Sex" <%= !item.NguoiNhan_Sex ? "checked" : "" %> type="radio"
                                                value='0' />
                                            Nam
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Số điện thoại:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiNhan_PhoneNumber" id="NguoiNhan_PhoneNumber"
                                                value="<%=item.NguoiNhan_PhoneNumber %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Email:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiNhan_Email" id="NguoiNhan_Email"
                                                value="<%=item.NguoiNhan_Email %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Địa chỉ:
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="NguoiNhan_Address" id="NguoiNhan_Address"
                                                value="<%=item.NguoiNhan_Address %>" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <table border="0" cellpadding="0" cellspacing="1" class="adminlist">
                            <thead>
                                <tr>
                                    <th colspan="4">
                                        Thông tin chi tiết đơn hàng
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td nowrap="nowrap">
                                        Mã đặt hàng:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                                            maxlength="255" />
                                    </td>
                                    <td>
                                        Ngày đặt hàng:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>"
                                            maxlength="255" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        Số sản phẩm:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="QuantityProduct" id="QuantityProduct"
                                            value="<%=item.QuantityProduct %>" maxlength="255" />
                                    </td>
                                    <td>
                                        Tổng số lượng:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="QuantityTotal" id="QuantityTotal" value="<%=item.QuantityTotal %>"
                                            maxlength="255" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        Giá trước giảm:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="TotalFriceFirst" id="TotalFriceFirst"
                                            value="<%=item.TotalFriceFirst %>" maxlength="255" price="true" />
                                    </td>
                                    <td>
                                        Chiết khấu:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="Discount" id="Discount" value="<%=item.Discount %>"
                                            maxlength="255" price="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        Giá sau giảm:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="TotalFrice" id="TotalFrice" value="<%=item.TotalFrice %>"
                                            maxlength="255" price="true" />
                                    </td>
                                    <td nowrap="nowrap">
                                        Ghi chú:
                                    </td>
                                    <td>
                                        <input class="text_input" type="text" name="Note" value="<%=item.Note %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        Trạng thái:
                                    </td>
                                    <td>
                                        <select style="width: 50%" name="Status" id="Status">
                                            <%= Utils.ShowDDLByConfigkey("Mod.OrderStatus", item.Status)%>
                                        </select>
                                    </td>
                                    <td nowrap="nowrap">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </tr>
                    <tr>
                        <table border="0" cellpadding="0" cellspacing="1" class="adminlist">
                            <thead>
                                <tr>
                                    <th colspan="4">
                                        Thông tin sản phẩm trong đơn hàng
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td colspan="4">
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
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
</form>
