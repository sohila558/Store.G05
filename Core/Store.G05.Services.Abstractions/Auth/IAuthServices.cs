using Store.G02.Shared.Dtos.Auth;
using Store.G05.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Abstractions.Auth
{
    public interface IAuthServices
    {
        Task<UserResponse> LoginAsync(LoginRequest logInDto);
        Task<UserResponse> RegisterAsync(RegisterRequest registerDto);
        Task<bool> CheckEmailExist(string email);
        Task<UserResponse?> GetCurrentUserAsync(string email);
        Task<AddressDto?> GetCurrentUserAdressAsync(string email);
        Task<AddressDto?> UpdateCurrentUserAdressAsync(AddressDto request, string email);
    }
}
