using NvPShop.Data.EF;
using NvPShop.Data.Entities;
using NvPShop.ViewModel.Catalog.Categories;
using NvPShop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NvpShop.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly NvPShopDbContext _context;
        public CategoryService(NvPShopDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request)
        {
            if(await _context.Languages.FindAsync(request.IdLanguage) == null)
            {
                return new ApiErrorResult<bool>("Ngôn ngữ không đúng");
            }
            var languages = _context.Languages;
            var translations = new List<CategoryTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.IdLanguage)
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = request.Name,
                        Description=request.Description,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        IdLanguage = request.IdLanguage,
                        
                    });
                }
                else
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = "N/A",
                        Description="N/A",
                        SeoDescription = "N/A",
                        SeoAlias = "N/A",
                        SeoTitle = "N/A",
                        IdLanguage = language.Id
                    });
                }
            }
            var category = new Category()
            {
                Status = true,
                IdParent = request.IdParent,
                SortOrder = request.SortOrder,
                CategoryTranslations = translations,
                DateCreate=DateTime.UtcNow.AddHours(7),
                DateUpdate=DateTime.UtcNow.AddHours(7),

            };
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Thêm thành công");
        }

        public async Task<ApiResult<bool>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null )
            {
                return new ApiErrorResult<bool>("Xóa không thành công");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<List<GetPagingCategory>>> GetAll(string idLanguage)
        {
            if ((await _context.Languages.FindAsync(idLanguage)) == null)
            {
                return new ApiErrorResult<List<GetPagingCategory>>("Ngôn ngữ không đúng");
            }
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.IdCategory
                        where ct.IdLanguage == idLanguage
                        select new { c, ct };
            

            var data = await query.OrderBy(x=>x.c.SortOrder).Where(x => x.c.IdParent == 0).Select(x => new GetPagingCategory()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                IdParent = x.c.IdParent,
                Description=x.ct.Description,
                Data = query.Where(y => y.c.IdParent == x.c.Id).Select(y => new CategoryVm()
                {
                    Id = y.c.Id,
                    Name = y.ct.Name,
                    IdParent = y.c.IdParent,
                    Descripion=y.ct.Description
                    
                }).ToList()
            }).ToListAsync();
            return new ApiSuccessResult<List<GetPagingCategory>>(data);
        }

        public async Task<ApiResult<CategoryVm>> GetById(string idLanguage, int id)
        {
            if ((await _context.Languages.FindAsync(idLanguage) == null) || (await _context.Categories.FindAsync(id) == null))
            {
                return new ApiErrorResult<CategoryVm>("Không tìm thấy");
            }
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.IdCategory
                        where ct.IdLanguage == idLanguage && c.Id == id
                        select new { c, ct };
            var data = await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                IdParent = x.c.IdParent,
                Descripion=x.ct.Description
            }).FirstOrDefaultAsync();

            return new ApiSuccessResult<CategoryVm>(data);
        }

        public async Task<ApiResult<bool>> UpdateCategory(UpdateCategoryRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            var categoryTranslation = await _context.CategoryTranslations.FirstOrDefaultAsync(x => x.IdCategory == request.Id && x.IdLanguage == request.IdLanguage);

            if (category == null || categoryTranslation == null)
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }
            category.IdParent = request.IdParent;
            category.SortOrder = request.SortOrder;
            category.DateUpdate = DateTime.UtcNow.AddHours(7);
            categoryTranslation.Name = request.Name;
            categoryTranslation.Description = request.Description;
            categoryTranslation.SeoAlias = request.SeoAlias;
            categoryTranslation.SeoTitle = request.SeoTitle;
            categoryTranslation.SeoDescription = request.SeoDescription;

            _context.Categories.Update(category);
            _context.CategoryTranslations.Update(categoryTranslation);

            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public Task<ApiResult<bool>> UpdateCategoryAction()
        {
            throw new NotImplementedException();
        }
    }
}
