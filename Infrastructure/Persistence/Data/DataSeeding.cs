using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class DataSeeding(StoredDbContext _dbContext) : IDataSeeding
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
                    var ProductBrandsData =  File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
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

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                //TODO
            }

        }
    }
}
