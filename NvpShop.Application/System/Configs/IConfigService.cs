using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Configs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Configs
{
    public interface IConfigService
    {
        Task<ApiResult<List<ConfigVm>>> GetAll();

        Task<ApiResult<bool>> CreateConfig(CreateConfigRequest request);

        Task<ApiResult<bool>> DeleteConfig(string id);

        Task<ApiResult<ConfigVm>> UpdateConfig(UpdateConfigRequest request);
    }
}
