using NvPShop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
