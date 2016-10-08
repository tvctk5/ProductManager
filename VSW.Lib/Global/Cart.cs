using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VSW.Lib.Global
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int MenuID { get; set; }
        public int LangID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public bool ShowPrice { get; set; }
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public double TotalFrice { get; set; }
        //// Có thể đặt hàng online đc giảm giá
        //public double Discount { get; set; }
        public bool SaleOffType { get; set; }
        public string PriceTextSaleView { get; set; }
        public bool VAT { get; set; }
        public bool ShowVAT { get; set; }
        public string ProductsConnection { get; set; }
        public string ProductsAttach { get; set; }
        public string Gifts { get; set; }
        public bool Attach { get; set; }
        public string Note { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is CartItem)
            {
                CartItem temp = obj as CartItem;

                return ProductID.Equals(temp.ProductID);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (ProductID).GetHashCode();
        }
    }

    public class Cart
    {
        private List<CartItem> listItem = new List<CartItem>();
        private string cKey = "VSW_Cart";

        public ReadOnlyCollection<CartItem> Items { get { return listItem.AsReadOnly(); } }

        public int Count
        {
            get { return listItem.Count; }
        }

        public Cart()
            : this(string.Empty)
        {

        }

        public Cart(string service_name)
        {
            cKey += service_name;

            if (ObjectCookies<List<CartItem>>.Exists(cKey))
                listItem = ObjectCookies<List<CartItem>>.GetValue(cKey);

            if (listItem == null)
                listItem = new List<CartItem>();
        }

        public bool Exists(CartItem Item)
        {
            if (listItem.Contains(Item))
                return true;
            else
                return false;
        }

        public void Add(CartItem Item)
        {
            Remove(Item);

            listItem.Add(Item);
        }

        public CartItem Find(CartItem Item)
        {
            return listItem.Find(o => o.Equals(Item));
        }

        public void Remove(CartItem Item)
        {
            if (Exists(Item))
                listItem.Remove(Item);
        }

        public void RemoveAll()
        {
            listItem.Clear();
        }

        public void Save()
        {
            if (listItem.Count > 0)
                ObjectCookies<List<CartItem>>.SetValue(cKey, listItem);
            else
                ObjectCookies<List<CartItem>>.Remove(cKey);
        }
    }
}
