using AuthenticationService.Logic;
using AuthenticationService.Mappers;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> logger;
        private AuthenticationFacade _facade;

        public AuthenticationController(ILogger<AuthenticationController> logger, AuthenticationFacade facade)
        {
            this.logger = logger;
            _facade = facade;
        }

        [HttpPost]
        [Route("/auth/register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Register([FromBody] UserDTO user)
        {
            Debug.WriteLine("register");

            _facade.Register(UserMapper.MapDTOToDBO(user));

            return Ok();
        }

        [HttpGet]
        [Route("/auth/login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] UserDTO User)
        {
            throw new NotImplementedException();
        }
    }
}
