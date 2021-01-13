using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.Catalog.Categories
{
    public class CategoryVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? IdParent { get; set; }

        public string Descripion { get; set; }
    }
}