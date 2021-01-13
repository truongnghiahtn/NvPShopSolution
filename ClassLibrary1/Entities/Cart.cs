using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class Cart
    {
        public int Id { set; get; }
        public int IdProduct { set; get; }
        public Guid IdUser { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdate { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}
