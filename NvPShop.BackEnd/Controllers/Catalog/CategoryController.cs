using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NvpShop.Application.Catalog.Categories;
using NvPShop.ViewModel.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NvPShop.BackEnd.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string idLanguage)
        {
            var result = await _categoryService.GetAll(idLanguage);
            return Ok(result);
        }

        [HttpGet("{id}/{languageId}")]
        public async Task<IActionResult> GetById(string idLanguage, int id)
        {
            var result = await _categoryService.GetById(idLanguage, id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.CreateCategory(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.UpdateCategory(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.DeleteCategory(id);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
