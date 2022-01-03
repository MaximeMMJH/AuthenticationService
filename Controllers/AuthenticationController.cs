using AuthenticationService.Logic;
using AuthenticationService.Mappers;
using AuthenticationService.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("/auth")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> logger;
        private AuthenticationFacade _facade;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationController(ILogger<AuthenticationController> logger, AuthenticationFacade facade, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.logger = logger;
            _facade = facade;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("/auth/ping")]
        public IActionResult Ping()
        {
            return Ok("Hello world!");
        }

        [HttpPost]
        [Route("/auth/register")]
        [ProducesResponseType(typeof(RegisterModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            Debug.WriteLine("register");

            IdentityUser identityUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (!result.Succeeded)
            {
                return Conflict(result.Errors.First().Description);
            }

            _facade.Register(UserMapper.MapDTOToDBO(user));

            return Ok(user);
        }

        [HttpPost]
        [Route("/auth/login")]
        [ProducesResponseType(typeof(IdentityUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var user = await _signInManager.UserManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return NotFound($"No user found for {model.Username}");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
            {
                return BadRequest("incorrect password");
            }

            IdentityServerUser issuer = new IdentityServerUser(model.Username)
            {
                DisplayName = model.Username,
            };

            await HttpContext.SignInAsync(issuer);

            return Ok(model);
        }
    }
}
