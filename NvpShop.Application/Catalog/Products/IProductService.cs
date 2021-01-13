using NvPShop.ViewModel.Catalog.Products;
using NvPShop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<ApiResult<bool>> Create(CreateProductRequest request);

        Task<ApiResult<bool>> Update(UpdateProductRequest request);

        Task<ApiResult<bool>> Delete(int idProduct);

        Task<ApiResult<ProductVm>> GetById(int idProduct, string idLanguage);

        Task<ApiResult<bool>> UpdatePrice(int idProduct, decimal newPrice);

        Task<ApiResult<bool>> UpdateStock(int idProduct, int addedQuantity);

        Task AddViewcount(int idProduct);
        Task<ApiResult<PagedResult<ProductVm>>> GetAllPaging(GetProductPagingRequest request);
    }
}
