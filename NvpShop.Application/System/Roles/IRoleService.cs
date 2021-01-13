using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Roles
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleVm>>> GetAll();

        Task<ApiResult<bool>> CreateRole(CreateRoleRequest request);

        Task<ApiResult<bool>> DeleteRole(Guid id);

        Task<ApiResult<RoleVm>> UpdateRole(UpdateRoleRequest request);
    }
}
