using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NvPShop.Data.Entities;
using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService( UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<AppRole> roleManager,IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<ResultLogin>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiErrorResult<ResultLogin>("Tài khoản không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var checkRole = false;
            foreach (var role in roles)
            {
                if (role != "User" && role != "")
                {
                    checkRole = true;
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded || checkRole == false)
            {
                return new ApiErrorResult<ResultLogin>("Đăng nhập không đúng");
            }
            var claims = new[]
{
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var data = new ResultLogin()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),

            };

            return new ApiSuccessResult<ResultLogin>(data);
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>("Xóa thành công");

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("Tài khoản không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserVm()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = roles
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)|| x.FullName.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword)|| x.Email.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new UserVm()
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
            }).ToListAsync();

            var pagedResult = new PagedResult<UserVm>()
            {
                
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data,
                TotalRecords = totalRow
            };
            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);

        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }

            user = new AppUser()
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate=DateTime.UtcNow.AddHours(7),
            };
            var role = await _roleManager.FindByIdAsync(request.IdRole.ToString());
            if (role == null)
            {
                return new ApiErrorResult<bool>("Quyền không xác định");
            }
            var result = await _userManager.CreateAsync(user, request.Password);
            var result1 = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded && result1.Succeeded)
            {
                return new ApiSuccessResult<bool>("Đăng ký thành công");
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                new ApiErrorResult<bool>("Tài khoản không đúng");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>("Đổi mật khẩu thành công");
            }
            return new ApiErrorResult<bool>("Đổi mật khẩu không thành công");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, AssignRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return new ApiSuccessResult<bool>("Gắng quyền thành công");
        }

        public async Task<ApiResult<UserVm>> Update(Guid id, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("tài khoản không tồn tại");
            }
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<UserVm>("Emai đã tồn tại");
            }
           
            user.Email = request.Email;
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.DateUpdate = DateTime.UtcNow.AddHours(7);

            var result = await _userManager.UpdateAsync(user);
            //var nUser = new UserVm()
            //{
            //    Id = id,
            //    FullName = request.FullName,
            //    Email = request.Email,
            //    PhoneNumber = request.PhoneNumber,
            //    UserName = user.UserName
            //};
            if (result.Succeeded)
            {
                return new ApiSuccessResult<UserVm>("Cập nhật thành công");
            }
           
            return new ApiErrorResult<UserVm>("Cập nhật không thành công");
        }
    }
}
