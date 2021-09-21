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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userRepo.GetUser(userId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            try
            {
                _userRepo.AddUser(user);
                return Created("/user", new { user.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
