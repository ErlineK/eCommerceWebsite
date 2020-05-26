using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erline_eCommerce.Models
{
    public class CartItem
    {
        public int ProdId { get; set; } //product ID from Products table
        public string Description { get; set; } //product name/description
        public string Pic { get; set; } //product pic reference
        public decimal Price { get; set; } // product price from DB
        public int Qty { get; set; } = 1; //product qty in cart

        public CartItem() { }
        public CartItem(int prodId, string description, string pic, decimal price)
        {
            ProdId = prodId;
            Description = description;
            Pic = pic;
            Price = price;
            Qty = 1;
        }

    }
}