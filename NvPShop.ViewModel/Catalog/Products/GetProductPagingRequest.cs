using NvPShop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.Catalog.Products
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public string IdLanguage { get; set; }

        public int? IdCategory { get; set; }
    }
}
