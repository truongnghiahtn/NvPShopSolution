using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class Promotion
    {
        public int Id { set; get; }
        public int IdProduct { get; set; }
        public string Name { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public int? DiscountPercent { set; get; }
        public string Description { set; get; }
        public bool Status { set; get; }
        public Product Product { get; set; }
       

    }
}
