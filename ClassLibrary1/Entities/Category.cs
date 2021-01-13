
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class Category
    {
        public int Id { set; get; }
        public int SortOrder { set; get; }
        public int IdParent { set; get; }
        public bool Status { set; get; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }

    }
}
