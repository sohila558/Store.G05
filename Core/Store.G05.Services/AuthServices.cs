using Microsoft.AspNetCore.Identity;
using Store.G02.Shared.Dtos;
using Store.G05.Domain.Entities.Identity;
using Store.G05.Domain.Exceptions;
using Store.G05.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services
{
    public class AuthServices(UserManager<AppUser> _userManager) : IAuthServices
    {
        public async Task<UserResultDto> LoginAsync(LogInDto logInDto)
        {
            var user = await _userManager.FindByEmailAsync(logInDto.Email);
            if (user is null) throw new UnAuthorizedException();
            var flag = await _userManager.CheckPasswordAsync(user, logInDto.Password);
            if (!flag) throw new UnAuthorizedException();
            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = ""
            };
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                throw new ValidationException(errors);
            }

            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = ""
            };
        }
    }
}
