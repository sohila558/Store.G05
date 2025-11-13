using AutoMapper;
using Store.G02.Shared.Dtos.Auth;
using Store.G05.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
