using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Quản lý Đơn hàng",
        Description = "Quản lý - Đơn hàng",
        Code = "ModProduct_Order",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component")]
    public class ModProduct_OrderController : CPController
    {
        public ModProduct_OrderController()
        {
            //khoi tao Service
            DataService = ModProduct_OrderService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_OrderModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_OrderService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_OrderModel model)
        {
            List<ModProduct_Order_DetailsEntity> lstOrder_Details = new List<ModProduct_Order_DetailsEntity>();
            if (model.RecordID > 0)
            {
                item = ModProduct_OrderService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
                lstOrder_Details = ModProduct_Order_DetailsService.Instance.CreateQuery().Where(p => p.OrderId == item.ID).ToList();
                if (lstOrder_Details == null)
                    lstOrder_Details = new List<ModProduct_Order_DetailsEntity>();
            }
            else
            {
                item = new ModProduct_OrderEntity();

                // khoi tao gia tri mac dinh khi insert
            }
            ViewBag.ListOrderDetails = lstOrder_Details;
            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public override void ActionDelete(int[] arrID)
        {
            string sMess = string.Empty;
            try
            {
                ModProduct_Order_DetailsService.Instance.Delete("[OrderId] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")");
                DataService.Delete("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")");

                // thành công
                sMess = "Đã xóa thành công.";
            }
            catch (Exception ex)
            {
                // Lỗi phát sinh
                sMess = "Xóa thất bại. Lỗi phát sinh trong quá trình xóa hoặc tồn tại ràng buộc dữ liệu:" + ex.Message;
            }

            //thong bao
            CPViewPage.SetMessage(sMess);
            CPViewPage.RefreshPage();
        }

        public void ActionDeleteproductincart(int[] arrID)
        {
            int ProductDetailId = arrID[0]; int iOrderId = arrID[1];

            if (ProductDetailId <= 0 || iOrderId <= 0)
            {
                CPViewPage.SetMessage("Không tìm thấy thông tin đơn hàng. Hãy thử lại");
                CPViewPage.RefreshPage();
                return;
            }

            ModProduct_Order_DetailsService.Instance.Delete(ProductDetailId);

            // Cập nhật lại thông tin đơn hàng
            var Order = ModProduct_OrderService.Instance.GetByID(iOrderId);
            if (Order == null)
            {
                CPViewPage.SetMessage("Xóa sản phẩm khỏi đơn hàng thất bại. Hãy thử lại");
                CPViewPage.RefreshPage();
                return;
            }

            var lstOrderDetail = ModProduct_Order_DetailsService.Instance.CreateQuery().Where(o => o.OrderId == Order.ID).ToList();
            if (lstOrderDetail == null || lstOrderDetail.Count <= 0)
            {
                Order.QuantityProduct = 0;
                Order.QuantityTotal = 0;
                Order.TotalFrice = 0;
                Order.TotalFriceFirst = 0;
            }
            // Nếu còn sản phẩm --> Cập nhật thông tin
            else
            {
                int iQuantityTotal = 0;
                int iQuantityProduct = 0;
                double dTotalFriceFirst = 0;

                foreach (var itemCart in lstOrderDetail)
                {
                    // Số lượng tất cả
                    iQuantityTotal += itemCart.Quantity;

                    // số lượng từng loại
                    iQuantityProduct++;

                    dTotalFriceFirst += itemCart.TotalFrice;
                }

                // Cập nhật lại thông tin đơn hàng
                Order.QuantityProduct = iQuantityProduct;
                Order.QuantityTotal = iQuantityTotal;
                Order.Discount = 0;

                // Số tiền tính được (Bao gồm cả VAT nếu có)
                Order.TotalFriceFirst = dTotalFriceFirst;

                // Sau khi đã giảm trừ (Giả sử từ cho 200 khi đặt hàng online)
                Order.TotalFrice = Order.TotalFriceFirst - Order.Discount;
            }

            // Ngày thay đổi
            Order.ModifiedDate = DateTime.Now;

            // Lưu lại
            ModProduct_OrderService.Instance.Save(Order);

            CPViewPage.SetMessage("Xóa sản phẩm khỏi đơn hàng thành công.");
            CPViewPage.RefreshPage();
        }

        public void ActionSave(ModProduct_OrderModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_OrderModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_OrderModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionNguoiDat_SexGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@NguoiDat_Sex", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        public void ActionNguoiNhan_SexGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@NguoiNhan_Sex", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModProduct_OrderEntity item = null;

        private bool ValidSave(ModProduct_OrderModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModProduct_OrderService.Instance.Save(item);
                }
                catch (Exception ex)
                {
                    Global.Error.Write(ex);
                    CPViewPage.Message.ListMessage.Add(ex.Message);
                    return false;
                }

                return true;
            }

            return false;
        }

        #endregion
    }

    public class ModProduct_OrderModel : DefaultModel
    {
    }
}

