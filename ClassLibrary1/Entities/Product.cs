using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class Product
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public bool? IsFeatured { get; set; }

        public bool status { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateUpdate { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Cart> Carts { get; set; }

        public List<ProductTranslation> ProductTranslations { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public List<Promotion> Promotions { get; set; }

        public List<ComboProduct> ComboProducts { get; set; }
        public List<CommentProduct> CommentProducts { get; set; }

        public List<LikesProduct> LikesProducts { get; set; }
    }
}
