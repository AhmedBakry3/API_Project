using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();

            var ObjectDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectDataSeeding.DataSeedAsync();
            await ObjectDataSeeding.IdentityDataSeedAsync();

        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }   
    }
}
