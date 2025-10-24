using Store.G02.Shared.Dtos.Products;
using Store.G05.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Specifications.Products
{
    public class ProductsCountSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsCountSpecifications(ProductQueryParams parameters) : base
            (
                P =>
                (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {
            
        }
    }
}
