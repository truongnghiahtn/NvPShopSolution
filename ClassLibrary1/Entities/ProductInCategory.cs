using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class ProductInCategory
    {
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }

        public Product Product { get; set; }

        public Category Category { get; set; }

    }
}
