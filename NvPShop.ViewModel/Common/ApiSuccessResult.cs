using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.Common
{
    public class ApiSuccessResult<T>:ApiResult<T>
    {
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
        public ApiSuccessResult( string mess)
        {
            Message = mess;
            IsSuccessed = true;
        }
        public ApiSuccessResult(T result)
        {
            IsSuccessed = true;
            Data = result;
            Message = "Thành công";
        }
    }
}
