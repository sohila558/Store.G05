using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.G02.Shared;
using Store.G02.Shared.Dtos.Auth;
using Store.G05.Domain.Entities.Identity;
using Store.G05.Domain.Exceptions;
using Store.G05.Domain.Exceptions.NotFoundExceptions;
using Store.G05.Domain.Exceptions.UnAuthorizedException;
using Store.G05.Services.Abstractions.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.G05.Services.Auth
{
    public class AuthServices(UserManager<AppUser> _userManager, IOptions<JwtOptions> options, IMapper _mapper) : IAuthServices
    {
        public async Task<bool> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<UserResponse?> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) throw new UserNotFoundException(email);

            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user)
            };
        }

        public async Task<AddressDto?> GetCurrentUserAdressAsync(string email)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);
            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<AddressDto?> UpdateCurrentUserAdressAsync(AddressDto request, string email)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);

            if (user.Address is null)
            {
                // Create New Address
                user.Address = _mapper.Map<Address>(request);
            }
            else
            {
                user.Address.FirstName = request.FirstName;
                user.Address.LastName = request.LastName;
                user.Address.City = request.City;
                user.Address.Country = request.Country;
                user.Address.Street = request.Street;
            }

            await _userManager.UpdateAsync(user);

            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<UserResponse> LoginAsync(LoginRequest logInDto)
        {
            var user = await _userManager.FindByEmailAsync(logInDto.Email);
            if (user is null) throw new UnAuthorizedException();
            var flag = await _userManager.CheckPasswordAsync(user, logInDto.Password);
            if (!flag) throw new UnAuthorizedException();
            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user)
            };
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerDto)
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

            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user)
            };
        }

        private async Task<string> GenerateJwtTokenAsync(AppUser user)
        {
            // Header
            // Payload
            // Signature

            var jwtOptions = options.Value;

            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authclaims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature));

            // Token 

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
