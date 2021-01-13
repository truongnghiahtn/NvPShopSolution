using Microsoft.EntityFrameworkCore;
using NvPShop.Data.EF;
using NvPShop.Data.Entities;
using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Configs
{
    public class ConfigService :IConfigService
    {
        private readonly NvPShopDbContext _context;
        public  ConfigService(NvPShopDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateConfig(CreateConfigRequest request)
        {
            var config = await _context.AppConfigs.FindAsync(request.Key);
            if (config != null)
            {
                return new ApiErrorResult<bool>("Config đã tồn tại");
            }
            config = new AppConfig()
            {
                Key = request.Key,
                Value = request.Key,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7)
            };

            _context.AppConfigs.Add(config);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Tạo thành công");
            
        }

        public async Task<ApiResult<bool>> DeleteConfig(string id)
        {
            var config = await _context.AppConfigs.FindAsync(id);
            if (config == null)
            {
                return new ApiErrorResult<bool>("Config không tồn tại");
            }

            _context.AppConfigs.Remove(config);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<List<ConfigVm>>> GetAll()
        {
            var data = await _context.AppConfigs.Select(x => new ConfigVm()
            {
                Key = x.Key,
                Value = x.Value,
            }).ToListAsync();

            return new ApiSuccessResult<List<ConfigVm>>(data);
        }

        public async Task<ApiResult<ConfigVm>> UpdateConfig(UpdateConfigRequest request)
        {
            var config = await _context.AppConfigs.FindAsync(request.Key);
            if (config == null)
            {
                return new ApiErrorResult<ConfigVm>("Config không tồn tại");
            }
            config.Value = request.Value;
            config.DateUpdate = DateTime.UtcNow.AddHours(7);

            _context.AppConfigs.Update(config);
            await _context.SaveChangesAsync();
            var nConfig = new ConfigVm()
            {
                Key = request.Key,
                Value = request.Value
            };
            return new ApiSuccessResult<ConfigVm>(nConfig);

        }
    }
}
