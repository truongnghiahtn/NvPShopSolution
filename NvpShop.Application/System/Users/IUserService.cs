using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Users;
using System;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<ResultLogin>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<UserVm>> Update(Guid id, UpdateUserRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserVm>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, AssignRoleRequest request);

        Task<ApiResult<bool>> ResetPassword(ResetPasswordRequest request);
    }
}
