using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using Persistence.Data.DbContexts.Identity;
using Persistence.Data.DbContexts.StoredDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class DataSeeding(StoredDbContext _dbContext,
                             UserManager<ApplicationUser> _userManager,
                             RoleManager<IdentityRole> _roleManager,
                             StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                    _dbContext.Database.Migrate();

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);

                    if (ProductBrands is not null && ProductBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);

                    if (ProductTypes is not null && ProductTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }
                if (!_dbContext.products.Any())
                {
                    var productsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);

                    if (products is not null && products.Any())
                        await _dbContext.products.AddRangeAsync(products);
                }

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);

                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public async Task IdentityDataSeedAsync()
        {

            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        Email = "Ahmed@gmail.com",
                        DisplayName = "Ahmed Bakry",
                        PhoneNumber = "01155325382",
                        UserName = "AhmedBakry",
                    };

                    var User02 = new ApplicationUser
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Bakry",
                        PhoneNumber = "0111963282",
                        UserName = "MohamedBakry",
                    };

                    await _userManager.CreateAsync(User01, "P@ssW0rd");
                    await _userManager.CreateAsync(User02, "P@ssW0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
