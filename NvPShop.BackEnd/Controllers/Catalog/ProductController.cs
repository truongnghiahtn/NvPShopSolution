using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NvpShop.Application.Catalog.Products;
using NvPShop.ViewModel.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NvPShop.BackEnd.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Paging")]

        public async Task<IActionResult> GetAllPaging([FromQuery] GetProductPagingRequest request)
        {

            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("GetById")]

        public async Task<IActionResult> GetById([FromQuery] int idProduct, string idLanguage)
        {

            var products = await _productService.GetById(idProduct,idLanguage);
            await _productService.AddViewcount(idProduct);
            return Ok(products);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Create(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Update(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UpdatePrice")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePrice(int idProduct, decimal newPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdatePrice(idProduct, newPrice);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UpdateStock")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateStock(int idProduct, int addedQuantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateStock(idProduct, addedQuantity);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]

        public async Task<IActionResult> Delete (int idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Delete(idProduct);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
