using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : View Cart", Code = "MViewCart", Order = 50)]
    public class MViewCartController : Controller
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

        //them vao gio hang
        public void ActionAdd(int ProductID, int Quantity)
        {
            if (Quantity < 1 || Quantity > 9999)
                Quantity = 1;

            Global.Cart _Cart = new Global.Cart();

            Global.CartItem item = _Cart.Find(new Global.CartItem() { ProductID = ProductID });

            var objProduct = ModProduct_InfoService.Instance.GetByID(ProductID);

            if (item == null && objProduct != null)
                _Cart.Add(new Global.CartItem()
                {
                    ProductID = ProductID,
                    Quantity = Quantity,
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
                    ProductsAttach = objProduct.ProductsAttach
                });
            else
                item.Quantity += Quantity;

            _Cart.Save();
            ViewPage.Response.Redirect("~/gio-hang.aspx");
        }

        //cap nhap gio hang
        public void ActionUpdate(MViewCartModel model)
        {
            Global.Cart _Cart = new Global.Cart();

            for (int i = 0; i < _Cart.Items.Count; i++)
            {
                _Cart.Items[i].Quantity = (model.ArrCount[i] <= 0) ? 1 : model.ArrCount[i];
            }
            _Cart.Save();

            ViewPage.Response.Redirect("~/vn/gio-hang.aspx");
        }

        //xoa 1 san pham trong gio hang
        public void ActionDelete(int ProductID)
        {
            Global.Cart _Cart = new Global.Cart();
            _Cart.Remove(new Global.CartItem() { ProductID = ProductID });
            _Cart.Save();
            ViewPage.Response.Redirect("~/vn/gio-hang.aspx");
        }
    }

    public class MViewCartModel
    {
        public int[] ArrCount { get; set; }
    }
}
