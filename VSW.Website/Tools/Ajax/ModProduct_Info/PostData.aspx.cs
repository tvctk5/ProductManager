using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using VSW.Lib.Global;

namespace VSW.Website.Tools.Ajax.Common.ModProduct_Info
{
    partial class PostData : System.Web.UI.Page
    {
        VSW.Website.CP.Tools.Common objCommon = new VSW.Website.CP.Tools.Common();
        DataOutput objDataOutput = new DataOutput();
        string sMessError = string.Empty;

        #region Tập các giá trị control được post lên để sử dụng
        int RecordID = 0;
        int iCountBuy = 0;
        #endregion

        #region Lấy các giá trị từ các control post
        /// <summary>
        /// Lấy các giá trị từ các control post
        /// </summary>
        private void GetValueControl()
        {
            RecordID = objCommon.ConvertToInt32(Request["RecordID"]);
            iCountBuy = objCommon.ConvertToInt32(Request["Price"]);
        }
        #endregion

        #region Methord Return Object
        /// <summary>
        /// Return Object
        /// </summary>
        /// <param name="objDataReturn"></param>
        private void RenderMessage(DataOutput objDataReturn)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonMessage = oSerializer.Serialize(objDataReturn);
            this.Page.Response.Clear();
            this.Page.Response.ContentType = "application/json";
            this.Page.Response.Write(strJsonMessage);
            this.Page.Response.End();
        }
        #endregion

        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string sType = Request.QueryString["Type"];

            // Lấy giá trị các control post lên
            GetValueControl();

            // Tùy từng trường hợp thao tác dữ liệu khác khau
            switch (sType)
            {
                // Add to cart 
                case "addtocart": AddToCart(); break;
                case "removefromcart": RemoveFromCart(); break;
                case "publiclogin": Publiclogin(); break;
                case "publiclogout": Publiclogout(); break;
                case "publicCreateAccount": PublicCreateAccount(); break;
                case "publicUpdateAccount": PublicUpdateAccount(); break;
                case "addcompareproduct": AddCompareProduct(); break;
                case "removecompareproduct": RemoveCompareProduct(); break;
                case "reloadlistcompareproduct": ReloadListCompareProduct(); break;
                case "productfilter": ProductFilter(); break;
                case "addcomment": AddComment(); break;
                case "requestresetpass": RequestResetPass(); break;
                case "resetpass": ResetPass(); break;
                case "sentsurvey": SentSurvey(); break;
                case "updatesurvey": UpdateSurvey(); break;
                case "updatecookie": UpdateCookie(); break;
                case "submitpopup_feedback": SentFeedback(); break;
                case "submitpopup_mailletter": AddMailLetter(); break;

            }

