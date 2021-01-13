using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }

        public List<Transaction> Transactions { get; set; }
        public List<LikesProduct> LikesProducts { get; set; }
        public List<CommentProduct> CommentProducts { get; set; }
    }
}
