using Store.G02.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Abstractions.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId, string? sort, string? search);
        Task<ProductResponse> GetProductsByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
