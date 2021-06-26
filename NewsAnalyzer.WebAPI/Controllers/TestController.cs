using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("getDefault")]
        public IActionResult GetDefault()
        {
            return Ok("Authorized User");
        }

        [HttpGet("getAdmin")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdmin()
        {
            return Ok("Authorized User");
        }

        [HttpGet("unauthorized")]
        [AllowAnonymous]
        public IActionResult Unauthorized()
        {
            return Ok("Unauthorized User");
        }
    }
}
