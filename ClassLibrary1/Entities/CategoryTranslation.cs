using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class CategoryTranslation
    {
        public int Id { set; get; }
        public int IdCategory { set; get; }
        public string IdLanguage { set; get; }
        public string Name { set; get; }
        public string Description { get; set; }
        public string? SeoDescription { set; get; }
        public string? SeoTitle { set; get; }
        public string? SeoAlias { set; get; }
        public Category Category { get; set; }
        public Language Language { get; set; }

    }
}
