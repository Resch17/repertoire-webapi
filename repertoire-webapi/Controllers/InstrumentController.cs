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
    public class InstrumentController : ControllerBase
    {
        private readonly IInstrumentRepository _instrumentRepo;
        public InstrumentController(IInstrumentRepository instrumentRepository)
        {
            _instrumentRepo = instrumentRepository;
        }

        [HttpGet]
        public IActionResult GetAllInstruments()
        {
            return Ok(_instrumentRepo.GetAllInstruments());
        }
    }
}
