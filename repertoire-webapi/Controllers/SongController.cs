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
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepo;
        public SongController(ISongRepository songRepository)
        {
            _songRepo = songRepository;
        }

        [HttpGet()]
        public IActionResult GetAllSongs(int userId)
        {
            return Ok(_songRepo.GetAllSongs(userId).OrderBy(s => s.Id));
        }

        [HttpGet("{songId}")]
        public IActionResult GetSongById(int songId)
        {
            var song = _songRepo.GetSongById(songId);
            if (song != null)
            {
                return Ok(song);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{songId}")]
        public IActionResult Update(int songId, Song song)
        {
            if (songId != song.Id)
            {
                return BadRequest();
            }
            _songRepo.UpdateSong(song);
            return NoContent();
        }

        [HttpDelete("{songId}")]
        public IActionResult Delete(int songId)
        {
            try
            {
                _songRepo.DeleteSong(songId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
