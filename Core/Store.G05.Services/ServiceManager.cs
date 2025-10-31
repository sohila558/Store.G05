﻿using AutoMapper;
using Store.G05.Domain.Contracts;
using Store.G05.Services.Abstractions;
using Store.G05.Services.Abstractions.Baskets;
using Store.G05.Services.Abstractions.Products;
using Store.G05.Services.Baskets;
using Store.G05.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepositry basketRepositry) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductServices(_unitOfWork, _mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(basketRepositry, _mapper);
    }
}
