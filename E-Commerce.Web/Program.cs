using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.Repositories;
using Service;
using ServiceAbstraction;


namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            #endregion

            var app = builder.Build();

            var Scope = app.Services.CreateScope();
            
            var ObjectDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectDataSeeding.DataSeedAsync();

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
