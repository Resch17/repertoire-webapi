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
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepo;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepo = genreRepository;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            return Ok(_genreRepo.GetAllGenres());
        }
    }
}
