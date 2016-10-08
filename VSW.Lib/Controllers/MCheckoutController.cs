using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : CheckOut", Code = "MCheckOut", Order = 50)]
    public class MCheckoutController : Controller
    {
        public void ActionIndex()
        {
            Global.Cart _Cart = new Global.Cart();

            string sListID = string.Empty;
            for (int i = 0; _Cart != null && i < _Cart.Count; i++)
                sListID += (sListID == "" ? "" : ",") + _Cart.Items[i].ProductID;

            if (sListID != string.Empty)
                ViewBag.Data = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Activity == true)
                        .WhereIn(o => o.ID, sListID)
                        .ToList();

            ViewBag.Cart = _Cart;
            ViewBag.Title = ViewPage.CurrentPage.Name;
        }

        private ModProduct_OrderEntity item = null;

        public void ActionCheckOut(ModProduct_CheckoutModel model)
        {
            List<ModProduct_Order_DetailsEntity> lstOrder_Details = new List<ModProduct_Order_DetailsEntity>();
            ModProduct_Order_DetailsEntity objOrder_Detail = null;
            int iQuantityProduct = 0;
            int iQuantityTotal = 0;
            double dTotalFriceFirst =0;

            #region Thêm thông tin đơn hàng
            ModProduct_OrderEntity objModProduct_Order = new ModProduct_OrderEntity();

            objModProduct_Order.TransportId = model.TransportId;
            objModProduct_Order.PaymentId = model.PaymentId;
            objModProduct_Order.CustomersCode = null;
            objModProduct_Order.IP = VSW.Core.Web.HttpRequest.IP;
            objModProduct_Order.Code = "[Order]" + VSW.Lib.Global.Utils.GetRandString();
            objModProduct_Order.NguoiDat_FullName = model.NguoiDat_FullName;
            objModProduct_Order.NguoiDat_Sex = model.NguoiDat_Sex;
            objModProduct_Order.NguoiDat_Address = model.NguoiDat_Address;
            objModProduct_Order.NguoiDat_Email = model.NguoiDat_Email;
            objModProduct_Order.NguoiDat_PhoneNumber = model.NguoiDat_PhoneNumber;
            objModProduct_Order.NguoiNhan_FullName = model.NguoiNhan_FullName;
            objModProduct_Order.NguoiNhan_Sex = model.NguoiNhan_Sex;
            objModProduct_Order.NguoiNhan_Address = model.NguoiNhan_Address;
            objModProduct_Order.NguoiNhan_Email = model.NguoiNhan_Email;
            objModProduct_Order.NguoiNhan_PhoneNumber = model.NguoiNhan_PhoneNumber;
            objModProduct_Order.Note = model.Note;
            objModProduct_Order.Status = (int)Global.EnumValue.OrderStatus.MOI;
            objModProduct_Order.CreateDate = DateTime.Now;

            ModProduct_OrderService.Instance.Save(objModProduct_Order);
            #endregion

            #region Lấy thông tin giỏ hàng - Chi tiết đơn hàng
            Global.Cart _Cart = new Global.Cart();
            foreach (var itemCart in _Cart.Items)
            {
                // Số lượng tất cả
                iQuantityTotal += itemCart.Quantity;

                // số lượng từng loại
                iQuantityProduct++;

                objOrder_Detail = new ModProduct_Order_DetailsEntity();

                objOrder_Detail.OrderId = objModProduct_Order.ID;
                objOrder_Detail.ProductInfoId = itemCart.ProductID;

                objOrder_Detail.MenuID = itemCart.MenuID;
                objOrder_Detail.LangID = itemCart.LangID;
                objOrder_Detail.Code = itemCart.Code;
                objOrder_Detail.Name = itemCart.Name;
                objOrder_Detail.File = itemCart.File;
                objOrder_Detail.Quantity = itemCart.Quantity;
                objOrder_Detail.FriceInput = itemCart.Price;
                objOrder_Detail.Frice = itemCart.Price;
                objOrder_Detail.PriceSale = itemCart.PriceSale;
                objOrder_Detail.VAT = itemCart.VAT;
                objOrder_Detail.SaleOffType = itemCart.SaleOffType;
                objOrder_Detail.PriceTextSaleView = itemCart.PriceTextSaleView;
                objOrder_Detail.TotalFrice = itemCart.TotalFrice;
                objOrder_Detail.Gifts = itemCart.Gifts;
                objOrder_Detail.Attach = itemCart.Attach;
                objOrder_Detail.Note = itemCart.Note;
                 
                dTotalFriceFirst += itemCart.TotalFrice;

                // Thêm vào danh sách
                lstOrder_Details.Add(objOrder_Detail);

            }

            // thêm thông tin chi tiết
            ModProduct_Order_DetailsService.Instance.Save(lstOrder_Details);
            #endregion

            // Cập nhật lại thông tin đơn hàng
            objModProduct_Order.QuantityProduct = iQuantityProduct;
            objModProduct_Order.QuantityTotal = iQuantityTotal;
            objModProduct_Order.Discount = 0;

            // Số tiền tính được (Bao gồm cả VAT nếu có)
            objModProduct_Order.TotalFriceFirst = dTotalFriceFirst;

            // Sau khi đã giảm trừ (Giả sử từ cho 200 khi đặt hàng online)
            objModProduct_Order.TotalFrice = objModProduct_Order.TotalFriceFirst - objModProduct_Order.Discount;
            
            // Lưu lại
            ModProduct_OrderService.Instance.Save(objModProduct_Order);

            // Xóa dữ liệu của giỏ hàng
            _Cart.RemoveAll();
            _Cart.Save();

            ViewPage.Alert("Đơn hàng đã gửi thành công. Xin cảm ơn quý khách hàng!");
            ViewPage.Navigate("/default.aspx");

        }
        /*
        public void ActionCheckPOST(ModOrderEntity entity)
        {
            if (entity.BillingName.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập họ tên..");

            if (entity.BillingAddress.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập địa chỉ..");

            if (entity.BillingPhone.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập điện thoại bàn.");

            if (entity.BillingTel.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập điện thoại di động.");


            string email = VSW.Lib.Global.Utils.GetEmailAddress(entity.BillingEmail.Trim());
            if (email == string.Empty)
                ViewPage.Message.ListMessage.Add("Email chưa nhập hoặc sai !");

            //if (entity.PaymentID == 0)
            //    ViewPage.Message.ListMessage.Add("Chọn phương thức thanh toán !");

            //hien thi thong bao loi
            if (ViewPage.Message.ListMessage.Count > 0)
            {
                string message = @"Các thông tin nhập còn thiếu hoặc chưa chính xác: \r\n";

                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                    message += @"\r\n + " + ViewPage.Message.ListMessage[i];

                ViewPage.Alert(message);
            }
            else
            {
                entity.ID = 0;
                entity.Code = "O_" + VSW.Lib.Global.Utils.GetRandString();
                entity.IP = VSW.Core.Web.HttpRequest.IP;
                entity.Created = DateTime.Now;

                ModOrderService.Instance.Save(entity);

                Global.Cart _Cart = new Global.Cart();

                long Total = 0;
                for (int i = 0; _Cart != null && i < _Cart.Items.Count; i++)
                {
                    ModProductEntity _Product = ModProductService.Instance.GetByID(_Cart.Items[i].ProductID);     
                    if (_Product == null)
                        continue;

                    Total += _Product.Price * _Cart.Items[i].Quantity;

                    ModOrderDetailEntity _OrderDetail = new ModOrderDetailEntity();

                    _OrderDetail.OrderID = entity.ID;
                    _OrderDetail.ProductID = _Product.ID;
                    _OrderDetail.Name = _Product.Name;
                    _OrderDetail.Quantity = _Cart.Items[i].Quantity;
                    _OrderDetail.Price = _Product.Price;

                    //luu DB
                    ModOrderDetailService.Instance.Save(_OrderDetail);
                }
                entity.Total = Total;

                ModOrderService.Instance.Save(entity);

                entity = new ModOrderEntity();

                _Cart.RemoveAll();
                _Cart.Save();

                ViewPage.Alert("Đơn hàng đã gửi thành công!");
                ViewPage.Navigate("/default.aspx");
            }

            ViewBag.Data = entity;
        }
         */
    }

    public class ModProduct_CheckoutModel : DefaultModel
    {
        public int TransportId { get; set; }

        public int PaymentId { get; set; }

        public int CustomersCode { get; set; }

        public int UserId { get; set; }

        public int UserModifiedId { get; set; }

        public string IP { get; set; }

        public string Code { get; set; }

        public int QuantityProduct { get; set; }

        public int QuantityTotal { get; set; }

        public double Discount { get; set; }

        public double TotalFriceFirst { get; set; }

        public double TotalFrice { get; set; }

        public string Note { get; set; }

        public int Status { get; set; }

        public string NguoiDat_FullName { get; set; }

        public bool NguoiDat_Sex { get; set; }

        public string NguoiDat_Address { get; set; }

        public string NguoiDat_Email { get; set; }

        public string NguoiDat_PhoneNumber { get; set; }

        public string NguoiNhan_FullName { get; set; }

        public bool NguoiNhan_Sex { get; set; }

        public string NguoiNhan_Address { get; set; }

        public string NguoiNhan_Email { get; set; }

        public string NguoiNhan_PhoneNumber { get; set; }

        //public DateTime CreateDate { get; set; }

        //public DateTime ModifiedDate { get; set; }
    }
}
