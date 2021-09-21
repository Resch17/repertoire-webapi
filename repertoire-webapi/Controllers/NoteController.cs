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
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepo;
        public NoteController(INoteRepository noteRepository)
        {
            _noteRepo = noteRepository;
        }

        [HttpGet]
        public IActionResult GetNotesBySong(int songId, int userId)
        {
            return Ok(_noteRepo.GetNotesBySongId(songId, userId));
        }
    }
}
