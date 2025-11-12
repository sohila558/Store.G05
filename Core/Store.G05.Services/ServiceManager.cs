using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.G02.Shared;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Identity;
using Store.G05.Services.Abstractions;
using Store.G05.Services.Abstractions.Auth;
using Store.G05.Services.Abstractions.Baskets;
using Store.G05.Services.Abstractions.Orders;
using Store.G05.Services.Abstractions.Products;
using Store.G05.Services.Auth;
using Store.G05.Services.Baskets;
using Store.G05.Services.Caches;
using Store.G05.Services.Orders;
using Store.G05.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepositry basketRepositry,
        ICacheRepositry cacheRepositry,
        UserManager<AppUser> userManager,
        IOptions<JwtOptions> options
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductServices(_unitOfWork, _mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(basketRepositry, _mapper);

        public ICacheServices CacheServices { get; } = new CacheServices(cacheRepositry);

        public IAuthServices AuthServices { get; } = new AuthServices(userManager, options);

        public IOrderServices OrderServices { get; } = new OrderServices(_unitOfWork, _mapper, basketRepositry);
    }
}