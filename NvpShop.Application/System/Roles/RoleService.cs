using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NvPShop.Data.Entities;
using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ApiResult<bool>> CreateRole(CreateRoleRequest request)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);
            if (role != null)
            {
                return new ApiErrorResult<bool>("Quyền đã tồn tại");
            }
            role = new AppRole()
            {
                Name = request.Name,
                Description = request.Desscription,
                NormalizedName = request.Name,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate=DateTime.UtcNow.AddHours(7)

            };
            await _roleManager.CreateAsync(role);
            return new ApiSuccessResult<bool>("Tạo thành công");
        }

        public async Task<ApiResult<bool>> DeleteRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return new ApiErrorResult<bool>("Quyền không tồn tại");
            }

            await _roleManager.DeleteAsync(role);
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<List<RoleVm>>> GetAll()
        {
            var roles = await _roleManager.Roles
            .Select(x => new RoleVm()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DateCreate=x.DateCreate,
                DateUpdate=x.DateUpdate
                
            }).ToListAsync();

            return new ApiSuccessResult<List<RoleVm>>(roles);
        }

        public async Task<ApiResult<RoleVm>> UpdateRole(UpdateRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                return new ApiErrorResult<RoleVm>("Quyền không tồn tại");
            }
            role.Name = request.Name;
            role.NormalizedName = request.Name;
            role.Description = request.Description;
            role.DateUpdate = DateTime.UtcNow.AddHours(7);

            await _roleManager.UpdateAsync(role);
            var nRole = new RoleVm()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                DateCreate=role.DateCreate,
                DateUpdate= DateTime.UtcNow.AddHours(7)
        };
            return new ApiSuccessResult<RoleVm>(nRole);
        }
    }
}
