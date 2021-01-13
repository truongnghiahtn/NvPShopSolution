using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class OrderDetail
    {
        public int IdOrder { set; get; }
        public int IdProduct { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
