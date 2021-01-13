using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public  class LikesProduct
    {

        public Guid IdUser { get; set; }

        public int IdProduct { get; set; }

        public bool Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public AppUser AppUser { get; set; }

        public Product Product { get; set; }
    }
}
