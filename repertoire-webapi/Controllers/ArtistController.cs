using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using repertoire_webapi.Interfaces;
using repertoire_webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace repertoire_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepo = artistRepository;
        }

        [HttpGet]
        public IActionResult GetAllArtists()
        {
            return Ok(_artistRepo.GetAllArtists().OrderBy(a=>a.Id));
        }

        [HttpGet("{artistId}")]
        public IActionResult GetArtistById(int artistId)
        {
            Artist artist = null;
            artist = _artistRepo.GetArtistById(artistId);
            if (artist != null)
            {
                return Ok(artist);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddArtist(Artist artist)
        {
            try
            {
                _artistRepo.AddArtist(artist);
                return Created("/artist", new { artist.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
