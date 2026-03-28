using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_Manager.Data;
using Task_Manager.DTOs;
using Task_Manager.enums;
using Task_Manager.Models;
using Task_Manager.services;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService authService;
        private readonly ApplicationDbContext context;

        public AuthController(IAuthService authService, ApplicationDbContext dbContext)
        {

            this.authService = authService;

            this.context = dbContext;
        }

        [HttpPost("register")]


        public IActionResult Register([FromBody] RegisterDTO register)
        {


            RegisterResult result = authService.Register(register.Email, register.Password);

            return HandleResult(result);

        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {

            LoginResult result = authService.Login(login.Email, login.Password);

            return HandleResult(result);

        }

        [Authorize]
        [HttpGet("test")]
        public string test()
        {

            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        }


        



    }
}