            // Trả lại dữ liệu
            RenderMessage(objDataOutput);
        }

        /// <summary>
        /// AddToCart
        /// </summary>
        private void AddToCart()
        {
            try
            {
                VSW.Lib.Global.Cart _Cart = new VSW.Lib.Global.Cart();

                VSW.Lib.Global.CartItem item = _Cart.Find(new VSW.Lib.Global.CartItem() { ProductID = RecordID });

                if (iCountBuy <= 0)
                    iCountBuy = 1;

                var objProduct = ModProduct_InfoService.Instance.GetByID(RecordID);
                double dTotalFrice = 0;
                double dPriceActivate = 0;

                dPriceActivate = objProduct.Price;
                if (objProduct.PriceSale > 0)
                    dPriceActivate = objProduct.PriceSale;

                // Có VAT
                if (objProduct.VAT == false && objProduct.ShowVAT == true)
                {
                    // Thêm tiền VAT
                    dTotalFrice = dPriceActivate + ((dPriceActivate * 10) / 100);
                }
                else
                    dTotalFrice = dPriceActivate;

                if (item == null && objProduct != null)
                    _Cart.Add(new VSW.Lib.Global.CartItem()
                    {
                        ProductID = RecordID,
                        Quantity = iCountBuy,
                        MenuID = objProduct.MenuID,
                        LangID = objProduct.LangID,
                        Code = objProduct.Code,
                        Name = objProduct.Name,
                        File = objProduct.File,
                        ShowPrice = objProduct.ShowPrice,
                        Price = objProduct.Price,
                        PriceSale = objProduct.PriceSale,
                        VAT = objProduct.VAT,
                        ShowVAT = objProduct.ShowVAT,
                        ProductsConnection = objProduct.ProductsConnection,
                        ProductsAttach = objProduct.ProductsAttach,
                        SaleOffType = objProduct.SaleOffType,
                        PriceTextSaleView = objProduct.PriceTextSaleView,
                        TotalFrice = dTotalFrice * iCountBuy,
                        Gifts = objProduct.Gifts,
                        Attach = (string.IsNullOrEmpty(objProduct.ProductsAttach) ? false : true),
                        Note = objProduct.InfoBasic
                    });
                else
                {
                    item.Quantity += iCountBuy;
                    // Cập nhật lại tổng giá thanh toán
                    item.TotalFrice = dTotalFrice * item.Quantity;
                }

                _Cart.Save();
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
            }
        }

        /// <summary>
        /// RemoveFromCart
        /// </summary>
        private void RemoveFromCart()
        {
            try
            {
                VSW.Lib.Global.Cart _Cart = new VSW.Lib.Global.Cart();
                _Cart.Remove(new VSW.Lib.Global.CartItem() { ProductID = RecordID });
                _Cart.Save();

                double dTotal = 0;
                double dCount = 0;
                double dVAT = 0;
                foreach (VSW.Lib.Global.CartItem itemCart in _Cart.Items)
                {
                    dVAT = 0;

                    if (itemCart.PriceSale > 0)
                        dCount = itemCart.PriceSale * itemCart.Quantity;
                    else
                        dCount = itemCart.Price * itemCart.Quantity;

                    if (itemCart.ShowVAT && itemCart.VAT == false)
                        // 10% VAT
                        dVAT = (dCount * 10) / 100;

                    dTotal += dCount + dVAT;
                }

                objDataOutput.MessSuccess = string.Format("{0:###,##0}", dTotal);
                if (_Cart.Items.Count <= 0)
                    objDataOutput.NoProduct = true;
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        private void Publiclogin()
        {
            try
            {
                string sUserName = Request.QueryString["UserName"].ToLower();
                string sPass = Request.QueryString["Pass"];
                // Mã hóa
                sPass = VSW.Lib.Global.Security.MD5(sPass);

                var objCustomer = ModProduct_CustomersService.Instance.CreateQuery()
                    .Where(o => o.UserName == sUserName && o.Pass == sPass)
                    .ToSingle();

                if (objCustomer == null)
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = "Đăng nhập thất bại.\r\nSai thông tin tài khoản hoặc mật khẩu.";
                    return;
                }

                // Lưu thông tin đăng nhập
                VSW.Lib.Global.Cookies.SetValue("VSW.CustomerId", objCustomer.ID.ToString());
                objDataOutput.MessSuccess = "Đăng nhập thành công";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Đăng nhập thất bại.\r\nSai thông tin tài khoản hoặc mật khẩu.";
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        private void Publiclogout()
        {
            try
            {
                // Xóa thông tin đăng nhập
                VSW.Lib.Global.Cookies.Remove("VSW.CustomerId");
                objDataOutput.MessSuccess = "Đăng xuất thành công";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Đăng xuất thất bại. Xin hãy thử lại!";
            }
        }

        /// <summary>
        /// Tạo tài khoản
        /// </summary>
        private void PublicCreateAccount()
        {
            try
            {
                ModProduct_CustomersEntity objCustomers = new ModProduct_CustomersEntity();

                objCustomers.UserName = ConvertTool.ConvertToString(Request["UserName"]).ToLower();
                objCustomers.Pass = ConvertTool.ConvertToString(Request["Pass"]);
                objCustomers.FullName = ConvertTool.ConvertToString(Request["FullName"]);
                objCustomers.Sex = ConvertTool.ConvertToBoolean(Request["Sex"]);
                if (Request["Birthday"] != null && !string.IsNullOrEmpty(Request["Birthday"]))
                    objCustomers.Birthday = ConvertTool.ConvertToDateTime(Request["Birthday"]);
                objCustomers.Address = ConvertTool.ConvertToString(Request["Address"]);
                objCustomers.Email = ConvertTool.ConvertToString(Request["Email"]).ToLower();
                objCustomers.PhoneNumber = ConvertTool.ConvertToString(Request["PhoneNumber"]);
                // MẶc định chưa có điểm nào
                objCustomers.PointTotal = 0;

                #region Kiểm tra tài khoản - email
                var objCheck = ModProduct_CustomersService.Instance.CreateQuery().Where(o => o.UserName == objCustomers.UserName).ToList();
                if (objCheck != null && objCheck.Count > 0)
                {
                    objDataOutput.Error = true;
                    objDataOutput.Type = 0;
                    objDataOutput.MessError = "Tài khoản đã được sử dụng.\r\nXin hãy tạo tài khoản khác hoặc đăng nhập nếu bạn đã có tài khoản.";
                    return;
                }

                objCheck = ModProduct_CustomersService.Instance.CreateQuery().Where(o => o.Email == objCustomers.Email).ToList();
                if (objCheck != null && objCheck.Count > 0)
                {
                    objDataOutput.Error = true;
                    objDataOutput.Type = 1;
                    objDataOutput.MessError = "Email đã được sử dụng.\r\nXin hãy nhập email khác hoặc đăng nhập nếu bạn đã có tài khoản.";
                    return;
                }
                #endregion


                objCustomers.Code = "A" + VSW.Lib.Global.Utils.GetRandString();
                objCustomers.Pass = VSW.Lib.Global.Security.MD5(objCustomers.Pass);
                objCustomers.Activity = true;

                #region Upload file (nếu có)
                string sFilePath = string.Empty;
                // Có tệp đính kèm

                if (Request.Files.Count > 0)
                {
                    var hpf = Request.Files[0];

                    if (hpf.ContentLength > 0)
                    {
                        string pathFileRoot = AppDomain.CurrentDomain.BaseDirectory;
                        if (!System.IO.Directory.Exists(pathFileRoot + "\\Data\\upload\\customer"))
                            System.IO.Directory.CreateDirectory(pathFileRoot + "\\Data\\upload\\customer");

                        string savedFileName = "Data/upload/customer/" + DateTime.Now.ToString("yyyyMMdd_hhmmss_ffff") + "_" + System.IO.Path.GetFileNameWithoutExtension(hpf.FileName) + System.IO.Path.GetExtension(hpf.FileName);
                        hpf.SaveAs(pathFileRoot + savedFileName.Replace("/", "\\"));

                        sFilePath = "~/" + savedFileName;
                    }
                }
                #endregion

                // Đẩy thông tin vào Db 
                objCustomers.File = sFilePath;
                objCustomers.CreateDate = DateTime.Now;
                objCustomers.ModifiedDate = DateTime.Now;

                // Thêm mới
                ModProduct_CustomersService.Instance.Save(objCustomers);

                #region Thêm Email vào danh sách nhận bản tin
                var MailLetter = ModListMailNewsLetterService.Instance.CreateQuery().Where(o => o.Email == objCustomers.Email).ToList();
                if (MailLetter == null || MailLetter.Count <= 0)
                {
                    var objMailLetter = new ModListMailNewsLetterEntity();
                    objMailLetter.Name = objCustomers.FullName;
                    objMailLetter.Email = objCustomers.Email;
                    objMailLetter.Sex = objCustomers.Sex;
                    objMailLetter.CodeRemoveList = "ML" + VSW.Lib.Global.Utils.GetRandString();
                    objMailLetter.IP = VSW.Core.Web.HttpRequest.IP;
                    objMailLetter.CreateDate = DateTime.Now;
                    objMailLetter.Activity = true;

                    // Lưu lại
                    ModListMailNewsLetterService.Instance.Save(objMailLetter);
                }
                #endregion

                VSW.Lib.Global.Cookies.SetValue("VSW.CustomerId", objCustomers.ID.ToString());
                objDataOutput.MessSuccess = "Tạo tài khoản thành công";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Tạo tài khoản thất bại. Xin hãy thử lại!";
            }
        }

        /// <summary>
        /// Cập nhật tài khoản
        /// </summary>
        private void PublicUpdateAccount()
        {
            try
            {
                ModProduct_CustomersEntity objCustomers = ModProduct_CustomersService.Instance.GetByID(RecordID);
                if (objCustomers == null)
                {
                    objDataOutput.Error = true;
                    objDataOutput.Type = -1;
                    objDataOutput.MessError = "Không tìm thấy thông tin tài khoản.\r\nXin hãy đăng nhập lại.";
                    return;
                }

                objCustomers.UserName = ConvertTool.ConvertToString(Request["UserName"]).ToLower();
                objCustomers.FullName = ConvertTool.ConvertToString(Request["FullName"]);
                objCustomers.Sex = ConvertTool.ConvertToBoolean(Request["Sex"]);
                if (Request["Birthday"] != null && !string.IsNullOrEmpty(Request["Birthday"]))
                    objCustomers.Birthday = ConvertTool.ConvertToDateTime(Request["Birthday"]);
                else
                    objCustomers.Birthday = null;

                objCustomers.Address = ConvertTool.ConvertToString(Request["Address"]);
                objCustomers.Email = ConvertTool.ConvertToString(Request["Email"]).ToLower();
                objCustomers.PhoneNumber = ConvertTool.ConvertToString(Request["PhoneNumber"]);

                #region Kiểm tra Mật khẩu cũ - email
                string sOldPass = ConvertTool.ConvertToString(Request["OldPass"]);

                // Nếu đổi mật khẩu
                if (!string.IsNullOrEmpty(sOldPass))
                {
                    sOldPass = VSW.Lib.Global.Security.MD5(sOldPass);

                    var objCheckOldPass = ModProduct_CustomersService.Instance.CreateQuery().Where(o => o.Pass == sOldPass && o.ID == RecordID).ToList();
                    if (objCheckOldPass == null && objCheckOldPass.Count <= 0)
                    {
                        objDataOutput.Error = true;
                        objDataOutput.Type = 0;
                        objDataOutput.MessError = "Mật khẩu cũ không đúng.\r\nXin hãy nhập lại.";
                        return;
                    }
                }

                var objCheck = ModProduct_CustomersService.Instance.CreateQuery().Where(o => o.Email == objCustomers.Email && o.ID != RecordID).ToList();
                if (objCheck != null && objCheck.Count > 0)
                {
                    objDataOutput.Error = true;
                    objDataOutput.Type = 1;
                    objDataOutput.MessError = "Email đã được sử dụng bởi tài khoản khác.\r\nXin hãy nhập email khác hoặc đặt lại các thông tin về mặc định.";
                    return;
                }
                #endregion

                // Cập nhật pass mới
                if (!string.IsNullOrEmpty(sOldPass))
                    objCustomers.Pass = VSW.Lib.Global.Security.MD5(ConvertTool.ConvertToString(Request["Pass"]));

                #region Upload file (nếu có)
                string sFilePath = string.Empty;
                // Có tệp đính kèm

                if (Request.Files.Count > 0)
                {
                    var hpf = Request.Files[0];

                    if (hpf.ContentLength > 0)
                    {
                        string pathFileRoot = AppDomain.CurrentDomain.BaseDirectory;
                        if (!System.IO.Directory.Exists(pathFileRoot + "\\Data\\upload\\customer"))
                            System.IO.Directory.CreateDirectory(pathFileRoot + "\\Data\\upload\\customer");

                        string savedFileName = "Data/upload/customer/" + DateTime.Now.ToString("yyyyMMdd_hhmmss_ffff") + "_" + System.IO.Path.GetFileNameWithoutExtension(hpf.FileName) + System.IO.Path.GetExtension(hpf.FileName);
                        hpf.SaveAs(pathFileRoot + savedFileName.Replace("/", "\\"));

                        sFilePath = "~/" + savedFileName;
                    }
                }
                #endregion

                // Đẩy thông tin vào Db 
                if (!string.IsNullOrEmpty(sFilePath))
                    objCustomers.File = sFilePath;
                objCustomers.ModifiedDate = DateTime.Now;

                // Cập nhật
                ModProduct_CustomersService.Instance.Save(objCustomers);

                #region Thêm Email vào danh sách nhận bản tin (Nếu có)
                var MailLetter = ModListMailNewsLetterService.Instance.CreateQuery().Where(o => o.Email == objCustomers.Email).ToList();
                if (MailLetter == null || MailLetter.Count <= 0)
                {
                    var objMailLetter = new ModListMailNewsLetterEntity();
                    objMailLetter.Name = objCustomers.FullName;
                    objMailLetter.Email = objCustomers.Email;
                    objMailLetter.Sex = objCustomers.Sex;
                    objMailLetter.CodeRemoveList = "ML" + VSW.Lib.Global.Utils.GetRandString();
                    objMailLetter.IP = VSW.Core.Web.HttpRequest.IP;
                    objMailLetter.CreateDate = DateTime.Now;
                    objMailLetter.Activity = true;

                    // Lưu lại
                    ModListMailNewsLetterService.Instance.Save(objMailLetter);
                }
                #endregion

                VSW.Lib.Global.Cookies.SetValue("VSW.CustomerId", objCustomers.ID.ToString());
                objDataOutput.MessSuccess = "Cập nhật thông tin tài khoản thành công";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Cập nhật thông tin tài khoản thất bại. Xin hãy thử lại!";
            }
        }

        /// <summary>
        /// Add product to compare
        /// </summary>
        private void AddCompareProduct()
        {
            try
            {
                // Lấy danh sách tất cả danh mục
                var ListWebMenu = WebMenuService.Instance.CreateQuery().ToList_Cache();

                var ListProductCompareID = new List<int>();
                if (VSW.Lib.Global.Session.Exists("ListProductCompareID"))
                    ListProductCompareID = (List<int>)VSW.Lib.Global.Session.GetValue("ListProductCompareID");

                // Nếu chưa có Item nào thì thêm luôn
                if (ListProductCompareID.Count <= 0)
                    ListProductCompareID.Add(RecordID);
                else
                // Kiểm tra xem đã tồn tại chưa, nếu chưa tồn tại thì thêm vào
                {
                    var objExists = ListProductCompareID.Where(o => o == RecordID).FirstOrDefault();
                    if (objExists == 0)
                        // Nếu đã 4 sản phẩm rồi thì không cho thêm nữa
                        if (ListProductCompareID.Count >= 4)
                        {
                            objDataOutput.Error = true;
                            objDataOutput.MessError = "Chỉ được phép so sánh tối đa 4 sản phẩm.";
                            return;
                        }
                        else
                            // Thêm vào danh sách
                            ListProductCompareID.Add(RecordID);
                }

                // Lưu lại
                VSW.Lib.Global.Session.SetValue("ListProductCompareID", ListProductCompareID);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = ListProductCompareString();
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Remove product to compare
        /// </summary>
        private void RemoveCompareProduct()
        {
            try
            {
                var ListProductCompareID = new List<int>();
                if (VSW.Lib.Global.Session.Exists("ListProductCompareID"))
                    ListProductCompareID = (List<int>)VSW.Lib.Global.Session.GetValue("ListProductCompareID");

                // Nếu chưa có Item nào thì thêm luôn
                if (ListProductCompareID.Count <= 0)
                {
                    // Tải lại dữ liệu
                    objDataOutput.MessSuccess = ListProductCompareString();
                    return;
                }

                // Kiểm tra xem có trong giỏ chưa, nếu rồi thì xóa đi
                var objExists = ListProductCompareID.Where(o => o == RecordID).FirstOrDefault();
                if (objExists != 0)
                    ListProductCompareID.Remove(objExists);

                // Lưu lại
                VSW.Lib.Global.Session.SetValue("ListProductCompareID", ListProductCompareID);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = ListProductCompareString();
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Lấy danh sách product so sánh dưới dạng chuỗi
        /// </summary>
        /// <returns></returns>
        private string ListProductCompareString()
        {
            try
            {
                string sData = string.Empty;
                var ListProductCompareID = new List<int>();

                if (VSW.Lib.Global.Session.Exists("ListProductCompareID"))
                    ListProductCompareID = (List<int>)VSW.Lib.Global.Session.GetValue("ListProductCompareID");

                // Không có sản phẩm nào
                if (ListProductCompareID.Count <= 0)
                {
                    sData = "<div class='div-no-product'>Chưa có sản phẩm nào chọn để so sánh</div>";
                    objDataOutput.NoProduct = true;

                    return sData;
                }

                // Lấy danh sách ID
                string ArrList_Id = VSW.Core.Global.Array.ToString(ListProductCompareID.ToArray());
                var ListProduct = ModProduct_InfoService.Instance.CreateQuery()
                    .WhereIn(o => o.ID, ArrList_Id)
                    .ToList_Cache();

                if (ListProduct == null || ListProduct.Count <= 0)
                {
                    sData = "<div class='div-no-product'>Chưa có sản phẩm nào chọn để so sánh</div>";
                    objDataOutput.NoProduct = true;
                    return sData;
                }

                #region Lấy danh sách dạng chuỗi
                string Url = string.Empty;
                string sFilePath = string.Empty;

                sData = "<ul class='ul-product-compare'>";
                foreach (var item in ListProduct)
                {
                    Url = (new VSW.Lib.MVC.ViewPage()).GetURL(item.MenuID, item.Code) + ".aspx";

                    sData += "<li class='li-product-compare'>";


                    sData += "<div class='div-product-compare-image-label'>";
                    // Ảnh sản phẩm
                    sData += "<div class='div-product-compare-image'>";
                    sData += "<img class='div-product-compare-image-delete' alt='Xóa sản phẩm khỏi danh sách so sánh' title='Xóa sản phẩm khỏi danh sách so sánh' productid='" + item.ID + "' onclick='return Product_Compare_Delete(this);' />";
                    sData += "</div>";

                    // Tên SP
                    sData += "<div class='div-product-compare-label'>";
                    sData += "<a href='" + Url + "' target='_blank'>";
                    sData += item.Name;
                    sData += "</a>";
                    sData += "</div>";
                    sData += "</div>";

                    // Giá
                    sData += "<div class='div-product-compare-price'>";
                    // Có hiển thị giá hay không
                    if (item.ShowPrice)
                    {
                        if (VSW.Lib.Global.ConvertTool.CheckSaleOff(item))
                        {
                            //Giá cũ
                            sData += "<div class='div-product-compare-price-old'>";
                            sData += ConvertTool.ConvertToMoney(item.Price);
                            sData += "</div>";

                            // Giá mới
                            sData += "<div class='div-product-compare-price-new'>";
                            sData += ConvertTool.ConvertToMoney(item.PriceSale);
                            sData += "</div>";
                        }
                        else
                        {
                            // Giá mới
                            sData += "<div class='div-product-compare-price-new'>";
                            sData += ConvertTool.ConvertToMoney(item.Price);
                            sData += "</div>";
                        }
                    }
                    // Hiển thị giá liên hệ 
                    else
                    {
                        // Giá mới
                        sData += "<div class='div-product-compare-price-new'>";
                        sData += "<p class='current_price_notshow'></p>";
                        sData += "</div>";
                    }

                    sData += "</div>";

                    sData += "</li>";
                }

                sData += "</ul>";
                #endregion

                return sData;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// ReloadListCompareProduct product to compare
        /// </summary>
        private void ReloadListCompareProduct()
        {
            try
            {
                // Tải lại dữ liệu
                objDataOutput.MessSuccess = ListProductCompareString();
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Product Filter
        /// </summary>
        private void ProductFilter()
        {
            try
            {
                List<ModProduct_InfoEntity> lstProduct_Info = null;
                string sListId = string.Empty;
                // Ngôn ngữ
                int languageId = VSW.Lib.Global.ConvertTool.ConvertToInt32(Request.QueryString["languageId"]);
                // Sắp xếp
                string sOrder = Request.QueryString["Order"];
                string sOrderType = Request.QueryString["typeorderby"];
                if (string.IsNullOrEmpty(sOrder))
                    sOrder = "ID";

                if (string.IsNullOrEmpty(sOrderType))
                    sOrderType = "DESC";

                // Page index và page size
                int iPageIndex = ConvertTool.ConvertToInt32(Request.Form["PageIndex"]);
                int iPageSize = ConvertTool.ConvertToInt32(Request.Form["PageSize"]);
                int iPageSizeTotal = 0;

                string sQuery_Filter = string.Empty;

                // Lấy các thông tin tìm kiếm
                string sName_Code = ConvertTool.ConvertToString(Request.Form["Name"]);
                string sManufacturer = ConvertTool.ConvertToString(Request.Form["Manufacturer"]);
                string sFilter = ConvertTool.ConvertToString(Request.Form["Filter"]);

                #region Lấy thông tin tìm kiếm từ các Nhóm thuộc tính lọc

                if (!string.IsNullOrEmpty(sFilter))
                {
                    List<string> lstFilterIDs = sFilter.Split(',').ToList();
                    // Duyệt lấy các ID
                    sQuery_Filter = " 1=1";

                    foreach (var itemFilterId in lstFilterIDs)
                    {
                        sQuery_Filter += " AND Mod_Product_Info.ProductFilterIds LIKE '%," + itemFilterId + ",%'";
                    }
                }
                #endregion

                string sQuery = string.Empty;
                sQuery += " SELECT Mod_Product_Info.ID, (ROW_NUMBER() OVER ( ORDER BY " + sOrder + " " + sOrderType + ")) AS Row_Index  FROM Mod_Product_Info";
                sQuery += " WHERE Mod_Product_Info.Activity=" + (int)VSW.Lib.Global.EnumValue.Activity.TRUE + " AND Mod_Product_Info.LangID=" + languageId;
                sQuery += " AND Mod_Product_Info.Deleted=" + (int)VSW.Lib.Global.EnumValue.Activity.FALSE;

                // Nhà sản xuất
                if (!string.IsNullOrEmpty(sManufacturer))
                    sQuery += " AND Mod_Product_Info.ManufacturerId IN (" + sManufacturer + ")";

                // Tên, Mã, 
                if (!string.IsNullOrEmpty(sName_Code))
                {
                    sQuery += " AND (Mod_Product_Info.Code LIKE N'%" + sName_Code + "%'";
                    sQuery += " OR Mod_Product_Info.Name LIKE N'%" + sName_Code + "%'";
                    sQuery += " OR Mod_Product_Info.PageKeywords LIKE N'%" + sName_Code + "%'";
                    sQuery += " OR Mod_Product_Info.PageTitle LIKE N'%" + sName_Code + "%')";
                }

                // Thuộc tính lọc
                if (!string.IsNullOrEmpty(sQuery_Filter))
                    sQuery += " AND " + sQuery_Filter;

                // Tìm kiếm
                System.Data.DataTable tblSearchResult = ModProduct_InfoService.Instance.ExecuteDataTable(sQuery);

                // Không tìm thấy dữ liệu
                if (tblSearchResult == null || tblSearchResult.Rows.Count <= 0)
                    lstProduct_Info = new List<ModProduct_InfoEntity>();

                else
                {
                    // Tổng số bản ghi
                    iPageSizeTotal = tblSearchResult.Rows.Count;

                    // Có dữ liệu Phân trang, lấy dữ liệu
                    if (tblSearchResult.Rows.Count <= iPageSize)
                    {
                        foreach (System.Data.DataRow dataRow in tblSearchResult.Rows)
                            sListId += ConvertTool.ConvertToString(dataRow["ID"]) + ",";
                    }
                    else
                    {
                        // Phân trang
                        System.Data.DataView dtView = tblSearchResult.DefaultView;
                        dtView.RowFilter = "Row_Index>=" + (iPageIndex * iPageSize + 1) + "AND Row_Index<=" + (iPageIndex * iPageSize + iPageSize);

                        if (dtView != null || dtView.Count > 0)
                        {
                            for (int iItem = 0; iItem < dtView.Count; iItem++)
                            {
                                sListId += ConvertTool.ConvertToString(dtView[iItem]["ID"]) + ",";
                            }
                        }
                    }

                    // Trim()
                    sListId = sListId.Trim(',');

                    // Lấy dữ liệu
                    lstProduct_Info = ModProduct_InfoService.Instance.CreateQuery()
                                                                        .WhereIn(o => o.ID, sListId)
                                                                        .OrderBy(sOrder + " " + sOrderType)
                                                                        .ToList();
                }

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = GetDataFilter(lstProduct_Info, iPageSizeTotal, iPageSize, iPageIndex);
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "<div class='no-product'>Phát sinh lỗi, xin hãy thử lại<div>";
            }
        }

        private string GetDataFilter(List<ModProduct_InfoEntity> listItem, int iPageSizeTotal, int iPageSize, int iPageIndex)
        {
            string sData = string.Empty;
            string Url = string.Empty;
            string sFilePath = string.Empty;

            // Không có sản phẩm nào thỏa mãn
            if (listItem == null || listItem.Count <= 0)
            {
                sData += "<div class='no-product'>Không tìm thấy sản phẩm nào thỏa mãn điều kiện tìm kiếm<div>";
                return sData;
            }

            bool bolPriceTypeSale_Percent = Convert.ToBoolean((int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_PriceTypeSale.Percent);
            bool bolPriceTypeSale_Money = Convert.ToBoolean((int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_PriceTypeSale.Mony);

            sData += "<div class='div_list_product'><ul class='list_product'>";
            foreach (var item in listItem)
            {
                Url = (new VSW.Lib.MVC.ViewPage()).GetURL(item.MenuID, item.Code) + ".aspx";
                sFilePath = Utils.GetResizeFile(item.File, 2, 175, 175);

                sData += "<li>";
                sData += "<div>";
                sData += "<a href='../" + Url + "' class='image' target='_blank'>";
                sData += "<img width='100%' src='" + sFilePath + "' alt='" + item.Name + "' />";

                // Icon giảm giá
                if (item.ShowIcon == (int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_ShowIcon.SALE && VSW.Lib.Global.ConvertTool.CheckSaleOff(item))
                {
                    if (item.SaleOffType == bolPriceTypeSale_Percent)
                        sData += "<span class='sale-percent'>-" + item.PriceTextSaleView + "</span>";

                    if (item.SaleOffType == bolPriceTypeSale_Money)
                        sData += "<span class='sale-mony'>-" + item.PriceTextSaleView + "</span>";
                }
                // New - Host
                else
                {
                    if (item.ShowIcon == (int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_ShowIcon.NEW)
                        sData += "<span class='product-new'>&nbsp;</span>";
                    else
                        if (item.ShowIcon == (int)VSW.Lib.Global.EnumValue.PRODUCT_INFO_ShowIcon.HOT)
                            sData += "<span class='product-hot'>&nbsp;</span>";
                }

                sData += "</a>";

                sData += "<h2 class='intro'><a href='" + Url + "' target='_blank'>" + item.Name + "</a></h2>";

                // Có hiển thị giá hay không
                if (item.ShowPrice)
                {
                    // Có hiển thị giá khuyến mại - giá cũ hay không
                    if (VSW.Lib.Global.ConvertTool.CheckSaleOff(item))
                    {
                        sData += "<p class='current_price'>" + VSW.Lib.Global.ConvertTool.ConvertToMoney(item.PriceSale) + "</p>";
                        sData += "<p class='old_price'>" + VSW.Lib.Global.ConvertTool.ConvertToMoney(item.Price) + "</p>";
                    }
                    else
                    {
                        sData += "<p class='current_price'>" + VSW.Lib.Global.ConvertTool.ConvertToMoney(item.Price) + "</p>";
                        sData += "<p class='old_price'></p>";
                    }
                }
                // Không hiển thị giá bán
                else
                {
                    sData += "<p class='current_price_notshow'></p>";
                    sData += "<p class='old_price'></p>";
                }

                sData += "</div>"; // Xem chi tiết
                sData += "<div class='show-detail'><div class='show-detail-inline'>";
                sData += "<div class='show-detail-left'><span class='action-sub-checkbox'><input type='checkbox' productid='" + item.ID + "' class='input-compare-product' /><span class='action-sub action-sub-checkbox-label'>&nbsp;So sánh</span></span></div>";
                sData += "<div class='show-detail-right'><a href='" + Url + "'>Chi tiết</a></div>";
                sData += "</div></div>";
                sData += "</li>";
            }
            sData += "</ul></div>";

            #region Phân trang nếu có
            if (iPageSizeTotal <= iPageSize)
                return sData;

            string sPageding = string.Empty;
            // Đếm xem có bao nhiêu trang
            int iCountPage = (iPageSizeTotal % iPageSize == 0 ? 0 : 1) + (iPageSizeTotal / iPageSize);

            sPageding += "<div class='div-page'>";
            for (int ipage = 0; ipage < iCountPage; ipage++)
            {
                if (iPageIndex == ipage)
                    sPageding += "<span class='span-page-current'>" + (ipage + 1) + "</span>";
                else
                    sPageding += "<span class='span-page' pageindex='" + ipage + "'>" + (ipage + 1) + "</span>";
            }
            sPageding += "</div>";

            // Tạo phân trang
            sData = sPageding + sData + sPageding;
            #endregion

            return sData;
        }

        /// <summary>
        /// Thêm mới comment
        /// </summary>
        private void AddComment()
        {
            try
            {
                var objProductComment = new ModProduct_CommentsEntity();
                objProductComment.ProductInfoId = RecordID;
                objProductComment.Name = ConvertTool.ConvertToString(Request["Comment_Name"]);
                objProductComment.Email = ConvertTool.ConvertToString(Request["Comment_Email"]);
                objProductComment.Address = ConvertTool.ConvertToString(Request["Comment_Address"]);
                objProductComment.PhoneNumber = ConvertTool.ConvertToString(Request["Comment_PhoneNumber"]);
                objProductComment.Content = ConvertTool.ConvertToString(Request["Comment_Content"]);
                objProductComment.Approved = true; // Mặc định được duyệt
                objProductComment.Activity = true;
                objProductComment.CreateDate = DateTime.Now;

                // Đẩy vào Db
                ModProduct_CommentsService.Instance.Save(objProductComment);

                // Cập nhật số lượt bình luận
                var Product = ModProduct_InfoService.Instance.GetByID(RecordID);
                Product.CommentCount = Product.CommentCount + 1;
                ModProduct_InfoService.Instance.Save(Product);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = "Thêm mới bình luận thành công";
                objDataOutput.ListComment = BinhLuan(RecordID);
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Lấy các thông tin bình luận
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string BinhLuan(int ProductId)
        {
            string sData = "<div class='div-no-comment'>Chưa có bình luận nào cho sản phẩm</div>";
            if (ProductId <= 0)
                return sData;

            // LẤy các comment đã được duyệt
            var lstDataComment = ModProduct_CommentsService.Instance.CreateQuery().Where(o => o.ProductInfoId == ProductId && o.Approved == true)
                                                              .OrderByDesc(o => o.CreateDate).ToList();

            if (lstDataComment == null || lstDataComment.Count <= 0)
                return sData;

            sData = string.Empty;

            foreach (var itemComment in lstDataComment)
            {
                sData += "<div class='div-comment-group'>";
                sData += "<div><span class='div-comment-group-info'>" + itemComment.Name + "</span><span class='div-comment-group-info-email'>" + (string.IsNullOrEmpty(itemComment.Email) ? string.Empty : " - ") + itemComment.Email + "</span>";
                sData += "<span class='div-comment-group-info-address'>" + (string.IsNullOrEmpty(itemComment.Address) ? string.Empty : " - ") + itemComment.Address + "</span><span class='div-comment-group-info-date'>(" + itemComment.CreateDate.ToString("dd/MM/yyyy HH:mm") + ")</span></div>";

                // nội dung comment
                sData += "<p class='div-comment-content'>";
                sData += itemComment.Content;
                sData += "</p>";
                sData += "</div>";
            }

            return sData;
        }

        /// <summary>
        /// Yêu cầu reset pass
        /// </summary>
        private void RequestResetPass()
        {
            try
            {
                string User = ConvertTool.ConvertToString(Request["User"]);
                string Email = ConvertTool.ConvertToString(Request["Email"]);
                string langCode = ConvertTool.ConvertToString(Request["langCode"]);

                // Kiểm tra sự tồn tại của tài khoản
                var objCustomer = ModProduct_CustomersService.Instance.CreateQuery().Where(o => o.UserName == User && o.Email == Email).ToSingle();
                if (objCustomer == null)
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = "Không tồn tại tài khoản có cùng thông tin tên đăng nhập và email đã nhập.\r\nXin liên hệ với quản trị để biết thêm thông tin chi tiết.";
                    return;
                }

                string sKeyReset = VSW.Lib.Global.ConvertTool.GetMd5Sum(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fffff"));
                objCustomer.DateRequestReset = DateTime.Now;
                objCustomer.Reseted = false;
                objCustomer.KeyReset = sKeyReset;

                // Đẩy vào Db
                ModProduct_CustomersService.Instance.Save(objCustomer);

                #region Gửi email
                string sHostApp = ConvertTool.GetKeyApp("HostApp");
                int iPort = ConvertTool.ConvertToInt32(ConvertTool.GetKeyApp("EmailPort"));
                string sHost = ConvertTool.GetKeyApp("EmailServer");
                string sTaiKhoanEmail = ConvertTool.GetKeyApp("EmailSent");
                string sMatKhau = ConvertTool.GetKeyApp("EmailPass");

                string sBody = "Xin chào " + objCustomer.FullName + " ( " + objCustomer.UserName + " )<br/>";
                sBody += "Chúng tôi nhận được yêu cầu đặt lại mật khẩu từ trang web " + sHostApp + ".<br/><br/>";
                sBody += "Bấm <a href='http://" + sHostApp + "/" + langCode + "/tai-khoan/Doi-mat-khau.aspx?Key=" + sKeyReset + "'>" + "Vào đây</a> để đặt lại mật khẩu.";
                sBody += "<br/><br/><hr/>";
                sBody += "<div style=\"font-weight:bold;\">Đây là Email gửi tự động của hệ thống quản lý website " + sHostApp + ". Xin không trả lời lại email này.</div>";

                bool bolSendmail = ConvertTool.DoSendMail("Admin - Quản trị website", Email, "Đặt lại mật khẩu trang web " + sHostApp, sBody,
                      iPort, sHost, sTaiKhoanEmail, sMatKhau);

                if (!bolSendmail)
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = "Phát sinh lỗi trong quá trình gửi mail yêu cầu đặt lại mật khẩu. Xin hãy thử lại sau.";
                    return;
                }
                #endregion

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = "Gửi yêu cầu thành công. Xin hãy kiểm tra hòm thư điện tử để đặt lại mật khẩu mới";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Reset pass
        /// </summary>
        private void ResetPass()
        {
            try
            {
                int CustomerID = ConvertTool.ConvertToInt32(Request["CustomerID"]);
                string Pass = ConvertTool.ConvertToString(Request["Pass"]);

                // Kiểm tra sự tồn tại của tài khoản
                var objCustomer = ModProduct_CustomersService.Instance.GetByID(CustomerID);
                if (objCustomer == null)
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = "Không tồn tại tài khoản.\r\nXin liên hệ với quản trị để biết thêm thông tin chi tiết.";
                    return;
                }

                objCustomer.Pass = VSW.Lib.Global.Security.MD5(Pass);
                objCustomer.DateReset = DateTime.Now;
                objCustomer.Reseted = true;

                // Đẩy vào Db
                ModProduct_CustomersService.Instance.Save(objCustomer);

                // Thông báo
                objDataOutput.MessSuccess = "Đặt lại mật khẩu thành công.\r\nXin hãy đăng nhập hệ thống.";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Gửi thông tin khảo sát
        /// </summary>
        private void SentSurvey()
        {
            try
            {
                string Name_Input = ConvertTool.ConvertToString(Request[Request.QueryString["name"]]);

                // Nếu không có input nào được chọn
                if (string.IsNullOrEmpty(Name_Input))
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = "Xin hãy chọn thông tin khảo sát trước khi gửi";
                    return;
                }

                // Lấy danh sách các thuộc tính Survey tăng chỉ số
                var listSurveyDetail = ModProduct_SurveyGroup_DetailService.Instance.CreateQuery()
                    .WhereIn(o => o.ID, Name_Input)
                    .ToList();

                if (listSurveyDetail == null || listSurveyDetail.Count <= 0)
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = "Không tìm thấy thông tin khảo sát nào đã chọn";
                    return;
                }

                foreach (var itemSurvey in listSurveyDetail)
                {
                    itemSurvey.Vote = itemSurvey.Vote + 1;
                }

                // Lưu thay đổi
                ModProduct_SurveyGroup_DetailService.Instance.Save(listSurveyDetail);

                // Set cookie xác nhận đã làm Surey --> Đóng form popup
                if (VSW.Lib.Global.Cookies.Exists("VSW.PopupSurvey"))
                    VSW.Lib.Global.Cookies.SetValue("VSW.PopupSurvey", "0");
                else
                    VSW.Lib.Global.Cookies.SetValue("VSW.PopupSurvey", "0", 30);

                // Thông báo
                objDataOutput.MessSuccess = "Gửi thông tin khảo sát thành công.\r\nXin chân thành cảm ơn thông tin phản hồi của khách hàng.";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Cập nhật trạng thái xem có mở popup hay không
        /// </summary>
        private void UpdateSurvey()
        {
            try
            {
                string PopupSurvey = ConvertTool.ConvertToString(Request.QueryString["PopupSurvey"]);

                // Set cookie xác nhận đã làm Surey --> Đóng form popup
                if (VSW.Lib.Global.Cookies.Exists("VSW.PopupSurvey"))
                    VSW.Lib.Global.Cookies.SetValue("VSW.PopupSurvey", PopupSurvey, 30);
                else
                    VSW.Lib.Global.Cookies.SetValue("VSW.PopupSurvey", PopupSurvey, 30);
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Cập nhật cookie
        /// </summary>
        private void UpdateCookie()
        {
            try
            {
                string CookieName = ConvertTool.ConvertToString(Request.QueryString["CookieName"]);
                string PopupStatus = ConvertTool.ConvertToString(Request.QueryString["PopupStatus"]);

                // Set cookie xác nhận đã làm Surey --> Đóng form popup
                if (VSW.Lib.Global.Cookies.Exists(CookieName))
                    VSW.Lib.Global.Cookies.SetValue(CookieName, PopupStatus, 30);
                else
                    VSW.Lib.Global.Cookies.SetValue(CookieName, PopupStatus, 30);
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Gửi thông tin phản hồi
        /// </summary>
        private void SentFeedback()
        {
            try
            {
                var item = new ModFeedbackEntity();

                string CookieName = ConvertTool.ConvertToString(Request["CookieName"]);
                item.Name = ConvertTool.ConvertToString(Request["Name" + CookieName]);
                item.Address = ConvertTool.ConvertToString(Request["Address" + CookieName]);
                item.Phone = ConvertTool.ConvertToString(Request["Phone" + CookieName]);
                item.Email = ConvertTool.ConvertToString(Request["Email" + CookieName]);
                item.Title = ConvertTool.ConvertToString(Request["Title" + CookieName]);
                item.Content = ConvertTool.ConvertToString(Request["Content" + CookieName]);

                item.IP = VSW.Core.Web.HttpRequest.IP;
                item.Created = DateTime.Now;

                // Lưu thay đổi
                ModFeedbackService.Instance.Save(item);

                // Set cookie xác nhận đã làm thực hiện --> Đóng form popup 
                VSW.Lib.Global.Cookies.SetValue(CookieName, "0", 30);

                // Thông báo
                objDataOutput.MessSuccess = "Gửi thông tin thành công.\r\nXin chân thành cảm ơn thông tin phản hồi của khách hàng.";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

        /// <summary>
        /// Đăng ký nhận Email
        /// </summary>
        private void AddMailLetter()
        {
            try
            {
                var item = new ModListMailNewsLetterEntity();
                string CookieName = ConvertTool.ConvertToString(Request["CookieName"]);
                item.Email = ConvertTool.ConvertToString(Request["Email" + CookieName]).ToLower();

                var CheckExist = ModListMailNewsLetterService.Instance.CreateQuery().Where(o => o.Email == item.Email).ToList();

                // Chưa tồn tại email
                if (CheckExist == null || CheckExist.Count <= 0)
                {
                    item.Name = ConvertTool.ConvertToString(Request["Name" + CookieName]);
                    item.Sex = ConvertTool.ConvertToInt32(Request["Sex" + CookieName]) == 0 ? false : true;
                    item.CodeRemoveList = "ML" + VSW.Lib.Global.Utils.GetRandString();
                    item.IP = VSW.Core.Web.HttpRequest.IP;
                    item.CreateDate = DateTime.Now;
                    item.Activity = true;

                    // Lưu thay đổi
                    ModListMailNewsLetterService.Instance.Save(item);

                    // Set cookie xác nhận đã làm thực hiện --> Đóng form popup 
                    VSW.Lib.Global.Cookies.SetValue(CookieName, "0", 30);
                }
                else
                {
                    objDataOutput.MessSuccess = "Email của bạn đã được đăng ký.\r\nXin chân thành cảm ơn quý khách hàng.";
                    return;
                }

                // Thông báo
                objDataOutput.MessSuccess = "Đăng ký nhận bản tin thành công.\r\nXin chân thành cảm ơn quý khách hàng.";
            }
            catch (Exception)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi, xin hãy thử lại";
            }
        }

    }

    /// <summary>
    /// Class, Data Output
    /// </summary>
    public class DataOutput : VSW.Website.Tools.Ajax.Common.DataOutput
    {
        /// <summary>
        /// 0: User name | 1: Email
        /// </summary>
        public int Type { get; set; }
        public bool NoProduct { get; set; }
        public string ListComment { get; set; }

        public DataOutput()
        {
            NoProduct = false;
        }
    }
}