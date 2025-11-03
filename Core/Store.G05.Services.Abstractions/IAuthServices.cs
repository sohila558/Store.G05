using Store.G02.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Abstractions
{
    public interface IAuthServices
    {
        Task<UserResultDto> LoginAsync(LogInDto logInDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    }
}
