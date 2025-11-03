using Microsoft.AspNetCore.Mvc;
using Store.G02.Shared.Dtos;
using Store.G05.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        // Login
        [HttpPost("login")] // 
        public async Task<IActionResult> Login(LogInDto logInDto)
        {
            var result = await serviceManager.AuthServices.LoginAsync(logInDto);
            return Ok(result);
        }

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await serviceManager.AuthServices.RegisterAsync(registerDto);
            return Ok(result);
        }

    }
}
