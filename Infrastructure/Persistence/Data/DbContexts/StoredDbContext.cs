using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.DbContexts
{
    public class StoredDbContext(DbContextOptions<StoredDbContext> Options) : DbContext(Options)
    {
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }
    }
}
