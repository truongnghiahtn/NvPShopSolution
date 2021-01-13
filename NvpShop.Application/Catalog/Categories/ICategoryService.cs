using NvPShop.ViewModel.Catalog.Categories;
using NvPShop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<ApiResult<List<GetPagingCategory>>> GetAll(string idLanguage);

        Task<ApiResult<CategoryVm>> GetById(string idLanguage, int id);

        Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request);

        Task<ApiResult<bool>> UpdateCategory(UpdateCategoryRequest request);

        Task<ApiResult<bool>> DeleteCategory(int id);

        Task<ApiResult<bool>> UpdateCategoryAction();
    }
}
