using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using Store.G02.Shared.Dtos.Products;
using Store.G05.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Mapping.Products
{
    public class ProductPictureUrlResolve(IConfiguration configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["BaseUrl"]}{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
