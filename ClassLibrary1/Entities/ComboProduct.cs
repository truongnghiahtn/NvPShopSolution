using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class ComboProduct
    {
        public int Id { get; set; }

        public int IdProduct { get; set; }
        
        public int IdCombo { get; set; }

        public Product Product { get; set; }

        public Combo Combo { get; set; }

    }
}
