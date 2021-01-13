using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Languages
{
    public interface ILanguageService
    {

        Task<ApiResult<List<LanguageVm>>> GetAll();
    }
}