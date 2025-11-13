using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Shared.Dtos.Auth;
using Store.G05.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> Login(LoginRequest logInDto)
        {
            var result = await serviceManager.AuthServices.LoginAsync(logInDto);
            return Ok(result);
        }

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerDto)
        {
            var result = await serviceManager.AuthServices.RegisterAsync(registerDto);
            return Ok(result);
        }

        // Check Email Exist
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            var result = await serviceManager.AuthServices.CheckEmailExist(email);
            return Ok(result);
        }

        // Get Current User
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await serviceManager.AuthServices.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }

        // Get Current User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await serviceManager.AuthServices.GetCurrentUserAdressAsync(email.Value);
            return Ok(result);
        }

        // Update Current User Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await serviceManager.AuthServices.UpdateCurrentUserAdressAsync(addressDto, email.Value);
            return Ok(result);
        }

    }
}
