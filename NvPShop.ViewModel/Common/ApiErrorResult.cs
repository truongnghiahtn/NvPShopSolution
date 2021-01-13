using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.Common
{
    public class ApiErrorResult<T>:ApiResult<T>
    {
        public ApiErrorResult()
        {
            IsSuccessed = false;
        }
        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}
