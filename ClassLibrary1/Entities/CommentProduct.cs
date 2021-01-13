using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public  class CommentProduct
    {
        public int Id { get; set; }
        public Guid IdUser { get; set; }

        public int IdProduct { get; set; }

        public string Message { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public AppUser AppUser { get; set; }

        public Product Product { get; set; }
    }
}
