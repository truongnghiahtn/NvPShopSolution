using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NvPShop.Data.EF;
using NvPShop.ViewModel.Common;
using NvPShop.ViewModel.System.Languages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly NvPShopDbContext _context;

        public LanguageService(NvPShopDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<List<LanguageVm>>> GetAll()
        {
            var languages = await _context.Languages.Select(x => new LanguageVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new ApiSuccessResult<List<LanguageVm>>(languages);
        }
    }
}