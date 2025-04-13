
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persentation.Data.DbContexts;
using Persistence.Data;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoredDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            #endregion

            var app = builder.Build();

            var Scope = app.Services.CreateScope();
            
            var ObjectDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            ObjectDataSeeding.DataSeed();

            #region Configure the HTTP request pipeline

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
