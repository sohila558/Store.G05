using Microsoft.EntityFrameworkCore;
using Store.G02.Persistence.Data.Contexts;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            // Create Db
            // Update Db

            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();
            }

            // Data Seeding
            
            // Product Brands 

            if(!_context.ProductBrands.Any())
            {
                // Read All Data From Json File brands.json
                //C:\Users\Mahmoud Rayan\source\repos\Store.G05\Infrastructure\Store.G02.Persistence\Data\DataSeeding

                var brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\brands.json");

                // Convert The JsonString To List<ProductBrand>

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);

                // Add List To Db

                if(brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands); 
                }
            }

            // Product Types

            if (!_context.ProductTypes.Any())
            {
                var typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);

                if(types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }
            }

            // Products

            if (!_context.Products.Any())
            {
                var productsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);

                if(products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }

            }

            await _context.SaveChangesAsync();

        }
    }
}
