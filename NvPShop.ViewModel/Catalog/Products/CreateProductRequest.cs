﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.Catalog.Products
{
    public class CreateProductRequest
    {
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public bool? IsFeatured { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public string IdLanguage { set; get; }
        public int IdCategory { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
