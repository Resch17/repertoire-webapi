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
    public class TuningController : ControllerBase
    {
        private readonly ITuningRepository _tuningRepo;
        public TuningController(ITuningRepository tuningRepository)
        {
            _tuningRepo = tuningRepository;
        }

        [HttpGet]
        public IActionResult GetAllTunings()
        {
            return Ok(_tuningRepo.GetAllTunings().ToList());
        }

        [HttpGet("{tuningId}")]
        public IActionResult GetTuningById(int tuningId)
        {
            Tuning tuning = null;
            tuning = _tuningRepo.GetTuningById(tuningId);
            if (tuning != null)
            {
                return Ok(tuning);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
