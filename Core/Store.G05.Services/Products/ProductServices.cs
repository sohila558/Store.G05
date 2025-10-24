using AutoMapper;
using Store.G02.Shared.Dtos.Products;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Products;
using Store.G05.Services.Abstractions.Products;
using Store.G05.Services.Specifications;
using Store.G05.Services.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Products
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId, string? sort, string? search)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(brandId, typeId, sort, search);

            var products = await _unitOfWork.GetRepositry<int, Product>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return result;
        }

        public async Task<ProductResponse> GetProductsByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(id);

            var product = await _unitOfWork.GetRepositry<int, Product>().GetByIdAsync(spec);
            var result = _mapper.Map<ProductResponse>(product);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandAsync()
        {
            var brands = await _unitOfWork.GetRepositry<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepositry<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return result;
        }
    }
}
