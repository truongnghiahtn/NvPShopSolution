using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NvpShop.Application.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NvPShop.BackEnd.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _languageService.GetAll();
            return Ok(result);
        }
    }
}
