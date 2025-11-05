using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Identity;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Identity;
using Store.G05.Domain.Entities.Orders;
using Store.G05.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class DbInitializer(
        StoreDbContext _context,
        StoreIdentityDbContext _identityDbContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager) : IDbInitializer
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

            // Delivery Method Seeding
            if (!_context.DeliveryMethods.Any())
            {
                // Read All Data From Json File brands.json
                //C:\Users\Mahmoud Rayan\source\repos\Store.G05\Infrastructure\Store.G02.Persistence\Data\DataSeeding

                var deliverydata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\delivery.json");

                // Convert The JsonString To List<ProductBrand>

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverydata);

                // Add List To Db

                if (deliveryMethods is not null && deliveryMethods.Count > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                }
            }

            // Product Brands 

            if (!_context.ProductBrands.Any())
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

        public async Task InitializeIdentityAsync()
        {
            // Create Db If it Doesn't Exist && Apply To Any Pending Migrations

            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });

                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"
                });
            }


            // Seeding

            if (!_userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin1@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789"
                };

                var AdminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin1@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789"
                };

                await _userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await _userManager.CreateAsync(AdminUser, "P@ssW0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");

            }


        }
    }
}
