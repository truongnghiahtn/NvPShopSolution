using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.Catalog.Categories
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public int IdParent { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { set; get; }
        public string SeoAlias { get; set; }
        public string IdLanguage { set; get; }
    }
}
