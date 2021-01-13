using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NvpShop.Application.Common;
using NvPShop.Data.EF;
using NvPShop.Data.Entities;
using NvPShop.ViewModel.Catalog.Products;
using NvPShop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly NvPShopDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ProductService(NvPShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<ApiResult<bool>> Create(CreateProductRequest request)
        {
            if( await _context.Languages.FindAsync(request.IdLanguage) == null)
            {
                return new ApiErrorResult<bool>("Ngôn ngữ không hợp lệ");
            }
            if (await _context.Categories.FindAsync(request.IdCategory) == null)
            {
                return new ApiErrorResult<bool>("Thể loại sản phẩm không hợp lệ");
            }
            var languages = _context.Languages;
            var translations = new List<ProductTranslation>();
            foreach(var language in languages)
            {
                if (language.Id == request.IdLanguage)
                {
                    translations.Add(new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        IdLanguage = request.IdLanguage
                    });
                }
                else
                {
                    translations.Add(new ProductTranslation()
                    {
                        Name = "N/A",
                        Description = "N/A",
                        SeoAlias = "N/A",
                        IdLanguage = language.Id
                    });
                }
            }
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.UtcNow.AddHours(7),
                ProductTranslations = translations
            };

            product.ProductInCategories = new List<ProductInCategory>()
            {
                new ProductInCategory()
                {
                    IdCategory=request.IdCategory
                }
            };
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

        
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Tạo thành công");
        }

        public async Task<ApiResult<bool>> Delete(int idProduct)
        {
            var product = await _context.Products.FindAsync(idProduct);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy sản phẩm");
            }

            var images = _context.ProductImages.Where(i => i.IdProduct == idProduct);
            foreach (var image in images)
            {
                string cutImage = image.ImagePath.Substring(14);
                await _storageService.DeleteFileAsync(cutImage);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<PagedResult<ProductVm>>> GetAllPaging(GetProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.IdProduct
                        join pic in _context.ProductInCategories on p.Id equals pic.IdProduct into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.IdCategory equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.Id equals pi.IdProduct into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.IdLanguage == request.IdLanguage && pi.IsDefault == true
                        select new { p, pt,pic, pi };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword)|| x.pt.Description.Contains(request.Keyword));

            if (request.IdCategory != null && request.IdCategory != 0)
            {
                query = query.Where(p => p.pic.IdCategory == request.IdCategory);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.IdLanguage,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<ProductVm>>(pagedResult);
        }

        public async Task<ApiResult<ProductVm>> GetById(int idProduct, string idLanguage)
        {
            var product = await _context.Products.FindAsync(idProduct);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.IdProduct == idProduct
            && x.IdLanguage == idLanguage);

            var categories = await(from c in _context.Categories
                                   join ct in _context.CategoryTranslations on c.Id equals ct.IdCategory
                                   join pic in _context.ProductInCategories on c.Id equals pic.IdCategory
                                   where pic.IdProduct == idProduct && ct.IdLanguage == idLanguage
                                   select ct.Name).ToListAsync();

            var image = await _context.ProductImages.Where(x => x.IdProduct == idProduct && x.IsDefault == true).FirstOrDefaultAsync();

            var productViewModel = new ProductVm()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.IdLanguage,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                Categories = categories,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return new ApiSuccessResult<ProductVm>(productViewModel) ;
        }

        public async Task<ApiResult<bool>> Update(UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.IdProduct == request.Id
            && x.IdLanguage == request.IdLanguage);

            if (product == null || productTranslations == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy sản phẩm");
            }    

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;

            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.IdProduct == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            _context.ProductTranslations.Update(productTranslations);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }

        public async Task<ApiResult<bool>> UpdatePrice(int idProduct, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(idProduct);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy sản phẩm");
            }
            product.Price = newPrice;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }

        public async Task<ApiResult<bool>> UpdateStock(int idProduct, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(idProduct);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy sản phẩm");
            }
            product.Stock += addedQuantity;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }

        public async Task AddViewcount(int idProduct)
        {
            var product = await _context.Products.FindAsync(idProduct);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}
