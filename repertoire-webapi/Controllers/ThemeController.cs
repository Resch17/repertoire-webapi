using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using repertoire_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeRepository _themeRepo;

        public ThemeController(IThemeRepository themeRepository)
        {
            _themeRepo = themeRepository;
        }

        [HttpGet]
        public IActionResult GetAllThemes()
        {
            return Ok(_themeRepo.GetAllThemes());
        }
    }
}
