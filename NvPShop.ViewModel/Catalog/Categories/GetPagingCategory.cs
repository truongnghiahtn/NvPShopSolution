using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.Catalog.Categories
{
    public class GetPagingCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? IdParent { get; set; }
        public string Description { get; set; }

        public List<CategoryVm> Data { get; set; }
    }
}
